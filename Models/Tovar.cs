namespace Kursach.Models
{
    public class Tovar
    {
        public int Id { get; set; }
        public string Genre { get; set; }

        // Названия полей dev и devId должны быть согласованы
        public int bokId { get; set; }
        public Bookname bok { get; set; } // Ссылка на запись таблицы Devices  


        // Названия полей firm и firmId должны быть согласованы
        public int authorId { get; set; }
        public Author aut { get; set; } // Ссылка на запись таблицы Firms  

        public int Price { get; set; }
        public int Kol { get; set; }

    }
}
