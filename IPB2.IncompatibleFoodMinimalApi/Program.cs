using IPB2.IncompatibleFoodApi.Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // If your AppDbContext already has OnConfiguring, this can be left empty.
    // But recommended is to configure connection string here.
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddScoped<ITestService, PLMService>();
builder.Services.AddScoped<ITestService, GTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/IncompatibleFood", async (AppDbContext db, int pageNo, int pageSize) =>
{
    var lst = await db.TblIncompatibleFoods
        .Skip((pageNo - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return Results.Ok(lst);
})
.WithName("GetIncompatibleFood")
.WithOpenApi();

app.MapGet("/api/IncompatibleFood/List", async (AppDbContext db, int pageNo = 1, int pageSize = 10) =>
{
    if (pageNo <= 0) pageNo = 1;
    if (pageSize <= 0) pageSize = 10;

    var query = db.TblIncompatibleFoods
        .AsNoTracking()
        .OrderBy(x => x.Id);

    var totalCount = await query.CountAsync();

    var data = await query
        .Skip((pageNo - 1) * pageSize)
        .Take(pageSize)
        .Select(x => new
        {
            x.Id,
            x.FoodA,
            x.FoodB,
            x.Description
        })
        .ToListAsync();

    var response = new
    {
        PageNo = pageNo,
        PageSize = pageSize,
        TotalCount = totalCount,
        PageCount = (int)Math.Ceiling(totalCount / (double)pageSize),
        Data = data
    };

    return Results.Ok(response);
})
.WithName("GetIncompatibleFoodList")
.WithOpenApi();

app.MapGet("/api/IncompatibleFood/CategoryList", async (AppDbContext db) =>
{
    var categories = await db.TblIncompatibleFoods
        .AsNoTracking()
        .Where(x => !string.IsNullOrEmpty(x.Description))
        .Select(x => x.Description!.Trim())
        .Distinct()
        .OrderBy(x => x)
        .ToListAsync();

    return Results.Ok(categories);
})
.WithName("GetCategoryList")
.WithOpenApi();

app.MapGet("/api/IncompatibleFood/Search", async (
    AppDbContext db,
    string? keyword = null,
    string? category = null,
    int pageNo = 1,
    int pageSize = 10) =>
{
    if (pageNo <= 0) pageNo = 1;
    if (pageSize <= 0) pageSize = 10;

    var query = db.TblIncompatibleFoods
        .AsNoTracking()
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(keyword))
    {
        keyword = keyword.Trim();

        query = query.Where(x =>
            x.FoodA!.Contains(keyword) ||
            x.FoodB!.Contains(keyword) ||
            x.Description!.Contains(keyword));
    }

    if (!string.IsNullOrWhiteSpace(category))
    {
        category = category.Trim();

        query = query.Where(x => x.Description != null && x.Description.Trim() == category);
    }

    query = query.OrderBy(x => x.Id);

    var totalCount = await query.CountAsync();

    var data = await query
        .Skip((pageNo - 1) * pageSize)
        .Take(pageSize)
        .Select(x => new
        {
            x.Id,
            x.FoodA,
            x.FoodB,
            x.Description
        })
        .ToListAsync();

    var response = new
    {
        PageNo = pageNo,
        PageSize = pageSize,
        TotalCount = totalCount,
        PageCount = (int)Math.Ceiling(totalCount / (double)pageSize),
        Data = data
    };

    return Results.Ok(response);
})
.WithName("SearchIncompatibleFood")
.WithOpenApi();

app.Run();

public interface ITestService
{
    void Test();
}

public class PLMService : ITestService
{
    public void Test()
    {

    }
}

public class GTService : ITestService
{
    public void Test()
    {

    }
}

public class AccountController
{
    private readonly ITestService _testService;
    public AccountController(ITestService testService)
    {
        _testService = testService;
    }
    public void Test()
    {
        _testService.Test();
    }
}