using System;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Application.Runtime
{
    public static class ApplicationExtensions
    {
        public static T GetModule<T>(this IApplication application) where T : class, IApplicationModule
        {
            if (application == null) throw new ArgumentNullException(nameof(application));

            return application.Provider.Get<T>();
        }

        public static T GetModule<T>(this IApplication application, GlobalId id) where T : class, IApplicationModule
        {
            if (application == null) throw new ArgumentNullException(nameof(application));

            return application.Provider.Get<T>(id);
        }
    }
}
