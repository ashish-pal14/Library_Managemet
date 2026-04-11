using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN)
            .IsUnique();

        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.Book)
            .WithMany()
            .HasForeignKey(b => b.BookId);

        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.Member)
            .WithMany()
            .HasForeignKey(b => b.MemberId);
    }
}
