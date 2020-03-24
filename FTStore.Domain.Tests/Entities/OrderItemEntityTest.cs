using FTStore.Domain.Entities;
using Xunit;

namespace FTStore.Domain.Tests.Entities
{
    public class OrderItemEntityTest
    {
        [Fact]
        public void ShouldCreateOrderItem()
        {
            var orderItem = new OrderItemEntity();
        }
    }
}
