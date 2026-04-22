using PetCareMini.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PetCareMini.WebApi v1");
        options.RoutePrefix = string.Empty;
    });
}

// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();