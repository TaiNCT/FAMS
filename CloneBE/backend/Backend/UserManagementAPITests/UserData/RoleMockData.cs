using Entities.Models;

namespace UserManagementAPITests.UserData
{
    public class RoleMockData
    {
        //Data of Role
        public static List<Role> GetRoles()
        {
            return
            [
                new Role
                {
                    RoleId = "817E56F4-E597-4AEB-A04C-144A30547334",
                    Id = 3,
                    Title = "Admin",
                    CreatedBy = "trong",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "trong",
                    ModifiedDate = DateTime.Now,
                    RoleName = "Admin",
                    //RolePermission = new RolePermission(),
                    Users = new List<User>(),
                },
                new Role
                {
                    RoleId = "FA2DD038-D819-4107-BC97-4485DE156120",
                    Id = 5,
                    Title = "Trainer",
                    CreatedBy = "trong",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "trong",
                    ModifiedDate = DateTime.Now,
                    RoleName = "Trainer",
                    //RolePermission = new RolePermission(),
                    Users = new List<User>(),
                }
            ];
        }
        //Data of RolePermission
        public static List<RolePermission> GetRolePermissions()
        {
            return
            [
                new RolePermission
                {
                    PermissionId = "23C678E9-30C6-42E2-83A9-8758DD65B82E",
                    RoleId = "817E56F4-E597-4AEB-A04C-144A30547334",
                    Syllabus = "19878473-33BA-4A48-B2C1-C17CAE56F283",
                    TrainingProgram = "7651047C-571D-4320-A46D-2FC0CFEADC0F",
                    Class = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    LearningMaterial = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                    UserManagement = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    Role = new Role
                    {
                        RoleName = "Admin",
                        RoleId= "817E56F4-E597-4AEB-A04C-144A30547334"
                    }
                },
                new RolePermission
                {
                    PermissionId = "6075BAE4-EB47-4532-AD64-3187B3A84E32",
                    RoleId = "FA2DD038-D819-4107-BC97-4485DE156120",
                    Syllabus = "19878473-33BA-4A48-B2C1-C17CAE56F283",
                    TrainingProgram = "7651047C-571D-4320-A46D-2FC0CFEADC0F",
                    Class = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    LearningMaterial = "79C7E0BD-58F6-4B9E-8472-6DCD544268B7",
                    UserManagement = "34595DFB-E78F-4828-909F-240CAF8FA101",
                    Role = new Role
                    {
                        RoleName = "Trainer",
                        RoleId= "FA2DD038-D819-4107-BC97-4485DE156120"
                    }
                }
            ];
        }
    }
}
