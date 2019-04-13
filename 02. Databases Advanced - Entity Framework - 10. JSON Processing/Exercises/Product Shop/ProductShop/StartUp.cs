using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using(ProductShopContext context = new ProductShopContext())
            {
                context.Database.EnsureCreated();
                //01. Import Users 
                //string output = ImportUsers(context, File.ReadAllText("../../../Datasets/users.json"));
                //Console.WriteLine(output);
                //02. Import Products 
                //string output = ImportProducts(context, File.ReadAllText("../../../Datasets/products.json"));
                //Console.WriteLine(output);
                //03. Import Categories 
                //string output = ImportCategories(context, File.ReadAllText("../../../Datasets/categories.json"));
                //Console.WriteLine(output);
                //04. Import Categories and Products 
                //string output = ImportCategoryProducts(context, File.ReadAllText("../../../Datasets/categories-products.json"));
                //Console.WriteLine(output);
                //05. Export Products In Range 
                //string products = GetProductsInRange(context);
                //string filePath = "../../../Datasets/products-in-range.json";
                //File.AppendAllText(filePath, products);
                //06. Export Sold Products 
                //string jsonString = GetSoldProducts(context);
                //string filePath = "../../../Datasets/users-sold-products.json";
                //File.AppendAllText(filePath, jsonString);
                //07. Export Categories By Products Count 
                //string jsonString = GetCategoriesByProductsCount(context);
                //string filePath = "../../../Datasets/categories-by-products.json";
                //File.AppendAllText(filePath, jsonString);
                //08. Export Users and Products 
                Mapper.Initialize(cnf => cnf.AddProfile(new ProductShopProfile()));
                string jsonString = GetUsersWithProducts(context);
                string filePath = "../../../Datasets/users-and-products.json";
                File.AppendAllText(filePath, jsonString);
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(inputJson)
                .Where(u=>u.LastName != null && u.LastName.Length >= 3)
                .ToList();
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(inputJson)
                .Where(p=>p.Name != null && p.Name.Trim().Length >= 3)
                .ToList();
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                .Where(c=>c.Name != null)
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            List<CategoryProduct> categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500m && p.Price <= 1000m)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = string.Concat(p.Seller.FirstName, " ", p.Seller.LastName)
                })
                .OrderBy(p=>p.price)
                .ToList();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count >= 1 && u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold.Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName
                    }).ToArray()
                })
                .ToArray();

            return JsonConvert.SerializeObject(users,Formatting.Indented);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = c.CategoryProducts
                                    .Average(p => p.Product.Price)
                                    .ToString("0.00"),
                    totalRevenue = c.CategoryProducts
                                    .Sum(p => p.Product.Price)
                                    .ToString("0.00")
                })
                .ToArray();

            return JsonConvert.SerializeObject(categories,Formatting.Indented);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                 .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                 .OrderByDescending(u => u.ProductsSold.Count(p=>p.Buyer != null))
                 .ProjectTo<UserDto>()
                 .ToList();
            var obj = new
            {
                UsersCount = users.Count,
                Users = users
            };
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
        }


    }
}