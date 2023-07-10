using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace Kursach.Models
{
    public class Tovary
    {
        public int Id { get; set; }
        public string Genre { get; set; }
        public string BookName { get; set; }  
        public string Author { get; set; } 
        public int Price { get; set; }
        public int Kol { get; set; }
    }
    public class FilterListViewModel
    {
        public IEnumerable<Bookname> Booknames { get; set; }
        public SelectList Genres { get; set; }
        public SelectList Authors { get; set; }
    }
}
