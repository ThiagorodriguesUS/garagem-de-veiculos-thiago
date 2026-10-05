using System.ComponentModel.DataAnnotations;
namespace GaragemVeiculos.Models;
public class Reserva
{
    public int Id { get; set; }
    [Range(1, int.MaxValue, ErrorMessage="Selecione um veículo.")] public int VeiculoId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage="Selecione uma pessoa.")] public int PessoaId { get; set; }
    [DataType(DataType.Date), Required] public DateOnly DataInicio { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    [DataType(DataType.Date), Required] public DateOnly DataFim { get; set; } = DateOnly.FromDateTime(DateTime.Today);
}
