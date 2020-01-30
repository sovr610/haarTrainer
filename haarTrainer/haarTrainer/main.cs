using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace haarTrainer
{
    public partial class main : Form
    {
        private string[] hist;
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\haar_training\\";
        public main()
        {
            InitializeComponent();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            else
            {
                if(!File.Exists(dir + "history.dat"))
                {
                    File.WriteAllLines(dir + "history.dat", new string[] { "test" });
                }
                hist = File.ReadAllLines(dir + "history.dat");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            string db = of.FileName;

            string[] keywords = File.ReadAllLines(db);

            if (!File.Exists(dir + "history.dat"))
            {
                File.WriteAllLines(dir + "history.dat", new string[] { "test" });
                foreach (var key in keywords)
                {
                    File.AppendAllLines(dir + "history.dat", new string[] { key });
                }
            }

            List<string> good_keywords = new List<string>();
            foreach (var key in keywords)
            {
                var IE = from a in hist
                         where a == key
                         select a;
                if (IE.Count() == 0)
                {
                    //records r = new records();
                    File.AppendAllLines(dir + "history.dat", new string[] { key });
                    good_keywords.Add(key);
                }
            }
            
            foreach (var kee in good_keywords) {
                if (File.Exists(Environment.CurrentDirectory + "\\scripts\\config.bat"))
                {
                    File.Delete(Environment.CurrentDirectory + "\\scripts\\config.bat");
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("@echo off");
                sb.AppendLine("cd scripts");
                sb.AppendLine("python google_images_download.py -k " + kee + " -l 400 -s medium -f jpg --chromedriver C:\\Users\\Parker\\Desktop\\tensor_flow\\haarTrainer\\haarTrainer\\haarTrainer\\bin\\Debug\\scripts\\chromedriver.exe");
                sb.AppendLine("pause");
                File.WriteAllText(Environment.CurrentDirectory + "\\scripts\\config.bat", sb.ToString());
                var proc = Process.Start(Environment.CurrentDirectory + "\\scripts\\config.bat");
                proc.WaitForExit();
            }

            foreach (var kee in good_keywords)
            {
                string[] imgs = Directory.GetFiles(Environment.CurrentDirectory + "\\scripts\\downloads\\" + kee);
                string[] pos = Directory.GetFiles(Environment.CurrentDirectory + "\\positive");
                int index = 0;

                foreach (var img in pos)
                {
                    File.Delete(img);
                }



                foreach(string f in imgs)
                {

                    string file = Environment.CurrentDirectory + "\\positive\\positive_" + index + ".jpg";
                    File.Copy(f, Environment.CurrentDirectory + "\\positive\\positive_" + index + ".jpg");
                    //System.Drawing.Image img = System.Drawing.Image.FromFile(Environment.CurrentDirectory + "\\positive\\positive_" + index + ".jpg");
                    int width = 800;
                    int height = 600;
                  
                    if(!File.Exists(Environment.CurrentDirectory + "\\positive\\info.txt"))
                    {
                        File.WriteAllLines(Environment.CurrentDirectory + "\\positive\\info.txt", new string[] { file + " 1 0 0 " + width + " " + height });

                    }
                    else
                    {
                        File.AppendAllLines(Environment.CurrentDirectory + "\\positive\\info.txt", new string[] { file + " 1 0 0 " + width + " " + height });
                    }
                    index++;

                }



                var pp = Process.Start(Environment.CurrentDirectory + "\\01 samples_creation.bat");
                pp.WaitForExit();
            }
            Console.ReadLine();


        }
    }
}
