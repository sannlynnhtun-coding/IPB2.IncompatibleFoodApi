using System;
using System.Collections.Generic;

namespace IPB2.IncompatibleFoodApi.Database.AppDbContextModels;

public partial class TblIncompatibleFood
{
    public int Id { get; set; }

    public string FoodA { get; set; } = null!;

    public string FoodB { get; set; } = null!;

    public string Description { get; set; } = null!;
}
