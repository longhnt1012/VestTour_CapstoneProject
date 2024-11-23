using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Helpers;
using VestTour.Repository.Interface;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Repositories;
using VestTour.Service.Common;
using VestTour.Service.Implementation;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;
using VestTour.Service.Services;
using VestTour.Services;
using AutoMapper;
using VestTour.Repository.Configuration;
using VestTour.Repository.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
// Configure DbContext
builder.Services.AddDbContext<VestTourDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VestTourDb")));

// Add Swagger
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

// Add distributed memory cache and session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure CORS
builder.Services.AddCors(policy =>
    policy.AddDefaultPolicy(corsBuilder =>
        corsBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Register repositories
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
builder.Services.AddScoped<ITailorPartnerRepository, TailorPartnerRepository>();
builder.Services.AddScoped<IProcessingTailorRepository, ProcessingTailorRepository>();
builder.Services.AddScoped<IProductInStoreRepository, ProductInStoreRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();

// Register services
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ITokenService, TokenService>();
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
builder.Services.AddScoped<IProcessingTailorService, ProcessingTailorService>();
builder.Services.AddScoped<ITailorPartnerService, TailorPartnerService>();
builder.Services.AddScoped<IProductInStoreService, ProductInStoreService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
// Configure email settings
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IEmailHelper, EmailHelper>();

// Add memory cache and OTP service
builder.Services.AddMemoryCache();
builder.Services.AddScoped<OtpService>();

// Configure PayPal client
builder.Services.AddSingleton(x => new PaypalClient(
    builder.Configuration["PaypalOptions:AppId"],
    builder.Configuration["PaypalOptions:AppSecret"],
    builder.Configuration["PaypalOptions:Mode"]
));

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

app.UseSession();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
