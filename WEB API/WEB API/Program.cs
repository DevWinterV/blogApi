using AutoMapper;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WEB_API.Models;
using WEB_API.Services.AuthService;
using WEB_API.Services.CommentsService;
using WEB_API.Services.ContactsService;
using WEB_API.Services.FileUpload;
using WEB_API.Services.ImageNews;
using WEB_API.Services.PostService;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Connect DB
        builder.Services.AddDbContext<ContactContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("RangDongDb"))
        );
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                  .AddEntityFrameworkStores<ContactContext>()
                  .AddDefaultTokenProviders();
        /*
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = "Code2night", 
                 ValidAudience = "Public",   
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9NNUmLnvq9+fYGUMiqwZZphV+MUKJozTG4B8qnvmfoI="))
             };
         });*/
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetSection("Jwt:isuser").Value,
                ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:key").Value))
            };
        });


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddTransient<FirebaseStorageService>(_ =>
                   new FirebaseStorageService("my-project-2024-7d367.appspot.com"));
 

        builder.Services.AddScoped<IImagenewsService, ImagenewService>();

        builder.Services.AddScoped<IPostService, PostService>();

        builder.Services.AddScoped<IAuthenService, AuthenService>();

        builder.Services.AddScoped<IContactService, ContactService>();

        builder.Services.AddScoped<ICommentService, CommentService>();


        // Cấu hình CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000") // Chỉ cho phép các yêu cầu từ origin localhost:3000 này
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
       // app.UseStaticFiles(); // This enables the serving of static files
        // Sử dụng Cors
        app.UseCors("AllowSpecificOrigin");
        app.UseAuthentication();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}