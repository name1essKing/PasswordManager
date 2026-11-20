using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Server.Models;

namespace PasswordManager.Server.Data.Configurations;
public class PasswordItemConfiguration : IEntityTypeConfiguration<PasswordItem>
{
    public void Configure(EntityTypeBuilder<PasswordItem> builder)
    {
        builder.ToTable("Passwords");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}