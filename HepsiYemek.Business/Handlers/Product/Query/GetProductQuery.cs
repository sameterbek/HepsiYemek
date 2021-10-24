using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Business.Constant;
using MongoDB.Bson;
using AutoMapper;
using HepsiYemek.Core.Caching;
using HepsiYemek.Dto.Product;
using HepsiYemek.Business.Handlers.Category.Query;
using Newtonsoft.Json;

namespace HepsiYemek.Business.Handlers.Product.Query
{
    public class GetProductQuery : IRequest<IDataResult<GetProductDto>>
    {
        public string Id { get; }
        public GetProductQuery(string id)
        {
            Id = id;
        }
        public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IDataResult<GetProductDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            private readonly ICacheManager _cacheManager;
            private readonly IMediator _mediator;
            public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper, ICacheManager cacheManager, IMediator mediator)
            {
                _productRepository = productRepository;
                _mapper = mapper;
                _cacheManager = cacheManager;
                _mediator = mediator;
            }

            //[CacheAspect]
            public async Task<IDataResult<GetProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var cachedData = _cacheManager.Get<GetProductDto>($"{CacheKeys.Product}{request.Id}");

                if (cachedData != null)
                    return new SuccessDataResult<GetProductDto>(cachedData);

                var product = await _productRepository.GetByIdAsync(ObjectId.Parse(request.Id));
                if (product == null)
                {
                    throw new Exception("not found");
                }

                var productDto = _mapper.Map<GetProductDto>(product);

                var category = await _mediator.Send(new GetCategoryQuery(product.categoryId.ToString()));
                productDto.Category = category.Data;

                _cacheManager.Add($"{CacheKeys.Product}{request.Id}", productDto, 5);

                return new SuccessDataResult<GetProductDto>(productDto);
            }
        }
    }
}
