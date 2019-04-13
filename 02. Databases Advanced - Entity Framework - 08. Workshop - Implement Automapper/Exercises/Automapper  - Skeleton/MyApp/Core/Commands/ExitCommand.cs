using MyApp.Core.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            Environment.Exit(Environment.ExitCode);
            return "Exit";
        }
    }
}
