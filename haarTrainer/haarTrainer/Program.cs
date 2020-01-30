using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haarTrainer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            main m = new main();
            m.ShowDialog();
        }
    }

    public class config_file
    {
        public List<records> Records = new List<records>();
    }

    public class records
    {
        public string keywords;
        public int limit;
        public string color;
        public bool print_urls;
    }
}
