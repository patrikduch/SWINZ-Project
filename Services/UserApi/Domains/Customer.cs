﻿//-----------------------------------------------------------------------
// <copyright file="Role.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace UserApi.Domains
{
    /// <summary>
    /// Model that represents Customer entity
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets customer`s identifier
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets customer`s first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets customer`s surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets customer`s User reference
        /// </summary>
        public User User { get; set; }
    }
}