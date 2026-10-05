using System.ComponentModel.DataAnnotations;
namespace GaragemVeiculos.Models;
public class Pessoa
{
    public int Id { get; set; }
    [Required(ErrorMessage="Nome é obrigatório.")] public string Nome { get; set; } = "";
    [Required(ErrorMessage="CPF é obrigatório.")] public string CPF { get; set; } = "";
    [Required(ErrorMessage="E-mail é obrigatório."), EmailAddress(ErrorMessage="Informe um e-mail válido.")] public string Email { get; set; } = "";
    public string? Telefone { get; set; }
}
