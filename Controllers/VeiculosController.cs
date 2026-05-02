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

    public async Task<IActionResult> Edit(Guid? publicId)
    {
        if (publicId == Guid.Empty)
            return NotFound();

        var veiculo = await _databaseContext.Veiculos
            .FirstOrDefaultAsync(v => v.PublicId == publicId);

        if (veiculo == null)
            return NotFound();

        return View(veiculo);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid publicId, Veiculo veiculo)
    {
        var veiculoDb = await _databaseContext.Veiculos
            .FirstOrDefaultAsync(v => v.PublicId == publicId);

        if (veiculoDb == null)
            return NotFound();

        veiculoDb.Nome = veiculo.Nome;
        veiculoDb.Placa = veiculo.Placa;
        veiculoDb.AnoFabricacao = veiculo.AnoFabricacao;
        veiculoDb.AnoModelo = veiculo.AnoModelo;

        await _databaseContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(Guid? publicId)
    {
        if (publicId == Guid.Empty)
            return NotFound();

        var veiculo = await _databaseContext.Veiculos.FirstOrDefaultAsync(v => v.PublicId == publicId);

        if (veiculo == null)
            return NotFound();

        return View(veiculo);
    }

    public async Task<IActionResult> Delete(Guid? publicId)
    {
        if (publicId == Guid.Empty)
            return NotFound();

        var veiculo = await _databaseContext.Veiculos
            .FirstOrDefaultAsync(v => v.PublicId == publicId);

        if (veiculo == null)
            return NotFound();

        return View(veiculo);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> Deleteconfirmed(Guid? publicId)
    {
        if (publicId == Guid.Empty)
            return NotFound();

        var veiculo = await _databaseContext.Veiculos.FirstOrDefaultAsync(v => v.PublicId == publicId);

        if (veiculo == null) return NotFound();

        _databaseContext.Veiculos.Remove(veiculo);
        await _databaseContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Relatorio(Guid? publicId)
    {
        if (publicId == null)
            return NotFound();

        var veiculo = await _databaseContext.Veiculos.FirstOrDefaultAsync(v => v.PublicId == publicId);

        if (veiculo == null)
            return NotFound();

        var consumos = await _databaseContext.Consumos.Where(c => c.VeiculoId == veiculo.Id)
            .OrderByDescending(c => c.Data).ToListAsync();

        var total = consumos.Sum(c => c.Valor);
        ViewBag.Total = total;
        ViewBag.Veiculo = veiculo;
        return View(consumos);
    }
}