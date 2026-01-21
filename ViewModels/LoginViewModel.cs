using System.Windows.Input;
using SentireChat.Services;

namespace SentireChat.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly IAuthService _auth;

    private string _statusText = "Faça login para acessar as conversas.";
    public string StatusText
    {
        get => _statusText;
        set => SetProperty(ref _statusText, value);
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private string _busyText = "Carregando...";
    public string BusyText
    {
        get => _busyText;
        set => SetProperty(ref _busyText, value);
    }

    private bool _isLogged;
    public bool IsLogged
    {
        get => _isLogged;
        set
        {
            if (SetProperty(ref _isLogged, value))
            {
                OnPropertyChanged(nameof(ShowLogin));
                OnPropertyChanged(nameof(ShowLogout));
            }
        }
    }

    public bool ShowLogin => !IsLogged;
    public bool ShowLogout => IsLogged;

    public ICommand LoginCommand { get; }
    public ICommand LogoutCommand { get; }

    public LoginViewModel(IAuthService auth)
    {
        _auth = auth;

        LoginCommand = new Command(async () => await LoginAsync(), () => !IsBusy);
        LogoutCommand = new Command(async () => await LogoutAsync(), () => !IsBusy);
    }

    public async Task InitializeAsync()
    {
        await RefreshUiAsync();
    }

    public async Task CheckLoginAsync()
    {
        try
        {
            SetBusy(true, "Verificando sessão...");
            IsLogged = await _auth.IsLoggedAsync();

            if (IsLogged)
            {
                StatusText = "Login realizado com sucesso! ✅";
                await Shell.Current.GoToAsync("//conversations");
            }
            else
            {
                StatusText = "Faça login para acessar as conversas.";
            }
        }
        catch (Exception ex)
        {
            StatusText = $"Erro: {ex.Message}";
            IsLogged = false;
        }
        finally
        {
            SetBusy(false);
        }
    }



    private async Task RefreshUiAsync()
    {
        try
        {
            SetBusy(true, "Verificando sessão...");
            IsLogged = await _auth.IsLoggedAsync();

            StatusText = IsLogged
                ? "Você já está logado. ✅"
                : "Faça login para acessar as conversas.";

            if (IsLogged)
                await Shell.Current.GoToAsync("//conversations");
        }
        catch (Exception ex)
        {
            StatusText = $"Erro: {ex.Message}";
            IsLogged = false;
        }
        finally
        {
            SetBusy(false);
        }
    }


    private async Task LoginAsync()
    {
        try
        {
            SetBusy(true, "Abrindo login...");
            var result = await _auth.LoginAsync();

            if (!result.Success)
            {
                StatusText = $"Falha no login: {result.Error}";
                return;
            }

            IsLogged = true;
            StatusText = "Login realizado com sucesso! ✅";
            // depois: navegar para conversas
            await Shell.Current.GoToAsync("//conversations");

        }
        catch (Exception ex)
        {
            StatusText = $"Falha no login: {ex.Message}";
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async Task LogoutAsync()
    {
        try
        {
            SetBusy(true, "Saindo...");
            await _auth.LogoutAsync();
            await RefreshUiAsync();
        }
        catch (Exception ex)
        {
            StatusText = $"Falha ao sair: {ex.Message}";
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void SetBusy(bool busy, string? text = null)
    {
        IsBusy = busy;
        if (!string.IsNullOrWhiteSpace(text))
            BusyText = text;

        ((Command)LoginCommand).ChangeCanExecute();
        ((Command)LogoutCommand).ChangeCanExecute();
    }
}