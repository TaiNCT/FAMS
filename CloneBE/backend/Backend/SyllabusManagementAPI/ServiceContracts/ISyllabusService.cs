using Microsoft.AspNetCore.Mvc;
﻿using SyllabusManagementAPI.Entities.DTO;
using SyllabusManagementAPI.Entities.DTO.Syllabus;
using SyllabusManagementAPI.Entities.Parameters;

namespace SyllabusManagementAPI.ServiceContracts
{
    public interface ISyllabusService
    {
        Task<ResponseDTO> GetAllSyllabusAsync(SyllabusParameters syllabusParameters);

        Task<ResponseDTO> GetSyllabusByIdAsync(string syllabusId);

        Task<ResponseDTO> SortSyllabusAsync(SyllabusParameters syllabusParameters, string Sortby);

        Task<ResponseDTO> FilterByDateSyllabusAsync(SyllabusParameters syllabusParameters, DateTime from, DateTime to);

        Task<SyllabusDTO> CreateSyllabusAsync(SyllabusForCreationDTO syllabus, bool isDraft);

        Task<SyllabusDTO> UpdateSyllabusAsync(SyllabusForUpdateDTO syllabus);

		Task<ResponseDTO> DuplicateSyllabusAsync(DuplicateSyllabusRequest request);

		Task DeleteSyllabusAsync(string syllabusId);

        //void UpdateSyllabus(SyllabusForUpdateDTO syllabus);

        Task<ResponseDTO> GetHeaderAsync(string syllabusId);

        Task<ResponseDTO> GetGeneralAsync(string syllabusId);

		Task<ResponseDTO> GetDeliveryTypePercentages(string syllabusId);

		Task ActivateDeactivateSyllabus(string syllabusId, [FromBody] bool activate);

        Task<SyllabusDTO> ImportSyllabus(IFormFile file);

        Task<SyllabusDTO> CreateSyllabusImportAsync(SyllabusForCreationDTO syllabus);

        Task<(SyllabusDTO, bool)> HandleDuplicate([FromForm] DuplicateHandlingDTO model);

        Task<ResponseDTO> SearchAsync(string keywords, SyllabusParameters syllabusParameters);
	}
}
