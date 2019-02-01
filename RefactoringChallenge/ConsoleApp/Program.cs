using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DrapperLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string actionToTake = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DapperDemoDB"].ConnectionString;

            do
            {
                Console.Write("What action do you want to take (Display, Add, or Quit): ");
                actionToTake = Console.ReadLine();

                switch(actionToTake.ToLower())
                {
                    case "display":
                        var records = DataAccess.GetUsers(connectionString);
                        records.ForEach(x => Console.WriteLine($"{ x.FirstName } { x.LastName }"));

                        Console.WriteLine();
                        break;
                    case "add":
                        Console.Write("What is the first name: ");
                        string firstName = Console.ReadLine();

                        Console.Write("What is the last name: ");
                        string lastName = Console.ReadLine();

                        using(IDbConnection cnn = new SqlConnection(connectionString))
                        {
                            var p = new
                            {
                                FirstName = firstName,
                                LastName = lastName
                            };

                            cnn.Execute("dbo.spSystemUser_Create", p, commandType: CommandType.StoredProcedure);
                        }
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
            } while(actionToTake.ToLower() != "quit");
        }
    }
}
