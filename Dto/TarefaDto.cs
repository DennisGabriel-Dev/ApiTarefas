namespace ApiTarefas.Dto;
public record TarefaDto
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool Concluida { get; set; }
}