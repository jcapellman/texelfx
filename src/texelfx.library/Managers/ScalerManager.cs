using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace texelfx.library.Managers
{
    public class ScalerManager
    {
        public static IEnumerable<BaseScaler> GetScalers() => Assembly.GetAssembly(typeof(ScalerManager)).GetTypes()
            .Where(a => !a.IsAbstract && a.BaseType == typeof(BaseScaler))
            .Select(b => (BaseScaler) Activator.CreateInstance(b));
    }
}