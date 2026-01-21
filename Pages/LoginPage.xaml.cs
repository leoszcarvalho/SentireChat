using SentireChat.Services;

namespace SentireChat.Pages;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _auth;

    public LoginPage(AuthService auth)
    {
        InitializeComponent();
        _auth = auth;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RefreshUiAsync();
    }

    private async Task RefreshUiAsync()
    {
        try
        {
            SetBusy(true, "Verificando sessão...");
            var logged = await _auth.IsLoggedAsync();

            if (logged)
            {
                StatusLabel.Text = "Você já está logado. ✅";
                LoginButton.IsVisible = false;
                LogoutButton.IsVisible = true;

                // Se você já tiver a tela de conversas, descomente:
                // await Shell.Current.GoToAsync("//conversations");
            }
            else
            {
                StatusLabel.Text = "Faça login para acessar as conversas.";
                LoginButton.IsVisible = true;
                LogoutButton.IsVisible = false;
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Erro: {ex.Message}";
            LoginButton.IsVisible = true;
            LogoutButton.IsVisible = false;
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            SetBusy(true, "Abrindo login...");
            _ = await _auth.GetTokenAsync(interactive: true);

            StatusLabel.Text = "Login realizado com sucesso! ✅";
            LoginButton.IsVisible = false;
            LogoutButton.IsVisible = true;

            // Se você já tiver a tela de conversas, descomente:
            // await Shell.Current.GoToAsync("//conversations");
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Falha no login: {ex.Message}";
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        try
        {
            SetBusy(true, "Saindo...");
            await _auth.LogoutAsync();
            await RefreshUiAsync();
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Falha ao sair: {ex.Message}";
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void SetBusy(bool isBusy, string? message = null)
    {
        LoadingOverlay.IsVisible = isBusy;
        LoginButton.IsEnabled = !isBusy;
        LogoutButton.IsEnabled = !isBusy;

        if (!string.IsNullOrWhiteSpace(message))
            LoadingText.Text = message;
    }
}
