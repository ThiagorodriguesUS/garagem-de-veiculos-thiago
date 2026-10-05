using GaragemVeiculos.Models;
namespace GaragemVeiculos.Repositories;
public class ReservaService(IReservaRepository reservas, IPessoaRepository pessoas, IVeiculoRepository veiculos)
{
    public string? Validar(Reserva r, int? idIgnorado=null)
    {
        if (r.DataFim < r.DataInicio) return "A data final não pode ser anterior à inicial.";
        if (pessoas.ObterPorId(r.PessoaId) is null) return "A pessoa selecionada não existe.";
        if (veiculos.ObterPorId(r.VeiculoId) is null) return "O veículo selecionado não existe.";
        if (reservas.ExisteConflito(r.VeiculoId,r.DataInicio,r.DataFim,idIgnorado)) return "Este veículo já está reservado em parte ou em todo esse período.";
        return null;
    }
}
