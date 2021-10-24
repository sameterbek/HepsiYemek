using FluentValidation;
using HepsiYemek.Business.Handlers.Category.Command;
using HepsiYemek.Business.Handlers.Product.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.Business.Handlers.Category.ValidationRules
{
    public class CreateproductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateproductValidator()
        {
            RuleFor(x => x.Product).NotNull();
            RuleFor(x => x.Product.name).NotEmpty();
            RuleFor(x => x.Product.price).NotNull();
            RuleFor(x => x.Product.currency).NotEmpty();
        }
    }

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Product).NotNull();
            RuleFor(x => x.Product.name).NotEmpty();
            RuleFor(x => x.Product.price).NotNull();
            RuleFor(x => x.Product.currency).NotEmpty();
        }
    }
}
