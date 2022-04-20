using System;
using System.Collections.Generic;
using static System.Console;

namespace CSharp8Console.Chapter5
{
    public partial class Person : object
    {
        #region fields
       
        public string Name;
        public DateTime DateOfBirth;
        public WondersOfTheAncientWorld FavoriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;
        public List<Person> Children = new List<Person>();
        #endregion

        // constants
        public const string Species = "Homo Sapien";

        // read-only fields
        public readonly string HomePlanet = "Earth";
        public readonly DateTime Instantiated;

        #region Constructors
        public Person()
        {
            // set default values for fields
            // including read-only fields 
            Name = "Unknown";
            Instantiated = DateTime.Now;
        }

        public Person(string initialName, string homePlanet)
        {
            Name = initialName;
            homePlanet = HomePlanet;
            Instantiated = DateTime.Now;
        }
        #endregion

        #region Methods
        public void WriteToConsole()
        {
            WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
        }

        public string GetOrigin()
        {
            return $"{Name} was born on {HomePlanet}.";
        }

        public (string, int) GetFruit()
        {
            return ("Apples", 5);
        }

        public (string Name, int Number) GetNamedFruit()
        {
            return (Name:"Oranges", Number:7);
        }

        //public (string Name, int Number) GetNamedFruit()
        //{
        //    return (Name: "Apples", Number: 5);
        //}

        public string SayHello()
        {
            return $"{Name} says 'Hello!'";
        }

        public string SayHello(string name)
        {
            return $"{Name} says 'Hello {name}!'";
        }

        public string OptionalParameters(string command = "Run!", double number = 0.0, bool active = true)
        {
            return string.Format(
              format: "command is {0}, number is {1}, active is {2}",
              arg0: command, arg1: number, arg2: active);
        }

        public void PassingParameters(int x, ref int y, out int z)
        {
            // out parameters cannot have a default
            // AND must be initialized inside the method 
            z = 99;

            // increment each parameter 
            x++;
            y++;
            z++;
        }
        #endregion

    }
}
