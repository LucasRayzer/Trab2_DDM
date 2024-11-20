using Trabalho02.Database;
using Trabalho02.Model;

namespace Trabalho02.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public LoginPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService; // Injeta a instância do serviço de banco de dados
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Captura as entradas do usuário
            var username = UsernameEntry.Text?.Trim();
            var password = PasswordEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Busca o usuário no banco de dados
                var user = await _databaseService.GetUserByUsernameAsync(username);

                if (user != null && user.Password == password)
                {
                    // Login bem-sucedido
                    await DisplayAlert("Sucesso", "Login realizado com sucesso!", "OK");
                    await Navigation.PushAsync(new ProjectListPage(_databaseService)); // Redireciona para a página de projetos
                }
                else
                {
                    await DisplayAlert("Erro", "Credenciais inválidas.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro", "Por favor, insira suas credenciais.", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Captura as entradas do usuário
            var username = UsernameEntry.Text?.Trim();
            var password = PasswordEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Verifica se o usuário já existe
                var existingUser = await _databaseService.GetUserByUsernameAsync(username);
                if (existingUser == null)
                {
                    // Salva o novo usuário
                    var newUser = new User { Username = username, Password = password };
                    await _databaseService.SaveUserAsync(newUser);

                    await DisplayAlert("Sucesso", "Usuário registrado com sucesso!", "OK");
                }
                else
                {
                    await DisplayAlert("Erro", "Este nome de usuário já está em uso.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro", "Por favor, insira um nome de usuário e uma senha.", "OK");
            }
        }
    }
}
