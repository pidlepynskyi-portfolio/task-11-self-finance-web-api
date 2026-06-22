using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelApi.Entities;

namespace ModelApi.Services.DataSource
{
    public class TypesIncomesConfiguration : IEntityTypeConfiguration<TypeIncome>
    {
        public void Configure(EntityTypeBuilder<TypeIncome> builder)
        {
            builder.UseTptMappingStrategy();
            builder.ToTable("TYPES_INCOMES");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("ID")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.OwnsOne(
                b => b.Name,
                n =>
                {
                    n.Property(p => p.Value)
                        .HasColumnName("NAME")
                        .HasColumnType("nvarchar(100)")
                        .IsRequired();
                });

            builder.OwnsOne(
                b => b.Description,
                d =>
                {
                    d.Property(p => p.Value)
                        .HasColumnName("DESCRIPTION")
                        .HasColumnType("nvarchar(max)");
                });

            builder.OwnsOne(
                b => b.CreateDate,
                cd =>
                {
                    cd.Property(p => p.Value)
                        .HasColumnName("CREATE_DATE")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");
                });

            builder.OwnsOne(
                b => b.UpdateDate,
                ud =>
                {
                    ud.Property(p => p.Value)
                        .HasColumnName("UPDATE_DATE")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");
                });
        }
    }
}
