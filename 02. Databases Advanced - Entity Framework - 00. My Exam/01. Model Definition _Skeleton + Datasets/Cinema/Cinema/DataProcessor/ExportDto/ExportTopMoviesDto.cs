using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.DataProcessor.ExportDto
{
    public class ExportTopMoviesDto
    {
        //    "MovieName": "SIS",
        //"Rating": "10.00",
        //"TotalIncomes": "184.04",
        //"Customers": [
        //  {
        //    "FirstName": "Davita",
        //    "LastName": "Lister",
        //    "Balance": "279.76"
        public string MovieName { get; set; }
        public string Rating { get; set; }
        public string TotalIncomes { get; set; }
        public ExportCustomerDto[] Customers { get; set; }
    }
}
