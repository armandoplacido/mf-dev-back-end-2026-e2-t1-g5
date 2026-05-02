using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_dev_back_end_2026_e2_t1_g5.Models;

[Table("Veiculos")]
public class Veiculo
{
    [Key] public int Id { get; set; }

    [Required(ErrorMessage = "Obrigatório informar o nome do Veiculo")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Obrigatório informar a placa")]
    public string Placa { get; set; }

    [Required(ErrorMessage = "Obrigatório informar o ano de fabricação")]
    [Display(Name = "Ano de Fabricação")]
    public int AnoFabricacao { get; set; }

    [Required(ErrorMessage = "Obrigatório informar o ano do modelo")]
    [Display(Name = "Ano do modelo")]
    public int AnoModelo { get; set; }
}