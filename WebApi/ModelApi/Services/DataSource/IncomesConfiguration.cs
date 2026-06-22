using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;

namespace ModelApi.Services.DataSource
{
    public class IncomesConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.UseTptMappingStrategy();
            builder.ToTable("INCOMES");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("ID")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.OwnsOne(
                b => b.Amount,
                a =>
                {
                    a.Property(p => p.Value)
                        .HasColumnName("AMOUNT")
                        .HasColumnType("money")
                        .IsRequired();
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

            builder.Property(p => p.TypeId)
                .HasColumnName("TYPE_INCOME_ID")
                .HasColumnType("integer")
                .IsRequired();
            builder.HasOne(i => i.TypeIncome)
                .WithMany(ti => ti.Incomes)
                .HasForeignKey(i => i.TypeId);

            builder.OwnsOne(
                b => b.Comments,
                d =>
                {
                    d.Property(p => p.Value)
                        .HasColumnName("COMMENTS")
                        .HasColumnType("nvarchar(max)");
                });
        }
    }
}
