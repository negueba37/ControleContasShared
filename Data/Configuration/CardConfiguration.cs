using ControleContas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ControleContas.Data.Configuration
{
	internal class CardConfiguration : IEntityTypeConfiguration<Card>
	{
		public void Configure(EntityTypeBuilder<Card> builder)
		{
			builder.ToTable("Cards");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Description).HasColumnType("VARCHAR(80)").IsRequired();
			builder.Property(p => p.MaturityDay).IsRequired();
			builder.Property(p => p.BestPurchaseDay).IsRequired();
			builder.Property(p => p.Limit).IsRequired();

		}
	}
}
