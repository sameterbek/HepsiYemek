using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Business.Constant;
using MongoDB.Bson;
using HepsiYemek.Core.Caching;
using HepsiYemek.Dto.Product;

namespace HepsiYemek.Business.Handlers.Product.Command
{

    public class UpdateProductCommand : IRequest<IResult>
    {
        public string Id { get; }
        public UpdateProductDto Product { get; }
        public UpdateProductCommand(string id, UpdateProductDto Product)
        {
            this.Id = id;
            this.Product = Product;
        }
        public class UpdateCateogryCommandHandler : IRequestHandler<UpdateProductCommand, IResult>
        {
            private readonly IProductRepository _ProductRepository;
            private readonly ICacheManager _cacheManager;

            public UpdateCateogryCommandHandler(IProductRepository ProductRepository, ICacheManager cacheManager)
            {
                _ProductRepository = ProductRepository;
                _cacheManager = cacheManager;
            }

            public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _ProductRepository.GetByIdAsync(ObjectId.Parse(request.Id));
                if (product == null)
                {
                    throw new Exception("not found");
                }

                product.description = request.Product.description;
                product.name = request.Product.name;
                product.categoryId = ObjectId.Parse(request.Product.categoryId);
                product.currency = request.Product.currency;
                product.price = request.Product.price;

                await _ProductRepository.UpdateAsync(product.Id, product);

                _cacheManager.Remove($"{CacheKeys.Product}{request.Id}");

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
