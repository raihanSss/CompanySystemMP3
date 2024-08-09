using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Models;

[Table("project")]
public partial class Project
{
    [Key]
    [Column("projno")]
    public int Projno { get; set; }

    [Column("projname")]
    [StringLength(100)]
    public string Projname { get; set; } = null!;

    [Column("deptno")]
    public int? Deptno { get; set; }

    [ForeignKey("Deptno")]
    [InverseProperty("Projects")]
    [JsonIgnore]
    public virtual Departement? DeptnoNavigation { get; set; }

    [InverseProperty("ProjnoNavigation")]
    [JsonIgnore]
    public virtual ICollection<Workson> Worksons { get; set; } = new List<Workson>();
}
