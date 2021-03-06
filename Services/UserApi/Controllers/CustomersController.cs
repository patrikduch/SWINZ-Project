﻿//-----------------------------------------------------------------------
// <copyright file="CustomersController.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace UserApi.Controllers
{
    using QueryObjects;
    using PersistenceLib.Domains.UserApi;
    using System.Linq;
    using UserApi.Interfaces.UnitOfWork;
    using Dto.Customers;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserApi.Interfaces.Controllers;
    using PaginationLib;

    /// <summary>
    /// Rest API Customers controller
    /// </summary>
    [Route("api/customers/")]
    [ApiController]
    public class CustomersController : ControllerBase, ICustomerController
    {
        #region Fields

        /// <summary>
        /// Reference to the customer`s unit of work
        /// </summary>
        private readonly ICustomerUnitOfWork _customerUnitOfWork;

        private readonly ICustomerQuery _customerQuery;

        #endregion
        #region Constructors

        /// <summary>
        /// Inject constructor for Customer`s Controller
        /// </summary>
        /// <param name="customerUnitOfWork">Unit of work for customer`a manipulation</param>
        /// <param name="customerQuery">Query object helper service</param>
        public CustomersController(ICustomerUnitOfWork customerUnitOfWork, ICustomerQuery customerQuery)
        {
            _customerUnitOfWork = customerUnitOfWork;
            _customerQuery = customerQuery;
        }

        #endregion
        #region Actions
        
        /// <summary>
        /// Creation of new customer
        /// </summary>
        /// <param name="customerDto">Data transfer object for customers</param>
        /// <returns>Instance of created user</returns>
        [HttpPost]
        [Route("create")]
        public async Task<CustomerUserDto> CreateCustomer([FromBody] CustomerRegisterDto customerDto)
        {
            var res = await _customerUnitOfWork.CustomerRepository.CreateCustomer(customerDto);
            await _customerUnitOfWork.Complete();

            var customerId =
                _customerUnitOfWork.CustomerRepository.Find(c =>
                    c.FirstName == res.FirstName && c.LastName == res.LastName).SingleOrDefault();

            return new CustomerUserDto
            {
                Id = customerId?.Id,
                FirstName = res.FirstName,
                LastName = res.LastName,
                Username = res.User.Username,
                Discount = res.Discount
               
            };
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>List of all customers</returns>
        [HttpGet]
        [Route("getAll")]
        public async Task<IEnumerable<CustomerUserDto>> GetAllCustomers()
        {
            return await _customerUnitOfWork.CustomerRepository.GetAllCustomers();
        }

        /// <summary>
        /// Get specific customer by identifier
        /// </summary>
        /// <returns>Customer entity</returns>
        [HttpGet]
        [Route("get/{id}")]
        public async Task<CustomerDto> GetCustomer(int id)
        {
            _customerQuery.FilterById = true;
            _customerQuery.CustomerId = id;
            return await _customerUnitOfWork.CustomerRepository.GetCustomer(_customerQuery);
        }

        /// <summary>
        /// Get specific customer by first name and lastname
        /// </summary>
        /// <returns>Customer entity</returns>
        [HttpGet]
        [Route("get/byName")]
        public async Task<CustomerDto> GetCustomer([FromBody] CustomerEditDto customerDto)
        {
            _customerQuery.FilterByName = true;
            _customerQuery.FirstName = customerDto.Firstname;
            _customerQuery.LastName = customerDto.Lastname;

            return await _customerUnitOfWork.CustomerRepository.GetCustomer(_customerQuery);
        }

        /// <summary>
        /// Get all customers for current page
        /// </summary>
        /// <returns>List of all customers</returns>
        [HttpGet]
        [Route("getPaged")]
        public async Task<IEnumerable<CustomerUserDto>> GetCustomersWithPagination([FromBody]PaginationTransferObject paginationTransfer)
        {
            var res = Paginator.GetPageInterval(paginationTransfer);
            return await _customerUnitOfWork.CustomerRepository.GetCustomersPaged(res.From, res.To, paginationTransfer.PageSize);
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
            // Get customers entity by primary key
            var entity = await _customerUnitOfWork.CustomerRepository.Get(id);

            if (entity == null || (entity.FirstName == dto.FirstName && entity.LastName == dto.LastName)) return null;
            // Change  entity values
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;

            // Save changes
            await _customerUnitOfWork.Complete();

            // Return modified customer object
            return entity;

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
            var userEntity = (_customerUnitOfWork.UserRepository.Find(c => c.Customer.Id == id)).SingleOrDefault();

            if (userEntity != null)
            {
                userEntity.Customer = await _customerUnitOfWork.CustomerRepository.Get(id);

                _customerUnitOfWork.UserRepository.Remove(userEntity);
                await _customerUnitOfWork.Complete();
            }
        }
        #endregion
    }
}