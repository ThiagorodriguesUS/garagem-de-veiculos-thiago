using GaragemVeiculos.Models;
using GaragemVeiculos.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace GaragemVeiculos.Controllers;
public class PessoaController(IPessoaRepository repository, IReservaRepository reservas) : Controller
{
    public IActionResult Index() => View(repository.ObterTodas());
    [HttpGet] public IActionResult Create() => View("PessoaForm",new Pessoa());
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Create(Pessoa pessoa) { Validar(pessoa); if(!ModelState.IsValid)return View("PessoaForm",pessoa); repository.Adicionar(pessoa); return RedirectToAction(nameof(Index)); }
    [HttpGet] public IActionResult Edit(int id) { var p=repository.ObterPorId(id); return p is null?NotFound():View("PessoaForm",p); }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Edit(Pessoa pessoa) { Validar(pessoa); if(!ModelState.IsValid)return View("PessoaForm",pessoa); repository.Atualizar(pessoa); return RedirectToAction(nameof(Index)); }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Delete(int id) { if(reservas.ObterTodas().Any(x=>x.PessoaId==id)){TempData["Erro"]="Não é possível excluir uma pessoa vinculada a uma reserva.";return RedirectToAction(nameof(Index));} repository.Remover(id); return RedirectToAction(nameof(Index)); }
    private void Validar(Pessoa p) { if(repository.ObterTodas().Any(x=>x.Id!=p.Id && x.CPF.Trim().Equals(p.CPF.Trim(),StringComparison.OrdinalIgnoreCase))) ModelState.AddModelError(nameof(p.CPF),"Este CPF já está cadastrado."); }
}
