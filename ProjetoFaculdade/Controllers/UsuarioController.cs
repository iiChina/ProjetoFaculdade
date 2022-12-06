using ProjetoFaculdade.Data;
using ProjetoFaculdade.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjetoFaculdade.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppCont _appCont;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsuarioController(AppCont appCont,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager)
        {
            _appCont = appCont;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var allUsuarios = _appCont.Usuarios.ToList();
            return View(allUsuarios);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _appCont.Usuarios.FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null)
                return NotFound();

            return PartialView(usuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Login, Senha, Nivel")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await _userManager.FindByEmailAsync(usuario.Login);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("Usuario.Email", "Já existe um cliente cadastrado com este e-mail");
                    return RedirectToAction(nameof(Index));
                }

                var identity = new IdentityUser
                {
                    UserName = usuario.Login,
                    Email = usuario.Login,
                };

                var result = await _userManager.CreateAsync(identity, usuario.Senha);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(usuario.Nivel.ToString()))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(usuario.Nivel.ToString()));
                    }

                    await _userManager.AddToRoleAsync(identity, usuario.Nivel.ToString());


                    _appCont.Usuarios.Add(usuario);

                    var afetados = await _appCont.SaveChangesAsync();

                    if (afetados > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        await _userManager.DeleteAsync(identity);
                        ModelState.AddModelError("Cliente", "Não foi possível efetuar o cadastro. Verifique e tente novamente. Se o problema" +
                            "persistir, entre em contato conosco.");

                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                    ModelState.AddModelError("Cliente.Email", "Não foi possível criar um usuário com este endereço. Use outro endereço de e-mail ou tente recuperar a senha deste.");



                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> LoginGet()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(usuario.Login, usuario.Senha, false, lockoutOnFailure: false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Fornecedor");
                else
                    return View();
            }

            return View();

        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Usuario");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _appCont.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return PartialView(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nome, Login, Senha, Nivel")] Usuario usuario)
        {
            if (id == usuario.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var userIdentity = await _userManager.FindByEmailAsync(usuario.Login);

                    userIdentity.Email = usuario.Login;
                    userIdentity.UserName = usuario.Nome;

                    _appCont.Update(usuario);
                    await _appCont.SaveChangesAsync();

                    await _userManager.UpdateAsync(userIdentity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _appCont.Usuarios.FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null)
                return NotFound();

            return PartialView(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _appCont.Usuarios.FindAsync(id);

            var userIdentity = await _userManager.FindByEmailAsync(usuario.Login);

            _appCont.Usuarios.Remove(usuario);
            await _appCont.SaveChangesAsync();
            await _userManager.DeleteAsync(userIdentity);

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _appCont.Usuarios.Any(e => e.Id == id);
        }
    }
}
