
using ControleDeBar.ConsoleApp2.Compartilhado;
using ControleDeBar.ConsoleApp2.ModuloGarcom;
using ControleDeBar.ConsoleApp2.ModuloMesa;
using ControleDeBar.ConsoleApp2.ModuloProduto;

namespace ControleDeBar.ConsoleApp2.ModuloConta
{
    public class TelaConta : ITela
    {
        private RepositorioConta repositorioConta;
        private RepositorioProduto repositorioProduto;
        private RepositorioMesa repositorioMesa;
        private RepositorioGarcom repositorioGarcom;

        public TelaConta(
            RepositorioConta repositorioConta,
            RepositorioProduto repositorioProduto,
            RepositorioMesa repositorioMesa,
            RepositorioGarcom repositorioGarcom
        )
        {
            this.repositorioConta = repositorioConta;
            this.repositorioProduto = repositorioProduto;
            this.repositorioMesa = repositorioMesa;
            this.repositorioGarcom = repositorioGarcom;
        }

        public char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine($"1 - Cadastro de Conta");
            Console.WriteLine($"2 - Gerênciar Pedidos da Conta");
            Console.WriteLine($"3 - Visualizar Contas");
            Console.WriteLine($"S - Sair");

            Console.WriteLine();

            Console.Write("Digite uma opção válida: ");
            char opcaoEscolhida = Console.ReadLine().ToUpper()[0];

            return opcaoEscolhida;
        }

        public void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine("Abertura de Conta");

            Console.WriteLine();

            Conta novaConta = ObterDados();

            string erros = novaConta.Validar();

            if (erros.Length > 0)
            {
                ApresentarMensagem(
                    string.Concat(erros, "\nDigite ENTER para continuar..."),
                    ConsoleColor.Red
                );

                CadastrarRegistro();

                return;
            }

            repositorioConta.Cadastrar(novaConta);

            ApresentarMensagem($"Conta aberta com sucesso!", ConsoleColor.Green);
        }

        public void ApresentarMenuGestaoPedidos()
        {
            throw new NotImplementedException();
        }

        public void EditarRegistro()
        {
        }

        public void ExcluirRegistro()
        {
        }

        public void VisualizarRegistros(bool exibirCabecalho)
        {
            if (exibirCabecalho)
                ExibirCabecalho();

            Console.WriteLine("Visualização de Contas");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -14} | {3, -20} | {4, -20} | {5, -20}",
                "Id", "Titular", "Mesa", "Garçom", "Abertura", "Status"
            );

            Conta[] contas = repositorioConta.SelecionarContas();

            for (int i = 0; i < contas.Length; i++)
            {
                Conta c = contas[i];

                if (c == null)
                    continue;

                string statusConta = c.EstaAberta ? "Aberta" : "Fechada";

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -14} | {3, -20} | {4, -20} | {5, -20}",
                    c.Id, c.Titular, c.Mesa.Numero, c.Garcom.Nome, c.Abertura.ToShortDateString(), statusConta
                );
            }

            ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        private void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|           Controle de Bar            |");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
        }

        private void ApresentarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;

            Console.WriteLine();
            Console.WriteLine(mensagem);

            Console.ResetColor();

            Console.ReadLine();
        }

        private Conta ObterDados()
        {
            string titular = string.Empty;

            while (string.IsNullOrWhiteSpace(titular))
            {
                Console.Write("Digite o nome do titular da conta: ");
                titular = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(titular))
                {
                    ApresentarMensagem("O titular é obrigatório!", ConsoleColor.DarkYellow);
                    Console.Clear();
                }
            }

            VisualizarMesas();

            Console.WriteLine();

            Console.Write("Digite o ID da mesa que deseja ocupar: ");
            int idMesa = Convert.ToInt32(Console.ReadLine());

            Mesa mesaSelecionada = repositorioMesa.SelecionarRegistroPorId(idMesa);

            VisualizarGarcons();

            Console.WriteLine();

            Console.Write("Digite o ID do garçom que atenderá a mesa: ");
            int idGarcom = Convert.ToInt32(Console.ReadLine());

            Garcom garcomSelecionado = repositorioGarcom.SelecionarRegistroPorId(idGarcom);

            return new Conta(titular, mesaSelecionada, garcomSelecionado);
        }

        private void VisualizarMesas()
        {
            Console.WriteLine();

            Console.WriteLine("Visualização de Mesas");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -30}",
                "Id", "Número", "Capacidade", "Status"
            );

            Mesa[] mesas = repositorioMesa.SelecionarRegistros();

            for (int i = 0; i < mesas.Length; i++)
            {
                Mesa m = mesas[i];

                if (m == null)
                    continue;

                string statusMesa = m.EstaOcupada ? "Ocupada" : "Disponível";

                Console.WriteLine(
                  "{0, -10} | {1, -20} | {2, -20} | {3, -30}",
                    m.Id, m.Numero, m.Capacidade, statusMesa
                );
            }
        }

        private void VisualizarGarcons()
        {
            Console.WriteLine();

            Console.WriteLine("Visualização de Garçons");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -30} | {2, -30}", "Id", "Nome", "CPF");

            Garcom[] garcons = repositorioGarcom.SelecionarRegistros();

            for (int i = 0; i < garcons.Length; i++)
            {
                Garcom g = garcons[i];

                if (g == null)
                    continue;

                Console.WriteLine("{0, -10} | {1, -30} | {2, -30}", g.Id, g.Nome, g.Cpf);
            }
        }
    }
}
