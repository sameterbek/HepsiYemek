using Castle.DynamicProxy;
using HepsiYemek.Core.Caching;
using HepsiYemek.Core.Utilities.Interceptors;
using HepsiYemek.Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace HepsiYemek.Core.Aspect.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private readonly ICacheManager _cacheManager;
        const string commandHandler = "CommandHandler";
        const string create = "Create";
        const string update = "Update";
        const string delete = "Delete";
        const string get = "Get";
        public CacheRemoveAspect(string pattern = "")
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            if (string.IsNullOrEmpty(_pattern))
            {
                string targetTypeName = invocation.TargetType.Name;
                targetTypeName = targetTypeName.Replace(commandHandler, string.Empty);
                targetTypeName = targetTypeName.Replace(create, string.Empty);
                targetTypeName = targetTypeName.Replace(update, string.Empty);
                targetTypeName = targetTypeName.Replace(delete, string.Empty);
                _pattern = get + targetTypeName;
            }
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
