using System;
using System.Collections.Generic;
using DrapperLibrary;
using DrapperLibrary.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string actionToTake = "";
            string filter = "";

            do
            {
                Console.Write("What action do you want to take (Display, Add, or Quit): ");
                actionToTake = Console.ReadLine();

                switch(actionToTake.ToLower())
                {
                    case "display":
                        var userList = DataAccess.GetUsers(filter);

                        ListUsers(userList);

                        break;
                    case "add":

                        var newUser = GetUserData();

                        DataAccess.AddUser(newUser);

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


        public static void ListUsers(List<UserModel> userList)
        {
            userList.ForEach(x => Console.WriteLine($"{ x.FirstName } { x.LastName }"));

            Console.WriteLine();
        }

    }
}
