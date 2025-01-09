using ControleContas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleContas.Data.Configuration
{
	internal class AccountConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.ToTable("Accounts");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn();
			builder.Property(p => p.CardId).IsRequired(false);
			builder.Property(p => p.Date).HasColumnType("Date").HasColumnType("Date");
			builder.Property(p => p.InstallmentQuantity);
			builder.Property(p => p.Description).HasColumnType("Text");
			builder.Ignore(i => i.User);


			builder.HasOne(x => x.Card)
				.WithMany(u => u.Accounts)
				.HasForeignKey(a => a.CardId);

			builder.HasOne(x => x.User)
				.WithMany(u => u.Accounts)
				.HasForeignKey(x => x.UserId);



		}
	}
}
