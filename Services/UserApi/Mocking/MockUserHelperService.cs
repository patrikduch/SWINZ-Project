﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Domains;
using UserApi.Dto.Users;
using UserApi.Interfaces.Helpers;

namespace UserApi.Mocking
{
    public class MockUserHelperService : IUserHelperService
    {
        public Task<User> PrepareUser(UserDto dto, string roleName)
        {
            return null;
        }
    }
}
