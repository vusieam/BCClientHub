using System.ComponentModel.DataAnnotations;

namespace ClientHubPortal.Models
{
    public class ClientViewModel
    {
        [Display(Name = "Identifier")]
        public Guid? Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Client Code")]
        public string? NameCode { get; set; }

        [Display(Name = "No. of Linked Contacts")]
        public int? NoOfContacts { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Status Description")]
        public DateTime? DeletedAt { get; set; }
    }
}
