using FTStore.Domain.Entities;

namespace FTStore.Domain.Tests.Fixture
{
    internal static class OrderItemEntityFixture
    {
        public const decimal QUANTITY = 10M;
        public const decimal INVALID_QUANTITY = 0M;
        public const decimal DISCOUNT = 2;

        public const decimal INVALID_DISCOUNT = -1;
        public const decimal NO_DISCOUNT = 0M;

        public static OrderItem GetValidOrderItem(int productId = 0)
        {
            var product = ProductEntityFixture.GetValidProduct();
            var orderItem = new OrderItem(
                product,
                QUANTITY,
                DISCOUNT);
            return orderItem;
        }

        public static OrderItem GetInvalidOrderItem(int productId = 0)
        {
            var product = ProductEntityFixture.GetInvalidProduct(productId);
            var orderItem = new OrderItem(
                product,
                INVALID_QUANTITY,
                INVALID_DISCOUNT);
            return orderItem;
        }
    }
}
