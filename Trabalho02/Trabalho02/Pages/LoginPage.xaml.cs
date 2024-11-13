namespace Trabalho02.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Lógica de autenticação (simplificada para exemplo)
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Redirecionar para a lista de projetos após o login
                await Navigation.PushAsync(new ProjectListPage());
            }
            else
            {
                await DisplayAlert("Erro", "Por favor, insira suas credenciais", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Lógica de cadastro (simplificada)
            await DisplayAlert("Registro", "Usuário registrado com sucesso", "OK");
        }
    }

}
