
using System;
using ControleDeBar.ConsoleApp2.Compartilhado;
using ControleDeBar.ConsoleApp2.ModuloMesa;

namespace ControleDeBar.ConsoleApp2.ModuloGarcom
{
    public class TelaGarcom : TelaBase<Garcom>, ITela
    {
        public TelaGarcom(RepositorioGarcom repositorio) : base("Garçom", repositorio)
        {
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.Write("------------------------------------------");

            Console.WriteLine();

            Garcom novoRegistro = ObterDados();

            string erros = novoRegistro.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);

                CadastrarRegistro();

                return;
            }
            Garcom[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Garcom garcomRegistrado = registros[i];

                if (garcomRegistrado == null)
                    continue;

                if (garcomRegistrado.Nome == novoRegistro.Nome || garcomRegistrado.Cpf == novoRegistro.Cpf)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Um amigo com este nome ou CPF já foi cadastrado!");
                    Console.Write("------------------------------------------");
                    Console.ResetColor();

                    ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);

                    CadastrarRegistro();
                    return;
                }
            }

            repositorio.CadastrarRegistro(novoRegistro);

            Console.Clear();
            Console.Write("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ResetColor();
            Console.Write("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.ResetColor();
            Console.Write("------------------------------------------");
            Console.ReadLine();
        }
        public override void EditarRegistro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"            Edição de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();

            VisualizarRegistros(false);
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.Clear();
            Console.WriteLine();
            Console.Write("Digite o id do registro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"            Edição de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();

            Garcom registroAtualizado = ObterDados();

            string erros = registroAtualizado.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);

                EditarRegistro();

                return;
            }

            Garcom[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Garcom garcomRegistrado = registros[i];

                if (garcomRegistrado == null)
                    continue;

                if (
                    garcomRegistrado.Id != idSelecionado &&
                    garcomRegistrado.Nome == registroAtualizado.Nome ||
                    garcomRegistrado.Cpf == registroAtualizado.Cpf)

                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("------------------------------------------");
                    Console.WriteLine("Um garçom com este nome ou CPF já foi cadastrado!");
                    Console.Write("------------------------------------------");

                    Console.ResetColor();

                    ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);

                    EditarRegistro();

                    return;
                }
            }

            repositorio.EditarRegistro(idSelecionado, registroAtualizado);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"{nomeEntidade} editado com sucesso!");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.Write("------------------------------------------");
            Console.WriteLine();

            Console.ResetColor();
            Console.ReadLine();
        }
        public override void VisualizarRegistros(bool exibirCabecalho)
        {
            if (exibirCabecalho)
                ExibirCabecalho();

            Console.WriteLine("Visualização de Garçons");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -30} | {2, -30}",
                "Id", "Nome", "CPF"
            );

            Garcom[] garcons = repositorio.SelecionarRegistros();

            for (int i = 0; i < garcons.Length; i++)
            {
                Garcom g = garcons[i];

                if (g == null)
                    continue;

                Console.WriteLine(
                            "{0, -10} | {1, -30} | {2, -30}",
                    g.Id, g.Nome, g.Cpf
                );
            }

            ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        protected override Garcom ObterDados()
        {
            string nome = string.Empty;

            while (string.IsNullOrWhiteSpace(nome))
            {
                Console.Write("Digite o nome do garçom: ");
                nome = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(nome))
                {
                    ApresentarMensagem("Digite um nome válido!", ConsoleColor.DarkYellow);
                    Console.Clear();
                }
            }

            string cpf = string.Empty; 

            while (string.IsNullOrWhiteSpace(cpf))
            {
                Console.Write("Digite o CPF do garçom: ");
                cpf = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(cpf))
                {
                    ApresentarMensagem("Digite um CPF válido!", ConsoleColor.DarkYellow);
                    Console.Clear();
                }
            }

            return new Garcom(nome, cpf);
        }
    }
}
