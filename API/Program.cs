using Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProjectContext>(i =>
{
    i.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("Store"));
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ProjectContext>();

builder.Services.AddScoped<AccountManager>();
builder.Services.AddScoped<RoleManager>();
builder.Services.AddScoped<ProductManager>();
builder.Services.AddScoped<VendorManager>();
builder.Services.AddScoped<CartManager>();
builder.Services.AddScoped<WishListManager>();
builder.Services.AddScoped<CategoryManeger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(); //wwwroot
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
