using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DrapperLibrary.Models;

namespace DrapperLibrary
{
    public class DataAccess
    {
        public static List<UserModel> GetUsers(string connectionString)
        {
            using(IDbConnection cnn = new SqlConnection(connectionString))
            {
                return cnn.Query<UserModel>("spSystemUser_Get", commandType: CommandType.StoredProcedure).ToList();

            }
        }

    }
}
