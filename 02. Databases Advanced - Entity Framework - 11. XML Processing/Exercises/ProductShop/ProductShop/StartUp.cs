using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cnf => cnf.AddProfile(new ProductShopProfile()));
            using (ProductShopContext context = new ProductShopContext())
            {
                context.Database.EnsureCreated();
                //01. Import Users 
                //string path = "../../../Datasets/users.xml";
                //string inputXml = File.ReadAllText(path);
                //string output = ImportUsers(context, inputXml);
                //Console.WriteLine(output);
                //02.Import Products
                //string path = "../../../Datasets/products.xml";
                //string inputXml = File.ReadAllText(path);
                //string output = ImportProducts(context, inputXml);
                //Console.WriteLine(output);
                //03.Import Categories
                //string path = "../../../Datasets/categories.xml";
                //string inputXml = File.ReadAllText(path);
                //string output = ImportCategories(context, inputXml);
                //Console.WriteLine(output);
                //04.Import Categories and Products
                //string path = "../../../Datasets/categories-products.xml";
                //string inputXml = File.ReadAllText(path);
                //string output = ImportCategoryProducts(context, inputXml);
                //Console.WriteLine(output);
                //05. Export Products In Range
                //string path = "../../../Exports/products-in-range.xml";
                //string xml = GetProductsInRange(context);
                //File.WriteAllText(path, xml);
                //06. Export Sold Products 
                //string path = "../../../Exports/users-sold-products.xml";
                //string xml = GetSoldProducts(context);
                //File.WriteAllText(path, xml);
                //07. Export Categories By Products Count 
                //string path = "../../../Exports/categories-by-products.xml";
                //string xml = GetCategoriesByProductsCount(context);
                //File.WriteAllText(path, xml);
                //08. Export Users and Products 
                string path = "../../../Exports/users-and-products.xml";
                string xml = GetUsersWithProducts(context);
                File.WriteAllText(path, xml);
                
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XDocument document = XDocument.Parse(inputXml);
            XElement[] elements = document.Root.Elements().ToArray();
            List<User> users = new List<User>();
            foreach (var element in elements)
            {
                User user = new User()
                {
                    FirstName = element.Element("firstName").Value,
                    LastName = element.Element("lastName").Value,
                    Age = int.Parse(element.Element("age").Value)
                };
                users.Add(user);
            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XDocument document = XDocument.Parse(inputXml);
            List<XElement> productsXml = document.Root
                .Elements()
                .ToList();
            List<Product> products = new List<Product>();
            foreach (var product in productsXml)
            {
                int number;
                Product currentProduct = new Product()
                {
                    Name = product.Element("name").Value,
                    Price = decimal.Parse(product.Element("price").Value),
                    SellerId = int.Parse(product.Element("sellerId").Value),
                    BuyerId = int.TryParse(product.Element("buyerId")?.Value, out number) ? number: (int?)null
                };
                products.Add(currentProduct);
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XDocument document = XDocument.Parse(inputXml);
            XElement[] elements = document.Root.Elements().ToArray();
            List<Category> categories = new List<Category>();
            foreach (var element in elements)
            {
                Category category = new Category()
                {
                    Name = element.Element("name").Value
                };
                categories.Add(category);
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XDocument document = XDocument.Parse(inputXml);
            XElement[] elements = document.Root.Elements().ToArray();
            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
            foreach (var element in elements)
            {
                int categoryId = Convert.ToInt32(element.Element("CategoryId").Value);
                int productId = Convert.ToInt32(element.Element("ProductId").Value);
                if (categoryId == 0 || productId == 0)
                {
                    continue;
                }
                CategoryProduct categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryId,
                    ProductId = productId
                };
                categoryProducts.Add(categoryProduct);
            }
            categoryProducts = categoryProducts.Distinct().ToList();
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            ProductsInRangeDto[] products = context.Products
                .Include(p => p.Buyer)
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .ProjectTo<ProductsInRangeDto>()
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ProductsInRangeDto[]), new XmlRootAttribute("Products"));
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = false,
            };
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            string xml = string.Empty;
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(writer, products, namespaces);
                }
                xml = stringWriter.ToString();
            }
            return xml;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            SoldItemsDto[] soldProducts = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .Select(u=>new SoldItemsDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                                    .Select(p=>new SoldProductDto
                                    {
                                        Name = p.Name,
                                        Price = p.Price
                                    })
                                    .ToArray()
                })
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(SoldItemsDto[]), new XmlRootAttribute("Users"));
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = false
            };
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            StringBuilder sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), soldProducts, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            CategoriesByProductsCountDto[] soldProducts = context.Categories
                .Select(c => new CategoriesByProductsCountDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts
                            .Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts
                            .Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c=>c.TotalRevenue)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(CategoriesByProductsCountDto[]), new XmlRootAttribute("Categories"));

            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = false
            };

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            StringBuilder sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), soldProducts, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            List<UserDto> users = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderByDescending(u => u.ProductsSold.Count)
                //.Take(10)
                .ProjectTo<UserDto>()
                .ToList();

            UsersAndProductsDto usersAndProducts = Mapper.Map<UsersAndProductsDto>(users);

            XmlSerializer serializer = new XmlSerializer(typeof(UsersAndProductsDto), new XmlRootAttribute("Users"));
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = false
            };
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            StringBuilder sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), usersAndProducts, namespaces);

            return sb.ToString().TrimEnd();
        }


    }
}