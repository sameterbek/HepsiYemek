using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Business.Constant;
using MongoDB.Bson;
using HepsiYemek.Core.Caching;

namespace HepsiYemek.Business.Handlers.Product.Command
{

    public class DeleteProductCommand : IRequest<IResult>
    {
        public string Id { get; }
        public DeleteProductCommand(string id)
        {
            Id = id;
        }
        public class DeleteCateogryCommandHandler : IRequestHandler<DeleteProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly ICacheManager _cacheManager;

            public DeleteCateogryCommandHandler(IProductRepository productRepository, ICacheManager cacheManager)
            {
                _productRepository = productRepository;
                _cacheManager = cacheManager;
            }

            public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(ObjectId.Parse(request.Id));
                if (product == null)
                {
                    throw new Exception("not found");
                }

                await _productRepository.DeleteAsync(product);

                _cacheManager.Remove($"{CacheKeys.Product}{request.Id}");

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
