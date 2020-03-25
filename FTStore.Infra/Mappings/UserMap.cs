using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Domain.Entities;

namespace FTStore.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder
                .HasKey(u => u.Id);
            builder
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder
               .HasIndex(u => u.Email)
               .IsUnique();
            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(400);

            builder
                .HasData(
                    new UserEntity
                    {
                        Name = "Administrator",
                        Surname = "Admin",
                        Email = "admin@admin.com",
                        Password = "admin",
                        IsAdmin = true
                    }
                );
        }
    }
}
