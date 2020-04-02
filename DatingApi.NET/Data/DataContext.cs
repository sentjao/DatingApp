using Microsoft.EntityFrameworkCore;
using DatingApi.NET.Models;

namespace DatingApi.NET.Data
{
    public class DataContext: DbContext
    {
       public DataContext(DbContextOptions<DataContext> options):base(options){}
       public DbSet<Value> Values{get; set;}
       public DbSet<User> Users{get; set;}
       public DbSet<Photo> Photos{get; set;} 

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           modelBuilder.Entity<Photo>()
            .HasOne<User>(x=>x.User)
            .WithMany(x=>x.Photos)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
       }
    }
}