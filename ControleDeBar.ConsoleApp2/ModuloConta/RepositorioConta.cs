

namespace ControleDeBar.ConsoleApp2.ModuloConta
{
    public class RepositorioConta
    {
        protected Conta[] registros = new Conta[100];
        protected int contadorRegistros = 0;
        protected int contadorIds = 0;

        public void Cadastrar(Conta novaConta)
        {
            novaConta.Id = ++contadorIds;

            registros[contadorRegistros++] = novaConta;
        }

        public Conta[] SelecionarContas()
        {
            return registros;
        }

        public Conta[] SelecionarContasPorData(DateTime dataFaturamento)
        {
            int qtdContasDoDia = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;

                if (registros[i].Fechamento.Date == dataFaturamento.Date)
                    qtdContasDoDia++;
            }

            Conta[] contasDoDia = new Conta[qtdContasDoDia];

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;

                if (registros[i].Fechamento.Date == dataFaturamento.Date)
                    contasDoDia[i] = registros[i];
            }

            return contasDoDia;
        }

        public Conta[] SelecionarContasEmAberto()
        {
            // contar as contas em aberto
            int qtdContasEmAberto = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;

                if (registros[i].EstaAberta)
                    qtdContasEmAberto++;
            }

            Conta[] contasEmAberto = new Conta[qtdContasEmAberto];

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;

                if (registros[i].EstaAberta)
                    contasEmAberto[i] = registros[i];
            }

            // retornar apenas as contas em aberto
            return contasEmAberto;
        }

        public Conta[] SelecionarContasFechadas()
        {
            // contar as contas fechadas
            int qtdContasFechadas = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;

                if (!registros[i].EstaAberta)
                    qtdContasFechadas++;
            }

            Conta[] contasFechadas = new Conta[qtdContasFechadas];

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;

                if (!registros[i].EstaAberta)
                    contasFechadas[i] = registros[i];
            }

            // retornar apenas as contas fechadas
            return contasFechadas;

        }

        public Conta SelecionarContaPorId(int idSelecionado)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                Conta registro = registros[i];

                if (registro == null)
                    continue;

                if (registro.Id == idSelecionado)
                    return registro;
            }

            return null;
        }
    }
}
