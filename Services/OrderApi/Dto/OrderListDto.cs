﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersistenceLib.Domains.OrderApi;
using PersistenceLib.Domains.UserApi;

namespace OrderApi.Dto
{
    /// <summary>
    /// Dto for listing all orders
    /// </summary>
    public class OrderListDto
    {

        /// <summary>
        /// Order identifier
        /// </summary>
        public int Id { get; set; }


        public List<Product> Products { get; set; }

        /// <summary>
        /// Creation date of order
        /// </summary>
        public DateTime CreationDate{ get; set; }


        /// <summary>
        /// Customers set
        /// </summary>
        public int CustomerId { get; set; }

    }
}
