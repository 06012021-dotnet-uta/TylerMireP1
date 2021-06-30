using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Persistence;

namespace Application.Handler
{
    internal class OrderHandler : BaseHandler, IHandler
    {
        public OrderHandler(DataContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Stores a new order into the database
        /// </summary>
        /// <param name="NewOrder">New order to be added into the database context</param>
        /// <returns>Operation success result</returns>

        public bool Create(Order NewOrder)
        {
            return base.Create<Order>(NewOrder);
        }

        /// <summary>
        /// Reads an order from the database context
        /// </summary>
        /// <param name="OrderId">Id of the order to be returned</param>
        /// <returns>Order that matches OrderId</returns>
        public Order Read(Guid OrderId)
        {
            return base.Read<Order>(OrderId);
        }

        /// <summary>
        /// Updates an order using the id in NewOrder to locate and the rest of the properties of NewOrder to update
        /// </summary>
        /// <param name="NewOrder">Order containing Id of order to be updated as well as updated properties</param>
        /// <returns>Operation success result</returns>
        public bool Update(Order NewOrder)
        {
            return base.Update<Order>(NewOrder, NewOrder.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            return base.Delete<Order>(id);
        }

        public List<Order> List()
        {
            return _context.Orders.ToList();
        }

        
    }
}