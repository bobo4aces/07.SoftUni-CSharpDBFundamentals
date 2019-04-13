using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Dtos.Export
{
    public class UsersAndProductsDto
    {
        public UsersAndProductsDto()
        {
            this.Users = new List<UserDto>();
        }
        [XmlElement("count")]
        public int Count { get; set; }
        [XmlArray("users")]
        public List<UserDto> Users { get; set; }
    }
    [XmlType("User")]
    public class UserDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public SoldProductsWithCountDto SoldProducts { get; set; }
    }

    public class SoldProductsWithCountDto
    {
        public SoldProductsWithCountDto()
        {
            this.Products = new List<SoldProductDto>();
        }
        [XmlElement("count")]
        public int Count { get; set; }
        [XmlArray("products")]
        public List<SoldProductDto> Products { get; set; }
    }
}
