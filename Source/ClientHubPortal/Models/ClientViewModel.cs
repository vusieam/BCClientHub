namespace ClientHubPortal.Models;

public class ClientViewModel
{
    [Display(Name = "Identifier")]
    public Guid? Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Client code")]
    public string? NameCode { get; set; }

    [Display(Name = "No. of linked contacts")]
    public int? NoOfContacts { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Date created")]
    public DateTime? CreatedAt { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Status description")]
    public DateTime? DeletedAt { get; set; }
}
