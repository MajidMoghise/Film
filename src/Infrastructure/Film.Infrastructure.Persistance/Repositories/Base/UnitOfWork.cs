using Film.Domain.Contract.Base.Repository;
using Film.Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Infrastructure.Persistance.Repositories.Base
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        public UnitOfWork(FilmDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public FilmDbContext Context { get; }





        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction;
        public Task BeginTransactionAsync()
        {
            Transaction = Context.Database.BeginTransaction();
            return Task.CompletedTask;
        }
        public void BeginTransaction()
        {
            Transaction = Context.Database.BeginTransaction();

        }


        public Task<int> CommitAsync()
        {

            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = Transaction is not null ? Transaction : Context.Database.BeginTransaction();
            try
            {
                var result = Context.SaveChangesAsync();
                transaction.CommitAsync();
                transaction.DisposeAsync();
                if (Transaction is not null) { Transaction.DisposeAsync(); Transaction = null; }
                return result;
            }
            catch (Exception)
            {
                transaction.RollbackAsync();
                throw;
            }
        }
        public int Commit()
        {

            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = Transaction is not null ? Transaction : Context.Database.BeginTransaction();
            try
            {
                int result = Context.SaveChanges();
                transaction.Commit();
                transaction.Dispose();
                if (Transaction is not null) { Transaction.Dispose(); Transaction = null; }
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            if (Transaction is not null)
            {
                Transaction.Rollback();
                Transaction.Dispose();
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }
                else
                {
                    Context.Dispose();
                }

            }

            disposed = true;
        }

    }
}
