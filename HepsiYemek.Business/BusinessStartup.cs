using FluentValidation;
using HepsiYemek.Core.DataAccess.MongoDb.Configurations;
using HepsiYemek.DataAccess.Abstract;
using HepsiYemek.DataAccess.Concrete;
using HepsiYemek.DataAccess.Concrete.Collections;
using HepsiYemek.DataAccess.Concrete.Context;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HepsiYemek.Business
{
    public partial class BusinessStartup
    {
        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        protected IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository>(x => new CategoryRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.Category));
            services.AddTransient<IProductRepository>(x => new ProductRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.Product));

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                    ?.GetName();
            };
        }
    }
}
