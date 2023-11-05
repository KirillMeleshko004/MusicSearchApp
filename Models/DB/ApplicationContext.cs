using Microsoft.EntityFrameworkCore;

namespace MusicSearchApp.Models.DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PublishRequest> PublishRequests { get; set; }
        public DbSet<Action> Actions { get; set; }


        public ApplicationContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User configuration
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired();
            modelBuilder.Entity<User>().HasMany<Song>().WithOne();
            #endregion
        }
    }
}