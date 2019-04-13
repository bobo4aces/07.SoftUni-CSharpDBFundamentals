using Microsoft.Extensions.DependencyInjection;
using MyApp.Core.Commands.Contracts;
using MyApp.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyApp.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Command";
        private readonly IServiceProvider serviceProvider;
        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public string Read(string[] inputArgs)
        {
            //type
            //ctor
            //ctor params
            //invoke
            //execute
            //return result
            string commandName = inputArgs[0] + Suffix; //AddEmployee
            string[] commandParams = inputArgs.Skip(1).ToArray();
            Type type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandName);

            if (type == null)
            {
                throw new ArgumentNullException("Invalid Command!");
            }

            ConstructorInfo constructor = type.GetConstructors()
                .FirstOrDefault();
            Type[] constructorParams = constructor
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var services = constructorParams
                .Select(this.serviceProvider.GetService)
                .ToArray();

            ICommand command = (ICommand)constructor.Invoke(services);

            var result = command.Execute(commandParams);

            return result;
        }
    }
}
