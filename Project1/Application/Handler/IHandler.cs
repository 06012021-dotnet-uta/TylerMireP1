using System;

namespace Application.Handler
{
    /// <summary>
    /// Enforces CRUD functionality
    /// </summary>
    public interface IHandler
    {
        bool Create<T>(T obj);
        T Read<T>(Guid id);
        bool Update<T>(T obj, Guid id);
        bool Delete<T>(Guid id);
    }
}