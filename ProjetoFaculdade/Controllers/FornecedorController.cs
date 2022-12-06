using ProjetoFaculdade.Data;
using ProjetoFaculdade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjetoFaculdade.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly AppCont _appCont;

        public FornecedorController(AppCont appCont)
        {
            _appCont = appCont;
        }

        public IActionResult Index()
        {
            var allFornecedores = _appCont.Fornecedores.ToList();
            return View(allFornecedores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var fornecedor = await _appCont.Fornecedores.FirstOrDefaultAsync(m => m.Id == id);

            if (fornecedor == null)
                return NotFound();

            return PartialView(fornecedor);
        }

        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Fantasia, Cnpj, Telefone")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                _appCont.Add(fornecedor);
                await _appCont.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(fornecedor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var fornecedor = await _appCont.Fornecedores.FindAsync(id);

            if (fornecedor == null)
                return NotFound();

            return PartialView(fornecedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, Nome, Fantasia, Cnpj, Telefone")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fornecedorEntity = await _appCont.Fornecedores.FirstOrDefaultAsync(x => x.Id == fornecedor.Id);
                    fornecedorEntity.Nome = fornecedor.Nome;
                    fornecedorEntity.Cnpj = fornecedor.Cnpj;
                    fornecedorEntity.Telefone = fornecedor.Telefone;
                    fornecedorEntity.Fantasia = fornecedor.Fantasia;

                    _appCont.Update(fornecedorEntity);
                    await _appCont.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var fornecedor = await _appCont.Fornecedores.FirstOrDefaultAsync(m => m.Id == id);

            if (fornecedor == null)
                return NotFound();

            return PartialView(fornecedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _appCont.Fornecedores.FindAsync(id);
            _appCont.Fornecedores.Remove(fornecedor);
            await _appCont.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id)
        {
            return _appCont.Fornecedores.Any(e => e.Id == id);
        }
    }
}
