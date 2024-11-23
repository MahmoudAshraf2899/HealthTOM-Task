
namespace Boilerplate.Shared.Consts
{
    public static class ConnectionStrings
    {
        public const string LocalConectionString = "Server=.;Database=Boilerplate-Test;Integrated Security=True;TrustServerCertificate=True;";

        public const string TestConectionString = "Server=SQL8005.site4now.net;Database=db_a8f062_Boilerplatedb;User Id=db_a8f062_Boilerplatedb_admin;Password=Admin_123-;TrustServerCertificate=True;";

        public const string StagingConectionString = "Server=SQL8006.site4now.net;Database=db_a8f062_Boilerplatestagedb;User Id=db_a8f062_Boilerplatestagedb_admin;Password=Admin_123-;TrustServerCertificate=True;";

        public const string ProductionConectionString = "Server=172.16.0.199;Database=Boilerplate;User Id=web;Password=web123;TrustServerCertificate=True;";
    }
}
