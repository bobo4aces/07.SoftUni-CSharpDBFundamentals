namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);
                //string inputArgs = Console.ReadLine();
                //string result = GetBooksByAgeRestriction(db, inputArgs);
                //Console.WriteLine(GetGoldenBooks(db));
                //Console.WriteLine(GetBooksByPrice(db));
                //int inputArgs = int.Parse(Console.ReadLine());
                //Console.WriteLine(GetBooksNotReleasedIn(db, inputArgs));
                //string inputArgs = Console.ReadLine();
                //Console.WriteLine(GetBooksByCategory(db, inputArgs));
                //string inputArgs = Console.ReadLine();
                //Console.WriteLine(GetBooksReleasedBefore(db, inputArgs));
                //string inputArgs = Console.ReadLine();
                //Console.WriteLine(GetAuthorNamesEndingIn(db, inputArgs));
                //string inputArgs = Console.ReadLine();
                //Console.WriteLine(GetBookTitlesContaining(db, inputArgs));
                //string inputArgs = Console.ReadLine();
                //Console.WriteLine(GetBooksByAuthor(db, inputArgs));
                //int inputArgs = int.Parse(Console.ReadLine());
                //Console.WriteLine(CountBooks(db, inputArgs));
                //Console.WriteLine(CountCopiesByAuthor(db));
                //Console.WriteLine(GetTotalProfitByCategory(db));
                //Console.WriteLine(GetMostRecentBooks(db));
				//IncreasePrices(db);
                Console.WriteLine(RemoveBooks(db));
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            AgeRestriction ageRestriction = Enum.Parse<AgeRestriction>(command, true);
            StringBuilder sb = new StringBuilder();
            context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList()
                .ForEach(b => sb.AppendLine(b));
            return sb.ToString().Trim();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList()
                .ForEach(b => sb.AppendLine(b));
            return sb.ToString().Trim();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            context.Books
                .Where(b => b.Price > 40m)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .ToList()
                .ForEach(b => sb.AppendLine($"{b.Title} - ${b.Price:f2}"));
            return sb.ToString().Trim();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();
            context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList()
                .ForEach(b => sb.AppendLine(b));
            return sb.ToString().Trim();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            List<string> categories = input.ToLower().Split(" ",StringSplitOptions.RemoveEmptyEntries).ToList();

            StringBuilder sb = new StringBuilder();
            context.Categories
                .Where(c => categories.Contains(c.Name.ToLower()))
                .SelectMany(c => c.CategoryBooks
                    .Select(b => b.Book.Title))
                .OrderBy(b => b)
                .ToList()
                .ForEach(b => sb.AppendLine(b));

            return sb.ToString().Trim();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateAsDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            StringBuilder sb = new StringBuilder();
            context.Books
                .Where(b => b.ReleaseDate.Value < dateAsDate)
                .OrderByDescending(b => b.ReleaseDate.Value)
                .Select(b => new
                {
                    b.Title,
                    EditionType = Enum.GetName(typeof(EditionType),b.EditionType),
                    b.Price
                })
                .ToList()
                .ForEach(b => sb.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:f2}"));
            return sb.ToString().Trim();
        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();
            context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                })
                .OrderBy(a=>a.FirstName)
                .ThenBy(a=>a.LastName)
                .ToList()
                .ForEach(a => sb.AppendLine($"{a.FirstName} {a.LastName}"));
            return sb.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();
            context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList()
                .ForEach(b => sb.AppendLine(b));
            return sb.ToString().Trim();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();
            context.Books
                .Where(b => b.Author.LastName.StartsWith(input,StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    b.Author.FirstName,
                    b.Author.LastName
                })
                .ToList()
                .ForEach(b => sb.AppendLine($"{b.Title} ({b.FirstName} {b.LastName})"));
            return sb.ToString().Trim();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    Copies = a.Books.Sum(b=>b.Copies)
                })
                .OrderByDescending(a=>a.Copies)
                .ToList()
                .ForEach(a => sb.AppendLine($"{a.FirstName} {a.LastName} - {a.Copies}"));
            return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks
                                    .Select(b=>new
                                    {
                                        Sum = b.Book.Copies * b.Book.Price
                                    })
                                    .Sum(b=>b.Sum)
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c=>c.Name)
                .ToList()
                .ForEach(c => sb.AppendLine($"{c.Name} ${c.Profit}"));
            return sb.ToString().Trim();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            var categoriesAndBooks = context.Categories
                .Select(c => new
                {
                    c.Name,
                    TopThreeBooks = c.CategoryBooks
                                    .OrderByDescending(r => r.Book.ReleaseDate)
                                    .Select(b => new
                                    {
                                        b.Book.Title,
                                        b.Book.ReleaseDate.Value.Year
                                    })
                                    .Take(3)
                                    .ToList()
                })
                .OrderBy(c=>c.Name)
                .ToList();
            foreach (var categoryAndBook in categoriesAndBooks)
            {
                sb.AppendLine($"--{categoryAndBook.Name}");
                foreach (var book in categoryAndBook.TopThreeBooks)
                {
                    sb.AppendLine($"{book.Title} ({book.Year})");
                }
            }
            return sb.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList()
                .ForEach(b=>b.Price+=5m);
            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            List<Book> books = context.Books
                                    .Where(b => b.Copies < 4200)
                                    .ToList();
            context.RemoveRange(books);

            context.SaveChanges();

            return books.Count;
        }
    }
}
