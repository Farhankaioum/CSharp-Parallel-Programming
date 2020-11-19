using Microsoft.EntityFrameworkCore;
using WizLib.Models.Models;

namespace WizLib_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetails> BookDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasOne(bd => bd.BookDetails)
                .WithOne(b => b.Book)
                .HasForeignKey<Book>("BookDetailsId");

            base.OnModelCreating(builder);
        }
    }
}
