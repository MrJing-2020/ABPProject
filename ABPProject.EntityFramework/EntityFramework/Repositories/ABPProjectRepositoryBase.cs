using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace ABPProject.EntityFramework.Repositories
{
    public abstract class ABPProjectRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ABPProjectDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ABPProjectRepositoryBase(IDbContextProvider<ABPProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
        //add common methods for all repositories
    }

    public abstract class ABPProjectRepositoryBase<TEntity> : ABPProjectRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ABPProjectRepositoryBase(IDbContextProvider<ABPProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
