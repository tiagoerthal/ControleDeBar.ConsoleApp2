﻿

using ControleDeBar.ConsoleApp2.Compartilhado;

namespace ControleDeBar.ConsoleApp2.ModuloProduto
{
    public class Produto : EntidadeBase<Produto>
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public Produto(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
        }

        public override void AtualizarRegistro(Produto registroAtualizado)
        {
            Nome = registroAtualizado.Nome;
            Valor = registroAtualizado.Valor;
        }

        public override string Validar()
        {
            string erros = string.Empty;

            if (Nome.Length< 2 || Nome.Length> 100)
             erros += "O campo \"Nome\" deve conter entre 2 e 100 caracteres.";

            if (Valor == 0.0m)
             erros += "O campo \"Valor\" deve conter um número positivo.";

            return erros;
        }
}
}
