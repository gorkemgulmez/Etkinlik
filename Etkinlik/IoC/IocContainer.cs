using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etkinlik
{
    public static class IoC
    {
        public static ApplicationDbContext ApplicationDbContext => IoCContainer.Provider.GetService<ApplicationDbContext>();
    }
    public class IoCContainer
    {
        public static ServiceProvider Provider { get; set; }
    }
}
