using EfData.Entities;
using EfData.Model;
using Microsoft.EntityFrameworkCore;

namespace EfData.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserSubscription> UserSubscription { get; set; }

        public DbSet<ThemeDictionary> ThemeDictionary { get; set; }
        public DbSet<SystemsDictionary> SystemsDictionary { get; set; }

        public DbSet<Notification> Notification { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<UserSubscription>()
                .Property<int>("SystemId");
            modelBuilder.Entity<UserSubscription>()
                .HasOne(p => p.SystemsDictionary)
                .WithMany(b => b.UserSubscription)
                .HasForeignKey("SystemId")
                .HasPrincipalKey(y => y.Id);

            modelBuilder.Entity<UserSubscription>()
                .Property<int>("ThemeId");
            modelBuilder.Entity<UserSubscription>()
                .HasOne(p => p.ThemeDictionary)
                .WithMany(b => b.UserSubscription)
                .HasForeignKey("ThemeId")
                .HasPrincipalKey(y => y.Id);




            modelBuilder.Entity<Notification>()
                  .Property<int>("UserId");

            modelBuilder.Entity<Notification>()
                .HasOne(p => p.User)
                .WithMany(b => b.Notification)
                .HasForeignKey("UserId")
                .HasPrincipalKey(y => y.Id);

            modelBuilder.Entity<UserSubscription>()
                   .Property<int>("UserId");

            modelBuilder.Entity<UserSubscription>()
                .HasOne(p => p.User)
                .WithMany(b => b.UserSubscription)
                .HasForeignKey("UserId")
                .HasPrincipalKey(y => y.Id);

            modelBuilder.Entity<ThemeDictionary>()
                  .Property<int>("SystemsDictionaryId");

            modelBuilder.Entity<ThemeDictionary>()
                .HasOne(p => p.System)
                .WithMany(b => b.Themes)
                .HasForeignKey("SystemsDictionaryId")
                .HasPrincipalKey(y => y.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
