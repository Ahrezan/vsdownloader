using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vsd5_1
{
    public partial class MainForm : MaterialForm
    {
        // Global exception handling (madde 5)
        static MainForm()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Beklenmeyen bir hata oluştu: " + e.Exception.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show("Ciddi bir hata oluştu: " + ex?.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private LanguageManager langManager = new LanguageManager();
        // Ayar dosyasının yolu: Belgeler\VSDApplication\settings.json
        private string settingsPath;

        // Ayarların kaydedilmediğini belirten flag
        private bool settingsChanged = false;
        // Yükleme sırasında event'ların gereksiz tetiklenmesini engellemek için
        private bool isInitializing = false;

        // Ek ayar kontrolleri: txtPath, txtFileParameter, txtFolderParameter, 
        // chkAlwaysFolder, chkOverwrite, chkForceIPv4 (form üzerinde mevcut)
        // Tema için: radioLight, radioDark, radioAuto
        // Eşzamanlı indirme sayısı: sliderConcurrent

        private readonly MaterialSkinManager materialSkinManager;
        private ImageList imageListYT;
        // Aynı videonun tekrar eklenmesini önlemek için
        private HashSet<string> loadedVideoIds = new HashSet<string>();
        // Eklenen videoların sırasını belirlemek için
        private int nextOrder = 0;

        // Video bilgilerini saklayan sınıf (playlist_title eklendi)
        private class VideoItemData
        {
            public bool IsTicked { get; set; }
            public int Order { get; set; }
            public string VideoUrl { get; set; }
            public string Title { get; set; }
            public string Channel { get; set; }
            public string Duration { get; set; }
            public string PlaylistTitle { get; set; }
        }

        // İndirme kuyruğu için gösterim verileri
        private class DownloadDisplayData
        {
            public string Title { get; set; }
            public string Status { get; set; }
            public string Speed { get; set; }
            public string FileSizeInfo { get; set; }
        }

        // Sıralama için alanlar
        private int sortColumn = -1;
        private SortOrder sortOrder = SortOrder.Ascending;

        // UI güncellemesi için video kuyruğu ve timer
        private ConcurrentQueue<JToken> videoQueue = new ConcurrentQueue<JToken>();
        private System.Windows.Forms.Timer videoUpdateTimer;

        // Tick simgeleri
        private Image tickImageChecked;
        private Image tickImageUnchecked;

        // İndirme işlemleri için alanlar
        private SemaphoreSlim downloadSemaphore;
        private Dictionary<ListViewItem, DownloadJobData> downloadJobs = new Dictionary<ListViewItem, DownloadJobData>();

        private class DownloadJobData
        {
            public CancellationTokenSource CancellationTokenSource { get; set; }
            public Process DownloadProcess { get; set; }
            public string Status { get; set; }
            public string OutputFile { get; set; }
        }

        private class FormatItem
        {
            public string FormatName { get; set; }
            public bool IsVideo { get; set; }
            public FormatItem(string formatName, bool isVideo)
            {
                FormatName = formatName;
                IsVideo = isVideo;
            }
            public override string ToString() => FormatName;
        }

        // İndirmenin kaydedileceği klasör
        private string downloadDestination;

        // Yardımcı metod: Kullanıcının profilindeki Downloads klasörünü elde eder.
        private static string GetDownloadsPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        }

        // Thumbnail yeniden boyutlandırma (16:9 oranında)
        private Image ResizeImageTo16by9(Image image, int targetWidth, int targetHeight)
        {
            Bitmap bmp = new Bitmap(targetWidth, targetHeight);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.Clear(Color.Black); // Arka plan siyah
                float scale = Math.Min((float)targetWidth / image.Width, (float)targetHeight / image.Height);
                int newWidth = (int)(image.Width * scale);
                int newHeight = (int)(image.Height * scale);
                int posX = (targetWidth - newWidth) / 2;
                int posY = (targetHeight - newHeight) / 2;
                g.DrawImage(image, posX, posY, newWidth, newHeight);
            }
            return bmp;
        }

        public MainForm()
        {
            InitializeComponent();

            // ListView kenarlıklarını kaldırmak için
            listViewYT.BorderStyle = BorderStyle.None;
            listViewLoadYT.BorderStyle = BorderStyle.None;

            // Resize olayında layout hesaplamalarını optimize etmek için
            this.Resize += MainForm_Resize;

            // 6- Uygulama ilk çalıştırıldığında, DefaultDownloadPath resources string’ini kullanıp "İndirilenler" klasörünü belirleyelim.
            string defaultPath;
            try
            {
                defaultPath = Properties.Resources.DefaultDownloadPath;
                if (string.IsNullOrEmpty(defaultPath))
                    defaultPath = GetDownloadsPath();
            }
            catch
            {
                defaultPath = GetDownloadsPath();
            }

            // Ayar dosyası yolunu ayarla (Belgeler\VSDApplication\settings.json)
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string settingsDir = Path.Combine(docs, "VSDApplication");
            if (!Directory.Exists(settingsDir))
                Directory.CreateDirectory(settingsDir);
            settingsPath = Path.Combine(settingsDir, "settings.json");
            LoadSettings();

            // Dosya yolu: Eğer settings.json'da kayıtlı bir değer varsa onu kullan, yoksa default
            if (!string.IsNullOrEmpty(txtPath.Text))
                downloadDestination = txtPath.Text;
            else
                downloadDestination = defaultPath;
            txtPath.Text = downloadDestination;

            // Ayar kontrollerinde değişiklik varsa flag güncelleniyor.
            txtFileParameter.TextChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };
            txtFolderParameter.TextChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };
            txtPath.TextChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };
            chkAlwaysFolder.CheckedChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };
            chkOverwrite.CheckedChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };
            chkForceIPv4.CheckedChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };
            sliderConcurrent.onValueChanged += (s, newVal) =>
            {
                settingsChanged = true;
                downloadSemaphore = new SemaphoreSlim(sliderConcurrent.Value);
            };

            radioLight.CheckedChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };
            radioDark.CheckedChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };
            radioAuto.CheckedChanged += (s, e) => { if (!isInitializing) settingsChanged = true; };

            // Form kapanırken ayarların kaydedilmemişse uyarı verilecek.
            this.FormClosing += MainForm_FormClosing;

            // ListView için double buffering (Performans için)
            typeof(ListView).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, listViewYT, new object[] { true });

            // listViewLoadYT için ayarlar
            listViewLoadYT.OwnerDraw = true;
            listViewLoadYT.View = View.Details;
            listViewLoadYT.FullRowSelect = true;
            listViewLoadYT.Columns.Clear();
            // Madde 4: Sütun genişliğini tam genişlikte ayarla
            listViewLoadYT.Columns.Add("DownloadQueue", listViewLoadYT.ClientSize.Width);
            listViewLoadYT.Resize += (s, e) =>
            {
                listViewLoadYT.Columns[0].Width = listViewLoadYT.ClientSize.Width;
            };
            listViewLoadYT.ColumnWidthChanging += listView_ColumnWidthChanging;
            listViewLoadYT.DrawColumnHeader += listViewLoadYT_DrawColumnHeader;
            listViewLoadYT.DrawItem += listViewLoadYT_DrawItem;
            listViewLoadYT.MouseClick += listViewLoadYT_MouseClick;

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            // Windows tema ayarına göre başlangıç teması
            if (IsDarkModeEnabled())
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            else
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            ApplyThemeFromSetting();

            radioLight.CheckedChanged += radioLight_CheckedChanged;
            radioDark.CheckedChanged += radioDark_CheckedChanged;
            radioAuto.CheckedChanged += radioAuto_CheckedChanged;

            tabControl.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            ApplyTabTheme(tabControl.SelectedTab);

            // listViewYT ayarları – sütun başlığı dil çevirisine göre belirlensin
            listViewYT.CheckBoxes = false;
            listViewYT.OwnerDraw = true;
            listViewYT.View = View.Details;
            listViewYT.FullRowSelect = true;
            listViewYT.Columns.Clear();
            // Madde 4: Sütun genişliğini tam genişlikte ayarla
            listViewYT.Columns.Add("ListViewYTHeader", listViewYT.ClientSize.Width);
            listViewYT.Resize += (s, e) =>
            {
                listViewYT.Columns[0].Width = listViewYT.ClientSize.Width;
            };
            listViewYT.ColumnWidthChanging += listView_ColumnWidthChanging;

            listViewYT.BackColor = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Color.FromArgb(48, 48, 48) : Color.White;
            listViewYT.ForeColor = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Color.White : Color.Black;
            listViewYT.Font = new Font("Segoe UI", 10);
            listViewYT.DrawColumnHeader += listViewYT_DrawColumnHeader;
            listViewYT.DrawSubItem += listViewYT_DrawSubItem;
            listViewYT.DrawItem += listViewYT_DrawItem;
            listViewYT.ColumnClick += listViewYT_ColumnClick;
            listViewYT.MouseClick += listViewYT_MouseClick; // Her tıklamada "Hepsini Seç" güncellensin

            imageListYT = new ImageList { ImageSize = new Size(160, 90) };
            listViewYT.SmallImageList = imageListYT;
            listViewLoadYT.SmallImageList = imageListYT;

            tickImageChecked = CreateTickImage(true);
            tickImageUnchecked = CreateTickImage(false);

            videoUpdateTimer = new System.Windows.Forms.Timer();
            videoUpdateTimer.Interval = 200;
            videoUpdateTimer.Tick += VideoUpdateTimer_Tick;
            videoUpdateTimer.Start();

            txtUrl.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnLoad.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            btnLoad.Click += async (s, e) => await LoadVideosAsync(txtUrl.Text.Trim());

            // İndirme ayarları
            SetupDownloadControls();
            cmbYTFormat.Visible = false;
            cmbYTQuality.Visible = false;
            btnYTDownload.Visible = false;
            btnYTDownload.Click += btnYTDownload_Click;
            btnYTCancel.Visible = false;
            btnYTCancel.Click += btnYTCancel_Click;

            sliderConcurrent.RangeMax = 7;
            sliderConcurrent.Visible = true;
            sliderConcurrent.onValueChanged += SliderConcurrent_onValueChanged;

            btnSelect.Click += btnSelect_Click;
            btnClearCache.Click += btnClearCache_Click;

            // Başlangıçta "Hepsini Seç" checkbox durumu
            chkSelectYTAll.Checked = true;
            chkSelectYTAll.Visible = false;
            chkSelectYTAll.CheckedChanged += chkSelectYTAll_CheckedChanged;

            btnSave.Click += btnSave_Click;
            btnDefaultSettings.Click += btnDefaultSettings_Click;

            // Çok önemli: downloadSemaphore’u formun başlangıcında sliderConcurrent’in varsayılan değerine göre initialize ediyoruz.
            downloadSemaphore = new SemaphoreSlim(sliderConcurrent.Value);

            settingsChanged = false;
        }

        #region Ayar Dosyası İşlemleri

        private void LoadSettings()
        {
            isInitializing = true;
            if (File.Exists(settingsPath))
            {
                try
                {
                    string json = File.ReadAllText(settingsPath);
                    var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    if (settings != null)
                    {
                        txtFileParameter.Text = settings.ContainsKey("FileTemplate") ? settings["FileTemplate"] : "$title";
                        txtFolderParameter.Text = settings.ContainsKey("FolderTemplate") ? settings["FolderTemplate"] : "$playlist_title";
                        chkAlwaysFolder.Checked = settings.ContainsKey("AlwaysFolder") && settings["AlwaysFolder"] == "true";
                        txtPath.Text = settings.ContainsKey("Path") ? settings["Path"] : GetDownloadsPath();
                        if (settings.ContainsKey("ConcurrentDownloads"))
                        {
                            int con;
                            if (int.TryParse(settings["ConcurrentDownloads"], out con))
                                sliderConcurrent.Value = con;
                        }
                        else
                        {
                            sliderConcurrent.Value = 2;
                        }
                        string theme = settings.ContainsKey("Theme") ? settings["Theme"] : "Auto";
                        switch (theme)
                        {
                            case "Light":
                                radioLight.Checked = true;
                                break;
                            case "Dark":
                                radioDark.Checked = true;
                                break;
                            default:
                                radioAuto.Checked = true;
                                break;
                        }
                        chkOverwrite.Checked = settings.ContainsKey("Overwrite") && settings["Overwrite"] == "true";
                        chkForceIPv4.Checked = settings.ContainsKey("ForceIPv4") && settings["ForceIPv4"] == "true";
                        // Dil tercihini de yükleyelim
                        if (settings.ContainsKey("language"))
                        {
                            string langCode = settings["language"];
                            langManager.LoadLanguage(langCode);
                            ApplyTranslations();
                            SetComboBoxLanguage(langCode);
                        }
                        settingsChanged = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(langManager.GetTranslation("errorLoadingSettings") + ex.Message,
                        langManager.GetTranslation("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtFileParameter.Text = "$title";
                txtFolderParameter.Text = "$playlist_title";
                chkAlwaysFolder.Checked = false;
                txtPath.Text = GetDownloadsPath();
                sliderConcurrent.Value = 2;
                radioAuto.Checked = true;
                chkOverwrite.Checked = false;
                chkForceIPv4.Checked = false;
                settingsChanged = false;
            }
            isInitializing = false;
        }

        private void SaveSettings()
        {
            try
            {
                Dictionary<string, string> settings;
                if (File.Exists(settingsPath))
                {
                    string json = File.ReadAllText(settingsPath);
                    settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
                }
                else
                {
                    settings = new Dictionary<string, string>();
                }
                settings["FileTemplate"] = txtFileParameter.Text;
                settings["FolderTemplate"] = txtFolderParameter.Text;
                settings["AlwaysFolder"] = chkAlwaysFolder.Checked ? "true" : "false";
                settings["Path"] = txtPath.Text;
                settings["ConcurrentDownloads"] = sliderConcurrent.Value.ToString();
                settings["Overwrite"] = chkOverwrite.Checked ? "true" : "false";
                settings["ForceIPv4"] = chkForceIPv4.Checked ? "true" : "false";
                if (radioLight.Checked)
                    settings["Theme"] = "Light";
                else if (radioDark.Checked)
                    settings["Theme"] = "Dark";
                else
                    settings["Theme"] = "Auto";

                // Dil tercihini de kaydedelim.
                settings["language"] = GetCurrentLanguageCode();

                string jsonOut = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(settingsPath, jsonOut);
                settingsChanged = false;
                MessageBox.Show(langManager.GetTranslation("settingsSaved"),
                    langManager.GetTranslation("information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(langManager.GetTranslation("errorSavingSettings") + ex.Message,
                    langManager.GetTranslation("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCurrentLanguageCode()
        {
            string selected = cmbLang.SelectedItem?.ToString() ?? "English";
            // Eğer "Varsayılan dil" seçili ise, sistem dilini al
            if (selected == "Varsayılan dil")
            {
                return GetDefaultLanguageCode();
            }
            switch (selected)
            {
                case "English":
                    return "english";
                case "Türkçe":
                    return "turkish";
                case "Español":
                    return "spanish";
                case "French":
                    return "french";
                case "German":
                    return "german";
                case "Chinese - Simplified":
                    return "chinese-simp";
                case "Russian":
                    return "russian";
                case "Arabic":
                    return "arabic";
                case "Japanese":
                    return "japanese";
                case "Italian":
                    return "italian";
                case "Portuguese - Brazil":
                    return "portuguese-brazil";
                case "Azerbaycan Türkçesi":
                    return "azerbaijani";
                default:
                    return "english";
            }
        }

        // Yeni: Sistem diline göre varsayılan dili belirleyen metot (madde 1)
        private string GetDefaultLanguageCode()
        {
            string lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            switch (lang)
            {
                case "tr":
                    return "turkish";
                case "es":
                    return "spanish";
                case "fr":
                    return "french";
                case "de":
                    return "german";
                case "zh":
                    return "chinese-simp";
                case "ru":
                    return "russian";
                case "ar":
                    return "arabic";
                case "ja":
                    return "japanese";
                case "it":
                    return "italian";
                case "pt":
                    return "portuguese-brazil";
                case "az":
                    return "azerbaijani";
                default:
                    return "english";
            }
        }

        // Eski SaveLanguagePreference ve LoadSavedLanguagePreference metotları artık ayar dosyasıyla entegre edildi.

        #endregion

        #region Ayarlar – Form Kapatma Kontrolü

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (settingsChanged)
            {
                var result = MessageBox.Show(langManager.GetTranslation("unsavedChanges"),
                    langManager.GetTranslation("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    settingsChanged = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btnDefaultSettings_Click(object sender, EventArgs e)
        {
            string defaultPath;
            try
            {
                defaultPath = Properties.Resources.DefaultDownloadPath;
                if (string.IsNullOrEmpty(defaultPath))
                    defaultPath = GetDownloadsPath();
            }
            catch
            {
                defaultPath = GetDownloadsPath();
            }
            txtFileParameter.Text = "$title";
            txtFolderParameter.Text = "$playlist_title";
            chkAlwaysFolder.Checked = false;
            txtPath.Text = defaultPath;
            sliderConcurrent.Value = 2;
            radioAuto.Checked = true;
            chkOverwrite.Checked = false;
            chkForceIPv4.Checked = false;
            settingsChanged = true;
            MessageBox.Show(langManager.GetTranslation("defaultSettingsRestored"),
                langManager.GetTranslation("information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Tema Ayarları

        private void SliderConcurrent_onValueChanged(object sender, int newValue)
        {
            downloadSemaphore = new SemaphoreSlim(sliderConcurrent.Value);
        }

        private bool IsDarkModeEnabled()
        {
            const string registryKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string registryValue = "AppsUseLightTheme";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKey))
            {
                if (key != null && key.GetValue(registryValue) is int value)
                    return value == 0;
            }
            return false;
        }

        private void ApplyDefaultColorScheme()
        {
            if (materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                materialSkinManager.ColorScheme = new ColorScheme(
                    Primary.Grey900, Primary.Grey800, Primary.Grey700,
                    Accent.Yellow700, TextShade.WHITE);
            }
            else
            {
                materialSkinManager.ColorScheme = new ColorScheme(
                    Primary.Grey300, Primary.Grey400, Primary.Grey500,
                    Accent.Orange400, TextShade.BLACK);
            }
        }

        private void ApplyTabTheme(TabPage selectedTab)
        {
            if (selectedTab.Name == "spotPage")
            {
                materialSkinManager.ColorScheme = new ColorScheme(
                    Primary.Green900, Primary.Green800, Primary.Green700,
                    Accent.Green400, TextShade.WHITE);
            }
            else if (selectedTab.Name == "ytPage")
            {
                materialSkinManager.ColorScheme = new ColorScheme(
                    Primary.Red900, Primary.Red800, Primary.Red700,
                    Accent.Red400, TextShade.WHITE);
            }
            else
            {
                ApplyDefaultColorScheme();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyTabTheme(tabControl.SelectedTab);
            this.Invalidate();
            this.Update();
        }

        private void radioLight_CheckedChanged(object sender, EventArgs e)
        {
            if (radioLight.Checked)
                ApplyTheme(MaterialSkinManager.Themes.LIGHT);
        }

        private void radioDark_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDark.Checked)
                ApplyTheme(MaterialSkinManager.Themes.DARK);
        }

        private void radioAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAuto.Checked)
            {
                bool isDark = IsDarkModeEnabled();
                ApplyTheme(isDark ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT);
            }
        }

        private void ApplyTheme(MaterialSkinManager.Themes theme)
        {
            materialSkinManager.Theme = theme;
            ApplyDefaultColorScheme();
        }

        private void ApplyThemeFromSetting()
        {
            if (radioLight.Checked)
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            else if (radioDark.Checked)
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            else
                materialSkinManager.Theme = IsDarkModeEnabled() ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT;
            ApplyDefaultColorScheme();
        }

        #endregion

        #region Video Yükleme & Thumbnail

        // Madde 2: yt-dlp.exe yolunu uygulamanın bulunduğu dizine göre ayarla.
        private async Task LoadVideosAsync(string input)
        {
            string args = GetYtDlpArguments(input);
            if (string.IsNullOrEmpty(args))
                return;

            btnLoad.Enabled = false;
            listViewYT.Items.Clear();
            imageListYT.Images.Clear();
            loadedVideoIds.Clear();
            nextOrder = 0;

            string ytDlpPath = Path.Combine(Application.StartupPath, "libs", "yt-dlp.exe");
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = ytDlpPath,
                Arguments = args,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proc = new Process())
            {
                proc.StartInfo = psi;
                proc.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                    {
                        try
                        {
                            JObject json = JObject.Parse(e.Data);
                            videoQueue.Enqueue(json);
                        }
                        catch (Exception exLine)
                        {
                            Debug.WriteLine("JSON parse hatası: " + exLine.Message);
                        }
                    }
                };
                proc.Start();
                proc.BeginOutputReadLine();
                await Task.Run(() => proc.WaitForExit());
            }

            if (listViewYT.Items.Count > 0)
            {
                cmbYTFormat.Visible = true;
                cmbYTQuality.Visible = true;
                btnYTDownload.Visible = true;
                chkSelectYTAll.Visible = true;
            }

            btnLoad.Enabled = true;
        }

        private string GetYtDlpArguments(string input)
        {
            if (input.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                string lower = input.ToLower();
                if (lower.Contains("youtube.com") || lower.Contains("youtu.be"))
                {
                    if (lower.Contains("playlist"))
                        return $"--flat-playlist -j \"{input}\"";
                    else
                        return $"-j \"{input}\"";
                }
                else
                {
                    MessageBox.Show(langManager.GetTranslation("onlyYouTubeAllowed"),
                        langManager.GetTranslation("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            else
            {
                MessageBox.Show(langManager.GetTranslation("onlyYouTubeAllowed"),
                    langManager.GetTranslation("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private async Task LoadVideosAsync2(string input)
        {
            // Alternatif metot (varsa)
            await LoadVideosAsync(input);
        }

        private void VideoUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (!ytPage.Visible) return;
            while (videoQueue.TryDequeue(out JToken token))
            {
                AddVideoItem(token);
            }
        }

        private async void AddVideoItem(JToken video)
        {
            string title = video["title"]?.ToString() ?? "Bilinmiyor";
            string channel = video["uploader"]?.ToString() ?? "Bilinmiyor";
            string duration = video["duration"] != null ? FormatDuration((int)video["duration"]) : "0:00";
            string thumbnailUrl = video["thumbnail"]?.ToString();
            string videoUrl = video["webpage_url"]?.ToString() ?? "";
            string playlistTitle = video["playlist_title"]?.ToString();

            if (string.IsNullOrEmpty(thumbnailUrl))
            {
                string id = video["id"]?.ToString();
                if (!string.IsNullOrEmpty(id))
                    thumbnailUrl = $"https://img.youtube.com/vi/{id}/hqdefault.jpg";
            }

            string videoKey = video["id"]?.ToString() ?? title;
            if (loadedVideoIds.Contains(videoKey))
                return;
            loadedVideoIds.Add(videoKey);

            VideoItemData vidData = new VideoItemData
            {
                IsTicked = true,
                Order = nextOrder++,
                VideoUrl = videoUrl,
                Title = title,
                Channel = channel,
                Duration = duration,
                PlaylistTitle = playlistTitle
            };

            ListViewItem item = new ListViewItem();
            item.Tag = vidData;
            listViewYT.Items.Add(item);

            if (!string.IsNullOrEmpty(thumbnailUrl))
            {
                var img = await Task.Run(() => GetImageFromUrl(thumbnailUrl));
                if (img != null)
                {
                    Image resizedImg = ResizeImageTo16by9(img, imageListYT.ImageSize.Width, imageListYT.ImageSize.Height);
                    if (!imageListYT.Images.ContainsKey(videoKey))
                        imageListYT.Images.Add(videoKey, resizedImg);
                    item.ImageKey = videoKey;
                    listViewYT.Invalidate(item.Bounds);
                }
            }
            UpdateSelectAllCheckbox();
        }

        private Image GetImageFromUrl(string url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("User-Agent", "Mozilla/5.0");
                    byte[] bytes = wc.DownloadData(url);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        return Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private string FormatDuration(int totalSeconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(totalSeconds);
            return t.Hours > 0 ? $"{t.Hours}:{t.Minutes:D2}:{t.Seconds:D2}" : $"{t.Minutes}:{t.Seconds:D2}";
        }

        #endregion

        #region Owner-Draw ListView, Tick Resim & Sıralama (listViewYT)

        private void listViewYT_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(materialSkinManager.ColorScheme.PrimaryColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, e.Bounds,
                Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void listViewYT_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            int offsetX = 5;
            if (!string.IsNullOrEmpty(e.Item.ImageKey) && imageListYT.Images.ContainsKey(e.Item.ImageKey))
            {
                Image thumb = imageListYT.Images[e.Item.ImageKey];
                Rectangle imageRect = new Rectangle(e.Bounds.Left + offsetX, e.Bounds.Top + 5, thumb.Width, thumb.Height);
                e.Graphics.DrawImage(thumb, imageRect);
                offsetX += thumb.Width + 10;
            }

            VideoItemData data = e.Item.Tag as VideoItemData;
            if (data != null)
            {
                using (Font titleFont = new Font(e.Item.Font, FontStyle.Bold))
                {
                    string title = data.Title;
                    string subLine = $"{data.Channel}, {data.Duration}";
                    Size titleSize = TextRenderer.MeasureText(title, titleFont);
                    Size subLineSize = TextRenderer.MeasureText(subLine, e.Item.Font);
                    int totalHeight = titleSize.Height + subLineSize.Height + 4;
                    int y = e.Bounds.Top + (e.Bounds.Height - totalHeight) / 2;
                    Rectangle titleRect = new Rectangle(e.Bounds.Left + offsetX, y, e.Bounds.Width - offsetX - 50, titleSize.Height);
                    TextRenderer.DrawText(e.Graphics, title, titleFont, titleRect, e.Item.ForeColor, TextFormatFlags.Left);
                    Rectangle subRect = new Rectangle(e.Bounds.Left + offsetX, y + titleSize.Height + 2, e.Bounds.Width - offsetX - 50, subLineSize.Height);
                    TextRenderer.DrawText(e.Graphics, subLine, e.Item.Font, subRect, e.Item.ForeColor, TextFormatFlags.Left);
                }

                Image tickImg = data.IsTicked ? tickImageChecked : tickImageUnchecked;
                if (tickImg != null)
                {
                    Size imgSize = tickImg.Size;
                    Rectangle tickRect = new Rectangle(e.Bounds.Right - imgSize.Width - 10,
                        e.Bounds.Top + (e.Bounds.Height - imgSize.Height) / 2,
                        imgSize.Width, imgSize.Height);
                    e.Graphics.DrawImage(tickImg, tickRect);
                }
            }
        }

        private void listViewYT_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (listViewYT.View != View.Details)
                e.DrawDefault = true;
        }

        private void listViewYT_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listViewYT.HitTest(e.Location);
            if (info.Item != null)
            {
                VideoItemData data = info.Item.Tag as VideoItemData;
                if (data != null)
                {
                    data.IsTicked = !data.IsTicked;
                    listViewYT.Invalidate(info.Item.Bounds);
                    UpdateSelectAllCheckbox();
                }
            }
        }

        private void listViewYT_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == sortColumn)
                sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            else
            {
                sortColumn = e.Column;
                sortOrder = SortOrder.Ascending;
            }
            listViewYT.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
            listViewYT.Sort();
        }

        public class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                VideoItemData dataX = ((ListViewItem)x).Tag as VideoItemData;
                VideoItemData dataY = ((ListViewItem)y).Tag as VideoItemData;
                if (dataX == null || dataY == null)
                    return 0;
                int result = dataX.Order.CompareTo(dataY.Order);
                return order == SortOrder.Ascending ? result : -result;
            }
        }

        private Image CreateTickImage(bool isChecked)
        {
            int size = 16;
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    g.DrawRectangle(pen, 0, 0, size - 1, size - 1);
                }
                if (isChecked)
                {
                    using (Pen pen = new Pen(Color.Green, 2))
                    {
                        g.DrawLine(pen, size / 4, size / 2, size / 2, size - 4);
                        g.DrawLine(pen, size / 2, size - 4, size - 2, 2);
                    }
                }
            }
            return bmp;
        }

        private void listView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            if (sender is ListView lv)
                e.NewWidth = lv.Columns[e.ColumnIndex].Width;
        }

        private void UpdateSelectAllCheckbox()
        {
            bool allChecked = true;
            foreach (ListViewItem item in listViewYT.Items)
            {
                VideoItemData data = item.Tag as VideoItemData;
                if (data != null && !data.IsTicked)
                {
                    allChecked = false;
                    break;
                }
            }
            chkSelectYTAll.CheckedChanged -= chkSelectYTAll_CheckedChanged;
            chkSelectYTAll.Checked = allChecked;
            chkSelectYTAll.CheckedChanged += chkSelectYTAll_CheckedChanged;
        }

        #endregion

        #region İndirme İşlemleri

        private void SetupDownloadControls()
        {
            cmbYTFormat.Items.Clear();
            cmbYTFormat.Items.Add(new FormatItem("mp4", true));
            cmbYTFormat.Items.Add(new FormatItem("webm", true));
            cmbYTFormat.Items.Add(new FormatItem("mkv", true));
            cmbYTFormat.Items.Add(new FormatItem("flv", true));
            cmbYTFormat.Items.Add(new FormatItem("avi", true));
            cmbYTFormat.Items.Add(new FormatItem("mov", true));
            cmbYTFormat.Items.Add(new FormatItem("mp3", false));
            cmbYTFormat.Items.Add(new FormatItem("m4a", false));
            cmbYTFormat.Items.Add(new FormatItem("opus", false));
            cmbYTFormat.Items.Add(new FormatItem("wav", false));
            cmbYTFormat.Items.Add(new FormatItem("aac", false));
            cmbYTFormat.SelectedIndex = 0;
            cmbYTFormat.SelectedIndexChanged += (s, e) => UpdateQualityComboBox();
            UpdateQualityComboBox();
        }

        private void UpdateQualityComboBox()
        {
            cmbYTQuality.Items.Clear();
            FormatItem selectedFormat = cmbYTFormat.SelectedItem as FormatItem;
            if (selectedFormat != null)
            {
                if (selectedFormat.IsVideo)
                {
                    cmbYTQuality.Items.Add(langManager.GetTranslation("highestQuality"));
                    cmbYTQuality.Items.Add(langManager.GetTranslation("premium1080p"));
                    cmbYTQuality.Items.Add("1080p");
                    cmbYTQuality.Items.Add(langManager.GetTranslation("premium720p"));
                    cmbYTQuality.Items.Add("720p");
                    cmbYTQuality.Items.Add("480p");
                    cmbYTQuality.Items.Add("360p");
                    cmbYTQuality.Items.Add("240p");
                    cmbYTQuality.Items.Add("144p");
                    cmbYTQuality.Items.Add(langManager.GetTranslation("lowestQuality"));
                    cmbYTQuality.SelectedIndex = 0;
                }
                else
                {
                    cmbYTQuality.Items.Add("320");
                    cmbYTQuality.Items.Add("192");
                    cmbYTQuality.Items.Add("128");
                    cmbYTQuality.Items.Add("64");
                    cmbYTQuality.SelectedIndex = 0;
                }
            }
        }

        private void chkSelectYTAll_CheckedChanged(object sender, EventArgs e)
        {
            bool check = chkSelectYTAll.Checked;
            foreach (ListViewItem item in listViewYT.Items)
            {
                VideoItemData data = item.Tag as VideoItemData;
                if (data != null)
                {
                    data.IsTicked = check;
                }
            }
            listViewYT.Invalidate();
        }

        private async void btnYTDownload_Click(object sender, EventArgs e)
        {
            if (settingsChanged)
            {
                MessageBox.Show(langManager.GetTranslation("pleaseSaveSettings"),
                    langManager.GetTranslation("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<ListViewItem> itemsToDownload = new List<ListViewItem>();
            foreach (ListViewItem item in listViewYT.Items)
            {
                VideoItemData data = item.Tag as VideoItemData;
                if (data != null && data.IsTicked)
                    itemsToDownload.Add(item);
            }

            if (itemsToDownload.Count == 0)
            {
                MessageBox.Show(langManager.GetTranslation("noSelectedVideo"),
                    langManager.GetTranslation("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listViewYT.Items.Count == itemsToDownload.Count)
            {
                cmbYTFormat.Visible = false;
                cmbYTQuality.Visible = false;
                btnYTDownload.Visible = false;
                chkSelectYTAll.Visible = false;
            }

            FormatItem selectedFormat = cmbYTFormat.SelectedItem as FormatItem;
            string selectedQuality = cmbYTQuality.SelectedItem.ToString();

            foreach (ListViewItem item in itemsToDownload)
            {
                VideoItemData data = item.Tag as VideoItemData;
                string videoTitle = data.Title;
                string downloadArgs = GetDownloadArguments(data, selectedFormat, selectedQuality);
                if (string.IsNullOrEmpty(downloadArgs))
                    continue;

                // Hesaplanan çıkış dosya yolunu alalım
                string outputFile = GetOutputPath(data, selectedFormat);

                DownloadDisplayData ddd = new DownloadDisplayData
                {
                    Title = videoTitle,
                    Status = langManager.GetTranslation("waiting"),
                    Speed = "",
                    FileSizeInfo = ""
                };
                ListViewItem dlItem = new ListViewItem();
                dlItem.Tag = ddd;
                listViewLoadYT.Items.Add(dlItem);

                CancellationTokenSource cts = new CancellationTokenSource();
                DownloadJobData jobData = new DownloadJobData
                {
                    CancellationTokenSource = cts,
                    Status = langManager.GetTranslation("waiting"),
                    OutputFile = outputFile
                };
                downloadJobs.Add(dlItem, jobData);
                StartDownload(dlItem, downloadArgs, jobData);

                listViewYT.Items.Remove(item);
                UpdateSelectAllCheckbox();
            }
            await Task.CompletedTask;
        }

        private string GetDownloadArguments(VideoItemData data, FormatItem format, string quality)
        {
            string url = data.VideoUrl;
            string fileTemplate = string.IsNullOrWhiteSpace(txtFileParameter.Text) ? "$title" : txtFileParameter.Text;
            fileTemplate = fileTemplate.Replace("$title", "%(title)s");

            string folderTemplate = txtFolderParameter.Text;
            if (string.IsNullOrWhiteSpace(folderTemplate) || folderTemplate.Trim().ToLower() == "$playlist_title")
            {
                folderTemplate = !string.IsNullOrEmpty(data.PlaylistTitle) ? "%(playlist_title)s" : "%(title)s";
            }
            else
            {
                folderTemplate = folderTemplate.Replace("$title", "%(title)s").Replace("$playlist_title", "%(playlist_title)s");
            }

            string output;
            if (chkAlwaysFolder.Checked)
            {
                output = Path.Combine(downloadDestination, folderTemplate, fileTemplate + ".%(ext)s");
            }
            else
            {
                output = Path.Combine(downloadDestination, fileTemplate + ".%(ext)s");
            }

            string extraArgs = "";
            if (chkOverwrite.Checked)
                extraArgs += " --force-overwrites";
            if (chkForceIPv4.Checked)
                extraArgs += " --force-ipv4";

            if (format.IsVideo)
            {
                if (quality == langManager.GetTranslation("lowestQuality"))
                {
                    return $"-f \"worstvideo[ext={format.FormatName}]+worstaudio/best\" {extraArgs} -o \"{output}\" \"{url}\"";
                }
                else if (quality == langManager.GetTranslation("highestQuality"))
                {
                    return $"-f \"bestvideo[ext={format.FormatName}]+bestaudio/best\" {extraArgs} -o \"{output}\" \"{url}\"";
                }
                else if (quality.StartsWith(langManager.GetTranslation("premium")))
                {
                    string numeric = quality.Replace(langManager.GetTranslation("premium"), "").Replace("p", "").Trim();
                    return $"-f \"bestvideo[ext={format.FormatName}][height={numeric}]+bestaudio/best\" {extraArgs} -o \"{output}\" \"{url}\"";
                }
                else
                {
                    string numeric = quality.Replace("p", "").Trim();
                    return $"-f \"bestvideo[ext={format.FormatName}][height<={numeric}]+bestaudio/best\" {extraArgs} -o \"{output}\" \"{url}\"";
                }
            }
            else
            {
                return $"-f bestaudio --extract-audio --audio-format {format.FormatName} --audio-quality {quality} {extraArgs} -o \"{output}\" \"{url}\"";
            }
        }

        // Yeni: Çıkış dosya yolunu hesaplayan yardımcı metot
        private string GetOutputPath(VideoItemData data, FormatItem format)
        {
            string fileTemplate = string.IsNullOrWhiteSpace(txtFileParameter.Text) ? "$title" : txtFileParameter.Text;
            fileTemplate = fileTemplate.Replace("$title", data.Title);
            string folderTemplate = txtFolderParameter.Text;
            if (string.IsNullOrWhiteSpace(folderTemplate) || folderTemplate.Trim().ToLower() == "$playlist_title")
            {
                folderTemplate = !string.IsNullOrEmpty(data.PlaylistTitle) ? data.PlaylistTitle : data.Title;
            }
            else
            {
                folderTemplate = folderTemplate.Replace("$title", data.Title).Replace("$playlist_title", data.PlaylistTitle);
            }
            string output;
            if (chkAlwaysFolder.Checked)
            {
                output = Path.Combine(downloadDestination, folderTemplate, fileTemplate + "." + format.FormatName);
            }
            else
            {
                output = Path.Combine(downloadDestination, fileTemplate + "." + format.FormatName);
            }
            return output;
        }

        // Madde 2: Relative yol kullanarak yt-dlp.exe'yi çağır
        private async void StartDownload(ListViewItem dlItem, string downloadArgs, DownloadJobData jobData)
        {
            await downloadSemaphore.WaitAsync();
            try
            {
                UpdateDownloadStatus(dlItem, langManager.GetTranslation("downloading"));
                string ytDlpPath = Path.Combine(Application.StartupPath, "libs", "yt-dlp.exe");
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = ytDlpPath,
                    Arguments = downloadArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process proc = new Process();
                proc.StartInfo = psi;
                jobData.DownloadProcess = proc;
                proc.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                        UpdateDownloadProgress(dlItem, e.Data);
                };
                proc.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                        UpdateDownloadProgress(dlItem, e.Data);
                };
                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                await Task.Run(() => proc.WaitForExit(), jobData.CancellationTokenSource.Token);
                if (proc.ExitCode == 0)
                {
                    UpdateDownloadStatus(dlItem, langManager.GetTranslation("completed"));
                    DownloadDisplayData ddd = dlItem.Tag as DownloadDisplayData;
                    ddd.Title += " (" + langManager.GetTranslation("downloaded") + ")";
                    listViewLoadYT.Invalidate(dlItem.Bounds);
                }
                else
                    UpdateDownloadStatus(dlItem, langManager.GetTranslation("failed"));
            }
            catch (OperationCanceledException)
            {
                UpdateDownloadStatus(dlItem, langManager.GetTranslation("cancelled"));
                if (jobData.DownloadProcess != null && !jobData.DownloadProcess.HasExited)
                    jobData.DownloadProcess.Kill();
            }
            catch (Exception ex)
            {
                UpdateDownloadStatus(dlItem, langManager.GetTranslation("error"));
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (downloadJobs.ContainsKey(dlItem))
                    downloadJobs.Remove(dlItem);
                btnYTCancel.Visible = downloadJobs.Count > 0;
                downloadSemaphore.Release();
            }
        }

        private void UpdateDownloadStatus(ListViewItem item, string status)
        {
            DownloadDisplayData ddd = item.Tag as DownloadDisplayData;
            if (ddd != null)
            {
                ddd.Status = status;
                listViewLoadYT.Invalidate(item.Bounds);
            }
        }

        private void UpdateDownloadProgress(ListViewItem item, string progressText)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateDownloadProgress(item, progressText)));
                return;
            }
            DownloadDisplayData ddd = item.Tag as DownloadDisplayData;
            if (ddd != null)
            {
                // Burada progressText’i hız/dosya bilgisi olarak gösterebilirsiniz.
                ddd.Speed = progressText;
                listViewLoadYT.Invalidate(item.Bounds);
            }
        }

        private void btnYTCancel_Click(object sender, EventArgs e)
        {
            foreach (var kvp in downloadJobs)
                kvp.Value.CancellationTokenSource.Cancel();
            downloadJobs.Clear();
            btnYTCancel.Visible = false;
        }

        // listViewLoadYT’nin OwnerDraw metodu: Başlık satırının altında durum, hız ve dosya bilgisi gösteriliyor; sağda X simgesi.
        private void listViewLoadYT_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false;
            DownloadDisplayData data = e.Item.Tag as DownloadDisplayData;
            if (data == null) return;
            Color bg = e.Item.Selected ? SystemColors.Highlight : listViewLoadYT.BackColor;
            e.Graphics.FillRectangle(new SolidBrush(bg), e.Bounds);
            int cancelWidth = 40;
            Rectangle cancelRect = new Rectangle(e.Bounds.Right - cancelWidth, e.Bounds.Top, cancelWidth, e.Bounds.Height);
            int textWidth = e.Bounds.Width - cancelWidth - 10;
            Rectangle titleRect = new Rectangle(e.Bounds.Left + 5, e.Bounds.Top, textWidth, e.Bounds.Height / 2);
            Rectangle infoRect = new Rectangle(e.Bounds.Left + 5, e.Bounds.Top + e.Bounds.Height / 2, textWidth, e.Bounds.Height / 2);
            TextRenderer.DrawText(e.Graphics, data.Title, e.Item.Font, titleRect, e.Item.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            string infoText = $"Status: {data.Status}   Speed: {data.Speed}   {data.FileSizeInfo}";
            TextRenderer.DrawText(e.Graphics, infoText, e.Item.Font, infoRect, e.Item.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(e.Graphics, "X", e.Item.Font, cancelRect, Color.Red, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void listViewLoadYT_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(materialSkinManager.ColorScheme.PrimaryColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            TextRenderer.DrawText(e.Graphics, langManager.GetTranslation("downloadQueue"), e.Font, e.Bounds,
                Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void listViewLoadYT_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listViewLoadYT.HitTest(e.Location);
            if (info.Item != null)
            {
                Rectangle itemBounds = info.Item.Bounds;
                int cancelWidth = 40;
                Rectangle cancelRect = new Rectangle(itemBounds.Right - cancelWidth, itemBounds.Top, cancelWidth, itemBounds.Height);
                if (cancelRect.Contains(e.Location))
                {
                    if (downloadJobs.TryGetValue(info.Item, out DownloadJobData jobData))
                    {
                        jobData.CancellationTokenSource.Cancel();
                        try
                        {
                            if (File.Exists(jobData.OutputFile))
                                File.Delete(jobData.OutputFile);
                        }
                        catch { }
                        downloadJobs.Remove(info.Item);
                        listViewLoadYT.Items.Remove(info.Item);
                        btnYTCancel.Visible = downloadJobs.Count > 0;
                    }
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = downloadDestination;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    downloadDestination = fbd.SelectedPath;
                    txtPath.Text = downloadDestination;
                }
            }
        }

        private void btnClearCache_Click(object sender, EventArgs e)
        {
            imageListYT.Images.Clear();
            MessageBox.Show(langManager.GetTranslation("cacheCleared"),
                langManager.GetTranslation("information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region CardBox Düzenlemeleri
        // Madde 3: Pencere yeniden boyutlandığında, ekran daraldıkça card’ları dikeyde hizalayacak şekilde konumlandırma ve Anchor özelliklerini kaldırma
        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.SuspendLayout();
            int gap = 14;
            int rightGap = 82; // Sağ kenar boşluğu
            listCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            loadCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            compCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            if (this.ClientSize.Width <= 1150)
            {
                // Madde 3: Küçük ekranda Anchor özelliklerini kaldırıyoruz.
                listCard.Anchor = AnchorStyles.Top;
                loadCard.Anchor = AnchorStyles.Top;
                compCard.Anchor = AnchorStyles.Top;

                // Tüm card’ları dikeyde hizala
                int availableWidth = this.ClientSize.Width - 17 - rightGap;
                listCard.Width = availableWidth;
                loadCard.Width = availableWidth;
                compCard.Width = availableWidth;
                listCard.Location = new Point(17, 85);
                loadCard.Location = new Point(17, listCard.Bottom + gap);
                compCard.Location = new Point(17, loadCard.Bottom + gap);
                compCard.Visible = true;
            }
            else if (this.ClientSize.Width < 1594)
            {
                compCard.Visible = false;
                int availableWidth = this.ClientSize.Width - 17 - gap - rightGap;
                listCard.Width = availableWidth / 2;
                loadCard.Width = availableWidth - listCard.Width;
                listCard.Location = new Point(17, 85);
                loadCard.Location = new Point(listCard.Right + gap, 85);
                listCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                loadCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                compCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            }
            else
            {
                compCard.Visible = true;
                int availableWidth = this.ClientSize.Width - 17 - 2 * gap - rightGap;
                int cardWidth = availableWidth / 3;
                listCard.Width = cardWidth;
                loadCard.Width = cardWidth;
                compCard.Width = availableWidth - 2 * cardWidth;
                listCard.Location = new Point(17, 85);
                loadCard.Location = new Point(listCard.Right + gap, 85);
                compCard.Location = new Point(loadCard.Right + gap, 85);
                listCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                loadCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                compCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            }
            this.ResumeLayout();
        }
        #endregion

        #region Dil ve Çeviri İşlemleri
        // Madde 1: cmbLang içerisine "Varsayılan dil" seçeneğini ekle ve sistem diline göre ayarla.
        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckLibraries();

            cmbLang.Items.Clear();
            cmbLang.Items.Add("Varsayılan dil");
            cmbLang.Items.Add("English");
            cmbLang.Items.Add("Türkçe");
            cmbLang.Items.Add("Español");
            cmbLang.Items.Add("French");
            cmbLang.Items.Add("German");
            cmbLang.Items.Add("Chinese - Simplified");
            cmbLang.Items.Add("Russian");
            cmbLang.Items.Add("Arabic");
            cmbLang.Items.Add("Japanese");
            cmbLang.Items.Add("Italian");
            cmbLang.Items.Add("Portuguese - Brazil");
            cmbLang.Items.Add("Azerbaycan Türkçesi");

            string langCode = LoadSavedLanguagePreference();
            if (string.IsNullOrEmpty(langCode))
                langCode = GetDefaultLanguageCode();
            langManager.LoadLanguage(langCode);
            ApplyTranslations();
            SetComboBoxLanguage(langCode); // Burada, Items eklenmiş olduğu için SelectedIndex ayarlaması geçerli olur.

            cmbLang.SelectedIndexChanged += cmbLang_SelectedIndexChanged;
        }

        private void CheckLibraries()
        {
            string libsPath = Path.Combine(Application.StartupPath, "libs");
            List<string> missingLibraries = new List<string>();
            Dictionary<string, string> downloadUrls = new Dictionary<string, string>()
            {
                { "yt-dlp.exe", "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe" },
                { "spotdl.exe", "https://github.com/spotDL/spotify-downloader/releases/download/v4.2.11/spotdl-4.2.11-win32.exe" },
                { "ffmpeg.exe", "https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip" } // Zip formatı
            };

            foreach (var lib in downloadUrls.Keys)
            {
                string filePath = Path.Combine(libsPath, lib);
                if (!File.Exists(filePath))
                {
                    missingLibraries.Add(lib);
                }
            }

            if (missingLibraries.Count > 0)
            {
                string message = GetLocalizedString("missing_libs") + string.Join("\n", missingLibraries) +
                    GetLocalizedString("download_prompt");
                DialogResult result = MessageBox.Show(message, GetLocalizedString("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Başlatma: asenkron işlem olduğundan beklemek için (örneğin, Form Load'da await edilebilir)
                    DownloadMissingLibrariesAsync(missingLibraries, downloadUrls).ConfigureAwait(false);
                }
                else
                {
                    DialogResult confirmation = MessageBox.Show(GetLocalizedString("confirm_exit"), GetLocalizedString("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmation == DialogResult.No)
                    {
                        DownloadMissingLibrariesAsync(missingLibraries, downloadUrls).ConfigureAwait(false);
                    }
                }
            }
        }

        private async Task DownloadMissingLibrariesAsync(List<string> libraries, Dictionary<string, string> urls)
        {
            // TLS 1.2'yi zorunlu kıl
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            foreach (string lib in libraries)
            {
                string filePath = Path.Combine(Application.StartupPath, "libs", lib);
                try
                {
                    if (lib == "ffmpeg.exe")
                    {
                        string downloadUrl = urls[lib];
                        await DownloadAndExtractFfmpegAsync(downloadUrl, filePath);
                        MessageBox.Show(GetLocalizedString("download_success") + lib,
                            GetLocalizedString("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Diğer dosyalar için senkron WebClient kullanılıyor (isteğe bağlı asenkron hale getirilebilir)
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(urls[lib], filePath);
                            MessageBox.Show(GetLocalizedString("download_success") + lib,
                                GetLocalizedString("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(GetLocalizedString("download_error") + lib + ": " + ex.Message,
                        GetLocalizedString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// HttpClient ile asenkron indirme ve SharpCompress ile ZIP arşivden ffmpeg.exe çıkartma.
        /// </summary>
        private async Task DownloadAndExtractFfmpegAsync(string downloadUrl, string destinationExe)
        {
            string libsPath = Path.Combine(Application.StartupPath, "libs");
            string zipPath = Path.Combine(libsPath, "ffmpeg.zip");
            string extractPath = Path.Combine(libsPath, "ffmpeg_temp");

            // Eğer varsa eski geçici klasörü temizle
            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true);

            // Asenkron indirme işlemi
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (FileStream fs = new FileStream(zipPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        await stream.CopyToAsync(fs);
                    }
                }
            }

            // SharpCompress ile ZIP dosyasını çıkartıyoruz
            Directory.CreateDirectory(extractPath);
            using (var archive = ArchiveFactory.Open(zipPath))
            {
                // Arşivdeki tüm dosyaları extractPath altına çıkarıyoruz
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(extractPath, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                }
            }

            // Çıkartılan klasör içerisinde "ffmpeg.exe" dosyasını arıyoruz
            // Genelde bu dosya, alt klasörler içerisinde (örneğin "ffmpeg-*-essentials_build/bin/ffmpeg.exe") bulunur.
            string foundFile = Directory.GetFiles(extractPath, "ffmpeg.exe", SearchOption.AllDirectories).FirstOrDefault();
            if (foundFile == null)
            {
                throw new Exception("ffmpeg.exe bulunamadı.");
            }

            // Bulunan ffmpeg.exe dosyasını hedef klasöre kopyalıyoruz
            File.Copy(foundFile, destinationExe, true);

            // Geçici dosyaları temizle
            File.Delete(zipPath);
            Directory.Delete(extractPath, true);
        }

        private string GetLocalizedString(string key)
        {
            // Burada uygulamanızın dil ayarına göre uygun mesajı döndürmelisiniz.
            switch (key)
            {
                case "missing_libs": return "The following libraries are not installed:\n";
                case "download_prompt": return "\nIt is required to download. Do you want to download it?\nTHE APPLICATION MAY NOT WORK IF YOU DO NOT DOWNLOAD.";
                case "warning": return "Warning";
                case "success": return "Success";
                case "error": return "Error";
                case "download_success": return "Download successful: ";
                case "download_error": return "Download failed: ";
                case "confirm_exit": return "Are you sure? The download process will be canceled.";
                default: return key;
            }
        }











        private void cmbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbLang.SelectedItem.ToString();
            string langCode = "english";
            if (selected == "Varsayılan dil")
            {
                langCode = GetDefaultLanguageCode();
            }
            else
            {
                switch (selected)
                {
                    case "English":
                        langCode = "english";
                        break;
                    case "Türkçe":
                        langCode = "turkish";
                        break;
                    case "Español":
                        langCode = "spanish";
                        break;
                    case "French":
                        langCode = "french";
                        break;
                    case "German":
                        langCode = "german";
                        break;
                    case "Chinese - Simplified":
                        langCode = "chinese-simp";
                        break;
                    case "Russian":
                        langCode = "russian";
                        break;
                    case "Arabic":
                        langCode = "arabic";
                        break;
                    case "Japanese":
                        langCode = "japanese";
                        break;
                    case "Italian":
                        langCode = "italian";
                        break;
                    case "Portuguese - Brazil":
                        langCode = "portuguese-brazil";
                        break;
                    case "Azerbaycan Türkçesi":
                        langCode = "azerbaijani";
                        break;
                    default:
                        langCode = "english";
                        break;
                }
            }
            langManager.LoadLanguage(langCode);
            ApplyTranslations();
            SaveLanguagePreference(langCode);
        }

        private void ApplyTranslations()
        {
            txtUrl.Hint = langManager.GetTranslation("url");
            settingsPage.Text = langManager.GetTranslation("settings");
            chkSelectYTAll.Text = langManager.GetTranslation("selectAll");
            cmbYTFormat.Hint = langManager.GetTranslation("format");
            cmbYTQuality.Hint = langManager.GetTranslation("quality");
            btnYTDownload.Text = langManager.GetTranslation("download");
            btnYTCancel.Text = langManager.GetTranslation("cancel");
            lblGeneral.Text = langManager.GetTranslation("general");
            txtPath.Hint = langManager.GetTranslation("path");
            txtFileParameter.Hint = langManager.GetTranslation("filePara");
            txtFolderParameter.Hint = langManager.GetTranslation("folderPara");
            btnSelect.Text = langManager.GetTranslation("select");
            radioLight.Text = langManager.GetTranslation("light");
            radioDark.Text = langManager.GetTranslation("dark");
            radioAuto.Text = langManager.GetTranslation("autoTheme");
            lblTheme.Text = langManager.GetTranslation("theme");
            lblDownload.Text = langManager.GetTranslation("lblDownload");
            sliderConcurrent.Text = langManager.GetTranslation("concurrent");
            chkOverwrite.Text = langManager.GetTranslation("overwrite");
            chkAlwaysFolder.Text = langManager.GetTranslation("alwaysFolder");
            chkForceIPv4.Text = langManager.GetTranslation("IPv4");
            cmbLang.Hint = langManager.GetTranslation("language");
            btnClearCache.Text = langManager.GetTranslation("cache");
            btnDefaultSettings.Text = langManager.GetTranslation("defaultSettings");
            btnSave.Text = langManager.GetTranslation("save");
            lblVersion.Text = langManager.GetTranslation("version");

            if (listViewYT.Columns.Count > 0)
                listViewYT.Columns[0].Text = langManager.GetTranslation("search");
            if (listViewLoadYT.Columns.Count > 0)
                listViewLoadYT.Columns[0].Text = langManager.GetTranslation("queue");

            UpdateQualityComboBox();
        }

        private void SetComboBoxLanguage(string langCode)
        {
            if (cmbLang.Items.Count == 0)
            {
                return;
            }

            int index = 0;
            switch (langCode)
            {
                case "english": index = 1; break;
                case "turkish": index = 2; break;
                case "spanish": index = 3; break;
                case "french": index = 4; break;
                case "german": index = 5; break;
                case "chinese-simp": index = 6; break;
                case "russian": index = 7; break;
                case "arabic": index = 8; break;
                case "japanese": index = 9; break;
                case "italian": index = 10; break;
                case "portuguese-brazil": index = 11; break;
                case "azerbaijani": index = 12; break;
                default: index = 0; break;
            }

            // Eğer index, mevcut ComboBox elemanlarının sınırlarını aşıyorsa hata engelle
            if (index >= cmbLang.Items.Count)
            {
                index = 0;
            }

            cmbLang.SelectedIndex = index;
        }

        private void SaveLanguagePreference(string langCode)
        {
            try
            {
                Dictionary<string, string> settings;
                if (File.Exists(settingsPath))
                {
                    string json = File.ReadAllText(settingsPath);
                    settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
                }
                else
                {
                    settings = new Dictionary<string, string>();
                }
                settings["language"] = langCode;
                string jsonOut = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(settingsPath, jsonOut);
            }
            catch (Exception ex)
            {
                MessageBox.Show(langManager.GetTranslation("errorSavingLanguage") + ex.Message,
                    langManager.GetTranslation("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string LoadSavedLanguagePreference()
        {
            if (File.Exists(settingsPath))
            {
                try
                {
                    var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(settingsPath));
                    if (settings != null && settings.ContainsKey("language"))
                        return settings["language"];
                }
                catch { }
            }
            return "";
        }
        #endregion

        private void materialButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("cmbLang eleman sayısı: " + cmbLang.Items.Count);

        }
    }

    public class LanguageManager
    {
        private Dictionary<string, string> translations = new Dictionary<string, string>();

        public void LoadLanguage(string langCode)
        {
            string langPath = Path.Combine(Application.StartupPath, "lang", $"{langCode}.json");
            if (File.Exists(langPath))
            {
                string json = File.ReadAllText(langPath);
                translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            else
            {
                MessageBox.Show($"Dil dosyası bulunamadı: {langPath}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetTranslation(string key)
        {
            return translations.ContainsKey(key) ? translations[key] : key;
        }
    }
}
