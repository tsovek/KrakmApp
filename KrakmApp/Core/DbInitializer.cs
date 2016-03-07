using System;
using System.Collections.Generic;
using System.Linq;

using KrakmApp.Entities;
using Microsoft.Framework.DependencyInjection;

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
            if (!context.Markers.Any())
            {
                // TODO: Markers with images
            }

            List<Partner> partners = null;
            if (!context.Partners.Any())
            {
                var testPartner1 = new Partner()
                {
                    Name = "First test partner",
                    Adress = "Krakowska 11/11",
                    Phone = "453139384",
                    Localization = new Localization { Latitude = 10.0001, Longitude = 10.0001 }
                };

                var testPartner2 = new Partner()
                {
                    Name = "Second test partner",
                    Adress = "Krakowska 20/11",
                    Phone = "983561094",
                    Localization = new Localization { Latitude = 10.0002, Longitude = 10.0002 }
                };

                partners = new List<Partner>() { testPartner1, testPartner2 };
            }

            if (!context.Hotels.Any())
            {
                var localizationMainHotel = new Localization()
                {
                    Latitude = 10.000000,
                    Longitude = 10.000000
                };

                var mainHotel = context.Hotels.Add(
                    new Hotel
                    {
                        Name = "Test Best Hotel",
                        Email = "test.best@hotel.com",
                        Adress = "Krakowska 10/10",
                        Phone = "517025880",
                        Localizations = new List<Localization>() { localizationMainHotel },
                        Partners = partners
                    }).Entity;

                context.SaveChanges();
            }
        }

        private static void InitializeUserRoles()
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role[] { new Role { Name = "Admin" } });

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User()
                {
                    Email = "tomasz.a.sowa@gmail.com",
                    Name = "tomasz",
                    HashedPassword = "9wsmLgYM5Gu4zA/BSpxK2GIBEWzqMPKs8wl2WDBzH/4=",
                    Salt = "GTtKxJA6xJuj3ifJtTXn9Q==",
                    IsLocked = false,
                    DateCreated = DateTime.Now
                });

                context.UserRoles.AddRange(
                    new UserRole[] { new UserRole() { RoleId = 1, UserId = 1 } });

                context.SaveChanges();
            }
        }
    }
}
