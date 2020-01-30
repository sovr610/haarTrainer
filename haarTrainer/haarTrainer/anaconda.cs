using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haarTrainer
{
    public class anaconda
    {
        public void runGoogle()
        {
            // Set working directory and create process
            var workingDirectory = Path.GetFullPath("Scripts");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WorkingDirectory = workingDirectory
                }
            };
            process.Start();
            // Pass multiple commands to cmd.exe
            using (var sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    // Vital to activate Anaconda
                    sw.WriteLine("C:\\Users\\Parker\\Anaconda3\\Scripts\\activate.bat");
                    // Activate your environment
                    sw.WriteLine("activate nlp_bot");
                    // Any other commands you want to run
                    // run your script. You can also pass in arguments
                    sw.WriteLine("python google_images_download.py -cf config.json");
                }
            }

            // read multiple output lines
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
        }
    }
}
