using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Business.Constant;
using HepsiYemek.Dto.Category;
using MongoDB.Bson;
using HepsiYemek.Core.Caching;
using HepsiYemek.Core.Aspect.Autofac.Validation;
using HepsiYemek.Business.Handlers.Category.ValidationRules;

namespace HepsiYemek.Business.Handlers.Category.Command
{

    public class UpdateCategoryCommand : IRequest<IResult>
    {
        public string Id { get; }
        public UpdateCategoryDto Category { get; }
        public UpdateCategoryCommand(string id, UpdateCategoryDto category)
        {
            Id = id;
            Category = category;
        }
        public class UpdateCateogryCommandHandler : IRequestHandler<UpdateCategoryCommand, IResult>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly ICacheManager _cacheManager;

            public UpdateCateogryCommandHandler(ICategoryRepository categoryRepository, ICacheManager cacheManager)
            {
                _categoryRepository = categoryRepository;
                _cacheManager = cacheManager;
            }

            [ValidationAspect(typeof(UpdateCategoryValidator), Priority = 1)]
            public async Task<IResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetByIdAsync(ObjectId.Parse(request.Id));
                if (category == null)
                {
                    throw new Exception("not found");
                }

                category.description = request.Category.description;
                category.name = request.Category.name;

                await _categoryRepository.UpdateAsync(category.Id, category);

                _cacheManager.Remove($"{CacheKeys.Category}{request.Id}");

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
