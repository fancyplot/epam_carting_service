using Asp.Versioning;
using CartingService.Domain.AutoMapper;
using CartingService.Domain.Interfaces.V1;
using CartingService.Domain.Models.V1;
using CartingService.Infrastructure.AutoMapper;
using CartingService.Infrastructure.Repositories.V1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Cart>());
builder.Services.AddAutoMapper(typeof(AutoMapperProfileInfrastructure), typeof(AutoMapperProfileDomain));
builder.Services.AddScoped<ICartsRepository, CartsRepository>();
builder.Services.AddScoped<CartingService.Domain.Interfaces.V2.ICartsRepository, CartingService.Infrastructure.Repositories.V2.CartsRepository>();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("x-api-version"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
