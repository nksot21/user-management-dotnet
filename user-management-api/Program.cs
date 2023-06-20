using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;
using user_management_api.Data;
using user_management_api.Middlewares;
using user_management_api.Repositories;
using user_management_api.Services;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRecordRequestService, RecordRequestService>();
builder.Services.AddScoped<ICacheService, CacheService>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCaching();


// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<IRedisContext, RedisContext>();

builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=UserManagementDB");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Middleware pipeline
app.UseMiddleware<RequestHandleMiddleware>();
app.UseMiddleware<ValidTokenVerifyMiddleware>();
app.UseMiddleware<RecordRequestMiddleware>();

app.MapControllers();

app.UseResponseCaching();

app.Run();
