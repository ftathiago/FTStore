using FTStore.Domain.Entities;

namespace FTStore.Domain.Tests.Fixture
{
    internal static class OrderItemEntityPrototype
    {
        public const decimal QUANTITY = 10M;
        public const decimal INVALID_QUANTITY = 0M;
        public const decimal DISCOUNT = 2;

        public const decimal INVALID_DISCOUNT = -1;
        public const decimal NO_DISCOUNT = 0M;

        public static OrderItemEntity GetValidOrderItem(int productId = 0)
        {
            var product = ProductEntityPrototype.GetValidProduct();
            var orderItem = new OrderItemEntity(
                product,
                QUANTITY,
                DISCOUNT);
            return orderItem;
        }

        public static OrderItemEntity GetInvalidOrderItem(int productId = 0)
        {
            var product = ProductEntityPrototype.GetInvalidProduct(productId);
            var orderItem = new OrderItemEntity(
                product,
                INVALID_QUANTITY,
                INVALID_DISCOUNT);
            return orderItem;
        }
    }
}
