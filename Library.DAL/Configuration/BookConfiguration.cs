using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DAL.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.Id);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Desc).HasMaxLength(250).IsRequired();
            builder.Property(p => p.SubCategoryId).IsRequired();

            builder.HasOne(p => p.SubCategoryFK).WithMany(p => p.Books).HasForeignKey(p => p.SubCategoryId);
        }
    }
}
