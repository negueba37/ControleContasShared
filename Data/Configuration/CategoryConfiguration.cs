using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Domain;

namespace ControleContas.Data.Configuration
{
	internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn();
			builder.Property(p => p.Description).IsRequired(true);

		}
	}
}
