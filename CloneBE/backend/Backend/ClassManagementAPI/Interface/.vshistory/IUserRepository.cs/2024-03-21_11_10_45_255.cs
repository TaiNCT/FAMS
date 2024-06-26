﻿using ClassManagementAPI.Dto.ClassDTO;
using Entities.Models;

namespace ClassManagementAPI.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserByListUserID(List<ClassUser> userID);
        Task<User> GetUserByUserID(ClassUser userID);
        Task<List<User>> GetAll();
        Task<List<Role>> GetAllRole();
    }
}
