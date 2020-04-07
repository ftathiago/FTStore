using FTStore.Lib.Common.Entities;
using FTStore.Domain.Validations;

namespace FTStore.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; protected set; }
        public string Details { get; protected set; }
        public decimal Price { get; protected set; }
        public string ImageFileName { get; protected set; }

        public Product(string name, string details, decimal price, string imageFileName)
        {
            Name = name;
            Details = details;
            Price = price;
            ImageFileName = imageFileName;
        }

        public override bool IsValid()
        {
            _validationResult = new ProductEntityValidations().Validate(this);
            return _validationResult.IsValid;
        }

        public void ChangeName(string newName)
        {
            this.Name = newName;
        }

        public void ChangeDetails(string newDetails)
        {
            this.Details = newDetails;
        }

        public void ChangePrice(decimal newPrice)
        {
            this.Price = newPrice;
        }

        public void DefineImageFileName(string newFileName)
        {
            this.ImageFileName = newFileName;
        }
    }
}
