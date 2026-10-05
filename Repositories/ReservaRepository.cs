using GaragemVeiculos.Models;
namespace GaragemVeiculos.Repositories;
public class ReservaRepository(IWebHostEnvironment env) : IReservaRepository
{
    private readonly string _path = JsonFile.PathFor(env, "reservas.json");
    public List<Reserva> ObterTodas() => JsonFile.Read<Reserva>(_path).OrderBy(x=>x.DataInicio).ToList();
    public Reserva? ObterPorId(int id) => JsonFile.Read<Reserva>(_path).FirstOrDefault(x=>x.Id==id);
    public bool ExisteConflito(int veiculoId, DateOnly inicio, DateOnly fim, int? ignorado=null) => JsonFile.Read<Reserva>(_path).Any(x=>x.VeiculoId==veiculoId && x.Id!=ignorado && inicio<=x.DataFim && fim>=x.DataInicio);
    public void Adicionar(Reserva r) { var xs=JsonFile.Read<Reserva>(_path); r.Id=xs.Count==0?1:xs.Max(x=>x.Id)+1; xs.Add(r); JsonFile.Write(_path,xs); }
    public void Atualizar(Reserva r) { var xs=JsonFile.Read<Reserva>(_path); var i=xs.FindIndex(x=>x.Id==r.Id); if(i>=0){xs[i]=r;JsonFile.Write(_path,xs);} }
    public void Remover(int id) { var xs=JsonFile.Read<Reserva>(_path); if(xs.RemoveAll(x=>x.Id==id)>0)JsonFile.Write(_path,xs); }
}
