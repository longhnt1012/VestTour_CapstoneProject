using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Mapper;
using VestTour.Repository.Implementation;
using VestTour.Repository.Interface;
using VestTour.Service.Interface;
using VestTour.Services;
using VestTour.Repository.Data;
using AutoMapper;
using System.Text;
using Microsoft.OpenApi.Models;
using VestTour.Service.Interfaces;
using VestTour.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using VestTour.Service.Common;
using VestTour.Service.Implementation;

using VestTour.Repository.Interfaces;
using VestTour.Repository.Repositories;

using VestTour.Repository.Configuration;
using VestTour.Repository.Helpers;
var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SEVestTour API",
        Version = "v1"
    });

    // Add Bearer authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Register HTTP client for GHTK service
builder.Services.AddHttpClient<ShippingService>();
builder.Services.AddHttpContextAccessor();


// Register necessary services
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IShipperPartnerRepository, ShipperPartnerRepository>();
builder.Services.AddScoped<IStyleRepository, StyleRepository>();
builder.Services.AddScoped<IStyleOptionRepository, StyleOptionRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ILiningRepository, LiningRepository>();
builder.Services.AddScoped<IFabricRepository, FabricRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBankingAccountRepository, BankingAccountRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IAddCartRepository, AddCartRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

//builder.Services.AddScoped<IProductStyleOptionervice, ProductStyleOptionervice>();
builder.Services.AddScoped<ITokenService, TokenService>();
//builder.Services.AddScoped<IVerificationService, VerificationService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAddCartService, AddCartService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFabricService, FabricService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStyleOptionService, StyleOptionService>();
builder.Services.AddScoped<IStyleService, StyleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShipperPartnerService, ShipperPartnerService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IEmailHelper, EmailHelper>();

// Add IMemoryCache 
builder.Services.AddMemoryCache(); // For in-memory cache
builder.Services.AddScoped<OtpService>();
// Add PayPal Client service
builder.Services.AddSingleton(x => new PaypalClient(
    builder.Configuration["PaypalOptions:AppId"],
    builder.Configuration["PaypalOptions:AppSecret"],
    builder.Configuration["PaypalOptions:Mode"]
));

// Add CORS policy
builder.Services.AddCors(co => co.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Configure DbContext
builder.Services.AddDbContext<VestTourDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VestTourDB")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

// JWT Configuration
var jwtSettings = new JWTSettings();
builder.Configuration.GetSection("Jwt").Bind(jwtSettings);
if (string.IsNullOrEmpty(jwtSettings.JWTSecretKey))
{
    throw new ArgumentNullException(nameof(jwtSettings.JWTSecretKey), "JWT secret key cannot be null or empty.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JWTSecretKey))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
