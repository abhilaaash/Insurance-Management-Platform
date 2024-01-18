global using EInsuranceProject.Data;
global using System.ComponentModel.DataAnnotations;
global using EInsuranceProject.MiddleWare;
global using EInsuranceProject.Model;
global using EInsuranceProject.Repository;
global using EInsuranceProject.Services;
global using FinalProjectInsurance.Service;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using System.Text.Json.Serialization;
global using EInsuranceProject.Validators;
global using EInsuranceProject.Wrapper;


namespace EInsuranceProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //Add Context 
            builder.Services.AddDbContext<InsuranceContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
            });
            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost4200",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod().WithExposedHeaders("*");
                    });
            });
            builder.Services.AddControllers();
            builder.Services.AddControllers()
       .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddTransient(typeof(IEntityRepository<>),typeof(EntityRepository<>));
            builder.Services.AddTransient<IPolicyRepository, PolicyRepository>();
            builder.Services.AddTransient<IAgentRepository, AgentRepository>();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<IPaymentRepository,PaymentRepository>();
            builder.Services.AddTransient<ICommissionRepository, CommissionRepository>();
            builder.Services.AddTransient<IInsuranceSchemeRepository, InsuranceSchemeRepository>();
            builder.Services.AddTransient<INominieeRepository, NominieeRepository>();
            builder.Services.AddTransient<IDocumentRepository, DocumentRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IAdminRepository, AdminRepository>();
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<IPlanSchemeDetailsRepository, PlanSchemeDetailsRepository>();
            builder.Services.AddTransient<ISchemeDetailRepository, SchemeDetailRepository>();
            builder.Services.AddTransient<IClaimRepository, ClaimRepository>();



            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IAgentService, AgentService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IInsurancePlanService, InsurancePlanService>();
            builder.Services.AddTransient<IInsuranceSchemesService, InsuranceSchemesService>();
            builder.Services.AddTransient<IPolicyService, PolicyService>();
            builder.Services.AddTransient<IDocumentService, DocumentService>();
            builder.Services.AddTransient<ISchemeDetailsService, SchemeDetailsService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IPaymentService, PaymentService>();
            builder.Services.AddTransient<IComplaintService, ComplaintService>();
            builder.Services.AddTransient<IClaimService, ClaimService>();
            builder.Services.AddTransient<ICommissionService, CommissionService>();
            builder.Services.AddTransient<IResponseService, ResponseService>();
            builder.Services.AddTransient<INominieService, NominieService>();
            builder.Services.AddTransient<IUserService, UserService>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                   };
               });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            app.UseMiddleware<ErrorHandlingMiddleWare>();
            app.UseCors("AllowLocalhost4200");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}