using HepsiYemek.Core.Utilities.Results;
using HepsiYemek.DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HepsiYemek.Entities.Entites;
using HepsiYemek.Business.Constant;
using HepsiYemek.Dto.Category;

namespace HepsiYemek.Business.Handlers.Category.Command
{
    public class CreateCateogryCommand : IRequest<IResult>
    {
        public string Id { get; }
        public CreateCategoryDto Category { get; }
        public CreateCateogryCommand(string id, CreateCategoryDto category)
        {
            Id = id;
            Category = category;
        }
        public class CreateGroupCommandHandler : IRequestHandler<CreateCateogryCommand, IResult>
        {
            private readonly ICategoryRepository _categoryRepository;


            public CreateGroupCommandHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }


            public async Task<IResult> Handle(CreateCateogryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = new Entities.Entites.Category
                    {
                        name = request.Category.name,
                        description = request.Category.description
                    };
                    await _categoryRepository.AddAsync(category);
                    return new SuccessResult(Messages.Added);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
