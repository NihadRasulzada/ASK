using App.Core.Entities;
using App.Core.Entities.Common;
using App.Core.Entities.Identity;
using App.Core.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.AccessControl;

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
        public DbSet<Video> Videos { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry<BaseEntity>> entries = ChangeTracker.Entries<BaseEntity>();
            List<AuditLog> allAuditLogs = new List<AuditLog>();

            IEnumerable<EntityEntry<AppUser>> appUserEntries = ChangeTracker.Entries<AppUser>();
            IEnumerable<EntityEntry<AppRole>> appRoleEntries = ChangeTracker.Entries<AppRole>();

            // Process AppUser and AppRole changes manually
            allAuditLogs.AddRange(ProcessAppUserChanges(appUserEntries, AuditAction.Update));
            allAuditLogs.AddRange(ProcessAppRoleChanges(appRoleEntries, AuditAction.Update));

            foreach (EntityEntry<BaseEntity> entry in entries)
            {
                BaseEntity entity = entry.Entity;
                //get userId from context or service provider
                string userId = _currentUserService.UserId; // Replace with actual logic to get the userId, e.g., from a service or context

                if (entry.State == EntityState.Added)
                {
                    DateTime date = DateTime.UtcNow;
                    foreach (IProperty property in entry.CurrentValues.Properties)
                    {
                        allAuditLogs.Add(new AuditLog
                        {
                            ChangeDate = date,
                            ChangeType = AuditAction.Create,
                            EntityId = entity.Id,
                            EntityName = entity.GetType().Name,
                            PropertyName = property.Name,
                            UserId = userId,
                            NewValue = entry.CurrentValues[property]?.ToString(),
                        });
                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    DateTime date = DateTime.UtcNow;
                    foreach (IProperty property in entry.CurrentValues.Properties)
                    {
                        allAuditLogs.Add(new AuditLog
                        {
                            ChangeDate = date,
                            ChangeType = AuditAction.HardDelete,
                            EntityId = entity.Id,
                            EntityName = entity.GetType().Name,
                            UserId = userId,
                        });
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    bool isDeletedOriginalValue = (bool)entry.OriginalValues["IsDeactive"];
                    bool isDeletedCurrentValue = (bool)entry.CurrentValues["IsDeactive"];

                    if (isDeletedOriginalValue != isDeletedCurrentValue)
                    {
                        DateTime date = DateTime.UtcNow;
                        foreach (IProperty property in entry.CurrentValues.Properties)
                        {
                            allAuditLogs.Add(new AuditLog
                            {
                                ChangeDate = date,
                                ChangeType = AuditAction.SoftDelete,
                                EntityId = entity.Id,
                                EntityName = entity.GetType().Name,
                                UserId = userId,
                            });
                        }
                    }
                    else
                    {
                        DateTime date = DateTime.UtcNow;
                        foreach (IProperty property in entry.CurrentValues.Properties)
                        {
                            allAuditLogs.Add(new AuditLog
                            {
                                ChangeDate = date,
                                ChangeType = AuditAction.Update,
                                EntityId = entity.Id,
                                EntityName = entity.GetType().Name,
                                UserId = userId,
                                NewValue = entry.CurrentValues[property]?.ToString(),
                                OldValue = entry.OriginalValues[property]?.ToString(),
                                PropertyName = property.Name
                            });
                        }
                    }
                }
            }

            // After all entries have been processed, save the audit logs
            if (allAuditLogs.Any())
            {
                await AuditLogs.AddRangeAsync(allAuditLogs);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        // Helper method to process AppUser and AppRole changes
        private List<AuditLog> ProcessAppUserChanges(IEnumerable<EntityEntry<AppUser>> entries, AuditAction action)
        {
            List<AuditLog> auditLogs = new List<AuditLog>();

            foreach (var entry in entries)
            {
                DateTime date = DateTime.UtcNow;
                string userId = _currentUserService.UserId; // Get userId from context or service provider if needed

                foreach (IProperty property in entry.CurrentValues.Properties)
                {
                    auditLogs.Add(new AuditLog
                    {
                        ChangeDate = date,
                        ChangeType = action,
                        EntityId = entry.Entity.Id,
                        EntityName = entry.Entity.GetType().Name,
                        PropertyName = property.Name,
                        UserId = userId,
                        NewValue = entry.CurrentValues[property]?.ToString(),
                        OldValue = entry.OriginalValues[property]?.ToString()
                    });
                }
            }

            return auditLogs;
        }

        private List<AuditLog> ProcessAppRoleChanges(IEnumerable<EntityEntry<AppRole>> entries, AuditAction action)
        {
            List<AuditLog> auditLogs = new List<AuditLog>();

            foreach (var entry in entries)
            {
                DateTime date = DateTime.UtcNow;
                string userId = _currentUserService.UserId; // Get userId from context or service provider if needed

                foreach (IProperty property in entry.CurrentValues.Properties)
                {
                    auditLogs.Add(new AuditLog
                    {
                        ChangeDate = date,
                        ChangeType = action,
                        EntityId = entry.Entity.Id,
                        EntityName = entry.Entity.GetType().Name,
                        PropertyName = property.Name,
                        UserId = userId,
                        NewValue = entry.CurrentValues[property]?.ToString(),
                        OldValue = entry.OriginalValues[property]?.ToString()
                    });
                }
            }

            return auditLogs;
        }
    }
}
