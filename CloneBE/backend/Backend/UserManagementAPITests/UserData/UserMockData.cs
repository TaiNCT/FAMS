using Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Nest;
using System.Net;
using System.Reflection;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPITests.UserData
{
    public static class UserMockData
    {
        //Data of User
        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    UserId = "67E07846-70A1-4372-893C-442285559296",
                    Id = 99,
                    FullName = "John Doe",
                    Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                    Address = "hcm",
                    Gender = "male",
                    Phone = "0987654331",
                    Username= "myphuong123",
                    Password= "admin@123456",
                    RoleId= "817E56F4-E597-4AEB-A04C-144A30547334",
                    CreatedBy= "string",
                    CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    ModifiedBy= "string",
                    ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Avatar= "string",
                    Status= true,
                    Email = "john@gmail.com",
                    Role = new Role
                    {
                        RoleName = "Admin" ,
                        RoleId= "817E56F4-E597-4AEB-A04C-144A30547334"
                    }
                },
                new User
                {
                    UserId = "2441389C-0344-4BD4-92B5-0FB55725EC06",
                    Id = 98,
                    FullName = "John Doe",
                    Dob = DateTime.Parse("2000-03-12T04:16:01.248Z"),
                    Address = "hcm",
                    Gender = "Female",
                    Phone = "0987654321",
                    Username= "myphuong",
                    Password= "admin@123456",
                    RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                    CreatedBy= "string",
                    CreatedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    ModifiedBy= "string",
                    ModifiedDate= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Avatar= "string",
                    Status= true,
                    Email = "john123@gmail.com",
                    Role = new Role
                    {
                        RoleName = "Trainer",
                        RoleId= "FA2DD038-D819-4107-BC97-4485DE156120"
                    }
                },
            };
        }

        //Data of UserPermission
        public static List<UserPermission> GetUserPermissions()
        {
            return new List<UserPermission>
            {
                new UserPermission()
                {
                    UserPermissionId = "82ED6390-D08B-4708-BA81-E5577BB0BEF9",
                    Id= 3,
                    CreatedBy= "",
                    CreatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    UpdatedBy= "",
                    UpdatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Name= "Access Denied"
                },
                new UserPermission()
                {
                    UserPermissionId = "19878473-33BA-4A48-B2C1-C17CAE56F283",
                    Id= 4,
                    CreatedBy= "",
                    CreatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    UpdatedBy= "",
                    UpdatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Name= "View"
                },
                new UserPermission()
                {
                    UserPermissionId = "7651047C-571D-4320-A46D-2FC0CFEADC0F",
                    Id= 5,
                    CreatedBy= "",
                    CreatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    UpdatedBy= "",
                    UpdatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Name= "Modify"
                },
                new UserPermission()
                {
                    UserPermissionId = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    Id= 6,
                    CreatedBy= "",
                    CreatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    UpdatedBy= "",
                    UpdatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Name= "Full Access"
                },
                new UserPermission()
                {
                    UserPermissionId = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                    Id= 7,
                    CreatedBy= "",
                    CreatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    UpdatedBy= "",
                    UpdatedTime= DateTime.Parse("2024-03-12T04:16:01.248Z"),
                    Name= "Create"
                }
            };
        }

        //Data for UserElasticDTO 
        public static List<UserElasticDTO> GetUserElasticDTOs()
        {
            return new List<UserElasticDTO>
            {
                new UserElasticDTO()
                {
                    UserId = "67E07846-70A1-4372-893C-442285559296",
                    FullName = "John Doe",
                    Dob = "2000-03-12T04:16:01.248Z",
                    Address = "hcm",
                    Gender = "male",
                    Phone = "0987654331",
                    RoleId= "817E56F4-E597-4AEB-A04C-144A30547334",
                    Status= true,
                    Email = "john@gmail.com",
                    RoleName = "Admin" ,
                },
                new UserElasticDTO()
                {
                    UserId = "2441389C-0344-4BD4-92B5-0FB55725EC06",
                    FullName = "John Doe",
                    Dob = "2000-03-12T04:16:01.248Z",
                    Address = "hcm",
                    Gender = "Female",
                    Phone = "0987654321",
                    RoleId= "FA2DD038-D819-4107-BC97-4485DE156120",
                    Status= true,
                    Email = "john123@gmail.com",
                    RoleName = "Trainer",
                },
            };
        }
    }
}
