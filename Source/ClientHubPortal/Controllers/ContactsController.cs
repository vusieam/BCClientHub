using ClientHubPortal.Services;

namespace ClientHubPortal.Controllers;

public class ContactsController : Controller
{
    #region ----------------- Protected Properties -----------------

    public readonly IClientService clientService;

    #endregion

    public ContactsController(IClientService clientService)
    {
        this.clientService = clientService;
    }


    #region ----------------- View Routes -----------------

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewBag.CurrentHeader = $"Contacts";
        var envelopeResponse = await clientService.GetContactsAsync();
        var clients = (envelopeResponse.Status) ? envelopeResponse.Data : new List<ContactViewModel>();
        return View(clients);
    }


    [HttpGet]
    public async Task<IActionResult> GetUnlinkedContacts(Guid clientId)
    {
        var contacts = await clientService.GetUnlinkedContactsAsync(clientId);
        ViewBag.ClientId = clientId;
        return PartialView("_UnlinkedContactsModal", contacts.Data);
    }




    [HttpGet]
    public IActionResult CreateContactModal(Guid? clientId = null)
    {
        return PartialView("_CreateContactModal", new ContactViewModel());
    }

    #endregion



    #region ----------------- API Routes -----------------


    [HttpPost]
    public async Task<IActionResult> CreateContactAjaxAsync(ContactViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var apiResponse = await clientService.CreateContactAsync(model);
        return Json(apiResponse);
    }


    [HttpPost]
    public async Task<IActionResult> LinkContactAsync(Guid clientId, Guid contactId)
    {
        var apiResponse = await clientService.LinkContactAsync(clientId, contactId);
        return Json(apiResponse);
    }


    [HttpPost]
    public async Task<IActionResult> DeLinkContactAsync(Guid clientId, Guid contactId)
    {
        var apiResponse = await clientService.DeLinkContactAsync(clientId, contactId);
        return Json(apiResponse);
    }


    #endregion



}
