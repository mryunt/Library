using FluentValidation;
using Library.DAL.Dtos.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Validation.SubCategory
{
    public class SubCategoryAddValidator : AbstractValidator<AddSubCategoryDto>
    {
        public SubCategoryAddValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20).WithMessage("Alt Kategor ismi alanı boş bırakılamaz ve 20 karakterden fazla girilemez.");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Kategori ID' si alanı boş bırakılamaz.");
        }
    }
}
