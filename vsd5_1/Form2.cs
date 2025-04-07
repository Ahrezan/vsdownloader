using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace vsd5_1
{
    public partial class LibraryDownloadForm : MaterialForm
    {
        private bool downloadInProgress = true;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private LanguageManager langManager = new LanguageManager();
        private bool isClosingConfirmed = false;

        private string LoadSavedLanguagePreference()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string settingsDir = Path.Combine(docs, "VSDApplication");
            string settingsPath = Path.Combine(settingsDir, "settings.json");

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

        public LibraryDownloadForm(List<string> libraries)
        {
            InitializeComponent();
            var savedLang = LoadSavedLanguagePreference();
            langManager.LoadLanguage(savedLang);
            this.HandleCreated += (s, e) => { };
            this.FormClosing += LibraryDownloadForm_FormClosing;
            lblDownLib.Text = langManager.GetTranslation("downloadingLibraries");
            this.MaximizeBox = false;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
        }

        public void UpdateProgress(string libraryName, int progress, string status)
        {
            // Kontrolün handle'ı oluşturulmamışsa güncelleme yapma.
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(libraryName, progress, status)));
                return;
            }

            if (libraryName.Equals("yt-dlp.exe", StringComparison.OrdinalIgnoreCase))
            {
                progressBar1.Value = progress;
                lblStat1.Text = status;
            }
            else if (libraryName.Equals("spotdl.exe", StringComparison.OrdinalIgnoreCase))
            {
                progressBar2.Value = progress;
                lblStat2.Text = status;
            }
            else if (libraryName.Equals("ffmpeg.exe", StringComparison.OrdinalIgnoreCase))
            {
                progressBar3.Value = progress;
                lblStat3.Text = status;
            }
        }

        public void CompleteDownloads()
        {
            downloadInProgress = false;
            this.Close();
        }

        private void LibraryDownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsHandleCreated)
                return;

            if (downloadInProgress && !isClosingConfirmed)
            {
                // Now that we check for the handle, we avoid calling Invoke too early.
                var result = MessageBox.Show(
                    langManager.GetTranslation("downloadInProgressPreventClose"),
                    langManager.GetTranslation("warning"),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    isClosingConfirmed = true;
                    return;
                }
                e.Cancel = true;
            }
        }
    }
}
