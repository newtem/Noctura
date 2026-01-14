using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
//using System.Drawing;

namespace Noctura_Updater
{
    public partial class Updater : Form
    {
        private const string downloadUrl = "https://github.com/newtem/Noctura/releases/latest/download/Noctura.exe";
        // for custom url
        // private const string downloadUrl = "https://newtem.me/repo/Noctura.exe";
        private const string tempExe = "NocturaNew.exe";
        private const string targetExe = "Noctura.exe";

        private const int progressBar = 20;
        private WebClient webClient;

        public Updater()
        {
            InitializeComponent();
        }

        private void StartDownload()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            webClient = new WebClient();
            webClient.DownloadProgressChanged += DownloadProgress;
            webClient.DownloadFileCompleted += DownloadNocturaCompleted;

            StatusLabel.Text = "Downloading Noctura...";
            LabelsSetCenter();
            webClient.DownloadFileAsync(new Uri(downloadUrl), tempExe);
        }

        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            int percent = e.ProgressPercentage;
            int filled = (percent * progressBar) / 100;
            int empty = progressBar - filled;

            var bar = new StringBuilder();
            bar.Append('[');
            bar.Append(new string('#', filled));
            bar.Append(new string('-', empty));
            bar.Append($"] {percent}%");

            ProgressLabel.Text = bar.ToString();
        }

        private void DownloadNocturaCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Download failed:\n" + e.Error.Message);
                return;
            }

            StatusLabel.Text = "Applying updates...";
            LabelsSetCenter();
            ApplyUpdate();
        }

        private void ApplyUpdate()
        {
            try
            {
                foreach (var proc in Process.GetProcessesByName(
                    Path.GetFileNameWithoutExtension(targetExe)))
                {
                    proc.WaitForExit(); // wait for exit Noctura.exe
                }

                if (File.Exists(targetExe))
                    File.Delete(targetExe);

                File.Move(tempExe, targetExe);

                StatusLabel.Text = "Update completed.";
                LabelsSetCenter();
                Process.Start(targetExe);

                // End of update. updater go to bed
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed:\n" + ex.Message);
            }
        }

        private void Updater_Load_1(object sender, EventArgs e)
        {
            StatusLabel.Text = "Preparing to download...";
            ProgressLabel.Text = "[--------------------] 0%";
            StartDownload();
            LabelsSetCenter();
        }

        private void LabelsSetCenter()
        {
            StatusLabel.Left = (this.ClientSize.Width - StatusLabel.Width) / 2;
            ProgressLabel.Left = (this.ClientSize.Width - ProgressLabel.Width) / 2;
        }
    }
}

