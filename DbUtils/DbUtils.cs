using MySql.Data.MySqlClient;

namespace WebApp.DbUtils
{
    public class DbUtils
    {
        public static MySqlConnection GetDbConnection()
        {
            Console.WriteLine("Getting connection to MySql");
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            if (connectionString == null)
            {
                throw new Exception("There is no data in connection string: " + connectionString);
            }
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                Console.WriteLine("Successfully connected to MySql!");
                return conn;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Couldn\'t connect to MySql");
                throw ex;
            }
        }
    }
}
