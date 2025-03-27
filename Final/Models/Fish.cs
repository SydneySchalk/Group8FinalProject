using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Fish
    {
        [PrimaryKey]
        public int FishID { get; set; }
        public string Name { get; set; }
        public bool Spring { get; set; } // True if can be caught in spring
        public bool Summer { get; set; } // True if can be caught in summer
        public bool Fall { get; set; } // True if can be caught in fall
        public bool Winter { get; set; } // True if can be caught in winter
        public bool Sun { get; set; } // True if can be caught in sun
        public bool Rain { get; set; } // True if can be caught in rain
        public string Time { get; set; }
        public string Image { get; set; }
        public bool Obtained { get; set; }
    }

}
