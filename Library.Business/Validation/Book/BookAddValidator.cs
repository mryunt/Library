using FluentValidation;
using Library.DAL.Dtos;

namespace Library.Business.Validation.Book
{
    public class BookAddValidator : AbstractValidator<AddBookDto>
    {
        public BookAddValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20).WithMessage("Kitap ismi alanı boş bırakılamaz ve 20 karakterden fazla girilemez.");
            RuleFor(p => p.Desc).NotEmpty().MaximumLength(255).WithMessage("Kitap açıklaması alanı boş bırakılamaz ve 255 karakterden fazla girilemez.");
            RuleFor(p => p.SubCategoryId).NotEmpty().WithMessage("Alt Kategori ID' si boş bırakılamaz.");
        }
    }
}
