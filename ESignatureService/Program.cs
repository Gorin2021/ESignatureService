using ESignatureService.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DependencyInjectionConfiguration.Configure(builder.Services, builder.Configuration);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{


    var dbContext = scope.ServiceProvider.GetRequiredService<SNSIDbContext>();

    //Применяем миграции (для продакшена)
    if (app.Environment.IsProduction())
    {
        await dbContext.Database.MigrateAsync();
    }
    //Гарантируем создание БД (для разработки)
    else
    {
        await dbContext.Database.EnsureCreatedAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}


app.UseAuthorization();

app.MapControllers();

app.Run();