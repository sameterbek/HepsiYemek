using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Business.Constant;
using HepsiYemek.Dto.Category;
using MongoDB.Bson;
using AutoMapper;
using HepsiYemek.Core.Caching;

namespace HepsiYemek.Business.Handlers.Category.Query
{
    public class GetCategoryQuery : IRequest<IDataResult<GetCategoryDto>>
    {
        public string Id { get; }
        public GetCategoryQuery(string id)
        {
            Id = id;
        }
        public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, IDataResult<GetCategoryDto>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            private readonly ICacheManager _cacheManager;

            public GetCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ICacheManager cacheManager)
            {
                _categoryRepository = categoryRepository; 
                _mapper = mapper;
                _cacheManager = cacheManager;
            }

            //[CacheAspect]
            public async Task<IDataResult<GetCategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                var cachedData = _cacheManager.Get<GetCategoryDto>($"{CacheKeys.Category}{request.Id}");

                if(cachedData != null)
                    return new SuccessDataResult<GetCategoryDto>(cachedData);

                var category = await _categoryRepository.GetByIdAsync(ObjectId.Parse(request.Id));
                if (category == null)
                {
                    throw new Exception("not found");
                }
                var userDto = _mapper.Map<GetCategoryDto>(category);
                _cacheManager.Add($"{CacheKeys.Category}{request.Id}", userDto, 5);

                return new SuccessDataResult<GetCategoryDto>(userDto);
            }
        }
    }
}
