using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MusicSearchApp.Models.DB
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        #region DbSets

        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PublishRequest> PublishRequests { get; set; }
        public DbSet<Action> Actions { get; set; }

        #endregion

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region PK configuration

            modelBuilder.Entity<Subscription>()
                .HasKey(s => new { s.SubscriberId, s.ArtistId} );
            modelBuilder.Entity<Genre>()
                .HasKey(g => g.Name);
            modelBuilder.Entity<PublishRequest>()
                .HasKey(r => r.RequestId);
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("UserId");
  
            #endregion
            
            #region FK configuration

            #region User FK
            //User - Song
            modelBuilder.Entity<User>()
                .HasMany(u => u.Songs)
                .WithOne(s => s.Artist)
                .HasForeignKey(s => s.ArtistId)
                .OnDelete(DeleteBehavior.ClientCascade);

            //User - Action
            modelBuilder.Entity<User>()
                .HasMany(u => u.Actions)
                .WithOne(a => a.Actor)
                .HasForeignKey(a => a.ActorId);

            //User(Subscriber) - Subsciption
            modelBuilder.Entity<User>()
                .HasMany(u => u.Subscriptions)
                .WithOne(s => s.Subscriber)
                .HasForeignKey(s => s.SubscriberId)
                .OnDelete(DeleteBehavior.ClientCascade);

            //User(Artist) - Subsciption
            modelBuilder.Entity<User>()
                .HasMany(u => u.Subsribers)
                .WithOne(s => s.Artist)
                .HasForeignKey(s => s.ArtistId);
            
            //User - Album
            modelBuilder.Entity<User>()
                .HasMany(u => u.Albums)
                .WithOne(a => a.Artist)
                .HasForeignKey(a => a.ArtistId);



            //User - Comment
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.ClientCascade);

            //User - Request
            modelBuilder.Entity<User>()
                .HasMany(u => u.Requests)
                .WithOne(r => r.Artist)
                .HasForeignKey(r => r.ArtistId)
                .OnDelete(DeleteBehavior.ClientCascade);

            //User - News
            modelBuilder.Entity<User>()
                .HasMany(u => u.News)
                .WithOne(n => n.Publisher)
                .HasForeignKey(n => n.PublisherId)
                .OnDelete(DeleteBehavior.ClientCascade);

            #endregion

            #region Song FK


            //Song - Comment. Cascade since User - Comment ClientCascade
            modelBuilder.Entity<Song>()
                .HasMany(s => s.Comments)
                .WithOne(c => c.Song)
                .HasForeignKey(c => c.SongId);

            #endregion

            #region Album FK

            //Album - Song
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Songs)
                .WithOne(s => s.Album)
                .HasForeignKey(s => s.AlbumId);

            
            //Album - News. Cascade since User - News ClientCascade
            modelBuilder.Entity<Album>()
                .HasOne(a => a.PublishNews)
                .WithOne(n => n.Album)
                .HasForeignKey<News>(n => n.AlbumId);

                
            //Album - Request. Cascade since User - Request ClientCascade
            modelBuilder.Entity<Album>()
                .HasOne(a => a.Request)
                .WithOne(r => r.Album)
                .HasForeignKey<PublishRequest>(r => r.AlbumId);

            #endregion
 
            //Genre - Song 
            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Songs)
                .WithOne(s => s.Genre)
                .HasForeignKey(s => s.GenreName)
                .OnDelete(DeleteBehavior.SetNull);

            #endregion
        
            #region Unique configuration

            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<Song>().HasIndex(s => s.FilePath).IsUnique();

            #endregion
 
            modelBuilder.Entity<User>().Property(u => u.UserName).HasMaxLength(30);
            base.OnModelCreating(modelBuilder);
            
        }
    }
}