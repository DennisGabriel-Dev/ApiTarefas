using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ApiTarefas.Models;

public class Tarefa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(100)]
    public required string Titulo { get; set; }
    [Column(TypeName = "text")]
    public required string Descricao { get; set; }
    public bool Concluida { get; set; } = false;
}