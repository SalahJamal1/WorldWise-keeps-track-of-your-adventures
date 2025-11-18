using System.Text;
using MappyApplication.Configuration;
using MappyApplication.Contract;
using MappyApplication.Data;
using MappyApplication.Exceptions;
using MappyApplication.MiddleWare;
using MappyApplication.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lg) => lg.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MappyDBContext>(options =>
{
    if (string.IsNullOrEmpty(connectionString))
        throw new Exception("You need to configure the connection string in appsettings.json");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddIdentityCore<User>(option =>
    {
        option.Password.RequireDigit = false;
        option.Password.RequireLowercase = false;
        option.Password.RequireUppercase = false;
        option.Password.RequireNonAlphanumeric = false;
        option.Password.RequiredLength = 6;
        option.User.RequireUniqueEmail = true;
    }).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MappyDBContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", opt =>
        opt.WithOrigins("http://localhost:5173", "http://localhost:3000", "http://localhost:8080").AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"] ?? string.Empty)),
        ValidIssuer = builder.Configuration["Jwt:issuer"],
        ValidAudience = builder.Configuration["Jwt:audience"]
    };
    var message = "you aren't authorized to access this resource.";
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async Task (ctx) =>
        {
            ctx.HandleResponse();
            await AppErrorResponse.HandelException(ctx.HttpContext, StatusCodes.Status401Unauthorized, message);
        },
        OnForbidden = async Task (ctx) =>
        {
            await AppErrorResponse.HandelException(ctx.HttpContext, StatusCodes.Status401Unauthorized, message);
        },
        OnMessageReceived = ctx =>
        {
            var auth = ctx.Request.Headers["Authorization"].SingleOrDefault();
            if (auth != null && auth.StartsWith("Bearer ")) ctx.Token = auth.Substring(7);

            return Task.CompletedTask;
        },
        OnTokenValidated = ctx =>
        {
            var tokenRepository = ctx.HttpContext.RequestServices.GetRequiredService<ITokenRepository>();

            // JWT token extracted already
            var jwt = ctx.SecurityToken as JsonWebToken;
            if (jwt == null)
            {
                ctx.Fail("Invalid token");
                return Task.CompletedTask;
            }

            var accessToken = jwt.EncodedToken;

            if (string.IsNullOrEmpty(accessToken))
            {
                ctx.Fail("Invalid token");
                return Task.CompletedTask;
            }

            var token = tokenRepository.FindByAccessToken(accessToken);

            if (token == null || token.Expired || token.Revoked) ctx.Fail("Invalid token");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);
builder.Services.AddScoped(typeof(IGenricRepository<>), typeof(GenricRepository<>));
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IWorkoutsRepository, WorkoutsRepository>();
builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MappyDBContext>();
    db.Database.EnsureCreated();
}
app.UseHttpsRedirection();
app.UseCors("AllowAll");


app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleWare>();
app.MapControllers();
app.Use(async (ctx, next) =>
{
    await next(ctx);
    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
        await AppErrorResponse.HandelException(ctx, 404, $"we couldn't find the url {ctx.Request.Path}");
});

app.Run();