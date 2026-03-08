using IPB2.IncompatibleFoodApi.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPB2.IncompatibleFoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncompatibleFoodController : ControllerBase
    {
        private readonly AppDbContext _db;

        public IncompatibleFoodController()
        {
            _db = new AppDbContext();
        }


        // async/await
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageNo, int pageSize)
        {
            var lst2 = await GetList2(pageNo, pageSize);
            var lst = await _db.TblIncompatibleFoods
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(lst);
        }

        private async Task<List<TblIncompatibleFood>> GetList2(int pageNo, int pageSize)
        {
            return await _db.TblIncompatibleFoods
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetList(int pageNo = 1, int pageSize = 10)
        {
            if (pageNo <= 0) pageNo = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _db.TblIncompatibleFoods
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

            return Ok(response);
        }

        [HttpGet("CategoryList")]
        public async Task<IActionResult> GetCategoryList()
        {
            var categories = await _db.TblIncompatibleFoods
                .AsNoTracking()
                .Where(x => !string.IsNullOrEmpty(x.Description))
                .Select(x => x.Description!.Trim())
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(
            string? keyword = null,
            string? category = null,
            int pageNo = 1,
            int pageSize = 10)
        {
            if (pageNo <= 0) pageNo = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _db.TblIncompatibleFoods
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

            return Ok(response);
        }
    }
}
