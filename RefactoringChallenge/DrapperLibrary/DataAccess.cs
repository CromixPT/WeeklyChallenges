using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DrapperLibrary.Models;

namespace DrapperLibrary
{
    public class DataAccess
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DapperDemoDB"].ConnectionString;

        public static List<UserModel> GetUsers()
        {

            using(IDbConnection cnn = new SqlConnection(connectionString))
            {
                return cnn.Query<UserModel>("spSystemUser_Get", commandType: CommandType.StoredProcedure).ToList();

            }
        }

        public static void AddUser(UserModel user)
        {

            using(IDbConnection cnn = new SqlConnection(connectionString))
            {

                cnn.Execute("dbo.spSystemUser_Create", new { FirstName = user.FirstName, LastName = user.LastName }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
