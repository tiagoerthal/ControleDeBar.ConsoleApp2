using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers;

public class MesaController : Controller
{
    private readonly RepositorioMesaEmArquivo repositorioMesa;

    public MesaController()
    {
        ContextoDados contexto = new ContextoDados(carregarDados: true);

        repositorioMesa = new RepositorioMesaEmArquivo(contexto);
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Mesa> mesas = repositorioMesa.SelecionarRegistros();

        VisualizarMesasViewModel visualizarVm = new VisualizarMesasViewModel(mesas);

        return View(visualizarVm);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        CadastrarMesaViewModel cadastrarVm = new CadastrarMesaViewModel();

        return View(cadastrarVm);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarMesaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        var entidade = new Mesa(cadastrarVm.Numero, cadastrarVm.Capacidade);

        repositorioMesa.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }
}