using System.Globalization;

namespace Boilerplate.Infrastructure.Extentions
{
    public class AppException : Exception
    {
        public Dictionary<string, string> Errors { get; set; }

        public AppException() : base()
        {
            Errors = new Dictionary<string, string>();
        }

        public AppException(string message) : base(message)
        {
            Errors = new Dictionary<string, string>();
        }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            Errors = new Dictionary<string, string>();
        }
    }
}
