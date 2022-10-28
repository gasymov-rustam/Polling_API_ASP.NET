using Microsoft.EntityFrameworkCore;
using Polling.Security.Dal;
using Polling.Security.Extensions;
using Polling.Security.Services;
using Polling.Security.TokenGenerator;
using Shared.Extensions;
using Shared.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SecurityContext>(options => options.UseNpgsql(builder.Configuration["Configuration:DefaultConnection"]));
//builder.Services.AddSingleton<IDistributedCache>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<JwtTokenGenerator>();
builder.Services.AddScoped<IClock, Clock>();
builder.Services.AddScoped<IJwtService, JwtService>();
//builder.Services.AddTransient<IAccessTokenService, AccessTokenService>();
//builder.Services.AddTransient<AccessTokenMiddleware>();
builder.Services.AddScoped<IPasswordManager, PasswordManager>();
builder.Services.AddScoped<IRefreshRepository, RefreshRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenStorage, TokenStorage>();
builder.Services.AddScoped<ISecurityRepository, SecurityRepository>();

builder.Services.AddAuthExtension(builder.Configuration);
builder.Services.AddCorsExtension();
builder.Services.AddAuthSwagger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<AccessTokenMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
