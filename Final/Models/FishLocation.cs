using Final.Models;
using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

public class FishLocation
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int FishID { get; set; }
    public int LocationID { get; set; }

    [ForeignKey("Fish")]
    public Fish Fish { get; set; }

    [ForeignKey("Location")]
    public Final.Models.Location Location { get; set; }
}
