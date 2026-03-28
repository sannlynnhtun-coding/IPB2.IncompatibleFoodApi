using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPB2.IncompatibleFoodApi.Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;

namespace PB2.IncompatibleFood.Domain.Features.IncompatibleFood;

public class IncompatibleFoodService
{
    private readonly AppDbContext _db;

    public IncompatibleFoodService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IncompatibleFoodListResponse> GetListAsync(IncompatibleFoodListRequest request)
    {
        if (request.PageNo <= 0) request.PageNo = 1;
        if (request.PageSize <= 0) request.PageSize = 10;

        var query = _db.TblIncompatibleFoods
            .AsNoTracking()
            .OrderBy(x => x.Id);

        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((request.PageNo - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new IncompatibleFoodDto
            {
                Id = x.Id,
                FoodA = x.FoodA,
                FoodB = x.FoodB,
                Description = x.Description
            })
            .ToListAsync();

        return new IncompatibleFoodListResponse
        {
            PageNo = request.PageNo,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            PageCount = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            Data = data
        };
    }

    public async Task<IncompatibleFoodSearchResponse> SearchAsync(IncompatibleFoodSearchRequest request)
    {
        if (request.PageNo <= 0) request.PageNo = 1;
        if (request.PageSize <= 0) request.PageSize = 10;

        var query = _db.TblIncompatibleFoods
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var keyword = request.Keyword.Trim();
            query = query.Where(x =>
                x.FoodA.Contains(keyword) ||
                x.FoodB.Contains(keyword) ||
                x.Description.Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            var category = request.Category.Trim();
            query = query.Where(x => x.Description.Trim() == category);
        }

        query = query.OrderBy(x => x.Id);

        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((request.PageNo - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new IncompatibleFoodDto
            {
                Id = x.Id,
                FoodA = x.FoodA,
                FoodB = x.FoodB,
                Description = x.Description
            })
            .ToListAsync();

        return new IncompatibleFoodSearchResponse
        {
            PageNo = request.PageNo,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            PageCount = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            Data = data
        };
    }

    public async Task<IncompatibleFoodCategoryListResponse> GetCategoriesAsync(IncompatibleFoodCategoryListRequest request)
    {
        var categories = await _db.TblIncompatibleFoods
            .AsNoTracking()
            .Where(x => !string.IsNullOrEmpty(x.Description))
            .Select(x => x.Description.Trim())
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();

        return new IncompatibleFoodCategoryListResponse
        {
            Categories = categories
        };
    }

    public async Task<CreateIncompatibleFoodResponse> CreateAsync(CreateIncompatibleFoodRequest request)
    {
        var item = new TblIncompatibleFood
        {
            FoodA = request.FoodA,
            FoodB = request.FoodB,
            Description = request.Description
        };

        _db.TblIncompatibleFoods.Add(item);
        await _db.SaveChangesAsync();

        return new CreateIncompatibleFoodResponse
        {
            Success = true,
            Message = "Created successfully.",
            Data = new IncompatibleFoodDto
            {
                Id = item.Id,
                FoodA = item.FoodA,
                FoodB = item.FoodB,
                Description = item.Description
            }
        };
    }

    public async Task<UpdateIncompatibleFoodResponse> UpdateAsync(UpdateIncompatibleFoodRequest request)
    {
        var item = await _db.TblIncompatibleFoods.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (item == null)
        {
            return new UpdateIncompatibleFoodResponse
            {
                Success = false,
                Message = "Not found."
            };
        }

        item.FoodA = request.FoodA;
        item.FoodB = request.FoodB;
        item.Description = request.Description;

        await _db.SaveChangesAsync();

        return new UpdateIncompatibleFoodResponse
        {
            Success = true,
            Message = "Updated successfully.",
            Data = new IncompatibleFoodDto
            {
                Id = item.Id,
                FoodA = item.FoodA,
                FoodB = item.FoodB,
                Description = item.Description
            }
        };
    }

    public async Task<DeleteIncompatibleFoodResponse> DeleteAsync(DeleteIncompatibleFoodRequest request)
    {
        var item = await _db.TblIncompatibleFoods.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (item == null)
        {
            return new DeleteIncompatibleFoodResponse
            {
                Success = false,
                Message = "Not found."
            };
        }

        _db.TblIncompatibleFoods.Remove(item);
        await _db.SaveChangesAsync();

        return new DeleteIncompatibleFoodResponse
        {
            Success = true,
            Message = "Deleted successfully."
        };
    }

    public async Task<GetIncompatibleFoodByIdResponse> GetByIdAsync(GetIncompatibleFoodByIdRequest request)
    {
        var item = await _db.TblIncompatibleFoods
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (item == null)
        {
            return new GetIncompatibleFoodByIdResponse
            {
                Success = false,
                Message = "Not found."
            };
        }

        return new GetIncompatibleFoodByIdResponse
        {
            Success = true,
            Message = "Success.",
            Data = new IncompatibleFoodDto
            {
                Id = item.Id,
                FoodA = item.FoodA,
                FoodB = item.FoodB,
                Description = item.Description
            }
        };
    }
}
