using Microsoft.AspNetCore.Mvc;
using ApiTarefas.Dto;
using ApiTarefas.Services.Interfaces;
using ApiTarefas.Models.Errors;
namespace ApiTarefas.Controllers;

[ApiController]
[Route("/tarefas")]
public class TarefasController : ControllerBase
{
    public TarefasController(ITarefaService tarefaService)
    {
        _service = tarefaService;
    }

    private ITarefaService _service;

    [HttpGet]
    public IActionResult Index(int page = 1)
    {
        var tarefas = _service.Lista(page);
        return StatusCode(200, tarefas);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TarefaDto tarefaDto)
    {
        try 
        {
            var tarefa = _service.Incluir(tarefaDto);
            return StatusCode(201, tarefa);
        } catch (TarefaError e)
        {
            return StatusCode(400, new TarefaError(e.Message));
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] TarefaDto tarefaDto)
    {
        try
        {
            var tarefa = _service.Atualizar(id ,tarefaDto);
            return StatusCode(201, tarefa);
        } 
        catch ( TarefaError e)
        {
            return StatusCode(400, new TarefaError(e.Message));   
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Destroy([FromRoute] int id)
    {
        try
        {
            _service.Excluir(id);
            return StatusCode(204);
        } catch (TarefaError e)
        {
            return StatusCode(400, new TarefaError(e.Message));
        }

    }


    [HttpGet("{id}")]
    public IActionResult Show([FromRoute] int id)
    {
        try
        {
            var tarefa = _service.Show(id);
            return StatusCode(200, tarefa);
        } catch (TarefaError e)
        {
            return StatusCode(400, new TarefaError(e.Message));
        }

    }
}
