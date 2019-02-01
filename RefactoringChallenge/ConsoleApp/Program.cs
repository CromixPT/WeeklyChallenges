using System;
using DrapperLibrary;
using DrapperLibrary.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string actionToTake = "";


            do
            {
                Console.Write("What action do you want to take (Display, Add, or Quit): ");
                actionToTake = Console.ReadLine();

                switch(actionToTake.ToLower())
                {
                    case "display":
                        var records = DataAccess.GetUsers("");
                        records.ForEach(x => Console.WriteLine($"{ x.FirstName } { x.LastName }"));
                        Console.WriteLine();
                        break;
                    case "add":

                        DataAccess.AddUser(GetUserData());

                        break;
                    default:
                        break;
                }
            } while(actionToTake.ToLower() != "quit");
        }

        public static UserModel GetUserData()
        {
            UserModel newUser = new UserModel();

            Console.Write("What is the first name: ");
            newUser.FirstName = Console.ReadLine();

            Console.Write("What is the last name: ");
            newUser.LastName = Console.ReadLine();

            Console.WriteLine();

            return newUser;


        }
    }
}
