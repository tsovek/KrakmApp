
namespace KrakmApp.Core.Common
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }

    public class ClientResult : Result
    {
        public string UniqueId { get; set; }
    }
}
