using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using System;
using Xunit;
using Application;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace Testing
{
    public class UnitTest1
    {
        DbContextOptions<DataContext> options;
        Mock<IMediator> mediator;
        IHostBuilder host;

        public UnitTest1()
        {
            options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
             .Options;

        }

        [Fact]
        public async void RegisterCustomer()
        {
            using(var context = new DataContext(options))
            {
                //Arrange
                var newCustomer = new Customer()
                {
                    UserName = "testusername",
                    LastName = "testLastName",
                    FirstName = "testFirstname",
                    AddressStreet = "testStreet",
                    AddressCity = "testCity",
                    AddressState = "AA"
                };

                var password = "testing";

                //Act
                new Application.Customers.Register.Command() 
                {
                    UserName = "testusername",
                    LastName = "testLastName",
                    FirstName = "testFirstname",
                    AddressStreet = "testStreet",
                    AddressCity = "testCity",
                    AddressState = "AA"
                };

                //Assert
            }
        }
    }
}
