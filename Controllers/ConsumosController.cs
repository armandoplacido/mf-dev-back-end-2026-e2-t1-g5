using mf_dev_back_end_2026_e2_t1_g5.Data;
using mf_dev_back_end_2026_e2_t1_g5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace mf_dev_back_end_2026_e2_t1_g5.Controllers;

public class ConsumosController : Controller
{
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<ConsumosController> _logger;

    public ConsumosController(ILogger<ConsumosController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    // GET: Consumos
    public async Task<IActionResult> Index()
    {
        var consumos = await _databaseContext.Consumos.Include(c => c.Veiculo).ToListAsync();
        return View(consumos);
    }

    // GET: Consumos/Details/5
    public async Task<IActionResult> Details(Guid? publicId)
    {
        if (publicId == null) return NotFound();

        var consumo = await _databaseContext.Consumos
            .Include(c => c.Veiculo)
            .FirstOrDefaultAsync(m => m.PublicId == publicId);

        if (consumo == null) return NotFound();

        return View(consumo);
    }

    // GET: Consumos/Create
    public IActionResult Create()
    {
        ViewData["VeiculoId"] = new SelectList(_databaseContext.Veiculos, "Id", "Nome");
        return View();
    }

    // POST: Consumos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("PublicId,Descricao,Data,Valor,Km,TipoCombustivel,VeiculoId")]
        Consumo consumo)
    {
        if (ModelState.IsValid)
        {
            _databaseContext.Add(consumo);
            await _databaseContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["VeiculoId"] = new SelectList(_databaseContext.Veiculos, "Id", "Nome", consumo.VeiculoId);
        return View(consumo);
    }

    // GET: Consumos/Edit/5
    public async Task<IActionResult> Edit(Guid? publicId)
    {
        if (publicId == null) return NotFound();

        var consumo = await _databaseContext.Consumos.FirstOrDefaultAsync(c => c.PublicId == publicId);

        if (consumo == null) return NotFound();

        ViewData["VeiculoId"] = new SelectList(_databaseContext.Veiculos, "Id", "Nome", consumo.VeiculoId);

        return View(consumo);
    }

    // POST: Consumos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid publicId,
        [Bind("PublicId,Descricao,Data,Valor,Km,TipoCombustivel,VeiculoId")]
        Consumo consumo)
    {
        _logger.LogInformation("Iniciando edição do consumo {PublicId}", publicId);

        if (publicId != consumo.PublicId) return NotFound();

        if (!ModelState.IsValid)
        {
            ViewData["VeiculoId"] = new SelectList(_databaseContext.Veiculos, "Id", "Nome", consumo.VeiculoId);
            return View(consumo);
        }

        try
        {
            var consumoDb = await _databaseContext.Consumos
                .FirstOrDefaultAsync(c => c.PublicId == publicId);

            if (consumoDb == null) return NotFound();

            consumoDb.Descricao = consumo.Descricao;
            consumoDb.Data = consumo.Data;
            consumoDb.Valor = consumo.Valor;
            consumoDb.Km = consumo.Km;
            consumoDb.TipoCombustivel = consumo.TipoCombustivel;
            consumoDb.VeiculoId = consumo.VeiculoId;

            await _databaseContext.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!ConsumoExists(publicId))
                return NotFound();

            throw;
        }
    }

    // GET: Consumos/Delete/5
    public async Task<IActionResult> Delete(Guid? publicId)
    {
        if (publicId == null) return NotFound();

        var consumo = await _databaseContext.Consumos
            .Include(c => c.Veiculo)
            .FirstOrDefaultAsync(c => c.PublicId == publicId);

        if (consumo == null) return NotFound();

        return View(consumo);
    }

    // POST: Consumos/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid publicId)
    {
        var consumo = await _databaseContext.Consumos.FirstOrDefaultAsync(c => c.PublicId == publicId);

        if (consumo != null) _databaseContext.Consumos.Remove(consumo);

        await _databaseContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ConsumoExists(Guid publicId)
    {
        return _databaseContext.Consumos.Any(c => c.PublicId == publicId);
    }
}