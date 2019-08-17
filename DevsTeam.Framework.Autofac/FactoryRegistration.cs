using System;
using Autofac;

namespace DevsTeam.Framework.Autofac
{
    public static class FactoryRegistration
    {
        public static void RegisterFactory<TResult, TParameter>(this ContainerBuilder parentBuilder, Action<ContainerBuilder, TParameter> childRegistration)
            where TParameter : class
        {
            parentBuilder.Register<Factory<TParameter, TResult>>(c =>
            {
                var parentScope = c.Resolve<ILifetimeScope>();
                return parameter =>
                {
                    var childScope = parentScope.BeginLifetimeScope(childBuilder => childRegistration(childBuilder, parameter));
                    return new Disposable<TResult>(childScope.Resolve<TResult>(), childScope.Dispose);
                };
            });
        }
    }
}