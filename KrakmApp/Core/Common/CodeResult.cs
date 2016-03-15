
namespace KrakmApp.Core.Common
{
    public class CodeResult
    {
        private int _status;
        private string _message;

        public CodeResult(int status)
        {
            if (status == 401)
            {
                _message = "Unauthorized access. Login required";
            }

            _status = status;
        }

        public CodeResult(int code, string message)
        {
            _status = code;
            _message = message;
        }

        public int Status
        {
            get { return _status; }
        }
        public string Message
        {
            get { return _message; }
        }
    }
}
