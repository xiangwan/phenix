using System.Configuration;
using System.Data;
using System.Data.Common; 

namespace Phenix.Data{
    public static class ConnectionFactory
    {
        public static IDbConnection CreateConnection()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var cnn = factory.CreateConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["phenix"].ConnectionString;
            cnn.ConnectionString = connectionString;
            cnn.Open(); 
            return cnn;
        }
    }
}