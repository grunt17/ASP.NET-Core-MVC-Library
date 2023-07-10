namespace Kursach.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Genre { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public int Kol { get; set; }
        public string CustomerName { get; set; }
    }
}
