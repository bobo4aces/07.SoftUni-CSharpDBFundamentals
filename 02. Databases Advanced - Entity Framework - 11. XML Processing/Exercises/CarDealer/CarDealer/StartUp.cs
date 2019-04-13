using AutoMapper;
using CarDealer.Data;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                context.Database.EnsureCreated();
                //09. Import Suppliers 
                //string path = "../../../Datasets/suppliers.xml";
                //string xml = File.ReadAllText(path);
                //string output = ImportSuppliers(context, xml);
                //Console.WriteLine(output);
                //10. Import Parts 
                //string path = "../../../Datasets/parts.xml";
                //string xml = File.ReadAllText(path);
                //string output = ImportParts(context, xml);
                //Console.WriteLine(output);
                //11. Import Cars 
                string path = "../../../Datasets/cars.xml";
                string xml = File.ReadAllText(path);
                string output = ImportCars(context, xml);
                Console.WriteLine(output);
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportSuppliersDto[]), new XmlRootAttribute("Suppliers"));
            ImportSuppliersDto[] suppliersDtos = (ImportSuppliersDto[])xmlSerializer.Deserialize(new StringReader(inputXml));
            List<Supplier> suppliers = new List<Supplier>();
            foreach (var suppliersDto in suppliersDtos)
            {
                if (suppliersDto?.Name == null || suppliersDto?.IsImporter == null)
                {
                    continue;
                }
                Supplier supplier = new Supplier()
                {
                    Name = suppliersDto.Name,
                    IsImporter = suppliersDto.IsImporter
                };
                suppliers.Add(supplier);
            }
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPartDto[]), new XmlRootAttribute("Parts"));
            ImportPartDto[] importPartDtos = (ImportPartDto[])xmlSerializer.Deserialize(new StringReader(inputXml));
            HashSet<int> suppliers = context.Suppliers
                .Select(s=>s.Id)
                .ToHashSet();
            List<Part> parts = new List<Part>();
            foreach (var importPartDto in importPartDtos)
            {
                if (importPartDto.SupplierId == 0 || !suppliers.Contains(importPartDto.SupplierId))
                {
                    continue;
                }
                Part part = new Part()
                {
                    Name = importPartDto.Name,
                    Price = importPartDto.Price,
                    Quantity = importPartDto.Quantity,
                    SupplierId = importPartDto.SupplierId
                };
                parts.Add(part);
            }
            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCarDto[]), new XmlRootAttribute("Cars"));
            ImportCarDto[] importCarDtos = (ImportCarDto[])xmlSerializer.Deserialize(new StringReader(inputXml));
            HashSet<int> partIds = context.Parts
                .Select(p => p.Id)
                .ToHashSet();
            List<Car> cars = new List<Car>();
            foreach (var importCarDto in importCarDtos)
            {
                Car car = new Car()
                {
                    Make = importCarDto.Make,
                    Model = importCarDto.Model,
                    TravelledDistance = importCarDto.TraveledDistance
                };
                foreach (var part in importCarDto.Parts.Distinct())
                {
                    if (!partIds.Contains(part.Id))
                    {
                        continue;
                    }
                    car.PartCars.Add(new PartCar()
                    {
                        CarId = car.Id,
                        PartId = part.Id
                    });
                }
            }
            return $"Successfully imported {cars.Count}";
        }






    }
}