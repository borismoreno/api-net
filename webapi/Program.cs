using System.Text;
using Application.Abstractions;
using Application.Interfaces;
using Application.Providers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
  options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));

var mongo = builder.Configuration["mongourl"].ToString();
var database = builder.Configuration["database"].ToString();

builder.Services.AddSingleton(serviceProvider =>
{
  var mongoClient = new MongoClient(mongo);
  return mongoClient.GetDatabase(database);
});

builder.Services.AddSingleton<IUserProvider, UserProvider>();
builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

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
