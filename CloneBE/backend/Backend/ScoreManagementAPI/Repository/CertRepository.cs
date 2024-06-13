using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Entities.Context;
using ScoreManagementAPI.Interfaces;
using Entities.Models;
using ScoreManagementAPI.DTO;
using MassTransit;
using Contracts.StudentManagement;

namespace ScoreManagementAPI.Repository
{
    public class CertRepository : ICertRepository
    {

        private readonly FamsContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        public CertRepository(FamsContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<int> UpdateOtherInfoStudent(FormatJSONOthers data)
        {
            // var student = await _context.Students.Where(sc => sc.StudentId == data.studentid.Trim()).FirstOrDefaultAsync();
            // Major major = await _context.Majors.Where(sc => sc.Name == data.major.Trim()).FirstOrDefaultAsync();

            // student.Gpa = data.gpa;
            // student.Recer = data.recer;
            // student.GraduatedDate = data.gradtime;
            // student.University = data.university;

            // // Check if the major exists
            // if(major != null)
            //     student.MajorId = major.MajorId;

            // await this._context.SaveChangesAsync();
            var otherStudentUpdated = new OtherStudentUpdated()
            {
                classid = data.classid,
                studentid = data.studentid,
                university = data.university,
                gpa = data.gpa,
                major = data.major,
                recer = data.recer,
                gradtime = data.gradtime
            };

            _publishEndpoint.Publish(otherStudentUpdated);

            return 0;
        }

        public async Task<object> GetMajor()
        {
            return await _context.Majors.ToListAsync();
        }

        public async Task<bool> UpdateStudent(FormatJSON data)
        {
            /*var student = await _context.Students.Where(sc => sc.StudentId == data.studentid).FirstOrDefaultAsync();
            // Updating the record
            student.Gender = data.gender.Trim();
            student.FullName = data.name.Trim();
            student.Dob = data.dob;

            student.Phone = data.phone.Trim();
            student.Email = data.email.Trim();
            student.Address = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"permanent_res",  data.permanentResidence.Trim()},
                {"location", data.location.Trim() }
            });
            student.MutatableStudentId = data.sid;


            // This will appear in the StudentClass table
            StudentClass sc = await _context.StudentClasses.Where(sc => sc.StudentId == data.studentid && sc.ClassId == data.classid).FirstOrDefaultAsync();

            sc.AttendingStatus = data.status;
            sc.CertificationStatus = data.certificateStatus ? 1 : 0;
            sc.CertificationDate = data.certificateDate; 

            // Save back the changes
            await _context.SaveChangesAsync();*/


            var generalStudentUpdated = new GeneralStudentUpdated()
            {
                classid = data.classid,
                studentid = data.studentid,
                sid = data.sid,
                name = data.name,
                gender = data.gender,
                dob = data.dob,
                status = data.status,
                phone = data.phone,
                email = data.email,
                permanentResidence = data.permanentResidence,
                certificateDate = data.certificateDate,
                certificateStatus = data.certificateStatus,
            };
            await _publishEndpoint.Publish(generalStudentUpdated);

            return true;
        }

        public async Task<StudentCertDTO> GetStudentCert(string id, string classid)
        // id = Student ID
        {
            // Extract the student out of the database first
            Student student = _context.Students.Where(s => s.StudentId == id).Select(s => s).FirstOrDefault();
            if (student == null) return null;

            // Grabbing the major
            Major major = await _context.Majors.Where(sc => sc.MajorId == student.MajorId).FirstOrDefaultAsync();

            // Getting status and stuffs from StudentCLass Entity
            StudentClass sc = await _context.StudentClasses.Where(sc => sc.StudentId == id && sc.ClassId == classid).FirstOrDefaultAsync();

            // Constructing an object
            return new StudentCertDTO
            {
                id = student.Id,
                sid = student.MutatableStudentId,
                name = student.FullName,
                dob = student.Dob,
                email = student.Email,
                phone = student.Phone,
                address = student.Address,
                gender = student.Gender,

                // Getting this from StudentClass entity
                status = sc.AttendingStatus,
                certificateDate = sc.CertificationDate,
                certificateStatus = sc.CertificationStatus == 1,

                // "Others" section
                university = student.University,
                gpa = student.Gpa,
                major = major != null ? major.Name : "",
                recer = student.Recer,
                gradtime = student.GraduatedDate,

            };
        }
    }
}
