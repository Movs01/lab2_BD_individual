using System;
using System.Collections.Generic;

namespace lab2_BD_individual;

public partial class BorrowedBook
{
    public long BookCode { get; set; }

    public long? ReaderCode { get; set; }

    public string? BorrowDate { get; set; }

    public string? ReturnDate { get; set; }

    public string? ReturnStatus { get; set; }

    public long? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
