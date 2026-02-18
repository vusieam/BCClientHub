namespace ClientHubPortal.Models;

public class ContactViewModel
{
    [Display(Name = "Unique Identifier")]
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Client Name is required.")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Client Name is required.")]
    [Display(Name = "Surname")]
    public string Surname { get; set; }

    [Display(Name = "Full Name")]
    public string? Fullname { get; set; }


    [Required(ErrorMessage = "Contact email address is required.")]
    [Display(Name = "Email Address")]
    public string EmailAddress { get; set; }


    [Display(Name = "No. of linked clients")]
    public int? NoOfClients { get; set; }


    [Display(Name = "Date created")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Status description")]
    public DateTime? DeletedAt { get; set; }

    [Display(Name = "Client Name")]
    public Guid? ClientId { get; set; }

    [Display(Name = "Client Name")]
    public string? ClientName { get; set; }
}
