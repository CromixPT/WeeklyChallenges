using System;
using System.Collections.Generic;

namespace foreach_Challenge
{
    class Program
    {
        static void Main(string[] args)

        {
            var people = new List<person>() {
                new person() { FirstName = "Ana", LastName = "luis" },
                new person() { FirstName = "Rui", LastName = "Pedro" },
                new person() { FirstName = "Alexandre", LastName = "Pias" },
                new person() { FirstName = "Tim", LastName = "Corey" },

            };

            foreach(var person in people)
            {
                Console.WriteLine("Hello " + person.FirstName + " " + person.LastName);
            }

        }
    }
}
