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
        var envelopeResponse = await clientService.GetClientsAsync();
        var clients = (envelopeResponse.Status) ? envelopeResponse.Data : new List<ClientViewModel>();

        return View(clients);
    }


    [HttpGet]

    public async Task<IActionResult> ContactsAsync()
    {
        var envelopeResponse = await clientService.GetClientsAsync();
        var clients = (envelopeResponse.Status) ? envelopeResponse.Data : new List<ClientViewModel>();

        return View(clients);
    }



    public IActionResult CreateModal()
    {
        return PartialView("_CreateClientModal", new ClientViewModel());
    }

    #endregion



    #region ----------------- API Routes -----------------


    [HttpPost]
    public async Task<IActionResult> CreateAjax(ClientViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var apiResponse = await clientService.CreateClientAsync(model);

        return Json(apiResponse);
    }

    #endregion
}
