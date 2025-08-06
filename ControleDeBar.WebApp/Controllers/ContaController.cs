using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloConta;
using ControleDeBar.Infraestrutura.Arquivos.ModuloGarcom;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.ModuloProduto;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers;

public class ContaController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly RepositorioContaEmArquivo repositorioConta;
    private readonly RepositorioMesaEmArquivo repositorioMesa;
    private readonly RepositorioGarcomEmArquivo repositorioGarcom;
    private readonly RepositorioProdutoEmArquivo repositorioProduto;

    public ContaController()
    {
        contextoDados = new ContextoDados(true);

        repositorioConta = new RepositorioContaEmArquivo(contextoDados);
        repositorioMesa = new RepositorioMesaEmArquivo(contextoDados);
        repositorioGarcom = new RepositorioGarcomEmArquivo(contextoDados);
        repositorioProduto = new RepositorioProdutoEmArquivo(contextoDados);
    }


    [HttpGet]
    public IActionResult Index()
    {
        List<Conta> contas = repositorioConta.SelecionarRegistros();

        VisualizarContasViewModel visualizarContasVm = new VisualizarContasViewModel(contas);

        return View(visualizarContasVm);
    }

    [HttpGet]
    public IActionResult Abrir()
    {
        List<Mesa> mesas = repositorioMesa.SelecionarRegistros();
        List<Garcom> garcons = repositorioGarcom.SelecionarRegistros();

        AbrirContaViewModel abrirContaVm = new AbrirContaViewModel(mesas, garcons);

        return View(abrirContaVm);
    }

    [HttpPost]
    public IActionResult Abrir(AbrirContaViewModel abrirVM)
    {
        if (!ModelState.IsValid)
            return View(abrirVM);

        Mesa mesaSelecionada = repositorioMesa.SelecionarRegistroPorId(abrirVM.MesaId);
        Garcom garcomSelecionado = repositorioGarcom.SelecionarRegistroPorId(abrirVM.GarcomId);

        Conta conta = new Conta(
            abrirVM.Titular,
            mesaSelecionada,
            garcomSelecionado
        );

        repositorioConta.CadastrarRegistro(conta);

        return RedirectToAction(nameof(Index));
    }
}