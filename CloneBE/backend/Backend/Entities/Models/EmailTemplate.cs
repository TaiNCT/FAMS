using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class EmailTemplate
{
    public int Id { get; set; }

    public string EmailTemplateId { get; set; } = null!;

    public int Type { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public int IdStatus { get; set; }

    public virtual ICollection<EmailSend> EmailSends { get; set; } = new List<EmailSend>();
}
