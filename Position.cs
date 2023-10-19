using System;
using System.Collections.Generic;

namespace lab2_BD_individual;

public partial class Position
{
    public long PositionsCode { get; set; }

    public string? PositionsName { get; set; }

    public double? Salary { get; set; }

    public string? Responsibilites { get; set; }

    public string? Requirments { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
