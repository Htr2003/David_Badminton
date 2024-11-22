using David_Badminton.Controllers;
using David_Badminton.IServices;
using David_Badminton.Models;
using David_Badminton.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DavidBadmintonContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOption => sqlOption.EnableRetryOnFailure(
        maxRetryCount:10,
        maxRetryDelay: TimeSpan.FromSeconds(10),
        errorNumbersToAdd:null)));

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICoachService, CoachService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<ILearningProcessService, LearningProcessService>();
builder.Services.AddScoped<IRollCallService, RollCallService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILearningScheduleService, LearningScheduleService>();
builder.Services.AddScoped<IRollCallCoachService, RollCallCoachService>();

//-------------------------------
builder.Services.AddScoped<ITuitionsService, TuitionsService>();
//-------------------------------

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        RequireExpirationTime = true,
    };
});
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "David Badminton API", Version = "v1" });

    // C?u hình cho phép s? d?ng Bearer Token trong Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Vui long nhap token theo dinh dang: Bearer {your token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
