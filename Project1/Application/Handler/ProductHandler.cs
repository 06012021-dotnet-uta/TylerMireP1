using Domain;
using Persistence;
using System;

namespace Application.Handler
{
    internal class ProductHandler : BaseHandler, IHandler
    {
        public ProductHandler(DataContext context)
            : base(context)
        {
        }

        public bool Create(Product newProduct)
        {
            return Create<Product>(newProduct);
        }

        public Product Read(Guid id)
        {
            return Read<Product>(id);
        }

        public bool Update(Product newProduct)
        {
            return Update<Product>(newProduct, newProduct.Id);
        }

        public bool Delete(Guid id)
        {
            return Delete<Product>(id);
        }
    }
}
