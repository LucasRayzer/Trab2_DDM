namespace DDM_T2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Registrar rotas adicionais
            Routing.RegisterRoute("CadastroTarefaPage", typeof(CadastroTarefaPage));
        }
    }
}
