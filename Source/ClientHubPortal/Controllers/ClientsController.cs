using ClientHubDatabase.Models;
using ClientHubPortal.Models;
using ClientHubPortal.Services;
using Microsoft.AspNetCore.Identity;

namespace ClientHubPortal.Controllers;

public class ClientsController : Controller
{
    #region ----------------- Protected Properties -----------------

    public readonly IClientService clientService;
    //private readonly SignInManager<ApplicationUser> signInManager;

    #endregion

    public ClientsController(IClientService clientService)
    {
        this.clientService = clientService;
    }


    #region ----------------- View Routes -----------------

    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        //ViewBag.Header = $"Clients";
        ViewBag.CurrentHeader = $"Clients";
        var envelopeResponse = await clientService.GetClientsAsync();
        var clients = (envelopeResponse.Status) ? envelopeResponse.Data : new List<ClientViewModel>();

        return View(clients);
    }



    [HttpGet]
    public async Task<IActionResult> ClientDetailsAsync(Guid Id)
    {
        //ViewBag.Header = $"Clients";
        ViewBag.CurrentHeader = $"Clients Details";
        var envelopeResponse = await clientService.GetClientsAsync();
        var client = envelopeResponse.Data.FirstOrDefault(f => f.Id == Id);
        if (client == null)
            return View();
        return View(client);
    }


    [HttpGet]
    public async Task<IActionResult> ClientInfoAsync(Guid clientId)
    {
        //ViewBag.Header = $"Clients";
        ViewBag.CurrentHeader = $"Clients Details";
        var envelopeResponse = await clientService.GetClientsAsync();
        var client = envelopeResponse.Data.FirstOrDefault(f => f.Id == clientId);        
        return PartialView("_ClientInfoPartial", client);
    }



    [HttpGet]
    public async Task<IActionResult> GetClientContactsAsync(Guid clientId)
    {
        //ViewBag.Header = $"Clients";
        ViewBag.CurrentHeader = $"Clients Details";
        var envelopeResponse = await clientService.GetClientContactsAsync(clientId);
        var clientContacts = envelopeResponse.Data;
        return PartialView("_ClientContactsPartial", clientContacts);
    }



    [HttpGet]
    public IActionResult CreateClientModal()
    {
        return PartialView("_CreateClientModal", new ClientViewModel());
    }

    #endregion



    #region ----------------- API Routes -----------------


    [HttpPost]
    public async Task<IActionResult> CreateClientAjax(ClientViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var apiResponse = await clientService.CreateClientAsync(model);
        return Json(apiResponse);
    }



    [HttpPost]
    public async Task<IActionResult> DeleteClientAsync(Guid clientId)
    {
        var apiResponse = await clientService.DeleteClientAsync(clientId);
        return Json(apiResponse);
    }

    #endregion
}
