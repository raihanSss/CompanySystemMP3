using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Models;

[Table("departement")]
public partial class Departement
{
    [Key]
    [Column("deptno")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Deptno { get; set; }

    [Column("deptname")]
    [StringLength(100)]
    public string Deptname { get; set; } = null!;

    [Column("mgrempno")]
    public int? Mgrempno { get; set; }

    [InverseProperty("DeptnoNavigation")]
    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("Mgrempno")]
    [InverseProperty("Departements")]
    [JsonIgnore]
    public virtual Employee? MgrempnoNavigation { get; set; }

    [InverseProperty("DeptnoNavigation")]
    [JsonIgnore]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
