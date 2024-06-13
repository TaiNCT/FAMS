using Microsoft.AspNetCore.Mvc;
using ScoreManagementAPI.Repository;
using ScoreManagementAPI.Interfaces;
using ScoreManagementAPI.DTO;
using Microsoft.IdentityModel.Tokens;

namespace ScoreManagementAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class certController : ControllerBase
    {

        private readonly ICertRepository _repository;

        public certController(ICertRepository repository)
        {
            this._repository = repository;
        }

        [HttpPost("update/other")]
        public async Task<IActionResult> UpdateOtherInformation(FormatJSONOthers data)
        {
            // Update the "Others" section of the student certificate

            await this._repository.UpdateOtherInfoStudent(data);
            return Ok(new
            {
                msg = "Successfully updated Others section."
            });
        }

        [HttpGet("get/major")]
        public async Task<IActionResult> GetMajor()
        {
            return Ok(await this._repository.GetMajor());
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateCertAwardAsync(FormatJSON data)
        {
            /*
             *
             * return a 200 response if everything works fine or :
             *      400 : missing fields or invalid data type being processed
             *      404 : Unable to find the ID of the student
             *      403 : Unauthorized to update the certificate
             *      500 : Unexpected error
             */
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!(await _repository.UpdateStudent(data)))
                return NotFound(new
                {
                    msg = $"No student with ID=\"{data.studentid}\" found."
                });
            
            return Ok(new
            {
                msg = $"Successfully updated record for student with ID=\"{data.studentid}\""
            });
        }


        [HttpPost("get")]
        [HttpGet("get")]
        [ProducesResponseType(200, Type = typeof(StudentClassesDTO))]
        public async Task<IActionResult> GetCertAwardAsync(CertRequest data)
        {
            /*
             * Should return a JSON with the following structure similar to StudentResp
             */

            if (data.studentid.Trim().IsNullOrEmpty())
                return BadRequest(new { msg = "Empty field detected." });

            var result = await _repository.GetStudentCert(data.studentid, data.classid);

            // Return back a message saying that the ID doesn't exist
            return result == null ? NotFound(new { msg = $"No student with ID=\"{data.studentid}\" found." }) : Ok(result);
        }

    }
}
