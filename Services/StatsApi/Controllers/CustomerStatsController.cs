﻿//-----------------------------------------------------------------------
// <copyright file="CustomerStatsController.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>

namespace StatsApi.Controllers
{
    using System.Linq;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Contexts;
    using Dto;


    /// <summary>
    /// REST API controller for handling customer`s statistics
    /// </summary>
    [Route("api/stats/customers/")]
    [ApiController]
    public class CustomerStatsController : ControllerBase
    {
        private readonly CustomerStatsContext _context;
        private readonly ICustomerStatsRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="customerStatsRepository"></param>
        public CustomerStatsController(CustomerStatsContext context, ICustomerStatsRepository customerStatsRepository)
        {
            _repository = customerStatsRepository;
            _context = context;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("summary")]
        [HttpGet]
        public ActionResult<CustomerSummaryDto> GetSummaryStats()
        {
            return Ok(new CustomerSummaryDto
            {
                CustomerCount = _context.Customers.Count(),
                ProductCount = _context.Products.Count(c => c.IsDeleted.Equals(false)),
                LatestIncome = _repository.GetLatestIncome(_context.Orders),
                SoldCount = _repository.GetSoldCount(_context.Orders)
            
            });
        }

       
    }
}
