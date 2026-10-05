using GaragemVeiculos.Models;
namespace GaragemVeiculos.Repositories;
public class PessoaRepository(IWebHostEnvironment env) : IPessoaRepository
{
    private readonly string _path = JsonFile.PathFor(env, "pessoas.json");
    public List<Pessoa> ObterTodas() => JsonFile.Read<Pessoa>(_path).OrderBy(x => x.Nome).ToList();
    public Pessoa? ObterPorId(int id) => JsonFile.Read<Pessoa>(_path).FirstOrDefault(x => x.Id == id);
    public void Adicionar(Pessoa p) { var xs=JsonFile.Read<Pessoa>(_path); p.Id=xs.Count==0?1:xs.Max(x=>x.Id)+1; xs.Add(p); JsonFile.Write(_path,xs); }
    public void Atualizar(Pessoa p) { var xs=JsonFile.Read<Pessoa>(_path); var i=xs.FindIndex(x=>x.Id==p.Id); if(i>=0){xs[i]=p;JsonFile.Write(_path,xs);} }
    public void Remover(int id) { var xs=JsonFile.Read<Pessoa>(_path); if(xs.RemoveAll(x=>x.Id==id)>0)JsonFile.Write(_path,xs); }
}
