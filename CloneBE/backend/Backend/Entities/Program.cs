using Entities.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adding a connection string
builder.Services.AddDbContext<FamsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Phase2Database")));


var app = builder.Build();
app.Run();
