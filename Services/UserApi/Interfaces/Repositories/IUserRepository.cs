﻿//-----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace UserApi.Interfaces.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto.Users;
    using PersistenceLib;
    using PersistenceLib.Domains.UserApi;

    /// <summary>
    /// Interface that implements User repository
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUsers();
        Task<User> CreateAdmin(string username, string password);
        Task<User> ValidateUser(RegisterUserDto userDto);
    }
}
