using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Repositories;
using LibraryManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server connection (Linux compatible)
builder.Services.AddDbContext<AppDbContext>(options =>
   // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
     options.UseInMemoryDatabase("LibraryManagementDb"));

// Register repositories (scoped)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<BookRepository>(); // for custom methods
builder.Services.AddScoped<MemberRepository>();
builder.Services.AddScoped<BorrowingRepository>();

// Register services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IBorrowingService, BorrowingService>();

var app = builder.Build();

app.UseDefaultFiles();   // serves index.html when root is requested
app.UseStaticFiles();    // enables static file serving

// Enable Swagger in development (or always for testing)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

// Apply migrations automatically at startup (optional)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
