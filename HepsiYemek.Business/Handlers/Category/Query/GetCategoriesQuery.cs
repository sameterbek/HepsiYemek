using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Dto.Category;
using AutoMapper;
using System.Linq.Expressions;
using HepsiYemek.Core.Utilities.LinqHelper;

namespace HepsiYemek.Business.Handlers.Category.Query
{
    public class GetCategoriesQuery : IRequest<IDataResult<IEnumerable<GetCategoryDto>>>
    {
        public GetCategoryFilter Category { get; set; }
        public GetCategoriesQuery(GetCategoryFilter category)
        {
            this.Category = category;
        }
        public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IDataResult<IEnumerable<GetCategoryDto>>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IEnumerable<GetCategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
            {
                var filters = new List<Expression<Func<Entities.Entites.Category, bool>>>();
                if (!String.IsNullOrWhiteSpace(request.Category.name))
                {
                    Expression<Func<Entities.Entites.Category, bool>> nameExp = s => s.name == request.Category.name;
                    filters.Add(nameExp);
                }
                if (!String.IsNullOrWhiteSpace(request.Category.description))
                {
                    Expression<Func<Entities.Entites.Category, bool>> descExp = s => s.description == request.Category.description;
                    filters.Add(descExp);
                }

                Expression<Func<Entities.Entites.Category, bool>> filter = null;
                filters.ForEach(p =>
                {
                    filter = LinqHelper.And(filter, p);
                });

                var categories = await _categoryRepository.GetListAsync(filter);

                var categoriesDto = _mapper.Map<List<GetCategoryDto>>(categories);

                return new SuccessDataResult<IEnumerable<GetCategoryDto>>(categoriesDto);
            }
        }
    }
}
