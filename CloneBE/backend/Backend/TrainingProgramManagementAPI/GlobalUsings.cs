/* Import all libraries, Class,... for all assembly workplace*/

/*System*/
global using System.Text;
global using System.Text.Json;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text.Json.Serialization;
global using System.Globalization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Authorization;

/*Extension libraries*/
global using AutoMapper;

/*Application*/
global using TrainingProgramManagementAPI.Utils;
global using TrainingProgramManagementAPI.Mappings;
global using TrainingProgramManagementAPI.Payloads;
global using TrainingProgramManagementAPI.Payloads.Responses;
global using TrainingProgramManagementAPI.Payloads.Requests;
global using TrainingProgramManagementAPI.Data;
global using TrainingProgramManagementAPI.Extensions;
global using TrainingProgramManagementAPI.DTOs;
// global using Entities.Context;
global using TrainingProgramManagementAPI.Enums;
global using TrainingProgramManagementAPI.Validations;

/*Authentication*/
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;

/*Validation*/
global using FluentValidation;
global using FluentValidation.Results;

