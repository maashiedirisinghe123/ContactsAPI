using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace ContactsAPI.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Contacts> Contacts { get; set; }
    }
}
