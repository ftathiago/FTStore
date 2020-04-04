using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Infra.Table;
using System.Text;

namespace FTStore.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<UserTable>
    {
        public void Configure(EntityTypeBuilder<UserTable> builder)
        {
            builder.ToTable("user");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
               .HasIndex(u => u.Email)
               .IsUnique();

            builder
                .Property(u => u.Hash)
                .IsRequired();

            builder
                .Property(u => u.Salt)
                .IsRequired();

            builder
                .HasData(
                    new UserTable
                    {
                        Id = 1,
                        Email = "admin@admin.com",
                        Hash = Encoding.UTF8.GetBytes("asdf"),
                        Salt = Encoding.UTF8.GetBytes("asdf"),
                        IsAdmin = true
                    }
                );
        }
    }
}
