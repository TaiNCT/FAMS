using ClosedXML.Excel;
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
    public class PermissionAPIController : Controller
    {
        private readonly FamsContext _db;
        public PermissionAPIController(FamsContext db)
        {
            _db = db;
        }

        [HttpGet("role-perms")]
        public ResponseDTO Get()
        {
            try
            {
                var roles = _db.RolePermissions.ToList();
                return new ResponseDTO { Result = roles, StatusCode = 200, IsSuccess = true, Message = "" };
            }
            catch
            {
                return new ResponseDTO { Result = false, StatusCode = 404, IsSuccess = false, Message = "Cannot get permission of role. Please try again." };
            }
        }

        [HttpGet("get-perms")]
        public ResponseDTO GetUserPerms() 
        {
            try
            {
                var usersPerms = _db.UserPermissions.Select(r => new { userPermsID = r.UserPermissionId, userPermsName = r.Name }).ToList();
                return new ResponseDTO { Result = usersPerms, StatusCode = 200, IsSuccess = true, Message = "Success" };
            }
            catch
            {
                return new ResponseDTO { Result = false, StatusCode = 404, IsSuccess = false, Message = "Cannot get role. Please try again." };
            }

        }

        [HttpPut("update-role-perms")]
        public ResponseDTO UpdateRolePermissions([FromBody] RolePermissionUpdateDTO updateRequest)
        {
            try
            {
                foreach (var rolePermissionDetails in updateRequest.RolePermissions)
                {
                    var roleExists = _db.Roles.Any(r => r.RoleId == rolePermissionDetails.RoleId);
                    if (!roleExists)
                    {
                        return new ResponseDTO { Result = false, StatusCode = 404, IsSuccess = false, Message = "Role does not exist for RoleId: " + rolePermissionDetails.RoleId };
                    }

                    var rolePermission = _db.RolePermissions.FirstOrDefault(rp => rp.RoleId == rolePermissionDetails.RoleId);
                    if (rolePermission == null)
                    {
                        return new ResponseDTO { Result = false, StatusCode = 404, IsSuccess = false, Message = "Role permission not found for RoleId: " + rolePermissionDetails.RoleId };
                    }
                    rolePermission.Syllabus = rolePermissionDetails.Syllabus;
                    rolePermission.TrainingProgram = rolePermissionDetails.TrainingProgram;
                    rolePermission.Class = rolePermissionDetails.Class;
                    rolePermission.LearningMaterial = rolePermissionDetails.LearningMaterial;
                    rolePermission.UserManagement = rolePermissionDetails.UserManagement;
                }

                _db.SaveChanges();

                return new ResponseDTO { StatusCode = 200, IsSuccess = true, Message = "Role permissions updated successfully for all provided roles." };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Result = false, StatusCode = 500, IsSuccess = false, Message = "An error occurred while updating role permissions: " + ex.Message };
            }
        }

        
        [HttpPost("import-user-perms")]
        public IActionResult ImportUser(IFormFile file)
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
                    IExcelDataReader reader = null;
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
                        return Ok(response);
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
                        return Ok(response);
                    }
                    reader.Close();
                    reader.Dispose();
                }

                List<ImportUserPermissionDTO> importList = new List<ImportUserPermissionDTO>();
                try
                {
                    int count = 0;
                    foreach (Dictionary<string, object> item in result)
                    {
                        if (count > 0)
                        {
                            ImportUserPermissionDTO importItem = new ImportUserPermissionDTO();
                            importItem.RoleName = item["Column0"].ToString().Trim();
                            importItem.Syllabus = item["Column1"].ToString().Trim();
                            importItem.TrainingProgram = item["Column2"].ToString().Trim();
                            importItem.Class = item["Column3"].ToString().Trim();
                            importItem.LearningMaterial = item["Column4"].ToString().Trim();
                            importItem.UserManagement = item["Column5"].ToString().Trim();
                            importList.Add(importItem);
                        }
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while add user to UserList: " + ex.Message;
                    return Ok(response);
                }
                List<RolePermissionsDetails> importPermissionList = new List<RolePermissionsDetails>();
                try
                {
                    int count = 0;
                    foreach (var importItem in importList)
                    {
                        count++;
                        // Verify trong file excel
                        RolePermissionsDetails importPermissionItem = new RolePermissionsDetails();
                        UserPermission? permission = new UserPermission();

                        if (importList.Select(u => u.RoleName.Trim()).ToList().Count(e => e == importItem.RoleName) > 1)
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Role name is dulicate in excel file" + ", ";
                        }

                        // Verify trong db
                        if (string.IsNullOrEmpty(importItem.RoleName))
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Role name is required" + ", ";
                        }
                        var role = _db.Roles.FirstOrDefault(u => u.RoleName.Trim() == importItem.RoleName.Trim());
                        if (role == null)
                        {
                            response.Message = response.Message + "Role No." + count + ": " + "Role name is not exist" + ", ";
                        }
                        else
                        {
                            importPermissionItem.RoleId = role.RoleId;
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
                            importPermissionItem.Syllabus = permission.UserPermissionId;
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
                            importPermissionItem.TrainingProgram = permission.UserPermissionId;
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
                            importPermissionItem.Class = permission.UserPermissionId;
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
                            importPermissionItem.LearningMaterial = permission.UserPermissionId;
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
                            importPermissionItem.UserManagement = permission.UserPermissionId;
                        }

                        importPermissionList.Add(importPermissionItem);
                    }
                    if (response.Message != "")
                    {
                        return BadRequest(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while verify : " + ex.Message;
                    return Ok(response);
                }
                try
                {
                    foreach (var rolePermissionDetails in importPermissionList)
                    {
                        var rolePermission = _db.RolePermissions.FirstOrDefault(rp => rp.RoleId == rolePermissionDetails.RoleId);
                        rolePermission.Syllabus = rolePermissionDetails.Syllabus;
                        rolePermission.TrainingProgram = rolePermissionDetails.TrainingProgram;
                        rolePermission.Class = rolePermissionDetails.Class;
                        rolePermission.LearningMaterial = rolePermissionDetails.LearningMaterial;
                        rolePermission.UserManagement = rolePermissionDetails.UserManagement;
                    }
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while saving to db : " + ex.Message;
                    return Ok(response);
                }
                response.Message = "import success";
                response.StatusCode = 200;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while import : " + ex.Message;
                return Ok(response);
            }
        }

        [HttpGet("export-user-perms")]
        public async Task<ActionResult> ExportUserPerms()
        {
            var response = new ResponseDTO
            {
                Message = "",
                IsSuccess = false,
                StatusCode = 500
            };
            try
            {
                var _empdata = GetEmpdate();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.AddWorksheet(_empdata, "ExportUserPermList");
                    using (MemoryStream ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportUserPermsList.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while exporting users: " + ex.Message;
                return BadRequest(response);
            }
        }

        [NonAction]
        private DataTable GetEmpdate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "ExportUserPermList";
            dt.Columns.Add("RoleName", typeof(string));
            dt.Columns.Add("Syllabus", typeof(string));
            dt.Columns.Add("TrainingProgram", typeof(string));
            dt.Columns.Add("Class", typeof(string));
            dt.Columns.Add("LearningMaterial", typeof(string));
            dt.Columns.Add("UserManagement", typeof(string));
            var userList = _db.RolePermissions.ToList();
            if (userList.Count > 0)
            {
                foreach (var item in userList)
                {
                    dt.Rows.Add(
                    GetRoleName(item.RoleId),
                    GetPermName(item.Syllabus),
                    GetPermName(item.TrainingProgram),
                    GetPermName(item.Class),
                    GetPermName(item.LearningMaterial),
                    GetPermName(item.UserManagement));
                }
            }
            return dt;
        }

        private string GetRoleName(string? roleId)
        {
            if (roleId != null)
            {
                Role currentRole = _db.Roles.FirstOrDefault(x => x.RoleId == roleId);
                return currentRole.RoleName;
            }
            return null;
        }

        private string GetPermName(string? permId)
        {
            if (permId != null)
            {
                UserPermission currentPerms = _db.UserPermissions.FirstOrDefault(x => x.UserPermissionId == permId);
                return currentPerms.Name;
            }
            return null;
        }
    }
}
