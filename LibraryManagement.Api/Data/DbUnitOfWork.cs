using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryManagement.Api.Data;

public class DbUnitOfWork<T>(T context) where T : DbContext
{
    public IDbContextTransaction BeginTransaction() => context.Database.BeginTransaction();
    public void RollbackTransaction() => context.Database.RollbackTransaction();
    public void CommitTransaction() => context.Database.CommitTransaction();
}