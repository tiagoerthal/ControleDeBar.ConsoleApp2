

using ControleDeBar.ConsoleApp2.Compartilhado;
using ControleDeBar.ConsoleApp2.ModuloProduto;

namespace ControleDeBar.ConsoleApp2.ModuloProduto
{
    public class TelaProduto : TelaBase<Produto>, ITela
    {
        public TelaProduto(RepositorioProduto repositorio) : base("Produto", repositorio)
        {

        }

        public override void CadastrarRegistro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"             Cadastro de {nomeEntidade}");
            Console.Write("------------------------------------------");
            Console.ResetColor();

            Console.WriteLine();

            Produto novoRegistro = ObterDados();

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
            Produto[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Produto amigoRegistrado = registros[i];

                if (amigoRegistrado == null)
                    continue;

                if (amigoRegistrado.Nome == novoRegistro.Nome)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Um produto com este nome já foi cadastrado!");
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

            Produto registroAtualizado = ObterDados();

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

            Produto[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Produto garconRegistrado = registros[i];

                if (garconRegistrado == null)
                    continue;

                if (
                    garconRegistrado.Id != idSelecionado &&
                    garconRegistrado.Nome == registroAtualizado.Nome)

                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Um produto com este nome já foi cadastrado!");
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

            Console.WriteLine("Visualização de Produtos");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -30} | {2, -30}", "Id", "Nome", "Valor");

            Produto[] produtos = repositorio.SelecionarRegistros();

            for (int i = 0; i < produtos.Length; i++)
            {
                Produto p = produtos[i];

                if (p == null)
                    continue;

                Console.WriteLine("{0, -10} | {1, -30} | {2, -30}", p.Id, p.Nome, p.Valor.ToString("C2"));
            }

            ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        protected override Produto ObterDados()
        {
            string nome = string.Empty;

            while (string.IsNullOrWhiteSpace(nome))
            {
                 Console.Write("Digite o nome do produto: ");
                 nome = Console.ReadLine()!;

                 if (string.IsNullOrWhiteSpace(nome))
                 {
                     ApresentarMensagem("Digite um nome válido!", ConsoleColor.DarkYellow);
                     Console.Clear();
                 }
            }

            bool conseguiuConverterValor = false;

            decimal valor = 0.0m;

            while (!conseguiuConverterValor)
            {
                 Console.Write("Digite o valor do produto: ");
                 conseguiuConverterValor = decimal.TryParse(Console.ReadLine(), out valor);

                 if (!conseguiuConverterValor)
                 {
                     ApresentarMensagem("Digite um valor numérico válido!", ConsoleColor.DarkYellow);
                     Console.Clear();
                 }
            }

            return new Produto(nome, valor);
        }
    }
}
