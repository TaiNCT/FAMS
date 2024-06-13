using AutoMapper;
using ClassManagementAPI.Dto;
using ClassManagementAPI.Dto.SyllabusDTO;
using ClassManagementAPI.Interface;
using Entities.Models;
using Entities.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassManagementAPI.Repositories
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly FamsContext _context;

        public SyllabusRepository(FamsContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Syllabus>> GetSyllabiByTrainingProgramCode(string trainingProgramCode)
        {
            var syllabusList = await _context.TrainingProgramSyllabi
                            .Where(tps => tps.TrainingProgramCode == trainingProgramCode)
                            .Select(tps => tps.Syllabus).ToListAsync();

            var result = new PagedResult<Syllabus>
            {
                TotalCount = syllabusList.Count,
                Items = syllabusList
            };

            return result;
        }
        public async Task<PagedResult<Syllabus>> GetListSyllabus(string TrainingCode = null)
        {
            if (TrainingCode == null)
            {
                var syllabusList = await _context.Syllabi.ToListAsync();
                var result = new PagedResult<Syllabus>
                {
                    TotalCount = syllabusList.Count,
                    Items = syllabusList
                };

                return result;
            }
            else { 
            var syllabusList = await _context.Syllabi.ToListAsync();
            var syllabusIdByProgram = await _context.TrainingProgramSyllabi.Where(x => x.TrainingProgramCode == TrainingCode)
                .Select(tps => tps.Syllabus).ToListAsync();
            var listSyllabusFilter = syllabusList.Where(s => !syllabusIdByProgram.Contains(s)).ToList();

            var result = new PagedResult<Syllabus>
            {
                TotalCount = listSyllabusFilter.Count,
                Items = listSyllabusFilter
            };

                return result;
            }
        }

        public async Task<bool> AddProgramSyllabus(TrainingProgramSyllabus insertProgramSyllabus)
        {
            if (insertProgramSyllabus != null)
            {
                try
                {
                    var result = await _context.TrainingProgramSyllabi.AddAsync(insertProgramSyllabus);
                    return await _context.SaveChangesAsync() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Something went wrong: " + ex);
                }
            }
            return false;
        }

        public async Task<bool> CreateSyllabus(Syllabus syllabus)
        {
            if (syllabus != null)
            {
                try
                {
                    var result = await _context.Syllabi.AddAsync(syllabus);
                    return await _context.SaveChangesAsync() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error" + ex.Message);
                }
            }
            return false;
        }

        public async Task DeleteSyllabusCard(string TrainingProgramCode, string SyllabusID)
        {
            var deleteCard = await _context.TrainingProgramSyllabi
        .FirstOrDefaultAsync(tps => tps.TrainingProgramCode == TrainingProgramCode && tps.SyllabusId == SyllabusID) ?? throw new Exception("TrainingProgramSyllabus not found");
            _context.TrainingProgramSyllabi.Remove(deleteCard);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Syllabus>> GetSyllabiByTrainingProgramCode1(string trainingProgramCode)
        {
            return await _context.Syllabi
                .Include(s => s.TrainingProgramSyllabi)
                .Where(s => s.TrainingProgramSyllabi.Any(ts => ts.TrainingProgramCode == trainingProgramCode))
                .ToListAsync();
        }
    }
}
