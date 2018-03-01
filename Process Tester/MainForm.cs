using FolderBrowserTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.Util;
using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using SV360.Data;
using SV360.Preprocessing;
using SV360.Image_Processing;
using System.Diagnostics;
using Process_Tester.Utils;
using System.Text.RegularExpressions;

namespace Process_Tester
{
    public partial class MainForm : Form
    {
        string userSelection;
        public MainForm()
        {
            InitializeComponent();
            userSelection = lbPath.Text = Properties.Settings.Default.savePath;
        }

        private void btLoad_Click(object sender, EventArgs e)
        {

            FolderBrowser fb = new FolderBrowser();
            fb.Description = "Please select a file or folder below:";
            fb.IncludeFiles = true;
            if (Properties.Settings.Default.savePath == null || Properties.Settings.Default.savePath == "")
                fb.InitialDirectory = @"C:\";
            else
                fb.InitialDirectory = Properties.Settings.Default.savePath;

            if (fb.ShowDialog() == DialogResult.OK)
            {
                userSelection = fb.SelectedPath;
                Properties.Settings.Default.savePath = userSelection;
                Properties.Settings.Default.Save();
                lbPath.Text = userSelection;
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            PrintImage printImage = new PrintImage(userSelection);
            printImage.Show();
        }

        private Seed SV360process(Image<Rgb, byte> imBg, Image<Rgb, byte> imSrc)
        {
            ConvoyeurPP convoyeurPP = new ConvoyeurPP();
           /* string path = @"C:\Users\damien.delhay\Documents\Travail\SV360\Validation machine\images calibration" + @"\";
            Image<Rgb, byte> imBg = new Image<Rgb, byte>(bg);
            Image<Rgb, byte> imSrc = new Image<Rgb, byte>(im);*/

            convoyeurPP.AddBg(imBg);

            SVTimer timer = new SVTimer();

            Seed seed = new Seed(new Acquisition(imSrc));

            convoyeurPP.preprocessing(seed.ImageCollection[0]);

            MorphoIP morphoIp = new MorphoIP();
            morphoIp.process(seed);

            Console.WriteLine("VLength: " + seed.VLength.ToString());
            Console.WriteLine("VWidth: " + seed.VWidth.ToString());
            Console.WriteLine("VThickness: " + seed.VThickness.ToString());

            return seed;
        }

        private void btExe_Click(object sender, EventArgs e)
        {

            processRecurse();
           /* ConvoyeurPP convoyeurPP = new ConvoyeurPP();
            string path = @"C:\Users\damien.delhay\Documents\Travail\SV360\Validation machine\images calibration" + @"\";
            Image<Rgb, byte> imBg = new Image<Rgb, byte>(path + "bg.bmp");
            Image<Rgb, byte> imSrc = new Image<Rgb, byte>(path + "image6.bmp");

            float time = 0;
            convoyeurPP.AddBg(imBg);
            for (int i = 0; i < 1; i++)
            {
                SVTimer timer = new SVTimer();

                List<Seed> seeds = new List<Seed>();
                seeds.Add(new Seed(new Acquisition(imSrc)));

                convoyeurPP.preprocessing(seeds[0].ImageCollection[0]);

                MorphoIP morphoIp = new MorphoIP();
                morphoIp.process(seeds[0]);

                Console.WriteLine("VLength: " + seeds[0].VLength.ToString());
                Console.WriteLine("VWidth: " + seeds[0].VWidth.ToString());
                Console.WriteLine("VThickness: " + seeds[0].VThickness.ToString());

                time += timer.GetTime();
                Debug.WriteLine(timer.ToString());
            }
            time /= 100;
            Debug.WriteLine("tps moy = " + time * 1000);*/
        }

        private void processRecurse()
        {

            string folderPath = @"C:\Users\damien.delhay\Documents\Travail\SV360\Validation machine\images calibration";
            string bgPath = folderPath + @"\bg.bmp";
            //static string rootPath = @"P:\Damien\Programmation\base de données\Images" + @"\";
            string rootPath = folderPath + @"\";// + "image6.bmp";
            //static string rootPath = @"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\test_IDS\test CALIBRAGE" + @"\";
            bool saveExcel = true;


            if (!File.Exists(bgPath)) throw new Exception("bgPath is wrong");

            Image<Rgb, Byte> imBg = new Image<Rgb, Byte>(new Bitmap(bgPath));

            if (File.Exists(rootPath))
            {

                System.IO.FileInfo FIRootPath = new FileInfo(rootPath);
                Debug.WriteLine(FIRootPath.DirectoryName);
                string excelPath = FIRootPath.DirectoryName + @"\data-" + DateTime.Now.ToString("dd-MM-yy_HH'h'mm'm'ss's'") + ".xlsx";
                ExcelStatsWriter excelWriter = new ExcelStatsWriter(excelPath);
                processFile(rootPath, imBg, excelWriter);

                if (saveExcel)
                {
                    Debug.WriteLine("Save on " + excelPath);
                    excelWriter.Save();
                    System.Diagnostics.Process.Start(excelPath);
                }
                else excelWriter.Dispose();
            }
            else if (Directory.Exists(rootPath))
            {
                string excelPath = rootPath + "data-" + DateTime.Now.ToString("dd-MM-yy_HH'h'mm'm'ss's'") + ".xlsx";
                ExcelStatsWriter excelWriter = new ExcelStatsWriter(excelPath);
                processDirectory(rootPath, imBg, excelWriter);

                if (saveExcel)
                {
                    Debug.WriteLine("Save on " + excelPath);
                    excelWriter.Save();
                    System.Diagnostics.Process.Start(excelPath);
                }
                else excelWriter.Dispose();
            }
            else
                Console.WriteLine("{0} is not a valid file or directory.", rootPath);

        }

        void processDirectory(string path, Image<Rgb, Byte> imBg, ExcelStatsWriter excelWriter)
        {
            string[] fileEntries = Directory.GetFiles(path, "image*.*");
            Array.Sort(fileEntries, (s1, s2) => s1.CompareTo(s2));
            Array.Sort(fileEntries, (a, b) => int.Parse(Regex.Replace(a, "[^00-99]", "")) - int.Parse(Regex.Replace(b, "[^00-99]", "")));


            excelWriter.Write(path);

            foreach (string fileName in fileEntries)
                processFile(fileName, imBg, excelWriter);



            // Recurse into subdirectories of this directory.

            string[] subdirectoryEntries = Directory.GetDirectories(path);
            foreach (string subdirectory in subdirectoryEntries)
                processDirectory(subdirectory, imBg, excelWriter);
        }

       

        void processFile(string fileName, Image<Rgb, Byte> imBg, ExcelStatsWriter excelWriter)
        {
            try
            {
                FileInfo fi = new FileInfo(fileName);
                Debug.WriteLine(fileName);
                Image<Rgb, Byte> imOrigin = new Image<Rgb, Byte>(fileName);

                //ExcelStatsWriter.processAndSave(imOrigin, imBg, excelWriter);

                Seed s = SV360process(imBg, imOrigin);

                List<object> dataExcel = new List<object>();

                
                dataExcel.Add(s.VLength.X);
                dataExcel.Add(s.VLength.Y);
                dataExcel.Add(s.VLength.Z);
                dataExcel.Add(s.VWidth.X);
                dataExcel.Add(s.VWidth.Y);
                dataExcel.Add(s.VWidth.Z);
                dataExcel.Add(s.VThickness.X);
                dataExcel.Add(s.VThickness.Y);
                dataExcel.Add(s.VThickness.Z);
                dataExcel.Add(fi.Name);
                dataExcel.Add(s.CoefMirror);
                excelWriter.WriteStats(dataExcel);
            }
            catch (Exception e)
            {
            }
        }

    }
}
