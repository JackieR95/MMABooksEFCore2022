using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MMABooksEFClasses.Models;
using NUnit.Framework;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        
        MMABooksContext dbContext;
        Customer? c;
        List<Customer>? customers;

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        // Verify that retrieving all customers returns the correct total count,
        // and that the first customer matches the expected record.
        [Test]
        public void GetAllTest()
        {
            customers = dbContext.Customers.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual("Abeyatunge, Derek", customers[0].Name);
            PrintAll(customers);
        }
        // Test that verify's a customer can be retrieved by its primary key CustomerID("102")
        [Test]
        public void GetByPrimaryKeyTest()
        {
            c = dbContext.Customers.Find(102);
            Assert.IsNotNull(c);
            Assert.AreEqual("Hernandez, Esta", c.Name);
            Console.WriteLine(c);
        }

        // Test to verify that we can retrieve multiple customers or a single specific customer.
        [Test]
        public void GetUsingWhere()
        {
            // get a list of all of the customers who live in OR
            customers = dbContext.Customers.Where(c => c.State.StartsWith("OR")).OrderBy(c => c.Name).ToList();
            Assert.AreEqual(5, customers.Count);
            Assert.AreEqual("Erpenbach, Lee", customers[0].Name);
            PrintAll(customers);
        }

        [Test]
        public void GetWithInvoicesTest()
        {
           // get the customer whose id is 20 and all of the invoices for that customer
        }
        /*
        [Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the customer id, name, statecode and statename
            var customers = dbContext.Customers.Join(
               dbContext.States,
               c => c.StateCode,
               s => s.StateCode,
               (c, s) => new { c.CustomerId, c.Name, c.StateCode, s.StateName }).OrderBy(r => r.StateName).ToList();
            Assert.AreEqual(696, customers.Count);
            // I wouldn't normally print here but this lets you see what each object looks like
            foreach (var c in customers)
            {
                Console.WriteLine(c);
            }
        }
        */

        [Test]
        public void DeleteTest()
        {

        }

        [Test]
        public void CreateTest()
        {

        }

        [Test]
        public void UpdateTest()
        {

        }

        public void PrintAll(List<Customer> customers)
        {
            foreach (Customer c in customers)
            {
                Console.WriteLine(c);
            }
        }
        
    }
}