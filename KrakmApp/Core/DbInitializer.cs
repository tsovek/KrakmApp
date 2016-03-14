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
            List<Partner> partners = null;
            if (!context.Partners.Any())
            {
                var loc1 = new Localization { Latitude = 10.0001, Longitude = 10.0001 };
                var loc2 = new Localization { Latitude = 10.0002, Longitude = 10.0002 };
                context.Localizations.AddRange(loc1, loc2);

                var mark = new Marker { Name = "standard" };
                context.Markers.Add(mark);

                var testPartner1 = new Partner()
                {
                    Name = "First test partner",
                    Adress = "Krakowska 11/11",
                    Phone = "453139384",
                    Localization = loc1,
                    Marker = mark
                };

                var testPartner2 = new Partner()
                {
                    Name = "Second test partner",
                    Adress = "Krakowska 20/11",
                    Phone = "983561094",
                    Localization = loc2,
                    Marker = mark
                };

                partners = new List<Partner>() { testPartner1, testPartner2 };
                context.Partners.AddRange(testPartner1, testPartner2);
            }

            if (!context.Hotels.Any())
            {
                var localizationMainHotel = new Localization()
                {
                    Latitude = 10.000000,
                    Longitude = 10.000000
                };
                context.Localizations.Add(localizationMainHotel);

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
                context.Hotels.Add(mainHotel);

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
