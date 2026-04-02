using App.Core.Entities;
using App.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace App.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public bool IgnoreSoftDeleteFilter { get; set; } = false;

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
        public DbSet<Service> Services { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Management> Management { get; set; }
        public DbSet<InternationalSolidarity> InternationalSolidarity { get; set; }
        public DbSet<ForeignRepresentatives> ForeignRepresentatives { get; set; }
        public DbSet<DistrictRepresentatives> DistrictRepresentatives { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Presidium> Presidium { get; set; }
        public DbSet<OurValues> OurValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplySoftDeleteQueryFilters(modelBuilder);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplySoftDeleteQueryFilters(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(SoftDeletableEntity).IsAssignableFrom(entityType.ClrType))
                    continue;

                // Only apply to root entity types; derived types inherit the filter
                if (entityType.BaseType != null)
                    continue;

                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var body = Expression.OrElse(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(SoftDeletableEntity.IsDeactive)),
                        Expression.Constant(false)
                    ),
                    Expression.Property(
                        Expression.Constant(this),
                        nameof(IgnoreSoftDeleteFilter)
                    )
                );

                var lambdaType = typeof(Func<,>).MakeGenericType(entityType.ClrType, typeof(bool));
                var lambda = Expression.Lambda(lambdaType, body, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
