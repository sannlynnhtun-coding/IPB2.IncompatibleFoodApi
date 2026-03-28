using System.Collections.Generic;

namespace PB2.IncompatibleFood.Domain.Features.IncompatibleFood;

public class IncompatibleFoodDto
{
    public int Id { get; set; }
    public string FoodA { get; set; } = null!;
    public string FoodB { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class IncompatibleFoodListRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class IncompatibleFoodListResponse
{
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PageCount { get; set; }
    public List<IncompatibleFoodDto> Data { get; set; } = new();
}

public class IncompatibleFoodSearchRequest
{
    public string? Keyword { get; set; }
    public string? Category { get; set; }
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class IncompatibleFoodSearchResponse
{
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PageCount { get; set; }
    public List<IncompatibleFoodDto> Data { get; set; } = new();
}

public class IncompatibleFoodCategoryListRequest { }

public class IncompatibleFoodCategoryListResponse
{
    public List<string> Categories { get; set; } = new();
}

public class CreateIncompatibleFoodRequest
{
    public string FoodA { get; set; } = null!;
    public string FoodB { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class CreateIncompatibleFoodResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public IncompatibleFoodDto? Data { get; set; }
}

public class UpdateIncompatibleFoodRequest
{
    public int Id { get; set; }
    public string FoodA { get; set; } = null!;
    public string FoodB { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class UpdateIncompatibleFoodResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public IncompatibleFoodDto? Data { get; set; }
}

public class DeleteIncompatibleFoodRequest
{
    public int Id { get; set; }
}

public class DeleteIncompatibleFoodResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
}

public class GetIncompatibleFoodByIdRequest
{
    public int Id { get; set; }
}

public class GetIncompatibleFoodByIdResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public IncompatibleFoodDto? Data { get; set; }
}
