using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace GmailAPIDemo.EntityFramework.Repositories
{
    public abstract class GmailAPIDemoRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<GmailAPIDemoDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected GmailAPIDemoRepositoryBase(IDbContextProvider<GmailAPIDemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories

    }

    public abstract class GmailAPIDemoRepositoryBase<TEntity> : GmailAPIDemoRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        

        protected GmailAPIDemoRepositoryBase(IDbContextProvider<GmailAPIDemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
