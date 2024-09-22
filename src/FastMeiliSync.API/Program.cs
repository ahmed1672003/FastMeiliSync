namespace FastMeiliSync.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpClient();
        builder.Services.AddCarter();
        builder.Services.AddCors(cfg =>
        {
            cfg.AddPolicy(
                "All",
                options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                }
            );
        });
        builder
            .Services.RegisterApplicationDepenedncies(builder.Configuration)
            .RegiserInfrastructureDependencies(builder.Configuration);
        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidAudience = JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(JwtSettings.Secret)
                    ),
                };
            });
        builder.Services.AddScoped<GlobalExceptionHandler>();
        builder.Services.AddAuthorization();

        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            options.SwaggerDoc(
                "v1",
                new()
                {
                    Title = "fast meili sync",
                    Version = "1.0.0",
                    Contact = new OpenApiContact()
                    {
                        Email = "ahmed.adel.elsayed.ali.basha@gmail.com",
                        Name = "ahmed adel"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Apache-2.0 License",
                        Url = new("http://www.apache.org/licenses/")
                    },
                }
            );
            options.EnableAnnotations();
            options.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                new()
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = JwtBearerDefaults.AuthenticationScheme,
                }
            );
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
        });

        var app = builder.Build();
        app.UseCors("All");
        app.UseMiddleware<GlobalExceptionHandler>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapCarter();

        await app.RunAsync();
    }
}
