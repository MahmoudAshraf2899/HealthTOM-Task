
namespace Boilerplate.Shared.Helpers
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmMailBody { get; set; }
        public string ConfirmAccountUrl { get; set; }
    }
}
