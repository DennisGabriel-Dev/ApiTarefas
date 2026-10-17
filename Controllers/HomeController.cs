using Microsoft.AspNetCore.Mvc;
using ApiTarefas.ModelViews;
namespace ApiTarefas.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    public HomeController()
    {
    }

    [HttpGet]
    public HomeView Index()
    {
       return new HomeView
       {
            Mensagem = "Bem-vindo à API de Tarefas!",
            Documentacao = "/swagger",
            NomeUsuario = "Dennis"
       };
    }
}
