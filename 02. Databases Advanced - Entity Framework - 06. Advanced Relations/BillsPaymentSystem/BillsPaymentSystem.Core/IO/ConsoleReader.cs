using BillsPaymentSystem.Core.IO.Contracts;
using System;

namespace BillsPaymentSystem.Core.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
