using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CSharp8Console.Interfaces;
using System;
using System.IO;
using CSharp8Console.Chapter5;
using System.Collections.Generic;

namespace CSharp8Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //Setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFooService, FooService>()
                .AddSingleton<IBarService, BarService>()
                .AddSingleton<IAdding, Adding>()
                .AddSingleton<IOperations, Operations>()
                .AddLogging(loggingBuilder => loggingBuilder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug))
                .BuildServiceProvider();


            /**********  legacy way of adding logging ****************/
            //configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);            


            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");


            //var add = serviceProvider.GetService<IAdding>();
            //int x = add.CallAdd();
            //Console.WriteLine($"Adding 2+7 results in {x}");

            var add = serviceProvider.GetService<IOperations>();
            add.Sum();

            //do the actual work here
            var bar = serviceProvider.GetService<IBarService>();
            bar.DoSomeRealWork();

            Console.WriteLine("Show the size of three number data types");

            Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range { int.MinValue:N0} to { int.MaxValue:N0}.");
            Console.WriteLine($"double uses {sizeof(double)} bytes and can store numbers in the range { double.MinValue:N0} to { double.MaxValue:N0}.");
            Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can store numbers in the range { decimal.MinValue:N0} to { decimal.MaxValue:N0}.");

            //Console.WriteLine("Using doubles:");
            //double a = 0.1;
            //double b = 0.2;
            //if (a + b == 0.3)
            //{
            //    Console.WriteLine($"{a} + {b} equals 0.3");
            //}
            //else
            //{
            //    Console.WriteLine($"{a} + {b} does NOT equal 0.3");
            //}


            object height = 1.88; // storing a double in an object
            object name = "Amir"; // storing a string in an object
            Console.WriteLine($"{name} is {height} metres tall.");
            var doble = height.GetType();
            Console.WriteLine(doble);
            Console.WriteLine(nameof(doble));
            //int length1 = name.Length; // gives compile error!
            int length2 = ((string)name).Length; // tell compiler it is a string
            Console.WriteLine($"{name} has {length2} characters.");
            Console.WriteLine(length2);

            // storing a string in a dynamic object
            dynamic anotherName = "Ahmed";
            // this compiles but would throw an exception at run-time
            // if you later store a data type that does not have a
            // property named Length
            int length = anotherName.Length;
            Console.WriteLine(length);

            int population = 66_000_000;
            Console.WriteLine(population);

            // add and remove the "" to change the behavior
            object o = 3;
            int j = 4;
            if (o is int i)
            {
                Console.WriteLine($"{i} x {j} = {i * j}");
            }
            else
            {
                Console.WriteLine("o is not an int so it cannot multiply!");
            }

            //Parse string to int and treat exception
            //string age = "Kermit";
            string age = "9876543210";
            try
            {
                int ageParsed = int.Parse(age);
                Console.WriteLine($"You are {ageParsed} years old.");
            }
            //catch(Exception ex) Now that we know which kind of exception can occur we can treat it better
            catch (System.FormatException)
            {
                //Console.WriteLine($"{ex.GetType()} says {ex.Message}"); //System.FormatException says Input string was not in a correct format.
                Console.WriteLine("The age you entered is not a valid number format.");
            }
            catch (System.OverflowException)
            {
                Console.WriteLine("Value was either too large or too small for an Int32 Yooooo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} says {ex.Message}");
            }

            //Throwing overflow exceptions with the checked statement
            checked //without checked keyword it does not overflow
            {
                try
                {
                    int x = int.MaxValue - 1;
                    Console.WriteLine($"Initial value: {x}");
                    x++;
                    Console.WriteLine($"After incrementing: {x}");
                    x++;
                    Console.WriteLine($"After incrementing: {x}");
                    x++;
                    Console.WriteLine($"After incrementing: {x}");
                }
                catch (System.OverflowException)
                {
                    Console.WriteLine("The number exceed its Max or Min Value");
                }
            }


            unchecked
            {
                int y = int.MaxValue + 1; //Without unchecked keyword this line would prevent application from compiling
                Console.WriteLine($"Initial value: {y}");
                y--;
                Console.WriteLine($"After decrementing: {y}");
                y--;
                Console.WriteLine($"After decrementing: {y}");
            }


            //RunTimesTable();


            // out and ref

            //The out is a keyword in C# which is used for the passing the arguments to methods as a reference type.
            //It is generally used when a method returns multiple values. The out parameter does not pass the property.
            // Declaring variable
            // without assigning value
            int G;

            // Pass variable G to the method
            // using out keyword
            Sum(out G);

            // Display the value G
            Console.WriteLine("The sum of" + " the value is: {0}", G);


            //The ref is a keyword in C# which is used for the passing the arguments by a reference.
            //Or we can say that if any changes made in this argument in the method will reflect in that variable when the control return to the calling method.
            //The ref parameter does not pass the property.
            // Assign string value
            string str = "Geek";

            // Pass as a reference parameter
            SetValue(ref str);

            // Display the given string
            Console.WriteLine(str);

            int zzz;
            Exp(out zzz);
            Console.WriteLine("Number return By Exp function with out parameter should be 10");
            Console.WriteLine(zzz);


            //Person Bob = new Person();
            Person Bob = new Person("Bob", "Mars");
            Person Joao = new Person("Joao", "Earth");
            Person Olga = new Person("Olga", "Earth");
            //List<Person> Filhos = new List<Person>{  };
            Bob.Children = new List<Person>() { Joao, Olga };
            
            (string, int) fruits = Bob.GetFruit();
            Console.WriteLine($"Bob got {fruits.Item2} {fruits.Item1}.");
            (string Name, int Number) fruit = Bob.GetNamedFruit();
            Console.WriteLine($"Bob also got {fruit.Number} {fruit.Name}");

            var thing1 = ("Neville", 4);
            Console.WriteLine($"{thing1.Item1} has {thing1.Item2} children.");
            var thing2 = (Bob.Name, Bob.Children.Count);
            Console.WriteLine($"{thing2.Name} has {thing2.Count} children.");

            logger.LogDebug("All done!");

        }

        static void TimesTable(byte number)
        {
            Console.WriteLine($"This is the {number} times table:");
            for (int row = 1; row <= 12; row++)
            {
                Console.WriteLine(
                $"{row} x {number} = {row * number}");
            }
            Console.WriteLine();
        }
        static void RunTimesTable()
        {
            bool isNumber;
            do
            {
                Console.Write("Enter a number between 0 and 255: ");
                isNumber = byte.TryParse(Console.ReadLine(), out byte number);
                if (isNumber)
                {
                    TimesTable(number);
                }
                else
                {
                    Console.WriteLine("You did not enter a valid number!");
                }
            }
            while (isNumber);
        }

        // Method in which out parameter is passed and this method returns the value of the passed parameter
        public static void Sum(out int G)
        {
            G = 80;
            G += G;
        }

        static void SetValue(ref string str1)
        {
            // Check parameter value
            if (str1 == "Geek")
            {
                Console.WriteLine("Hello!!Geek");
            }

            // Assign the new value of the parameter
            str1 = "GeeksforGeeks";
        }

        static void Exp(out int xxx)
        {
            xxx = 5;
            xxx += xxx;
        }

        
    }
}
   
    
    


#region Comparisions
//double z = a+b;
//if(z.Equals(0.3))
//{
//    Console.WriteLine($"z equals 0.3");
//}
//else
//{
//    Console.WriteLine($"z does NOT equal 0.3");
//}

//decimal c = 0.1m;
//decimal d = 0.2m;
//if (c + d == 0.3m)
//{
//    Console.WriteLine($"{c} + {d} equals 0.3m");
//}
//else
//{
//    Console.WriteLine($"{c} + {d} does NOT equal 0.3m");
//}

//float e = 0.1f;
//float f = 0.2f;
//if (e + f == 0.3f)
//{
//    Console.WriteLine($"{e} + {f} equals 0.3f");
//}
//else
//{
//    Console.WriteLine($"{e} + {f} does NOT equal 0.3f");
//}
#endregion

#region Logic
//Console.WriteLine("Logic");
//bool a = true;
//bool b = false;
//Console.WriteLine($"AND | a | b ");
//Console.WriteLine($"a | {a & a,-5} | {a & b,-5} ");
//Console.WriteLine($"b | {b & a,-5} | {b & b,-5} ");
//Console.WriteLine();
//Console.WriteLine($"OR | a | b ");
//Console.WriteLine($"a | {a | a,-5} | {a | b,-5} ");
//Console.WriteLine($"b | {b | a,-5} | {b | b,-5} ");
//Console.WriteLine();
//Console.WriteLine($"XOR | a | b ");
//Console.WriteLine($"a | {a ^ a,-5} | {a ^ b,-5} ");
//Console.WriteLine($"b | {b ^ a,-5} | {b ^ b,-5} ");

#endregion

#region Bitwise

//int a = 10; // 0000 1010
//int b = 6; // 0000 0110
//Console.WriteLine($"a = {a}");
//Console.WriteLine($"b = {b}");
//Console.WriteLine($"a & b = {a & b}"); // 2-bit column only
//Console.WriteLine($"a | b = {a | b}"); // 8, 4, and 2-bit columns
//Console.WriteLine($"a ^ b = {a ^ b}"); // 8 and 4-bit columns

//// 0101 0000 left-shift a by three bit columns
//Console.WriteLine($"a << 3 = {a << 3}");
//// multiply a by 8
//Console.WriteLine($"a * 8 = {a * 8}");
//// 0000 0011 right-shift b by one bit column
//Console.WriteLine($"b >> 1 = {b >> 1}");

#endregion

#region Bynary_Base64
////Convert from a binary object to a string

////Allocate array of 128 bytes
//byte[] binaryObject = new byte[128];
//// populate array with random bytes
//(new Random()).NextBytes(binaryObject);
//Console.WriteLine("Binary Object as bytes:");
//for (int index = 0; index < binaryObject.Length; index++)
//{
//    Console.Write($"{binaryObject[index]:X} ");
//}
//Console.WriteLine();
//// convert to Base64 string and output as text
//string encoded = Convert.ToBase64String(binaryObject);
//Console.WriteLine($"Binary Object as Base64: {encoded}");


////Read binary file into bytes
//string pathSource = @"D:\Temp\hermanos.jpg";
//string pathNew = @"D:\Temp\HermanosCopyByCSharp.jpg";

//try
//{

//    using (FileStream fsSource = new FileStream(pathSource,
//        FileMode.Open, FileAccess.Read))
//    {

//        // Read the source file into a byte array.
//        byte[] bytes = new byte[fsSource.Length];
//        int numBytesToRead = (int)fsSource.Length;
//        int numBytesRead = 0;
//        while (numBytesToRead > 0)
//        {
//            // Read may return anything from 0 to numBytesToRead.
//            int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

//            // Break when the end of the file is reached.
//            if (n == 0)
//                break;

//            numBytesRead += n;
//            numBytesToRead -= n;
//        }
//        numBytesToRead = bytes.Length;

//        // Write the byte array to the other FileStream.
//        using (FileStream fsNew = new FileStream(pathNew,
//            FileMode.Create, FileAccess.Write))
//        {
//            fsNew.Write(bytes, 0, numBytesToRead);
//        }
//    }
//}
//catch (FileNotFoundException ioEx)
//{
//    Console.WriteLine(ioEx.Message);
//}

#endregion