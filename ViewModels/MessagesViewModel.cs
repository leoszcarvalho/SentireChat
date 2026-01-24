using System.Collections.ObjectModel;
using System.Windows.Input;
using SentireChat.Models;
using SentireChat.Services;

namespace SentireChat.ViewModels;

[QueryProperty(nameof(ConversationId), "ConversationId")]
public sealed class MessagesViewModel : BaseViewModel
{
    private readonly ApiClient _api;

    public ObservableCollection<MessageItemDto> Items { get; } = new();

    private int _conversationId;
    public int ConversationId
    {
        get => _conversationId;
        set => SetProperty(ref _conversationId, value);
    }

    private string _textToSend = "";
    public string TextToSend
    {
        get => _textToSend;
        set => SetProperty(ref _textToSend, value);
    }

    public ICommand RefreshCommand { get; }
    public ICommand SendCommand { get; }

    public MessagesViewModel(ApiClient api)
    {
        _api = api;

        RefreshCommand = new Command(async () => await LoadAsync());
        SendCommand = new Command(async () => await SendAsync(), () => !IsBusy);
    }

    public async Task LoadAsync()
    {
        if (IsBusy || ConversationId <= 0) return;

        try
        {
            IsBusy = true;
            var list = await _api.GetMessagesAsync(ConversationId);

            Items.Clear();
            foreach (var m in list.OrderBy(x => x.TimestampUtc))
                Items.Add(m);
        }
        finally
        {
            IsBusy = false;
            ((Command)SendCommand).ChangeCanExecute();
        }
    }

    private async Task SendAsync()
    {
        if (ConversationId <= 0) return;
        if (string.IsNullOrWhiteSpace(TextToSend)) return;

        try
        {
            IsBusy = true;
            ((Command)SendCommand).ChangeCanExecute();

            var text = TextToSend.Trim();
            TextToSend = "";

            await _api.SendReplyAsync(ConversationId, text);
            await LoadAsync();
        }
        finally
        {
            IsBusy = false;
            ((Command)SendCommand).ChangeCanExecute();
        }
    }
}
