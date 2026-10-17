namespace ApiTarefas.Services.Interfaces;
using ApiTarefas.Models;
using ApiTarefas.Dto;

public interface ITarefaService
{

    public List<Tarefa> Lista(int page);
    public Tarefa Incluir(TarefaDto tarefaDto);
    public Tarefa? BuscarPorId(int id);
    public Tarefa Atualizar(int id, TarefaDto tarefaDto);
    public void Excluir(int id);
    public Tarefa Show(int id);
}