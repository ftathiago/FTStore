using Microsoft.EntityFrameworkCore;

using FTStore.Infra.Context;
using FTStore.Infra.Tests.Fixtures;

using Xunit;

namespace FTStore.Infra.Tests.Repositories
{
    public abstract class BaseRepositoryTest<TModel> : IClassFixture<ContextFixture>, IClassFixture<AutoMapperFixture>
        where TModel : class
    {
        protected readonly ContextFixture ContextFixture;
        protected readonly AutoMapperFixture MapperFixture;
        public BaseRepositoryTest(ContextFixture contextFixture, AutoMapperFixture autoMapperFixture)
        {
            ContextFixture = contextFixture;
            MapperFixture = autoMapperFixture;
        }

        protected abstract TModel GetModelPrototype(int id = 0);

        protected virtual TModel AddAtRepository(FTStoreDbContext context, int id = 0)
        {
            var model = GetModelPrototype(id);
            context.Set<TModel>().Add(model);
            context.SaveChanges();
            context.Entry(model).State = EntityState.Detached;
            return model;
        }

        public abstract void ShouldRegister();
        public abstract void ShouldConserveDataAfterRegister();
        public abstract void ShouldUpdate();
        public abstract void ShouldDeleteByEntityReference();
    }
}
