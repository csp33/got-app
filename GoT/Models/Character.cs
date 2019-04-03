using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoT.Models
{
    public class Character
    {
        public string url { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string culture { get; set; }
        public string born { get; set; }
        public string died { get; set; }
        public List<string> titles { get; set; } = new List<string>();
        public List<string> aliases { get; set; } = new List<string>();
        public string father { get; set; }
        public string mother { get; set; }
        public string spouse { get; set; }
        public List<string> allegiances { get; set; } = new List<string>();
        public List<string> books { get; set; } = new List<string>();
        public List<string> povBooks { get; set; } = new List<string>();
        public List<string> tvSeries { get; set; } = new List<string>();
        public List<string> playedBy { get; set; } = new List<string>();

        public override string ToString()
        {
            return name;
        }
    }
}
