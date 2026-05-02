using System.Security.Claims;
using mf_dev_back_end_2026_e2_t1_g5.Data;
using mf_dev_back_end_2026_e2_t1_g5.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_dev_back_end_2026_e2_t1_g5.Controllers;

[Authorize(Roles = "Admin")]
public class UsuariosController : Controller
{
    private readonly DatabaseContext _databaseContext;

    public UsuariosController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _databaseContext.Usuarios.ToListAsync());
    }

    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }


    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(Usuario credentials)
    {
        var usuario = await _databaseContext.Usuarios
            .FirstOrDefaultAsync(u => u.Email == credentials.Email);

        if (usuario == null)
        {
            ViewBag.Message = "Usuário e/ou senha invalidos!";
            return View();
        }

        var senhaOk = BCrypt.Net.BCrypt.Verify(credentials.Senha, usuario.Senha);

        if (senhaOk)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Name),
                new(ClaimTypes.Email, usuario.Email),
                new(ClaimTypes.NameIdentifier, usuario.PublicId.ToString()),
                new(ClaimTypes.Role, usuario.Perfil.ToString())
            };

            var usuarioIdentidy = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(usuarioIdentidy);

            var props = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.ToLocalTime().AddHours(8),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(principal, props);

            return Redirect("/");
        }

        ViewBag.Message = "E-mail e/ou senha invalidos!";


        return View();
    }

    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Login", "Usuarios");
    }

    public async Task<IActionResult> Details(Guid? publicId)
    {
        if (publicId == null) return NotFound();

        var usuario = await _databaseContext.Usuarios
            .FirstOrDefaultAsync(u => u.PublicId == publicId);
        if (usuario == null) return NotFound();

        return View(usuario);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PublicId,Name,Email,Senha,Perfil")] Usuario usuario)
    {
        if (!ModelState.IsValid) return View(usuario);

        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
        _databaseContext.Add(usuario);
        await _databaseContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? publicId)
    {
        if (publicId == null) return NotFound();

        var usuario = await _databaseContext.Usuarios.FirstOrDefaultAsync(u => u.PublicId == publicId);

        if (usuario == null) return NotFound();

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid? publicId, [Bind("PublicId,Email,Name,Senha,Perfil")] Usuario usuario)
    {
        if (publicId != usuario.PublicId) return NotFound();

        if (!ModelState.IsValid) return View(usuario);

        try
        {
            var usuarioDb = await _databaseContext.Usuarios.FirstOrDefaultAsync(u => u.PublicId == publicId);

            if (usuarioDb == null) return NotFound();

            usuarioDb.Name = usuario.Name;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            usuarioDb.Perfil = usuario.Perfil;

            _databaseContext.Update(usuarioDb);
            await _databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(usuario.PublicId)) return NotFound();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid? publicId)
    {
        if (publicId == null) return NotFound();

        var usuario = await _databaseContext.Usuarios
            .FirstOrDefaultAsync(m => m.PublicId == publicId);
        if (usuario == null) return NotFound();

        return View(usuario);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid publicId)
    {
        var usuario = await _databaseContext.Usuarios.FirstOrDefaultAsync(u => u.PublicId == publicId);

        if (usuario != null) _databaseContext.Usuarios.Remove(usuario);

        await _databaseContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(Guid publicId)
    {
        return _databaseContext.Usuarios.Any(e => e.PublicId == publicId);
    }
}