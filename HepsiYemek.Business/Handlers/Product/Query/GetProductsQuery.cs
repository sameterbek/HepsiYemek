using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq.Expressions;
using HepsiYemek.Core.Utilities.LinqHelper;
using HepsiYemek.Dto.Product;
using HepsiYemek.Business.Handlers.Category.Query;

namespace HepsiYemek.Business.Handlers.Product.Query
{
    public class GetProductsQuery : IRequest<IDataResult<IEnumerable<GetProductDto>>>
    {
        public GetProductFilter Product { get; set; }
        public GetProductsQuery(GetProductFilter product)
        {
            this.Product = product;
        }

        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IDataResult<IEnumerable<GetProductDto>>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper, IMediator mediator)
            {
                _productRepository = productRepository;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<IDataResult<IEnumerable<GetProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                var filters = new List<Expression<Func<Entities.Entites.Product, bool>>>();
                if (!String.IsNullOrWhiteSpace(request.Product.name))
                {
                    Expression<Func<Entities.Entites.Product, bool>> nameExp = s => s.name == request.Product.name;
                    filters.Add(nameExp);
                }
                if (!String.IsNullOrWhiteSpace(request.Product.description))
                {
                    Expression<Func<Entities.Entites.Product, bool>> descExp = s => s.description == request.Product.description;
                    filters.Add(descExp);
                }

                Expression<Func<Entities.Entites.Product, bool>> filter = null;
                filters.ForEach(p =>
                {
                    filter = LinqHelper.And(filter, p);
                });

                var products = await _productRepository.GetListAsync(filter);

                var productsDto = _mapper.Map<List<GetProductDto>>(products);

                foreach (var productDto in productsDto)
                {
                    var category = await _mediator.Send(new GetCategoryQuery(productDto.categoryId.ToString()));
                    productDto.Category = category.Data;
                }
                return new SuccessDataResult<IEnumerable<GetProductDto>>(productsDto);
            }
        }
    }
}
