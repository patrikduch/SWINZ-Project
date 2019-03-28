﻿//-----------------------------------------------------------------------
// <copyright file="CustomerRepository.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace UserApi.Repositories
{
    using Contexts;
    using Domains;
    using Dto.Customers;
    using Dto.Users;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PersistenceLib;
    using UserApi.Interfaces.Helpers;
    using Mocking;

    /// <summary>
    /// Repository for customer`s manipulation
    /// </summary>
    public class CustomerRepository : Repository<Customer>, ICustomerRepository 
    {
        #region Fields
        /// <summary>
        /// User context instance
        /// </summary>
        private readonly IUserHelperService _userHelperService;
        #endregion

        public UserContext UserContext => Context as UserContext;

        #region Constructors
        /// <summary>
        /// Inject constructor for Customer repository
        /// </summary>
        /// <param name="userContext">Context of all users</param>
        public CustomerRepository(IUserContextService context, IUserHelperService userHelperService) : base(context.UserContext)
        {
            _userHelperService = userHelperService;
        }


        #endregion

        #region Methods

        /// <summary>
        /// Transformer of Customer entity into DTO
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public IEnumerable<CustomerUserDto> CustomerEntityToDto(IEnumerable<Customer> customers)
        {
            return customers.Select(customer => new CustomerUserDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                Lastname = customer.LastName,
                Username = customer.User.Username
            });
        }


        /// <summary>
        /// Creation of customer
        /// </summary>
        /// <param name="customerDto">Data transfer object for customers</param>
        /// <returns></returns>
        public async Task<Customer> CreateCustomer(CustomerRegisterDto customerDto)
        {
            if (customerDto.FirstName == string.Empty|| customerDto.Lastname == string.Empty)
                return null; // Creation cannot be performed

            var user = await _userHelperService.PrepareUser(new UserDto
            {
                Username = customerDto.Username,
                Password = customerDto.Password
            }, "Customer");

            // Creation of customer object from user a customer data
            var customerResult = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.Lastname,
                User = user
            };

            // Add new object to the customer collection
            UserContext.Customers.Add(customerResult);

            // Return new added object
            return customerResult;
        }


        #endregion
    }
}
