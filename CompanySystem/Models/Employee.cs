using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Models;

[Table("employee")]
public partial class Employee
{
    [Key]
    [Column("empno")]

    public int Empno { get; set; }

    [Column("fname")]
    [StringLength(50)]
    public string Fname { get; set; } = null!;

    [Column("lname")]
    [StringLength(50)]
    public string Lname { get; set; } = null!;

    [Column("address")]
    public string? Address { get; set; }

    [Column("dob")]
    public DateOnly? Dob { get; set; }

    [Column("sex")]
    public char? Sex { get; set; }


    [Column("position")]
    [StringLength(50)]
    public string? Position { get; set; }

    [Column("deptno")]
    public int? Deptno { get; set; }

    [InverseProperty("MgrempnoNavigation")]
    [JsonIgnore]
    public virtual ICollection<Departement> Departements { get; set; } = new List<Departement>();

    [ForeignKey("Deptno")]
    [InverseProperty("Employees")]
    [JsonIgnore]
    public virtual Departement? DeptnoNavigation { get; set; }

    [InverseProperty("EmpnoNavigation")]
    [JsonIgnore]
    public virtual ICollection<Workson> Worksons { get; set; } = new List<Workson>();
}
