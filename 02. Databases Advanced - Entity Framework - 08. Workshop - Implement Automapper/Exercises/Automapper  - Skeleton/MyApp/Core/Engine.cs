using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using MyApp.Core.Contracts;

namespace MyApp.Core
{
    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;
        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Run()
        {
            while (true)
            {
                
                try
                {
                    string[] inputArgs = Console.ReadLine()
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .ToArray();

                    ICommandInterpreter commandInterpreter = this.serviceProvider.GetService<ICommandInterpreter>();
                    string result = commandInterpreter.Read(inputArgs);
                    Console.WriteLine(result);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine($"Invalid command!");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Invalid command!");
                }
            }
            
        }
    }
}
