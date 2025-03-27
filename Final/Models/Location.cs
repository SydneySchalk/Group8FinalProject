using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }

}
