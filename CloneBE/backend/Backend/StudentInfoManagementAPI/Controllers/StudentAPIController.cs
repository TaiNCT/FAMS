using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Microsoft.IdentityModel.Tokens;
using StudentInfoManagementAPI.DTO;
using StudentInfoManagementAPI.Service;
using System.Data;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static Nest.MachineLearningUsage;
using System.Net.NetworkInformation;

namespace StudentInfoManagementAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        public IStudentService_LamNS _studentService;
        public IUploadFileDL _uploadFileDL;
        public IStudentService_QuyNDC _studentServiceQuyNDC;
        public IEditStatusStudentInBatch _editStatusService;
        public IAddStudentClassService _addStudentClassService;
        public StudentAPIController(IStudentService_LamNS studentService, IUploadFileDL uploadFileDL, IStudentService_QuyNDC studentServiceQuyNDC, IEditStatusStudentInBatch editStatusService, IAddStudentClassService addStudentClassService)
        {
            _studentService = studentService;
            _uploadFileDL = uploadFileDL;
            _studentServiceQuyNDC = studentServiceQuyNDC;
            _editStatusService = editStatusService;
            _addStudentClassService = addStudentClassService;

        }

        [HttpGet]
        [Route("GetStudentsInclass")]
        public Task<ResponseDTO> Get(string classId, string? keyword, DateTime? dob, string? gender, string? status, string? sortBy = "id", string? sortOrder = "desc", int pageNumber = 1, int pageSize = 5)
        {
            return _studentService.GetListStudentInClass(classId, keyword, dob, gender, status, sortBy, sortOrder, pageNumber, pageSize);
        }


        [HttpGet]
        [Route("SelectAllStudentsByClass")]
        public ResponseDTO Get(string classId)
        {
            return _studentService.SelectAllStudentByClass(classId);
        }

        [HttpGet]
        [Route("GetListClassStudentNotIn")]
        public ResponseDTO GetListClass(string studentId)
        {
            return _studentService.getListClassStudentNotIn(studentId);
        }

        [HttpPost]
        [Route("uploadElasticData")]
        public async Task<IActionResult> uploadDocument()
        {
            var response = await _studentService.CreateDocumentAsync();
            return Ok(response);
        }

        [HttpPost]
        [Route("AddNewStudentClassInfor")]
        public Task<ResponseDTO> Put(StudentClassDTO request)
        {
            return _studentService.addNewClasInfor(request);
        }

        [HttpGet]
        [Route("GetListClassInfor")]
        public ResponseDTO getListClassInfor(string studentId)
        {
            return _studentService.getListClassInfor(studentId);
        }

        [HttpGet]
        [Route("ExportStudentList")]
        public ActionResult export(string classId, string? keyword, DateTime? dob, string? gender, string? status, string? sortBy = "id", string? sortOrder = "desc")
        {

            var _empdata = _studentService.exportStudentInclass(classId, keyword, dob, gender, status, sortBy, sortOrder);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_empdata, "Employee Records");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "sample.xlsx");
                }
            }
        }

        [HttpGet]
        [Route("ExportStudentSystemList")]
        public ActionResult exportSystem(string? keyword, string? dob, string? gender, string? status, string? sortBy = "id", string? sortOrder = "desc")
        {
            var _empdata = _studentServiceQuyNDC.exportStudentInSystem(keyword, dob, gender, status, sortBy, sortOrder);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_empdata, "Employee Records");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "sample.xlsx");
                }
            }
        }

        [HttpPost]
        [Route("UploadExcelFile")]
        public async Task<IActionResult> UploadExcelFile([FromForm] UploadExcelFileRequest request)
        {
            UploadExcelFileResponse response = new UploadExcelFileResponse();
            string Path = "C:\\UploadExcelFile\\" + request.File.FileName;

            if (string.IsNullOrEmpty(request.classId))
            {
                return BadRequest("ClassId is required.");
            }

            if (request.File == null)
            {
                return BadRequest("File is required.");
            }

            if (request.File.Length == 0)
            {
                return BadRequest("File is empty.");
            }

            try
            {
                using (FileStream stream = new FileStream(Path, FileMode.CreateNew))
                {
                    await request.File.CopyToAsync(stream);
                }

                bool hasDuplicate = await _uploadFileDL.HasDuplicates(request.File);
                if (!hasDuplicate)
                {
                    response = await _uploadFileDL.UploadXMLFile(request, Path);
                }
                else
                {
                    if (request.duplicateOption == "Allow")
                    {
                        await _uploadFileDL.UploadXMLFile(request, Path);
                        response.IsSuccess = true;
                        response.Message = "Data uploaded successfully with 'Allow' option.";
                        return Ok(response);
                    }
                    else if (request.duplicateOption == "Replace")
                    {
                        await _uploadFileDL.UploadOption(request, Path);
                        await _uploadFileDL.HandleReplaceOption(request.classId);
                        response.IsSuccess = true;
                        response.Message = "Data uploaded successfully with 'Replace' option.";
                        return Ok(response);
                    }
                    else if (request.duplicateOption == "Skip")
                    {
                        await _uploadFileDL.UploadOption(request, Path);
                        response.IsSuccess = true;
                        response.Message = "Data uploaded successfully with 'Skip' option.";
                        return Ok(response);
                    }

                    response.IsSuccess = false;
                    response.Message = "Data has been duplicated.";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                if (System.IO.File.Exists(Path))
                {
                    System.IO.File.Delete(Path);
                }
            }
            return Ok(response);
        }



        [HttpGet]
        [Route("GetAllStudents")]
        public Task<ResponseDTO> Get(string? keyword, string? dob, string? gender, string? status, string? sortBy = "id", string? sortOrder = "desc", int pageNumber = 1, int pageSize = 5)
        {
            return _studentServiceQuyNDC.GetListStudent(keyword, dob, gender, status, sortBy, sortOrder, pageNumber, pageSize);
        }

        [HttpDelete("delete/{studentId}")]
        public Task<ResponseDTO> DeleteStudent(string studentId)
        {
            return _studentServiceQuyNDC.ChangeStudentStatus(studentId);
        }

        [HttpGet]
        [Route("GetMajor/{majorId}")]
        public ResponseDTO GetMajor(string majorId)
        {
            return _studentServiceQuyNDC.GetMajor(majorId);
        }

        [HttpGet]
        [Route("GetAllMajor")]
        public ResponseDTO GetAllMajor()
        {
            return _studentServiceQuyNDC.GetAllMajor();
        }

        [HttpGet]
        [Route("GetAllClass")]
        public ResponseDTO GetAllClass()
        {
            return _studentServiceQuyNDC.GetAllClass();
        }

        [HttpGet]
        [Route("GetAllId")]
        public Task<ResponseDTO> GetAllId(string? keyword, string? dob, string? gender, string? status, string? sortBy, string? sortOrder, int pageNumber = 1, int pageSize = 5)
        {
            return _studentServiceQuyNDC.GetAllId(keyword, dob, gender, status, sortBy, sortOrder, pageNumber, pageSize);
        }

        [HttpPost("changeStatus/s/{studentId}/c/{classId}/status/{newStatus}")]
        public async Task<IActionResult> EditStudentStatusInBatch(string studentId, string newStatus, string classId)
        {
            var response = await _editStatusService.EditStudentStatusInBatch(studentId, newStatus, classId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("create-student")]
        public async Task<IActionResult> AddStudent(AddStudentDTO dto)
        {
            try
            {
                return Ok(await _addStudentClassService.Add(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("testUpdateStudent")]
        public Task<ResponseDTO> testUpdate(UpdateStudentDTO dto)
        {
            return _studentService.testUpdateStudent(dto);
        }
    }
}



