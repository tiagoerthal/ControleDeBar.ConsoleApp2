
namespace ControleDeBar.ConsoleApp2.Compartilhado
{
    public interface ITela
    {
        char ApresentarMenu();
        void CadastrarRegistro();
        void EditarRegistro();
        void ExcluirRegistro();
        void VisualizarRegistros(bool exibirCabecalho);
    }
}