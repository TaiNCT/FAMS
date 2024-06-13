using Entities.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace ScoreManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class templateController : ControllerBase
    {
        private readonly FamsContext _context;

        public templateController(FamsContext context)
        {
            _context = context;
        }

        [HttpGet("get/{id}")]
        public ActionResult getTemplateById(string id)
        {

            var ret = _context.EmailSends.FirstOrDefault(sc=>sc.TemplateId == id);
            string content;

            if (ret == null)
            {
                content = JsonConvert.SerializeObject(new
                {
                    from = "",
                    isactive = false,
                    subject = "",
                    body = "",
                    isdearname = false,
                    quick = false,
                    attendscore = false,
                    isaudit = false,
                    ispracticescore = false,
                    isgpa = false,
                    finalstatus = false

                });
                _context.EmailSends.Add(new Entities.Models.EmailSend
                {
                    ReceiverType = 0,
                    SenderId = null,
                    TemplateId = id,
                    SendDate = DateTime.Now,
                    Content = content
                });
                _context.SaveChanges();
            }
            else
                content = ret.Content;

            return Ok(new
            {
                content = content
            }); 
        }

    }
}
