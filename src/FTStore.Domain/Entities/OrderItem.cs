using FTStore.Domain.Common.Entities;
using FTStore.Domain.Validations;

namespace FTStore.Domain.Entities
{
    public class OrderItem : Entity
    {
        public int ProductId { get; protected set; }
        public virtual Product Product { get; protected set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public decimal Quantity { get; protected set; }
        public decimal Discount { get; protected set; }
        public decimal Total
        {
            get => (Price * Quantity) - Discount;
        }

        public OrderItem() : base() { }
        public OrderItem(Product product, decimal quantity,
            decimal discount) : base()
        {
            CopyProductsData(product);
            Quantity = quantity;
            Discount = discount;
        }

        private void CopyProductsData(Product product)
        {
            if (product == null)
                return;
            ProductId = product.Id;
            Title = product.Name;
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
