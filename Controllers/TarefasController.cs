using Microsoft.AspNetCore.Mvc;
using ApiTarefas.ModelViews;
using ApiTarefas.Data;
using ApiTarefas.Models;
using ApiTarefas.Dto;
namespace ApiTarefas.Controllers;

[ApiController]
[Route("/tarefas")]
public class TarefasController : ControllerBase
{
    public TarefasController(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    private AppDbContext _db;

    [HttpGet]
    public IActionResult Index()
    {
        var tarefas = _db.Tarefas.ToList();
        return StatusCode(200, tarefas);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TarefaDto tarefaDto)
    {
        if (string.IsNullOrEmpty(tarefaDto.Titulo))
        {
            return StatusCode(400, new ErrorView
            {
                Mensagem = "O título da tarefa é obrigatório.",
            });
        }
        var tarefa = new Tarefa
        {
            Titulo = tarefaDto.Titulo,
            Descricao = tarefaDto.Descricao,
            Concluida = tarefaDto.Concluida
        };
        _db.Tarefas.Add(tarefa);
        _db.SaveChanges();

        return StatusCode(201, tarefa);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] TarefaDto tarefaDto)
    {
        if (string.IsNullOrEmpty(tarefaDto.Titulo))
        {
            return StatusCode(400, new ErrorView
            {
                Mensagem = "O título da tarefa é obrigatório.",
            });
        }
        var tarefaDb = _db.Tarefas.Find(id);
        if (tarefaDb == null)
        {
            return StatusCode(404, new ErrorView
            {
                Mensagem = "Tarefa não encontrada.",
            });
        }
        tarefaDb.Titulo = tarefaDto.Titulo;
        tarefaDb.Descricao = tarefaDto.Descricao;
        tarefaDb.Concluida = tarefaDto.Concluida;

        _db.Tarefas.Update(tarefaDb);
        _db.SaveChanges();

        return StatusCode(201, tarefaDb);
    }


    [HttpDelete("{id}")]
    public IActionResult Destroy([FromRoute] int id)
    {
        var tarefaDb = _db.Tarefas.Find(id);
        if (tarefaDb == null)
        {
            return StatusCode(404, new ErrorView
            {
                Mensagem = "Tarefa não encontrada.",
            });
        }
    
        _db.Tarefas.Remove(tarefaDb);
        _db.SaveChanges();

        return StatusCode(204);
    }


}
