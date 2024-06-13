using System;
using System.Collections.Generic;

namespace ReservationManagementAPI.Entities.Models;

public partial class EmailTemplate
{
    public string EmailTemplateId { get; set; }

    public int Id { get; set; }

    public int Type { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public virtual ICollection<EmailSend> EmailSends { get; set; } = new List<EmailSend>();
}
