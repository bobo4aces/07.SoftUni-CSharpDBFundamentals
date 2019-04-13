using BillsPaymentSystem.Core.IO.Contracts;
using System;

namespace BillsPaymentSystem.Core.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
