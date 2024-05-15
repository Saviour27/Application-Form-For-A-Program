using Microsoft.Azure.Cosmos;
using Program_Application_Form.Abstractions;
using Program_Application_Form.Repositories;
using Program_Application_Form.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Cosmos DB configuration
var cosmosEndpoint = builder.Configuration["CosmosDb:AccountEndpoint"];
var cosmosKey = builder.Configuration["CosmosDb:AccountKey"];
var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
builder.Services.AddSingleton(cosmosClient);

// Register repositories
builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

// Register services
builder.Services.AddTransient<ICandidateService, CandidateService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
