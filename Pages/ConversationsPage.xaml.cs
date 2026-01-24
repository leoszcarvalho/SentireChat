using SentireChat.ViewModels;

namespace SentireChat.Pages;

public partial class ConversationsPage : ContentPage
{
    public ConversationsPage(ConversationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((ConversationsViewModel)BindingContext).LoadAsync();
    }
}