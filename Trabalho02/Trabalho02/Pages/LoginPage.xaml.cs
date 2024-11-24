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
            _databaseService = databaseService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text?.Trim();
            var password = PasswordEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var user = await _databaseService.GetUserByUsernameAsync(username);

                if (user != null && user.Password == password)
                {
                    await DisplayAlert("Sucesso", "Login realizado com sucesso!", "OK");
                    await Navigation.PushAsync(new ProjectListPage(_databaseService));
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
            var username = UsernameEntry.Text?.Trim();
            var password = PasswordEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var existingUser = await _databaseService.GetUserByUsernameAsync(username);
                if (existingUser == null)
                {
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
