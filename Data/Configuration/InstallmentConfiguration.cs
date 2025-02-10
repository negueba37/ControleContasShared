
using ControleContas.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ControleContasData.Domain;

namespace ControleContasData.Data.Configuration
{
	internal class InstallmentConfiguration
	{
		public class CardConfiguration : IEntityTypeConfiguration<Installment>
		{
			public void Configure(EntityTypeBuilder<Installment> builder)
			{
				builder.ToTable("Installments");
				builder.HasKey(p => p.Id);
				builder.Property(p => p.Due).HasColumnType("Date").IsRequired();
				builder.Property(p => p.Price).IsRequired();
				builder.Property(p => p.BankSlipId).IsRequired(false);
				builder.Property(p => p.AccountId).IsRequired(false);

			}
		}
	}
}
