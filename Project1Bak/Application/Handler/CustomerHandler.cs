using System.Collections.Generic;
using Persistence;
using Domain;
using System;
using System.Linq;

namespace Application.Handler
{
    internal class CustomerHandler : BaseHandler, IHandler
    {
        public CustomerHandler(DataContext dataContext)
            : base(dataContext)
        { 
        }

        public bool Create(Customer newCustomer)
        {
            return Create<Customer>(newCustomer);
        }

        public Customer Read(Guid id)
        {
            return Read<Customer>(id);
        }

        public bool Update(Customer newCustomer)
        {
            return Update<Customer>(newCustomer, newCustomer.Id);
        }

        public bool Delete(Guid id)
        {
            return Delete<Customer>(id);
        }

        public List<Customer> List()
        {
            List<Customer> locations = _context.Customers.ToList();
            return locations;
        }
    }
}