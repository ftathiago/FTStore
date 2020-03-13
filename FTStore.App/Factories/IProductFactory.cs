using FTStore.App.Models;
using FTStore.Domain.Entity;

namespace FTStore.App.Factories
{
    public interface IProductFactory
    {
        ProductEntity Convert(Product product);
    }
}