
namespace Sattelite.Data.Infras
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether is in transaction.
        /// </summary>
        bool IsInTransaction { get; }

        /// <summary>
        /// The save changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <param name="saveOptions">
        /// The save options.
        /// </param>
        Task SaveChangesAsync();

        /// <summary>
        /// The begin transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <param name="isolationLevel">
        /// The isolation level.
        /// </param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// The roll back transaction.
        /// </summary>
        void RollBackTransaction();

        /// <summary>
        /// The commit transaction.
        /// </summary>
        void CommitTransaction();
    }
}
