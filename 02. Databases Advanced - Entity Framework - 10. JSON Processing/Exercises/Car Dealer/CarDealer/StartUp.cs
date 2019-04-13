using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            Mapper.Initialize(cnf => cnf.AddProfile(new CarDealerProfile()));

            using (CarDealerContext context = new CarDealerContext())
            {
                context.Database.EnsureCreated();
                //09. Import Suppliers 
                //string path = "../../../Datasets/suppliers.json";
                //string inputJson = File.ReadAllText(path);
                //string output = ImportSuppliers(context, inputJson);
                //Console.WriteLine(output);
                //10. Import Parts 
                //string path = "../../../Datasets/parts.json";
                //string inputJson = File.ReadAllText(path);
                //string output = ImportParts(context, inputJson);
                //Console.WriteLine(output);
                //11. Import Cars 
                //string path = "../../../Datasets/cars.json";
                //string inputJson = File.ReadAllText(path);
                //string output = ImportCars(context, inputJson);
                //Console.WriteLine(output);
                //12. Import Customers 
                //string path = "../../../Datasets/customers.json";
                //string inputJson = File.ReadAllText(path);
                //string output = ImportCustomers(context, inputJson);
                //Console.WriteLine(output);
                //13. Import Sales 
                //string path = "../../../Datasets/sales.json";
                //string inputJson = File.ReadAllText(path);
                //string output = ImportSales(context, inputJson);
                //Console.WriteLine(output);
                //14. Export Ordered Customers
                //string path = "../../../Exports/ordered-customers.json";
                //string outputJson = GetOrderedCustomers(context);
                //File.WriteAllText(path, outputJson);
                //15. Export Cars From Make Toyota 
                //string path = "../../../Exports/toyota-cars.json";
                //string outputJson = GetCarsFromMakeToyota(context);
                //File.WriteAllText(path, outputJson);
                //16. Export Local Suppliers 
                //string path = "../../../Exports/local-suppliers.json";
                //string outputJson = GetLocalSuppliers(context);
                //File.WriteAllText(path, outputJson);
                //17. Export Cars With Their List Of Parts 
                //string path = "../../../Exports/cars-and-parts.json";
                //string outputJson = GetCarsWithTheirListOfParts(context);
                //File.WriteAllText(path, outputJson);
                //18. Export Total Sales By Customer 
                //string path = "../../../Exports/customers-total-sales.json";
                //string outputJson = GetTotalSalesByCustomer(context);
                //File.WriteAllText(path, outputJson);
                //19. Export Sales With Applied Discount 
                string path = "../../../Exports/sales-discounts.json";
                string outputJson = GetSalesWithAppliedDiscount(context);
                File.WriteAllText(path, outputJson);
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            Supplier[] suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Length}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            Part[] parts = JsonConvert.DeserializeObject<Part[]>(inputJson).ToArray();
            foreach (var part in parts)
            {
                if (context.Suppliers.Any(s=>s.Id == part.SupplierId))
                {
                    context.Add(part);
                }
            }
            //context.Parts.AddRange(parts.Where(p => p.SupplierId != 0));
            int partsCount = context.SaveChanges();
            return $"Successfully imported {partsCount}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            CarInsertDto[] cars = JsonConvert.DeserializeObject<CarInsertDto[]>(inputJson);
            List<Car> mappedCars = new List<Car>();

            foreach (var car in cars)
            {
                Car vehicle = Mapper.Map<CarInsertDto, Car>(car);
                mappedCars.Add(vehicle);
                List<int> partIds = car.PartsId
                                .Distinct()
                                .ToList();
                if (partIds == null)
                {
                    continue;
                }
                partIds.ForEach(id =>
                {
                    PartCar currentPair = new PartCar()
                    {
                        Car = vehicle,
                        PartId = id
                    };
                    vehicle.PartCars.Add(currentPair);
                });
            }
            context.Cars.AddRange(mappedCars);
            context.SaveChanges();
            return $"Successfully imported {cars.Length}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            Customer[] customers = JsonConvert.DeserializeObject<Customer[]>(inputJson).ToArray();
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Length}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            Sale[] sales = JsonConvert.DeserializeObject<Sale[]>(inputJson).ToArray();
            context.Sales.AddRange(sales);
            int salesCount = context.SaveChanges();
            return $"Successfully imported {salesCount}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            CustomerDto[] customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .ProjectTo<CustomerDto>()
                .ToArray();

            return JsonConvert.SerializeObject(customers,Formatting.Indented);
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            CarDto[] cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<CarDto>()
                .ToArray();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            SupplierDto[] suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .ProjectTo<SupplierDto>()
                .ToArray();
            return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsAndParts = context.Cars
                .Select(c=> new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance,
                        
                    },
                    parts = context.PartCars
                            .Where(p => p.CarId == c.Id)
                            .Select(p => new
                            {
                                Name = p.Part.Name,
                                Price = p.Part.Price.ToString("0.00")
                            })
                            .ToArray()
                })
                .ToArray();

            return JsonConvert.SerializeObject(carsAndParts,Formatting.Indented);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count >= 1)
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                })
                .ToArray()
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToArray();

            return JsonConvert.SerializeObject(customers, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("0.00"),
                    price = s.Car.PartCars.Sum(p => p.Part.Price).ToString("0.00"),
                    priceWithDiscount = (s.Car.PartCars.Sum(p => p.Part.Price) - (s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100)).ToString("0.00")
                });

            return JsonConvert.SerializeObject(sales,Formatting.Indented);
        }
    }
}