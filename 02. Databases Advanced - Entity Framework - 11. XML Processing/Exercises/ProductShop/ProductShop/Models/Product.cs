namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Product
    {
        public Product()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }

        [XmlIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int SellerId { get; set; }
        [XmlIgnore]
        public User Seller { get; set; }

        public int? BuyerId { get; set; }
        public User Buyer { get; set; }
        [XmlIgnore]
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}