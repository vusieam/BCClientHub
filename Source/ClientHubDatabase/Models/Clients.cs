using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientHubDatabase.Models;

public class Clients
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NameCode { get; set; }
    public int NoOfContacts { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
