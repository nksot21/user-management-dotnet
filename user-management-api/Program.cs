using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using user_management_api.Middlewares;
using user_management_api.Repositories;
using user_management_api.Services;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IMongoService, MongoService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRecordRequestService, RecordRequestService>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
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
app.UseMiddleware<RecordRequestMiddleware>();

app.MapControllers();



app.Run();
