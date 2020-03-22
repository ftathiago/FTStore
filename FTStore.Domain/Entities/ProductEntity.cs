using System;
using FTStore.Domain.Validations;

namespace FTStore.Domain.Entities
{
    public class ProductEntity : FTStore.Domain.Common.Entities.Entity
    {
        public string Title { get; protected set; }
        public string Details { get; protected set; }
        public decimal Price { get; protected set; }
        public string ImageFileName { get; protected set; }

        public ProductEntity(string title, string details, decimal price, string imageFileName)
        {
            Title = title;
            Details = details;
            Price = price;
            ImageFileName = imageFileName;
        }

        public override bool IsValid()
        {
            _validationResult = new ProductEntityValidations().Validate(this);
            return _validationResult.IsValid;
        }

        public void ChangeTitle(string newTitle)
        {
            this.Title = newTitle;
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
