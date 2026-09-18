
using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Stock { get; private set; }

        public Product(string name, decimal unitPrice, int stock)
        {
            ValidateProduct(name, unitPrice, stock);

            Name = name;
            UnitPrice = unitPrice;
            Stock = stock;
        }

        public Product(int id, string name, decimal unitPrice, int stock) : this(name, unitPrice, stock)
        {
            if (id <= 0)
                throw new DomainException("Id must be greater than zero");

            Id = id;
        }

        public void Update(string name, decimal unitPrice, int stock)
        {
            ValidateProduct(name, unitPrice, stock);

            Name = name;
            UnitPrice = unitPrice;
            Stock = stock;
        }

        private void ValidateProduct(string name, decimal unitPrice, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cant be null or empty");

            if (unitPrice <= 0)
                throw new DomainException("Price must be greater than zero");

            if (stock < 0)
                throw new DomainException("Stock must be greater than zero");
        }
    }
}
