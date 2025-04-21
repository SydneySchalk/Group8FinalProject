using SQLite;
using System;

namespace Final.Models
{
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int LocationID { get; set; }
        public string LocationName { get; set; }

        public Location() { }
    }
}

