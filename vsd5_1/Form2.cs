using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vsd5_1
{
    public partial class LibraryDownloadForm : MaterialForm
    {

        // İndirme devam ediyorsa true olacak, onay alındığında false yapılacak
        private bool downloadInProgress = true;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Çoklu dil desteği için LanguageManager örneği ekledik
        private LanguageManager langManager = new LanguageManager();

        // Kapama onayının zaten alındığını tutan bayrak
        private bool isClosingConfirmed = false;

        public LibraryDownloadForm(List<string> libraries)
        {
            InitializeComponent();
            this.FormClosing += LibraryDownloadForm_FormClosing;

            // Maximize butonunu devre dışı bırak
            this.MaximizeBox = false;

            // MaterialSkinManager örneği oluştur
            var materialSkinManager = MaterialSkinManager.Instance;

            // Tema yönetimini etkinleştir ve formu ayarla
            materialSkinManager.AddFormToManage(this);

            // Tema modunu koyu olarak ayarla
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
        }

        // Her bir kütüphanenin indirme ilerlemesini güncellemek için metod.
        public void UpdateProgress(string libraryName, int progress, string status)
        {
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
            // İndirme bitti, artık kapanmaya izin ver
            downloadInProgress = false;
            this.Close();
        }

        private void LibraryDownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (downloadInProgress && !isClosingConfirmed)
            {
                // Kullanıcıya bir kere uyar, ikinci seferinde kapatsın
                var result = MessageBox.Show(
                    langManager.GetTranslation("downloadInProgressPreventClose"),
                    langManager.GetTranslation("warning"),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    isClosingConfirmed = true;
                    // İlk onaydan sonra yeniden Close() çağrılırsa kapanacak
                    this.Close();
                }
                e.Cancel = true;
            }
        }
    }
}
