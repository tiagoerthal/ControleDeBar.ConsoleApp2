
using ControleDeBar.ConsoleApp2.Compartilhado;

namespace ControleDeBar.ConsoleApp2.ModuloMesa
{
    public class TelaMesa : TelaBase<Mesa>, ITela
    {
        public TelaMesa(RepositorioMesa repositorioMesa) : base("Mesa", repositorioMesa)
        {
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.Write("------------------------------------------");

            Console.WriteLine();

            Mesa novoRegistro = ObterDados();

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
            Mesa[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Mesa mesaRegistrada = registros[i];

                if (mesaRegistrada == null)
                    continue;

                if (mesaRegistrada.Numero == novoRegistro.Numero)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Uma mesa com este número já foi cadastrado!");
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

            Mesa registroAtualizado = ObterDados();

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
            Mesa[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Mesa mesaRegistrado = registros[i];

                if (mesaRegistrado == null)
                    continue;

                if (
              mesaRegistrado.Id != idSelecionado && mesaRegistrado.Numero == registroAtualizado.Numero || mesaRegistrado.Capacidade == registroAtualizado.Capacidade || mesaRegistrado.EstaOcupada == registroAtualizado.EstaOcupada)

                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Uma mesa com este número já foi cadastrado!");
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

            Console.WriteLine("Visualização de Mesas");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -30}",
                "Id", "Número", "Capacidade", "Status"
            );

            Mesa[] mesas = repositorio.SelecionarRegistros();

            for (int i = 0; i < mesas.Length; i++)
            {
                Mesa m = mesas[i];

                if (m == null)
                    continue;

                string statusMesa = m.EstaOcupada ? "Ocupada" : "Disponível";

                Console.WriteLine(
                  "{0, -10} | {1, -20} | {2, -30}",
                    m.Id, m.Numero, m.Capacidade, statusMesa
                );
            }

            ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        protected override Mesa ObterDados()
        {
            bool conseguiuConverterNumero = false;

            int numero = 0;

            while (!conseguiuConverterNumero)
            {
                Console.Write("Digite o número da mesa: ");
                conseguiuConverterNumero = int.TryParse(Console.ReadLine(), out numero);

                if (!conseguiuConverterNumero)
                {
                    ApresentarMensagem("Digite um número válido!", ConsoleColor.DarkYellow);
                    Console.Clear();
                }
            }

            bool conseguiuConverterCapacidade = false;

            int capacidade = 0;

            while (!conseguiuConverterCapacidade)
            {
                Console.Write("Digite a capacidade da mesa: ");
                conseguiuConverterCapacidade = int.TryParse(Console.ReadLine(), out capacidade);

                if (!conseguiuConverterNumero)
                {
                    ApresentarMensagem("Digite um número válido!", ConsoleColor.DarkYellow);
                    Console.Clear();
                }
            }

            return new Mesa(numero, capacidade);
        }
    }
}
