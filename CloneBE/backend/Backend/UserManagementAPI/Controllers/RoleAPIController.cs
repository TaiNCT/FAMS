using Entities.Context;
using Entities.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Controllers
{
    [Route("api")]
    public class RoleAPIController : Controller
    {
        private readonly FamsContext _db;
        public RoleAPIController(FamsContext db)
        {
            _db = db;
        }

        //get all role
        [HttpGet("get-role")]
        public ResponseDTO Get()
        {
            try
            {
                var roles = _db.Roles.Select(r => new { roleId = r.RoleId, roleName = r.RoleName }).ToList();
                return new ResponseDTO { Result = roles, StatusCode = 200, IsSuccess = true, Message = "" };
            }
            catch
            {
                return new ResponseDTO { Result = false, StatusCode = 404, IsSuccess = false, Message = "Cannot get role. Please try again." };
            }
        }


        [HttpPost("import-new-role")]
        public async Task<IActionResult> ImportNewRole(IFormFile file)
        {
            var response = new ResponseDTO
            {
                Message = "",
                IsSuccess = false,
                StatusCode = 500
            };
            try
            {

                List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();


                if (file != null && file.Length > 0)
                {
                    Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    Stream stream = file.OpenReadStream();
                    IExcelDataReader? reader = null;
                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        response.Message = "this file fotmat is not suppported";
                        return BadRequest(response);
                    }

                    try
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = reader.AsDataSet().Tables[0];
                        result = dataTable.AsEnumerable()
                            .Select(row => dataTable.Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => row[col]))
                            .ToList();
                    }
                    catch (Exception ex)
                    {
                        response.Message = "An error occurred while upload: " + ex.Message;
                        return BadRequest(response);
                    }
                    reader.Close();
                    reader.Dispose();
                }

                List<ImportNewRoleDTO> importList = new List<ImportNewRoleDTO>();
                try
                {
                    int count = 0;
                    foreach (Dictionary<string, object> item in result)
                    {
                        if (count > 0)
                        {
                            ImportNewRoleDTO importItem = new ImportNewRoleDTO();
                            importItem.RoleName = item["Column0"].ToString().Trim();
                            importItem.Title = item["Column1"].ToString().Trim();
                            importItem.Syllabus = item["Column2"].ToString().Trim();
                            importItem.TrainingProgram = item["Column3"].ToString().Trim();
                            importItem.Class = item["Column4"].ToString().Trim();
                            importItem.LearningMaterial = item["Column5"].ToString().Trim();
                            importItem.UserManagement = item["Column6"].ToString().Trim();
                            importList.Add(importItem);
                        }
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while add user to UserList: " + ex.Message;
                    return BadRequest(response);
                }
                List<ImportNewRoleDTO> importNewRoleList = new List<ImportNewRoleDTO>();
                try
                {
                    int count = 0;
                    foreach (var importItem in importList)
                    {
                        count++;

                        // Verify trong file excel
                        ImportNewRoleDTO importNewRoleItem = new ImportNewRoleDTO();
                        UserPermission? permission = new UserPermission();

                        if (importList.Select(u => u.RoleName.Trim()).ToList().Count(e => e == importItem.RoleName) > 1)
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Role name is dulicate in excel file" + ", ";
                        }

                        // Verify trong db
                        //role name 
                        if (string.IsNullOrEmpty(importItem.RoleName))
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Role name is required" + ", ";
                        }
                        else
                        {
                            importNewRoleItem.RoleName = importItem.RoleName;
                            importNewRoleItem.Title = importItem.Title;
                        }

                        //syllabus
                        if (string.IsNullOrEmpty(importItem.Syllabus.Trim()))
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Syllabus permisstion is required" + ", ";
                        }
                        permission = _db.UserPermissions.FirstOrDefault(u => u.Name.Trim() == importItem.Syllabus.Trim());
                        if (permission == null)
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Invalid syllabus permission " + ", ";
                        }
                        else
                        {
                            importNewRoleItem.Syllabus = permission.UserPermissionId;
                        }

                        //traning program
                        if (string.IsNullOrEmpty(importItem.TrainingProgram.Trim()))
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Training Program permisstion is required" + ", ";
                        }
                        permission = _db.UserPermissions.FirstOrDefault(u => u.Name.Trim() == importItem.TrainingProgram.Trim());
                        if (permission == null)
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Invalid traning program permission " + ", ";
                        }
                        else
                        {
                            importNewRoleItem.TrainingProgram = permission.UserPermissionId;
                        }

                        //class
                        if (string.IsNullOrEmpty(importItem.Class.Trim()))
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Class permisstion is required" + ", ";
                        }
                        permission = _db.UserPermissions.FirstOrDefault(u => u.Name.Trim() == importItem.Class.Trim());
                        if (permission == null)
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Invalid class permission " + ", ";
                        }
                        else
                        {
                            importNewRoleItem.Class = permission.UserPermissionId;
                        }

                        //Learning Material
                        if (string.IsNullOrEmpty(importItem.LearningMaterial.Trim()))
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Learning Material permisstion is required" + ", ";
                        }
                        permission = _db.UserPermissions.FirstOrDefault(u => u.Name.Trim() == importItem.LearningMaterial.Trim());
                        if (permission == null)
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Invalid Learning Material permission " + ", ";
                        }
                        else
                        {
                            importNewRoleItem.LearningMaterial = permission.UserPermissionId;
                        }

                        //User
                        if (string.IsNullOrEmpty(importItem.UserManagement.Trim()))
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "User permisstion is required" + ", ";
                        }
                        permission = _db.UserPermissions.FirstOrDefault(u => u.Name.Trim() == importItem.UserManagement.Trim());
                        if (permission == null)
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Invalid User permission " + ", ";
                        }
                        else
                        {
                            importNewRoleItem.UserManagement = permission.UserPermissionId;
                        }

                        importNewRoleList.Add(importNewRoleItem);
                    }
                    if (response.Message != "")
                    {
                        return BadRequest(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while verify : " + ex.Message;
                    return BadRequest(response);
                }
                try
                {
                    foreach (var importItem in importNewRoleList)
                    {
                        Role newRole = new Role();
                        newRole.RoleName = importItem.RoleName;
                        newRole.Title = importItem.Title;
                        newRole.CreatedDate = DateTime.Now;
                        newRole.ModifiedDate = DateTime.Now;
                        _db.Roles.Add(newRole);
                        _db.SaveChanges();
                        RolePermission newRolePermission = new RolePermission();
                        var role = _db.Roles.FirstOrDefault(u => u.RoleName.Trim() == importItem.RoleName.Trim());
                        if (role == null)
                        {
                            response.Message = response.Message + importItem.RoleName + ": is not exist in DB" + ", ";
                            return BadRequest(response);
                        }
                        else
                        {
                            newRolePermission.RoleId = role.RoleId;
                            newRolePermission.Syllabus = importItem.Syllabus;
                            newRolePermission.TrainingProgram = importItem.TrainingProgram;
                            newRolePermission.Class = importItem.Class;
                            newRolePermission.LearningMaterial = importItem.LearningMaterial;
                            newRolePermission.UserManagement = importItem.UserManagement;
                            _db.RolePermissions.Add(newRolePermission);
                            _db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while saving to db : " + ex.Message;
                    return BadRequest(response);
                }
                response.Message = "import success";
                response.StatusCode = 200;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while import : " + ex.Message;
                return BadRequest(response);
            }
        }

    }
}
