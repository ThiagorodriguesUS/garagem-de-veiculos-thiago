using GaragemVeiculos.Models;
using GaragemVeiculos.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace GaragemVeiculos.Controllers;
public class VeiculoController(IVeiculoRepository repository, IReservaRepository reservas) : Controller
{
    public IActionResult Index() => View(repository.ObterTodas());
    [HttpGet] public IActionResult Create() => View("VeiculoForm",new Veiculo{Ano=DateTime.Today.Year});
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Create(Veiculo veiculo) { Validar(veiculo); if(!ModelState.IsValid)return View("VeiculoForm",veiculo); repository.Adicionar(veiculo); return RedirectToAction(nameof(Index)); }
    [HttpGet] public IActionResult Edit(int id) { var v=repository.ObterPorId(id); return v is null?NotFound():View("VeiculoForm",v); }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Edit(Veiculo veiculo) { Validar(veiculo); if(!ModelState.IsValid)return View("VeiculoForm",veiculo); repository.Atualizar(veiculo); return RedirectToAction(nameof(Index)); }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Delete(int id) { if(reservas.ObterTodas().Any(x=>x.VeiculoId==id)){TempData["Erro"]="Não é possível excluir um veículo vinculado a uma reserva.";return RedirectToAction(nameof(Index));} repository.Remover(id); return RedirectToAction(nameof(Index)); }
    private void Validar(Veiculo v) { if(repository.ObterTodas().Any(x=>x.Id!=v.Id && x.Placa.Trim().Equals(v.Placa.Trim(),StringComparison.OrdinalIgnoreCase))) ModelState.AddModelError(nameof(v.Placa),"Esta placa já está cadastrada."); }
}
