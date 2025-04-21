using SQLite;

namespace Final.Models
{
    public class FishLocation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Foreign Key for Fish
        public int FishID { get; set; }

        // Foreign Key for Location
        public int LocationID { get; set; }

        // Navigation properties for Fish and Location
        [Ignore]  // Ignored by SQLite, not stored in the DB, but useful for object relationships
        public Fish Fish { get; set; }
        [Ignore]
        public Location Location { get; set; }
    }
}
