using System.ComponentModel.DataAnnotations;

namespace Kursach.Models
{
    public class Customer
    {

        public int CustomerId { get; set; }
        
        public string CustomerName { get; set; }
    }
}
