namespace Consumption
{
    using SQLite;
    using System;

    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed, MaxLength(50)]
        public string Tag { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public DateTime DateTime { get; set; }
    }
}