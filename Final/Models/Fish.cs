using SQLite;
using System;

namespace Final.Models
{
    public class Fish
    {
        [PrimaryKey, AutoIncrement]
        public int FishID { get; set; }
        public string Name { get; set; }
        public bool Spring { get; set; } // True if can be caught in spring
        public bool Summer { get; set; } // True if can be caught in summer
        public bool Fall { get; set; }   // True if can be caught in fall
        public bool Winter { get; set; } // True if can be caught in winter
        public bool Sun { get; set; }    // True if can be caught in sun
        public bool Rain { get; set; }   // True if can be caught in rain
        public string Time { get; set; }
        public string Image { get; set; }
        public bool Obtained { get; set; }

        // Constructor matching all the fields except FishID (usually auto-assigned)
        public Fish(string name, bool spring, bool summer, bool fall, bool winter,
                    bool sun, bool rain, string time, string image, bool obtained)
        {
            Name = name;
            Spring = spring;
            Summer = summer;
            Fall = fall;
            Winter = winter;
            Sun = sun;
            Rain = rain;
            Time = time;
            Image = image;
            Obtained = obtained;
        }

        // Parameterless constructor for SQLite
        public Fish() { }
    }
}


