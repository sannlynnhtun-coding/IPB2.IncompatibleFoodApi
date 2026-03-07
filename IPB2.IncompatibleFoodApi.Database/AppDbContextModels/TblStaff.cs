using System;
using System.Collections.Generic;

namespace IPB2.IncompatibleFoodApi.Database.AppDbContextModels;

public partial class TblStaff
{
    public Guid StaffId { get; set; }

    public string StaffCode { get; set; } = null!;

    public string StaffName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Position { get; set; } = null!;

    public decimal Salary { get; set; }

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }

    public DateOnly JoinDate { get; set; }

    public bool IsDelete { get; set; }
}
