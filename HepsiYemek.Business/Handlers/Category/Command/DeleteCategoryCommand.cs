using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Business.Constant;
using MongoDB.Bson;
using HepsiYemek.Core.Caching;

namespace HepsiYemek.Business.Handlers.Category.Command
{

    public class DeleteCategoryCommand : IRequest<IResult>
    {
        public string Id { get; }
        public DeleteCategoryCommand(string id)
        {
            Id = id;
        }
        public class DeleteCateogryCommandHandler : IRequestHandler<DeleteCategoryCommand, IResult>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly ICacheManager _cacheManager;

            public DeleteCateogryCommandHandler(ICategoryRepository categoryRepository, ICacheManager cacheManager)
            {
                _categoryRepository = categoryRepository;
                _cacheManager = cacheManager;
            }

            public async Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetByIdAsync(ObjectId.Parse(request.Id));
                if (category == null)
                {
                    throw new Exception("not found");
                }

                await _categoryRepository.DeleteAsync(category);

                _cacheManager.Remove($"{CacheKeys.Category}{request.Id}");

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
