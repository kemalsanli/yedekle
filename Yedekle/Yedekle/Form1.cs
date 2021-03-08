using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Yedekle
{
    public partial class Yedekle : Form
    {
        public const String appName = "Yedekle.exe";
        public const String folderName = "Yedekler";
        public Yedekle()
        {
            InitializeComponent();
        }

        private async Task rollback(String path)
        {
            await Task.Run(() =>
            {
                //Eğer yedeklerde klasör sayısı 0'dan yüksekse
                if (System.IO.Directory.GetDirectories($"{path}\\{folderName}").Length > 0)
                {

                    //En son değiştirilmiş klasörü seç
                    DirectoryInfo lastBackup = new DirectoryInfo($"{path}\\{folderName}").GetDirectories()
                           .OrderByDescending(d => d.LastWriteTimeUtc).First();
                    //İçeriğini dosyaya kopyala
                    DirectoryInfo DIPath = new DirectoryInfo(path);
                    CopyFilesRecursivelyAndOverride(lastBackup, DIPath);

                }
            });
        }
        void EnableAndDisableButtons(string description="Yedekle") 
        {
            if (backupButton.Enabled==false)
            {
                backupButton.Text = "Yedekle";
                backupButton.Enabled = true;
                rollbackButton.Enabled = true;
                clearButton.Enabled = true;
            }
            else
            {
                backupButton.Enabled = false;
                rollbackButton.Enabled = false;
                clearButton.Enabled = false;
                backupButton.Text = description;
            }
        }

        //MD5 Hesaplama
        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        void folderCheck(string path)
        {
            
                //yedekler klasörünü kontrol ettik, bulunmuyorsa oluşturduk
                if (!File.Exists($"{path}\\{folderName}"))
                    Directory.CreateDirectory($"{path}\\{folderName}");
            
        }

        private async Task backupFolder(string path)
        {
            await Task.Run(() =>
            {
                // Zaman eklemesini yaptık.
                string date = DateTime.Now.ToString("dd-MM-yyyy H/mm");

                //Kaçıncı revizyon olduğunu öğrenmek için klasördeki toplam klasör sayısını bulduk
                int directoryCount = System.IO.Directory.GetDirectories($"{path}\\{folderName}").Length;
                //İçinde bulunduğumuz klasörün adını aldık
                string lastFolderName = Path.GetFileName(Directory.GetCurrentDirectory());

                //Yeni klasör adı belirle ve yeni klasör oluştur,
                string newFolderPath = $"{path}\\{folderName}\\{lastFolderName} Rev{directoryCount + 1} Date {date}";
                Directory.CreateDirectory(newFolderPath);
                //directory info'ya çevirdim.
                DirectoryInfo DIPath = new DirectoryInfo(path);
                DirectoryInfo DInewFolderPath = new DirectoryInfo(newFolderPath);

                //Recursive kopyalama fonksiyonunu çağırdım.
                CopyFilesRecursively(DIPath, DInewFolderPath);
            });
        }

        public static async Task CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)


        {
            await Task.Run(() =>
            {
                //Klasörleri bul ve kopyala
                foreach (DirectoryInfo dir in source.GetDirectories())
                {
                    //Yedekler klasörünü pas geçtim
                    if (dir.Name == folderName)
                        continue;
                    CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
                }
                //Dosyaları bul ve kopyala
                foreach (FileInfo file in source.GetFiles())
                {
                    //Uygulamayı pas geçiyoruz bunu hash kontrolüne döndüreceğim
                    if (file.Name == appName)
                        continue;

                    file.CopyTo(Path.Combine(target.FullName, file.Name));
                }
            });
          


        }

        public static async Task DeleteFilesRecursively(DirectoryInfo source)
        {
            await Task.Run(() =>
            {
                //Klasörleri bul ve sil
                foreach (DirectoryInfo dir in source.GetDirectories())
                {
                    //Yedekler klasörünü pas geçtim
                    if (dir.Name == folderName)
                        continue;
                    Directory.Delete(dir.FullName, true);
                }
                //Dosyaları bul ve sil
                foreach (FileInfo files in source.GetFiles())
                {
                    //Uygulamayı pas geçiyoruz bunu hash kontrolüne döndüreceğim
                    if (files.Name == appName)
                        continue;

                    File.Delete(Path.Combine(source.FullName, files.Name));
                }
            });
        }

        public static async Task CopyFilesRecursivelyAndOverride(DirectoryInfo source, DirectoryInfo target)
        {
            await Task.Run(() =>
            {
                //Klasörleri bul ve kopyala
                foreach (DirectoryInfo dir in source.GetDirectories())
                {
                    //Yedekler klasörünü pas geçtim
                    if (dir.Name == folderName)
                        continue;
                    CopyFilesRecursivelyAndOverride(dir, target.CreateSubdirectory(dir.Name));
                }
                //Klasörleri bul ve üzerine yaz
                foreach (FileInfo file in source.GetFiles())
                {
                    if (file.Name == appName)
                        continue;

                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                }
            });
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void backupButton_Click(object sender, EventArgs e)
        {
            EnableAndDisableButtons("Yedekleniyor...");
            //bulunulan klasör yolunu aldık
            var CurrentDirectory = Directory.GetCurrentDirectory();
            await backupFolder(CurrentDirectory);
            await Task.Delay(500);
            EnableAndDisableButtons();
        }

        private async void rollbackButton_Click(object sender, EventArgs e)
        {
            EnableAndDisableButtons("Düşürülüyor...");
            //bulunulan klasör yolunu aldık
            var CurrentDirectory = Directory.GetCurrentDirectory();
            await rollback(CurrentDirectory);
            await Task.Delay(500);
            EnableAndDisableButtons();
        }

        private void Yedekle_Load(object sender, EventArgs e)
        {
            
            //bulunulan klasör yolunu aldık
            var CurrentDirectory = Directory.GetCurrentDirectory();
            folderCheck(CurrentDirectory);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private async void clearButton_Click(object sender, EventArgs e)
        {
            EnableAndDisableButtons("Temizleniyor...");
            //bulunulan klasör yolunu aldık
            var path = Directory.GetCurrentDirectory();
            DirectoryInfo DICurrentDirectory = new DirectoryInfo(path);
            await DeleteFilesRecursively(DICurrentDirectory);
            await Task.Delay(500);
            EnableAndDisableButtons();
        }


        //Kenarları dışında tutup sürülemek için
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Yedekle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnTray_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int _eggCounter = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (_eggCounter>6)
            {
                MessageBox.Show("İnsanlığın işini kolaylaştırmak için Kemal SANLI Tarafından 2021 yılında üretilmiştir. Uygulamayı istediğiniz gibi paylaşmakta özgürsünüz. \n\n github.com/kemalsanli", "Hakkında");
            }
            _eggCounter++;
        }
    }
}
