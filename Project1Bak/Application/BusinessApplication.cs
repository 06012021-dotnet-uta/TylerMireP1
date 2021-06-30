using Persistence;
using Application.Handler;
using System.Collections.Generic;
using System;
using System.Text;
using Domain;

namespace Application
{
    public class BusinessApplication
    {
        private readonly LocationHandler _locationHandler;
        private readonly CustomerHandler _customerHandler;
        private readonly ProductHandler _productHandler;
        private readonly OrderHandler _orderHandler;
        private readonly DataContext _context;

        public BusinessApplication(DataContext context)
        {
            _locationHandler = new LocationHandler(context);
            _customerHandler = new CustomerHandler(context);
            _productHandler = new ProductHandler(context);
            _orderHandler = new OrderHandler(context);
            _context = context;
        }

        /// <summary>
        /// Registers a new user and add their info into the database for future logins
        /// </summary>
        /// <param name="customer">New customer to be added into the database context</param>
        /// <param name="password">New password to be assigned to user</param>
        /// <param name="response">Function response if any errors occur</param>
        /// <returns>Operation success result</returns>
        public bool RegisterCustomer(Customer customer, string password, out string response)
        {
            response = "";

            //Add password hash to customer
            customer.PasswordHash = GetPasswordHash(password);

            //Check to see if username is already taken
            List < Customer > DBCustomerList = _customerHandler.List();
            foreach(Customer c in DBCustomerList)
            {
                if(customer.Username == c.Username)
                {
                    response = "Username already in use.";
                    return false;
                }
            }

            try{
                _customerHandler.Create(customer);
                return true;
            }
            catch(Exception e)
            {
                response = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Hashes password and returns the hashed string as an encrypted byte array
        /// </summary>
        /// <param name="password">string to be hashed</param>
        /// <returns>Hashed string</returns>
        private Byte[] GetPasswordHash(string password)
        {
            Byte[] passwordHash = new Byte[64];
            passwordHash = Encoding.ASCII.GetBytes(password);
            return passwordHash;
        }

        /// <summary>
        /// Verifies that the username and password pair are present and accepted in the database
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        /// <param name="response">Function response if any errors occur</param>
        /// <returns>Customer on successful login attempt. Null if failed.</returns>
        public Customer LoginCustomer(string username, string password, out string response)
        {
            response = "";

            List<Customer> DBCustomerList = _customerHandler.List();
            foreach(Customer c in DBCustomerList)
            {
                if(c.Username == username)
                {
                    string passwordHashA = BitConverter.ToString(GetPasswordHash(password));
                    string passwordHashB = BitConverter.ToString(c.PasswordHash).Substring(0, passwordHashA.Length);
                    if ( passwordHashA == passwordHashB)
                    {
                        return c;
                    }
                    else
                    {
                        response = "Incorrect password.";
                        return null;
                    }
                }
            }

            response = $"No account found for {username}";
            return null;
        }

        /// <summary>
        /// Retrieves a list of all stored locations in database
        /// </summary>
        /// <returns>List containing all stored locations</returns>
        public List<Location> GetLocationList()
        {
            return _locationHandler.List();
        }

        /// <summary>
        /// Retrieves a Location based on the index of the dbset
        /// </summary>
        /// <param name="locationIndex">Index of dbset</param>
        /// <returns>Location object located in the specified index or null if not found</returns>
        public Location GetLocation(int locationIndex)
        {
            if (locationIndex < _locationHandler.List().Count && locationIndex >= 0)
                return _locationHandler.List()[locationIndex];
            else
                return null;
        }

        /// <summary>
        /// Returns location based on the specified location id
        /// </summary>
        /// <param name="locationId">Location id of the desired location</param>
        /// <returns>Location object with the matching location id or null if not found</returns>
        public Location GetLocation(Guid locationId)
        {
            foreach(Location l in GetLocationList())
            {
                if (l.Id == locationId) 
                    return l;
            }
            return null;
        }

        /// <summary>
        /// Retrieves a location product details based on the specified location id and product id
        /// </summary>
        /// <param name="locationId">Location id to search by</param>
        /// <param name="productId">product id to search by</param>
        /// <returns>Location product inventory junction object that matches the specified location product id. Null if nothing is found</returns>
        public LocationProductInfo GetLocationProductDetails(Guid locationId, Guid productId)
        {
            foreach(LocationProductInfo l in GetLocationProductList(locationId))
            {
                if (l.ProductId == productId)
                    return l;
            }

            return null;
        }

        /// <summary>
        /// Retrieves a list of location product details on the given location id
        /// </summary>
        /// <param name="locationId">Location id to search by </param>
        /// <returns>List of location product inventory junctions. Null if nothing is found</returns>
        public List<LocationProductInfo> GetLocationProductList(Guid locationId)
        {
            List<LocationProductInfo> locationProductInventories =
                _locationHandler.ListLocationInventory(locationId);

            return locationProductInventories;
        }

        /// <summary>
        /// Returns a product object based on it's product id
        /// </summary>
        /// <param name="productId">Id to seach by</param>
        /// <returns>Product object with matching product id. Null if nothing is found</returns>
        public Product GetProductDetails(Guid? productId)
        {
            if (productId != null)
            {
                return _productHandler.Read((Guid)productId);
            }
            else
                return null;
        }

        /// <summary>
        /// Checks out the list of orders and saves changes to the database
        /// </summary>
        /// <param name="orders">Orders to checkout</param>
        /// <returns>Bool on success status</returns>
        public bool Checkout(List<Order> orders)
        {
            foreach(Order o in orders)
            {
                o.LastOrderDate = DateTime.Now;
                o.OrderCreationDate = DateTime.Now;
            }

            _context.AddRange(orders);

            bool success = _context.SaveChanges() > 0;
            return success;
        }

        /// <summary>
        /// Retrieves customer order history based on customer Id
        /// </summary>
        /// <param name="customerId">Customer id to search by</param>
        /// <returns>List of orders that match customer id. Null if nothing is found</returns>
        public List<Order> GetCustomerHistory(Guid customerId)
        {
            List<Order> customerOrderList = new List<Order>();
            foreach(Order o in _orderHandler.List())
            {
                if (o.CustomerId == customerId)
                    customerOrderList.Add(o);
            }
            if (customerOrderList.Count > 0)
                return customerOrderList;
            else
                return null;
        }
    }
}
