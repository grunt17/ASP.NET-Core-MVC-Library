using Microsoft.EntityFrameworkCore;

namespace Kursach.Models
{
    public class LibraryContext: DbContext
    {
        public DbSet<Bookname> Booknames { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tovar> Tovars { get; set; }
        public DbSet<Korzina> Korzinas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Korzina2> Korzinas2 { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tovary> Tovarys { get; set; }


        public LibraryContext(DbContextOptions<LibraryContext> options)
              : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
