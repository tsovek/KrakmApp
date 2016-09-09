using System;
using System.Collections.Generic;
using System.Linq;

using KrakmApp.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace KrakmApp.Core
{
    public static class DbInitializer
    {
        private static KrakmAppContext context;
        public static void Initialize(IServiceProvider serviceProvider, string imagesPath)
        {
            context = serviceProvider.GetService<KrakmAppContext>();

            InitializeHotelsWithPartners(imagesPath);
            InitializeUserRoles();
        }

        private static void InitializeHotelsWithPartners(string imagesPath)
        {
            return;
        }

        private static void InitializeUserRoles()
        {

        }
    }
}
