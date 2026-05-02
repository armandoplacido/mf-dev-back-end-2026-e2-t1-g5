using mf_dev_back_end_2026_e2_t1_g5.Data;
using mf_dev_back_end_2026_e2_t1_g5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_dev_back_end_2026_e2_t1_g5.Controllers;

public class VeiculosController : Controller
{
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<VeiculosController> _logger;

    public VeiculosController(DatabaseContext databaseContext, ILogger<VeiculosController> logger)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Busca de veiculos");
        var veiculos = await _databaseContext.Veiculos.ToListAsync();
        _logger.LogInformation("Busca Finalizada");
        return View(veiculos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Veiculo veiculo)
    {
        if (!ModelState.IsValid) return View(veiculo);

        _logger.LogInformation("Adicionar veiculo");
        _databaseContext.Veiculos.Add(veiculo);
        await _databaseContext.SaveChangesAsync();
        _logger.LogInformation("Veiculo adicionado");

        return RedirectToAction("Index");
    }
}