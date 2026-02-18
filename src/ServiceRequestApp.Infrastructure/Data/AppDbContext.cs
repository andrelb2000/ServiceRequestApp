using Microsoft.EntityFrameworkCore;
using ServiceRequestApp.Domain.Entities;
namespace ServiceRequestApp.Infrastructure.Data;

public class AppDbContext : DbContext {
 public AppDbContext(DbContextOptions<AppDbContext> options)
 : base(options) { }

 public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
 protected override void OnModelCreating(ModelBuilder modelBuilder) {
              base.OnModelCreating(modelBuilder);
                    modelBuilder.Entity<ServiceRequest>(entity => {
                        entity.ToTable("ServiceRequests");
                        entity.HasKey(e => e.Id);
                        entity.Property(e => e.Title)
                              .IsRequired()
                              .HasMaxLength(100);
                       entity.Property(e => e.Description)
                             .IsRequired()
                             .HasMaxLength(500);
                       entity.Property(e => e.Status)
                             .IsRequired();
                       entity.Property(e => e.CreatedAt)
                             .HasDefaultValueSql("GETDATE()");
                       entity.Property(e => e.CompletedAt)
                             .IsRequired(false);
              });
    }
}
