﻿//-----------------------------------------------------------------------
// <copyright file="Product.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace PersistenceLib.Domains.OrderApi
{
    /// <summary>
    /// Entity that represents products
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets product`s identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets product`s name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets product`s price
        /// </summary>
        public int Price { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
