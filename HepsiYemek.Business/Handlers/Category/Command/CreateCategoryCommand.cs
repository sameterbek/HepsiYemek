using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Business.Constant;
using HepsiYemek.Dto.Category;
using HepsiYemek.Core.Aspect.Autofac.Validation;
using HepsiYemek.Business.Handlers.Category.ValidationRules;

namespace HepsiYemek.Business.Handlers.Category.Command
{
    public class CreateCategoryCommand : IRequest<IResult>
    {
        public CreateCategoryDto Category { get; }
        public CreateCategoryCommand(CreateCategoryDto category)
        {
            Category = category;
        }
        public class CreateCateogryCommandHandler : IRequestHandler<CreateCategoryCommand, IResult>
        {
            private readonly ICategoryRepository _categoryRepository;


            public CreateCateogryCommandHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            [ValidationAspect(typeof(CreateCategoryValidator), Priority = 1)]
            public async Task<IResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = new Entities.Entites.Category
                {
                    name = request.Category.name,
                    description = request.Category.description
                };
                await _categoryRepository.AddAsync(category);
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
