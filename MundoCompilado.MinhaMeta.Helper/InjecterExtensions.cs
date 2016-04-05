using Omu.ValueInjecter;
using System;

namespace MundoCompilado.MinhaMeta.Helper
{
    public static class InjecterExtensions
    {
        public static T Inject<T>(this object source) where T : class
        {
            var from = Activator.CreateInstance<T>();
            from.InjectFrom(source);
            return from;
        }

        public static TTarget Inject<TSource, TTarget>(this object source, TTarget target = null)
            where TTarget : class
        { 
            if(target == null) target = Activator.CreateInstance<TTarget>();

            target.InjectFrom(source);
            return target;
        }

        public static TTarget ConvertTo<TSource, TTarget>(this object source)
        {
            var target = Activator.CreateInstance<TTarget>();
            target.InjectFrom(source);
            return target;
        }
    }
}
