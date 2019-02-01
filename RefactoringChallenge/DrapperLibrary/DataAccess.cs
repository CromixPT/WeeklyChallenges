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
        public static List<UserModel> GetUsers(string filter)
        {
            using(IDbConnection connection = GetConnection())
            {
                if(filter != "")
                {
                    return connection.Query<UserModel>("spSystemUser_GetFiltered", new { Filter = filter }, commandType: CommandType.StoredProcedure).ToList();
                }
                return connection.Query<UserModel>("spSystemUser_Get", commandType: CommandType.StoredProcedure).ToList();
            }

        }

        public static void AddUser(UserModel user)
        {
            using(IDbConnection connection = GetConnection())
            {
                connection.Execute("dbo.spSystemUser_Create", new { FirstName = user.FirstName, LastName = user.LastName }, commandType: CommandType.StoredProcedure);
            }
        }

        private static IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DapperDemoDB"].ConnectionString;)
        }
    }
}
