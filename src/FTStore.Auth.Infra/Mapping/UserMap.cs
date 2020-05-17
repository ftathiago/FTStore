using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FTStore.Auth.Domain.Entities;

using FTStore.Auth.Domain.ValueObjects;

using FTStore.Auth.Infra.Tables;


namespace FTStore.Auth.Infra.Mappings
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

            var userTable = GetUserTable();
            builder.HasData(userTable);
        }

        public UserTable GetUserTable()
        {
            var pass = new Password("admin");
            var user = new User("admin", "admin", "admin@admin.com", pass);
            var userTable = new UserTable
            {
                Id = 1,
                Email = user.Email,
                Hash = user.Password.Hash,
                Salt = user.Password.Salt,
                IsAdmin = true
            };
            return userTable;
        }
    }
}
