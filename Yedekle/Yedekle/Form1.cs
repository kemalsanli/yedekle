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

        void rollback(String path)
        {
            if (System.IO.Directory.GetDirectories($"{path}\\{folderName}").Length > 0)
            {
                
                DirectoryInfo lastBackup = new DirectoryInfo($"{path}\\{folderName}").GetDirectories()
                       .OrderByDescending(d => d.LastWriteTimeUtc).First();
                DirectoryInfo DIPath = new DirectoryInfo(path);
                CopyFilesRecursivelyAndOverride(lastBackup, DIPath);
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

        void backupFolder(string path)
        {

            // Zaman eklemesini yaptık.
            string date = DateTime.Now.ToString("dd-MM-yyyy H/mm");
            label1.Text = date;

            //Kaçıncı revizyon olduğunu öğrenmek için klasördeki toplam klasör sayısını bulduk
            int directoryCount = System.IO.Directory.GetDirectories($"{path}\\{folderName}").Length;
            //İçinde bulunduğumuz klasörün adını aldık
            string lastFolderName = Path.GetFileName(Directory.GetCurrentDirectory());

            //Yeni klasör adı belirle ve yeni klasör oluştur,
            string newFolderPath = $"{path}\\{folderName}\\{lastFolderName} Rev{directoryCount+1} Date {date}";
            label1.Text = newFolderPath;

            Directory.CreateDirectory(newFolderPath);

            //directory info'ya çevirdim.
            DirectoryInfo DIPath = new DirectoryInfo(path);
            DirectoryInfo DInewFolderPath = new DirectoryInfo(newFolderPath);

            //Recursive kopyalama fonksiyonunu çağırdım.
            CopyFilesRecursively(DIPath,DInewFolderPath);
        }

        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories()) {
                //Yedekler klasörünü pas geçtim
                if(dir.Name == folderName)
                         continue;
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
                    }
            foreach (FileInfo file in source.GetFiles()) {
                //Uygulamayı pas geçiyoruz bunu hash kontrolüne döndüreceğim
                if (file.Name == appName)
                    continue;

                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        }

        public static void DeleteFilesRecursively(DirectoryInfo source)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                //Yedekler klasörünü pas geçtim
                if (dir.Name == folderName)
                    continue;
                Directory.Delete(dir.FullName, true);
            }
            foreach (FileInfo files in source.GetFiles())
            {
                //Uygulamayı pas geçiyoruz bunu hash kontrolüne döndüreceğim
                if (files.Name == appName)
                    continue;

                File.Delete(Path.Combine(source.FullName, files.Name));
            }
        }

        public static void CopyFilesRecursivelyAndOverride(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                //Yedekler klasörünü pas geçtim
                if (dir.Name == folderName)
                    continue;
                CopyFilesRecursivelyAndOverride(dir, target.CreateSubdirectory(dir.Name));
            }
            foreach (FileInfo file in source.GetFiles())
            {
                if (file.Name == appName)
                    continue;

                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void backupButton_Click(object sender, EventArgs e)
        {
            var CurrentDirectory = Directory.GetCurrentDirectory();
            backupFolder(CurrentDirectory);
        }

        private void rollbackButton_Click(object sender, EventArgs e)
        {
            //bulunulan klasör yolunu aldık
            var CurrentDirectory = Directory.GetCurrentDirectory();
            rollback(CurrentDirectory);
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

        private void clearButton_Click(object sender, EventArgs e)
        {
            //bulunulan klasör yolunu aldık
            var path = Directory.GetCurrentDirectory();
            DirectoryInfo DICurrentDirectory = new DirectoryInfo(path);
            DeleteFilesRecursively(DICurrentDirectory);
        }
    }
}
