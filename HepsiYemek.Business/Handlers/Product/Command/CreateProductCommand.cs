using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Business.Constant;
using HepsiYemek.Dto.Product;
using MongoDB.Bson;

namespace HepsiYemek.Business.Handlers.Product.Command
{
    public class CreateProductCommand : IRequest<IResult>
    {
        public CreateProductDto Product { get; }
        public CreateProductCommand(CreateProductDto product)
        {
            this.Product = product;
        }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;


            public CreateProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = new Entities.Entites.Product
                {
                    name = request.Product.name,
                    description = request.Product.description,
                    categoryId = ObjectId.Parse(request.Product.categoryId), 
                    currency = request.Product.currency,
                    price = request.Product.price
                };
                await _productRepository.AddAsync(product);
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
