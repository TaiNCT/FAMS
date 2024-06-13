using Microsoft.AspNetCore.Mvc;
using Entities.Context;
using SyllabusManagementAPI.ServiceContracts;

namespace SyllabusManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewSyllabusController : ControllerBase
    {
        private IServiceWrapper _service;
        // private readonly FamsContext _context;
        //private ResponseDTO _response;

        public ViewSyllabusController(IServiceWrapper service)
        {
            _service = service;
        }

        [HttpGet("/header")]
        public async Task<IActionResult> GetHeader(string syllabusId)
        {
            //var syllabus = _context.Syllabi
            //	.FirstOrDefault(s => s.SyllabusId == syllabusId)
            var syllabus = await _service.SyllabusService.GetHeaderAsync(syllabusId);
            //if (syllabus == null)
            //{
            //	return NotFound();
            //}

            //var headerResult = new ViewDTO.HeaderViewModel
            //{
            //	TopicCode = syllabus.TopicCode,
            //	TopicName = syllabus.TopicName,
            //	Version = syllabus.Version,
            //	ModifiedBy = syllabus.ModifiedBy,
            //	ModifiedDate = (DateTime)syllabus.ModifiedDate
            //};
            //_response.StatusCode = StatusCodes.Status200OK;
            //_response.Title = "Success";
            //_response.Result = headerResult;
            return Ok(syllabus);
        }

        [HttpGet("/general")]
        public async Task<IActionResult> GetGeneral(string syllabusId)
        {
            var syllabus = await _service.SyllabusService.GetGeneralAsync(syllabusId);
            //var syllabus = _context.Syllabi
            //    .Include(s => s.SyllabusDays)
            //    .ThenInclude(sd => sd.SyllabusUnits)
            //    .ThenInclude(su => su.UnitChapters)
            //    .ThenInclude(uc => uc.OutputStandard)
            //    .FirstOrDefault(s => s.SyllabusId == syllabusId);

            //if (syllabus == null)
            //{
            //    return NotFound();
            //}

            //var generalResult = new ViewDTO.GeneralViewModel
            //{
            //    AttendeeNumber = syllabus.AttendeeNumber,
            //    Level = syllabus.Level,
            //    TechnicalRequirement = syllabus.TechnicalRequirement,
            //    CourseObjective = syllabus.CourseObjective,
            //    Days = syllabus.Days,
            //    Hours = syllabus.Hours,
            //    OutputStandardCode = syllabus.SyllabusDays.SelectMany(sd => sd
            //        .SyllabusUnits.SelectMany(su => su
            //        .UnitChapters)).Select(uc => uc
            //        .OutputStandard.Code).FirstOrDefault()
            //};
            //_response.StatusCode = StatusCodes.Status200OK;
            //_response.Title = "Success";
            //_response.Result = generalResult;
            return Ok(syllabus);
        }

        [HttpGet("/outline")]
        public async Task<IActionResult> GetOutline(string syllabusId)
        {
            // var syllabusDays = _context.SyllabusDays
            //    .Where(sd => sd.Syllabus.SyllabusId == syllabusId)
            //    .Include(sd => sd.SyllabusUnits)
            //    .ThenInclude(su => su.UnitChapters)
            //    .ThenInclude(uc => uc.OutputStandard)
            //    .ToList();

            // var outlineResult = syllabusDays.Select(sd => new SyllabusDayViewModel
            // {
            //     DayNo = sd.DayNo.Value,
            //     SyllabusUnits = sd.SyllabusUnits.Select(su => new SyllabusUnitViewModel
            //     {
            //         UnitNo = su.UnitNo,
            //         Name = su.Name,
            //         Duration = su.Duration,
            //         UnitChapters = su.UnitChapters.Select(uc => new UnitChapterViewModel
            //         {
            //             Name = uc.Name,
            //             Duration = uc.Duration,
            //             IsOnline = uc.IsOnline,
            //             OutputStandardCode = uc.OutputStandard.Code
            //         }).ToList()
            //     }).ToList()
            // }).ToList();

            //_response.StatusCode = StatusCodes.Status200OK;
            //_response.Title = "Success";
            //_response.Result = outlineResult;

            var outlineResult = await _service.SyllabusDayService.GetSyllabusDaysOutlineBySyllabusIdAsync(syllabusId);
            return Ok(outlineResult);
        }

        [HttpGet("/other")]
        public async Task<IActionResult> GetOther(string syllabusId)
        {
            var assessmentScheme = await _service.AssessmentSchemeService.GetAssessmentSchemeByIdAsync(syllabusId);
            //var assessmentScheme = _context.AssessmentSchemes
            //    .Include(a => a.Syllabus)
            //    .FirstOrDefault(a => a.SyllabusId == syllabusId);

            //if (assessmentScheme == null)
            //{
            //    return NotFound();
            //}

            //var assessmentResult = new ViewDTO.AssessmentSchemeViewModel
            //{
            //    Assignment = assessmentScheme.Assignment,
            //    FinalPractice = assessmentScheme.FinalPractice,
            //    Final = assessmentScheme.Final,
            //    FinalTheory = assessmentScheme.FinalTheory,
            //    Gpa = assessmentScheme.Gpa,
            //    Quiz = assessmentScheme.Quiz
            //};
            //_response.StatusCode = StatusCodes.Status200OK;
            //_response.Title = "Success";
            //_response.Result = assessmentResult;
            return Ok(assessmentScheme);
        }

        [HttpGet("/time-allocation")]
        public async Task<IActionResult> GetDeliveryTypePercentages(string syllabusId)
        {
            try
            {
                var percentages = await _service.SyllabusService.GetDeliveryTypePercentages(syllabusId);
                return Ok(percentages);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
