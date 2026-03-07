using System;
using System.Collections.Generic;

namespace IPB2.IncompatibleFoodApi.Database.AppDbContextModels;

public partial class TblStudent
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public int Age { get; set; }

    public string ClassNo { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string ParentName { get; set; } = null!;

    public bool IsDelete { get; set; }

    public decimal? Fees { get; set; }
}
