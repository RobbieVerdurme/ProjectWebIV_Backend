using Microsoft.AspNetCore.Identity;
using ProjectWebIV_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectWebIV_Backend.Data
{
    public class PostDataInitalizer
    {
        #region Properties
        private readonly PostContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Constructor
        public PostDataInitalizer(PostContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Method
        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await CreateRoles();
                Customer customer = new Customer { Email = "t@t", FirstName = "Adam", LastName = "Master" };
                _dbContext.Customers.Add(customer);
                await CreateUser(customer.Email, "P@ssword1111", "Admin");

                Customer student = new Customer { Email = "az@az", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Customers.Add(student);
                await CreateUser(student.Email, "P@ssword1111", "User");

                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string email, string password, string role)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
            await _userManager.AddToRoleAsync(user, role);
        }

        private async Task CreateRoles()
        {
                await _roleManager.CreateAsync(new IdentityRole("User"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }

    #endregion
}
}
