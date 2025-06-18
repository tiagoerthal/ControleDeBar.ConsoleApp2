﻿
using ControleDeBar.ConsoleApp2.Compartilhado;

namespace ControleDeBar.ConsoleApp2.ModuloMesa
{
    public class Mesa : EntidadeBase<Mesa>
    {
        public int Numero { get; set; }
        public int Capacidade { get; set; }
        public bool EstaOcupada { get; set; }

        public Mesa(int numero, int capacidade)
        {
            Numero = numero;
            Capacidade = capacidade;
            EstaOcupada = false;
        }

        public void Ocupar()
        {
            EstaOcupada = true;
        }

        public void Desocupar()
        {
            EstaOcupada = false;
        }

        public override void AtualizarRegistro(Mesa registroAtualizado)
        {
            Numero = registroAtualizado.Numero;
            Capacidade = registroAtualizado.Capacidade;
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (Numero < 1)
                erros += "O campo \"Número\" deve conter um valor maior que 0.";

            if (Capacidade < 1)
                erros += "O campo \"Capacidade\" deve conter um valor maior que 0.";

            return erros;
        }
    }
}
