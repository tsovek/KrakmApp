using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrakmApp.Core.Repositories.Base;

namespace KrakmApp.Core.Services
{
    public interface IRandomGeneratorService
    {
        string GenerateRandomUniqueKey();
    }

    public class RandomGeneratorService : IRandomGeneratorService
    {
        private IClientRepository _clients;

        public RandomGeneratorService(
            IClientRepository clients)
        {
            _clients = clients;
        }

        public string GenerateRandomUniqueKey()
        {
            const int lenghtOfKey = 4;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            string key = new string(Enumerable
                .Repeat(chars, lenghtOfKey)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            if (_clients.GetAll().Any(c => c.UniqueKey == key))
            {
                return GenerateRandomUniqueKey();
            }
            return key;
        }
    }
}
