using System;
using System.Collections.Generic;

namespace lab2_BD_individual;

public partial class Employee
{
    public long EmployeeId { get; set; }

    public string? FullName { get; set; }

    public long? Age { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PassportData { get; set; }

    public long? PositionCode { get; set; }

    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

    public virtual Position? PositionCodeNavigation { get; set; }
}
