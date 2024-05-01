using Erm.BusinessLayer;
using Erm.BusinessLayer.Mapper;
using Erm.BusinessLayer.Services;
using Erm.DataAccess;
using Erm.DataAccess.Repositories;
using Erm.PresentationLayer.WebApi;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<RiskProfileDtoProfile>();
});
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<BusinessProcessDtoProfile>();
});
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<RiskProfileDtoOutProfile>();
});
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<BusinessProcessDtoOutProfile>();
});

builder.Services.AddDbContext<ErmDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddStackExchangeRedisCache(options
    => options.Configuration = builder.Configuration.GetConnectionString("RedisConnection"));

builder.Services.AddScoped<RiskProfileRepository>();
builder.Services.AddScoped<RiskProfileRepositoryProxy>();
builder.Services.AddScoped<BusinessProcessRepository>();

builder.Services.AddScoped<IRiskProfileService, RiskProfileService>();
builder.Services.AddScoped<IBusinessProcessService, BusinessProcessService>();

builder.Services.AddScoped<IValidator<RiskProfileDto>, RiskProfileInfoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.OperationFilter<AddRequiredHeaderParametr>());
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<AuthMiddleware>();
app.MapControllers();
app.Run();

//myproject