

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
