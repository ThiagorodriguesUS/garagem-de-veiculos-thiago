using System.ComponentModel.DataAnnotations;
namespace GaragemVeiculos.Models;
public class Veiculo
{
    public int Id { get; set; }
    [Required(ErrorMessage="Placa é obrigatória.")] public string Placa { get; set; } = "";
    [Required(ErrorMessage="Marca é obrigatória.")] public string Marca { get; set; } = "";
    [Required(ErrorMessage="Modelo é obrigatório.")] public string Modelo { get; set; } = "";
    [Range(1886, 2100, ErrorMessage="Informe um ano válido.")] public int Ano { get; set; }
    public string? Cor { get; set; }
}
