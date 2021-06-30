using Persistence;
using System;

namespace Application.Handler{
    internal abstract class BaseHandler
    {
        protected readonly DataContext _context;

        public BaseHandler(DataContext context)
        {
            this._context = context;
        }

        public bool Create<T>(T t)
        {
            var createObj = t;

            _context.Add(createObj);
            bool success = _context.SaveChanges() > 0;

            if(success == false)
                throw new Exception("Problem saving changes.");
            
            return success;
        }

        public bool Delete<T>(Guid id)
        {
           
            var deleteTarget = _context.Find(typeof(T), id);

            if(deleteTarget == null)
                throw new Exception("Unable to locate delete target");
            
            _context.Remove(deleteTarget);
            bool success = _context.SaveChanges() > 0;

            if(success == false)
                throw new Exception("Problem saving changes.");
            
            return success;
        }

        public T Read<T>(Guid id)
        {
            var returnObj = _context.Find(typeof(T), id);

            if(returnObj == null)
                throw new Exception("Unable to locate requested data");

            return (T)returnObj;
        }

        public bool Update<T>(T t, Guid id)
        {
            var updateObj = _context.Find(typeof(T), id);

            if(updateObj == null)
                throw new Exception("Unable to locate requested data to be updated");
            
            //Since entity from context is reference we can just reassign - "Probably"
            updateObj = t;

            bool success = _context.SaveChanges() > 0;

            if(success == false)
                throw new Exception("Problem saving changes.");
            
            return success;
        }
    }
}