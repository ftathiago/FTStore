using FTStore.App.Models;
using FTStore.Domain.Entities;

namespace FTStore.App.Factories
{
    public interface IProductFactory
    {
        ProductEntity Convert(Product product);
    }
}
