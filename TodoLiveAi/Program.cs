using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoLiveAi.Core;
using TodoLiveAi.Infrastructure.Data;
using TodoLiveAi.Infrastructure.Repositories;
using TodoLiveAi.Service;
using AutoMapper;
using TodoLiveAi.Web.MappedModels;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("TodoLiveAi.Infrastructure")));

builder.Services.AddScoped<UserManager<AppUser>>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddMvc();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoLiveAi", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    //app.UseMigrationsEndPoint();


}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("/swagger/v1/Index.html", "gpt");
    //c.SwaggerEndpoint("/swagger/v1/Index.html", "Todos");
    //c.SwaggerEndpoint("./v1/swagger.json", "Todos");
    c.SwaggerEndpoint("/swagger/v1/Index.html", "TodoLiveAi");
});

app.Run();
