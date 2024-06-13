using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class SyllabusClass
{
    public int Id { get; set; }

    public string SyllabusId { get; set; } = null!;

    public string ClassId { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;

    public virtual Syllabus Syllabus { get; set; } = null!;
}
