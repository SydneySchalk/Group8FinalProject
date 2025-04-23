using SQLite;
using System;

namespace Final.Models
{
    public class Fish
    {
        [PrimaryKey, AutoIncrement]
        public string Name { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty; // Comma-separated values: Spring, Summer, Fall, Winter
        public string Weather { get; set; } = string.Empty; // Comma-separated values: Sun, Rain
        public string Time { get; set; }
        public string Image { get; set; } = string.Empty; // Image path
        public bool Obtained { get; set; }

        // Constructor matching all the fields except FishID (usually auto-assigned)
        public Fish(string name, string season, string weather, string time, string image, bool obtained)
        {
            Name = name;
            Season = season; // Example: "Spring,Summer"
            Weather = weather; // Example: "Sun,Rain"
            Time = time;
            Image = image;
            Obtained = obtained;
        }

        // Parameterless constructor for SQLite
        public Fish() { }
    }
}


