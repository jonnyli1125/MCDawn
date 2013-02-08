using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace MCDawn.Gui
{
    public partial class Updater : Form
    {
        public Updater()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        public void CheckForUpdates()
        {
            try
            {
                txtStatus.Text = "Retrieving updates";
                Update();
                txtCurVersion.Text = Server.LatestVersion();
                using (WebClient w = new WebClient())
                    w.DownloadFile("http://updates.mcdawn.com/revs.txt", "revs.txt");
                if (File.Exists("revs.txt"))
                {
                    liDownloads.Items.Clear();
                    foreach (string s in File.ReadAllLines("revs.txt"))
                        if (!String.IsNullOrEmpty(s))
                            liDownloads.Items.Add(s);
                    File.Delete("revs.txt");
                }
                else MessageBox.Show("Error downloading versions list", "Updater");
                txtStatus.Text = "Retrieved updates";
                txtCurVersion.DeselectAll(); txtStatus.DeselectAll();
            }
            catch
            {
                txtStatus.Text = "Error (Time Out)";
                Update();
                MessageBox.Show("Error with retrieving updates", "Updater");
                return;
            }
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (liDownloads.SelectedIndex == -1)
                {
                    liDownloads.Text = "Canceled";
                    Update();
                    MessageBox.Show("Please choose a download!", "Updater");
                    return;
                }
                if (liDownloads.SelectedIndex != -1)
                {
                    using (WebClient client = new WebClient())
                    {
                        txtStatus.Text = "Downloading......";
                        Update();
                        Server.selectedrevision = liDownloads.SelectedItem.ToString();
                        Uri uri = new Uri("http://updates.mcdawn.com/dll/" + Server.selectedrevision + "/MCDawn_.dll");
                        client.DownloadFileAsync(uri, "MCDawn_.new");
                        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                        client.DownloadFileCompleted += Downloaded;
                    }
                }
            }
            catch { MessageBox.Show("An error occured with downloading the update!"); }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            pBarDownload.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        public void Downloaded(object sender, AsyncCompletedEventArgs e)
        {
            txtStatus.Text = "Download Complete";
            Update();
            Thread.Sleep(1500);
            txtStatus.Text = "Restarting MCDawn";
            Update();
            Thread.Sleep(1500);
            MCDawn_.Gui.Program.PerformUpdate(true);
        }
    }
}
