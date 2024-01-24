using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace AspNetProjMVC.Controllers
{
    public class DatabaseController
    {
        private readonly string _connectionString;
        private readonly string _connectionString2;


        public DatabaseController() 
        {
            string server = "sql5.freesqldatabase.com";
            string database = "sql5673207";
            string username = "sql5673207";
            string password = "8PH51R8Euv";
            _connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";

            string server2 = "sql10.freesqldatabase.com";
            string database2 = "sql10675811";
            string username2 = "sql10675811";
            string password2 = "bBLs1iXr6v";
            _connectionString2 = $"Server={server2};Database={database2};Uid={username2};Pwd={password2};";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public MySqlConnection GetConnection2()
        {
            return new MySqlConnection(_connectionString2);
        }
    }
}
