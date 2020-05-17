using AutoMapper;
using FTStore.Auth.Infra.Tables;
using FTStore.Auth.Infra.Test.Fixtures;
using FTStore.Auth.Infra.Tests.Fixtures;
using FTStore.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FTStore.Auth.Infra.Test.Repositories
{
    public abstract class BaseRepositoryTest<TModel> : IClassFixture<ContextFixture>, IClassFixture<AutoMapperFixture>
        where TModel : class
    {
        protected ContextFixture Context { get; private set; }
        protected AutoMapperFixture Mapper { get; private set; }

        public BaseRepositoryTest(ContextFixture context, AutoMapperFixture mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        protected abstract TModel GetModelPrototype(int id = 0);

        protected virtual TModel AddAtRepository(FTStoreAuthContext context, int id = 0)
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
