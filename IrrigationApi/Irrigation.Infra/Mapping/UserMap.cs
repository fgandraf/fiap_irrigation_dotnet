using Irrigation.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irrigation.Infra.Mapping;

public class UserMap: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //// Table
        builder.ToTable("tbl_user");
        
        //// Primary Key - Identity
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("user_id")
            .ValueGeneratedOnAdd()
            .ValueGeneratedOnAdd();
        
        //// Properties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .HasAnnotation("EmailAddress", true);
        
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("password_hash")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        
        builder.Property(x => x.Active)
            .IsRequired()
            .HasColumnName("active")
            .HasColumnType("BIT");
        
        //// Index
        builder.HasIndex(x => x.Email, "IX_User_Email")
            .IsUnique();
        
        //// Relation
        // User <-> Role
        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>
            (
                "tbl_user_role",
                user => user.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("role_id")
                    .HasConstraintName("FK_UserRole_RoleId"),
                role => role.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("user_id")
                    .HasConstraintName("FK_UserRole_UserId")
            );
    }
}