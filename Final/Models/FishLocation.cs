using Final.Models;
using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
using Location = Final.Models.Location;

public class FishLocation
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int FishID { get; set; }
    public int LocationID { get; set; }

    public Fish Fish { get; set; }
    public Location Location { get; set; }
}