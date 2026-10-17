namespace ApiTarefas.Services;
using ApiTarefas.Data;
using ApiTarefas.Models;
using ApiTarefas.Models.Errors;
using ApiTarefas.Dto;
using ApiTarefas.Services.Interfaces;

public class TarefaService : ITarefaService
{
    public TarefaService(AppDbContext db)
    {
        _db = db;
    }

    private AppDbContext _db;

    public List<Tarefa> Lista(int page = 1)
    {
        if (page < 1) page = 1;
        int limit = 10;
        int offset = (page - 1) * limit;
        return _db.Tarefas.Skip(offset).Take(limit).ToList();
    }

    public Tarefa Incluir(TarefaDto tarefaDto)
    {
        var tarefa  = new Tarefa
        {
            Titulo = tarefaDto.Titulo,
            Descricao = tarefaDto.Descricao,
            Concluida = tarefaDto.Concluida
        };
        _db.Tarefas.Add(tarefa);
        _db.SaveChanges();
        return tarefa;
    }

    public Tarefa? BuscarPorId(int id)
    {
        var tarefa = _db.Tarefas.Find(id);
        if (tarefa == null)
            throw new TarefaError("Tarefa não encontrada.");

        return tarefa;
    }

    public Tarefa Atualizar(int id, TarefaDto tarefaDto)
    {
        if (string.IsNullOrEmpty(tarefaDto.Titulo))
            throw new TarefaError("O título da tarefa é obrigatório.");

        var tarefaDb = BuscarPorId(id);
         if (tarefaDb == null)
            throw new TarefaError("Tarefa não encontrada.");

        tarefaDb.Titulo = tarefaDto.Titulo;
        tarefaDb.Descricao = tarefaDto.Descricao;
        tarefaDb.Concluida = tarefaDto.Concluida;

        _db.Tarefas.Update(tarefaDb);
        _db.SaveChanges();
        return tarefaDb;
    }

    public void Excluir(int id)
    {
        var tarefaDb = BuscarPorId(id);
        if (tarefaDb == null)
            throw new TarefaError("Tarefa não encontrada.");
        _db.Tarefas.Remove(tarefaDb);
        _db.SaveChanges();
    }

    public Tarefa Show(int id)
    {
        var tarefa = BuscarPorId(id);
        if (tarefa == null)
            throw new TarefaError("Tarefa não encontrada.");
        return tarefa;
    }
}