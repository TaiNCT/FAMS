using Contracts.UserManagement;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize]
    public class UserCalendarAPIController : Controller
    {
        
        private readonly FamsContext _db;
        private readonly UserResponse _response;

        public UserCalendarAPIController(FamsContext db)
        {
            _db = db;
            _response = new UserResponse();
        }

        [HttpGet("get-user-calendar")]
        public IActionResult getCarlendar(string Username)
        {
            ResponseDTO responseDTO = new ResponseDTO
            {
                IsSuccess = false,
                StatusCode = 500,
            };
            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    responseDTO.Message = "Username is empty";
                    return BadRequest(responseDTO);
                }
                var currentUser = _db.Users.FirstOrDefault(x => x.Username == Username);
                var classUsersList = _db.ClassUsers.Where(x => x.UserId == currentUser.UserId).ToList();
                List<UserCalendarDTO> userCalendarList = new List<UserCalendarDTO>();
                if (classUsersList != null)
                {
                    foreach (var itemList in classUsersList)
                    {
                        var currentClass = _db.Classes.FirstOrDefault(u => u.ClassId == itemList.ClassId);
                        if (currentClass != null)
                        {
                            /*if ((currentClass.ClassStatus.ToUpper() == "OPENNING") || (currentClass.ClassStatus.ToUpper() == "SCHEDULED"))
                            {*/
                                UserCalendarDTO currentUserCalendar = new UserCalendarDTO();
                                currentUserCalendar.ClassStatus = currentClass.ClassStatus;
                                currentUserCalendar.StartDate = currentClass.StartDate;
                                currentUserCalendar.EndDate = currentClass.EndDate;
                                currentUserCalendar.StartTime = currentClass.StartTime;
                                currentUserCalendar.EndTime = currentClass.EndTime;
                                currentUserCalendar.ClassName = currentClass.ClassName;
                                var currentFSU = _db.Fsus.FirstOrDefault(u => u.FsuId == currentClass.FsuId);
                                if (currentFSU == null)
                                {
                                    responseDTO.Message = currentClass.ClassName + "with ID" + currentClass.ClassId + "FSU is empty";
                                    return BadRequest(responseDTO);
                                }
                                currentUserCalendar.FsuName = currentFSU.Name;
                                var currentLocation = _db.Locations.FirstOrDefault(u => u.LocationId == currentClass.LocationId);
                                if (currentLocation == null)
                                {
                                    responseDTO.Message = currentClass.ClassName + "with ID" + currentClass.LocationId + " is empty";
                                    return BadRequest(responseDTO);
                                }
                                currentUserCalendar.Address = currentLocation.Address;
                                userCalendarList.Add(currentUserCalendar);
                            //}
                        }
                    }
                }
                responseDTO.Result = userCalendarList;
                responseDTO.IsSuccess = true;
                responseDTO.StatusCode = 200;
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO.Message = "Errors while get carlendar " + ex.Message;
                return BadRequest(responseDTO);
            }
        }
    }
}
