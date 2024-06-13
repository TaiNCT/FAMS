using System.Data;
using AutoMapper;
using Entities.Context;
using Nest;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReservationManagementAPI.Entities;
using ReservationManagementAPI.Entities.DTOs;
using ReservationManagementAPI.Contracts;

namespace ReservationManagementAPI.Repository;

public class ExportRepository : RepositoryBase<DataTable>, IExportRepository
{
    private readonly FamsContext _FamsContext;
    private readonly IElasticClient _client;

    public ExportRepository(FamsContext repositoryContext, IElasticClient  client)
            : base(repositoryContext)
    {
        _FamsContext = repositoryContext;
        _client = client;
    }

    public async Task<DataTable> exportReservedStudent(List<StudentReservedDTO> studentReservedList)
    {
        DataTable dt = new DataTable();
        dt.TableName = "Reserved list";
        dt.Columns.Add("FullName");
        dt.Columns.Add("Student Code");
        dt.Columns.Add("Gender");
        dt.Columns.Add("Birthday");
        dt.Columns.Add("Hometown");
        dt.Columns.Add("Class Name");
        dt.Columns.Add("Reseved Module");
        dt.Columns.Add("Reason");
        dt.Columns.Add("Start Date");
        dt.Columns.Add("End Date");

        try
        {
            if (!studentReservedList.IsNullOrEmpty())
            {
                studentReservedList.ForEach(item =>
                    {
                        dt.Rows.Add(item.StudentName, item.MutatableStudentId, item.Gender, item.Dob, item.Address, item.ClassName, item.ModuleName, item.Reason, item.StartDate, item.EndDate);
                    });
            }
        }
        catch (Exception e)
        {
            string error = e.ToString();


        }
        return dt;
    }
}