﻿//-----------------------------------------------------------------------
// <copyright file="CustomersController.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace UserApi.Controllers
{
    using Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Domains;
    using Interfaces;

    /// <summary>
    /// Rest API Customers controller
    /// </summary>
    [Route("api/customers/")]
    [ApiController]
    public class CustomersController : ControllerBase, ICustomerController
    {
        #region Fields
        /// <summary>
        /// Reference to the customer`s repository
        /// </summary>
        private readonly ICustomerRepository _customerRepository;
        #endregion
        #region Constructors
        /// <summary>
        /// Inject constructor for Customer`s Controller
        /// </summary>
        /// <param name="customerRepository">Interface for customer repository</param>
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion
        #region Actions
        /// <summary>
        /// Get all customers from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll")]
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetCustomers();
        }

        /// <summary>
        /// Update customer by his id
        /// </summary>
        /// <param name="id">Customer`s identifier</param>
        /// <param name="dto">Data transfer object for customers</param>
        /// <returns>Customer instance</returns>
        [HttpPut]
        [Route("update/{id}")]
        public async Task<Customer> UpdateCustomer(int id, [FromBody] CustomerDto dto)
        {
            return await _customerRepository.UpdateCustomer(id, dto);
        }

        /// <summary>
        /// Remove customer by id
        /// </summary>
        /// <param name="id">customer identifier</param>
        /// <returns></returns>
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task DeleteCustomer(int id)
        {
            await _customerRepository.RemoveCustomer(id);
        }



        #endregion
    }
}