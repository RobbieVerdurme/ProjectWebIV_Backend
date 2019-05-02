using Microsoft.EntityFrameworkCore;
using ProjectWebIV_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebIV_Backend.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PostContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(PostContext dbContext)
        {
            _context = dbContext;
            _customers = dbContext.Customers;
        }

        public Customer GetBy(string email)
        {
            return _customers.SingleOrDefault(c => c.Email == email);
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
