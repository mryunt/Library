using FluentValidation;
using Library.DAL.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Validation.Category
{
    public class CategoryUpdateValidator : AbstractValidator<UpdateCategoryDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20).WithMessage("Kategori ismi alanı boş bırakılamaz ve 20 karakterden fazla girilemez.");
        }
    }
}
