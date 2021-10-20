using FluentValidation;
using Library.DAL.Dtos.Category;

namespace Library.Business.Validation.Category
{
    public class CategoryAddValidator : AbstractValidator<AddCategoryDto>
    {
        public CategoryAddValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20).WithMessage("Kategori ismi alanı boş bırakılamaz.");
        }
    }
}
