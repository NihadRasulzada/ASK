using App.Core.Entities;
using App.Core.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.DAL.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<President> Presidents { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity cədvəllərinin konfiqurasiyası üçün vacibdir
            base.OnModelCreating(modelBuilder);

            // App.DAL assembly-sindəki bütün IEntityTypeConfiguration<T>
            // implementasiyalarını avtomatik tapır və tətbiq edir
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// AuditableEntity-lər üçün CreatedOn və UpdatedOn sahələrini avtomatik setləyir.
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTimeOffset.UtcNow;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = now;
                        entry.Entity.UpdatedOn = now;
                        // CreatedBy / UpdatedBy: Auth implementasiyasından sonra real Guid doldurulacaq
                        // entry.Entity.CreatedBy = currentUserId;
                        // entry.Entity.UpdatedBy = currentUserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = now;
                        // entry.Entity.UpdatedBy = currentUserId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
