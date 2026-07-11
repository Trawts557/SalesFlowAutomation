
using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Stock { get; private set; }

        public Product(int id, string name, decimal price, int stock)
        {
            if (id <= 0)
                throw new DomainException("Id must be greater than zero");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cant be null or empty");

            if (price <= 0)
                throw new DomainException("Price must be greater than zero");

            Id = id;
            Name = name;
            UnitPrice = price;
            Stock = stock;
        }
    }
}
