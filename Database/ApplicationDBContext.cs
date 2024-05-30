using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIDiscussion.Database
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }

    public class Book
    {
        [ForeignKey("Id")]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string PublisherName { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
