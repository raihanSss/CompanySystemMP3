using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Models;

[Table("workson")]
public partial class Workson
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("empno")]
    public int? Empno { get; set; }

    [Column("projno")]
    public int? Projno { get; set; }

    [Column("dateworked")]
    public DateOnly? Dateworked { get; set; }

    [Column("hoursworked")]
    [Precision(5, 2)]
    public decimal? Hoursworked { get; set; }

    [ForeignKey("Empno")]
    [InverseProperty("Worksons")]
    [JsonIgnore]
    public virtual Employee? EmpnoNavigation { get; set; }

    [ForeignKey("Projno")]
    [InverseProperty("Worksons")]
    [JsonIgnore]
    public virtual Project? ProjnoNavigation { get; set; }
}
