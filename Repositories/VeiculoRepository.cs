using GaragemVeiculos.Models;
namespace GaragemVeiculos.Repositories;
public class VeiculoRepository(IWebHostEnvironment env) : IVeiculoRepository
{
    private readonly string _path = JsonFile.PathFor(env, "veiculos.json");
    public List<Veiculo> ObterTodas() => JsonFile.Read<Veiculo>(_path).OrderBy(x=>x.Placa).ToList();
    public Veiculo? ObterPorId(int id) => JsonFile.Read<Veiculo>(_path).FirstOrDefault(x=>x.Id==id);
    public void Adicionar(Veiculo v) { var xs=JsonFile.Read<Veiculo>(_path); v.Id=xs.Count==0?1:xs.Max(x=>x.Id)+1; xs.Add(v); JsonFile.Write(_path,xs); }
    public void Atualizar(Veiculo v) { var xs=JsonFile.Read<Veiculo>(_path); var i=xs.FindIndex(x=>x.Id==v.Id); if(i>=0){xs[i]=v;JsonFile.Write(_path,xs);} }
    public void Remover(int id) { var xs=JsonFile.Read<Veiculo>(_path); if(xs.RemoveAll(x=>x.Id==id)>0)JsonFile.Write(_path,xs); }
}
