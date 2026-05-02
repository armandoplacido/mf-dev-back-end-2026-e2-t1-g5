using System.ComponentModel.DataAnnotations;

namespace mf_dev_back_end_2026_e2_t1_g5.Models;

public class Usuario
{
    [Key] public int Id { get; set; }

    [Required] public Guid PublicId { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [Display(Name = "Nome do Usuario")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    public Perfil Perfil { get; set; }
}

public enum Perfil
{
    Admin,
    User
}