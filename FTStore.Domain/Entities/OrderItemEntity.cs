using FTStore.Domain.Common.Entities;
using FTStore.Domain.Validations;

namespace FTStore.Domain.Entities
{
    public class OrderItemEntity : Entity
    {
        public int ProductId { get; protected set; }
        public virtual ProductEntity Product { get; protected set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public decimal Quantity { get; protected set; }
        public decimal Discount { get; protected set; }
        public decimal Total
        {
            get => (Price * Quantity) - Discount;
        }

        public OrderItemEntity(ProductEntity product, decimal quantity,
            decimal discount) : base()
        {
            CopyProductsData(product);
            Quantity = quantity;
            Discount = discount;
        }

        private void CopyProductsData(ProductEntity product)
        {
            if (product == null)
                return;
            ProductId = product.Id;
            Title = product.Title;
            Price = product.Price;
            Product = product;
        }

        public override bool IsValid()
        {
            _validationResult = new OrderItemEntityValidations().Validate(this);
            return _validationResult.IsValid;
        }
    }
}