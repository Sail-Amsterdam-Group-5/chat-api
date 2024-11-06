// Program.cs
using Microsoft.OpenApi.Models;
using sailchat.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SAIL Chat API",
        Version = "v1",
        Description = "API for SAIL Amsterdam event chat system"
    });

    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "X-API-Key",
        In = ParameterLocation.Header,
        Description = "API Key authentication"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
            },
            new string[] { }
        }
    });
});

// Register services
builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddSingleton<IChatService, ChatService>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Define a constant for the API key
const string API_KEY = "SAIL2025"; // Change this to any value you want

// Add API Key middleware
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("API Key was not provided");
        return;
    }

    // Compare with the defined API key
    if (!API_KEY.Equals(extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid API Key");
        return;
    }

    await next();
});

app.Run();