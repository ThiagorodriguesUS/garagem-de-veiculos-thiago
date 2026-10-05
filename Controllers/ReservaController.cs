using GaragemVeiculos.Models;
using GaragemVeiculos.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace GaragemVeiculos.Controllers;
public class ReservaController(IReservaRepository repository, ReservaService service, IPessoaRepository pessoas, IVeiculoRepository veiculos) : Controller
{
    public IActionResult Index() { ViewBag.Pessoas=pessoas.ObterTodas(); ViewBag.Veiculos=veiculos.ObterTodas(); ViewBag.Hoje=DateOnly.FromDateTime(DateTime.Today); return View(repository.ObterTodas()); }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Create(Reserva reserva) { var erro=service.Validar(reserva); if(erro is not null){TempData["Erro"]=erro;return RedirectToAction(nameof(Index));} repository.Adicionar(reserva); TempData["Sucesso"]="Reserva cadastrada."; return RedirectToAction(nameof(Index)); }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Edit(int id, DateOnly dataInicio, DateOnly dataFim) { var r=repository.ObterPorId(id); if(r is null)return NotFound(); r.DataInicio=dataInicio;r.DataFim=dataFim;var erro=service.Validar(r,id);if(erro is not null){TempData["Erro"]=erro;return RedirectToAction(nameof(Index));}repository.Atualizar(r);TempData["Sucesso"]="Período atualizado.";return RedirectToAction(nameof(Index)); }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Delete(int id) { repository.Remover(id);TempData["Sucesso"]="Reserva cancelada.";return RedirectToAction(nameof(Index)); }
}
