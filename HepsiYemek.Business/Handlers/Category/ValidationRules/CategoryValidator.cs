using FluentValidation;
using HepsiYemek.Business.Handlers.Category.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.Business.Handlers.Category.ValidationRules
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Category).NotNull();
            RuleFor(x => x.Category.name).NotEmpty();
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Category).NotNull();
            RuleFor(x => x.Category.name).NotEmpty();
        }
    }
}
