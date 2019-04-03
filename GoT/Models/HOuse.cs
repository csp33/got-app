using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoT.Models
{
    public class House
    {
        public string url { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public string coatOfArms { get; set; }
        public string words { get; set; }
        public List<string> titles { get; set; } = new List<string>();
        public List<string> seats { get; set; } = new List<string>();
        public string currentLord { get; set; }
        public string heir { get; set; }
        public string overlord { get; set; }
        public string founded { get; set; }
        public string founder { get; set; }
        public string diedOut { get; set; }
        public List<string> ancestralWeapons { get; set; } = new List<string>();
        public List<string> cadetBranches { get; set; } = new List<string>();
        public List<string> swornMembers { get; set; } = new List<string>();

        public override string ToString()
        {
            return name;
        }

    }
}
