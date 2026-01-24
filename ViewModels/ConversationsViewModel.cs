using SentireChat.Models;
using SentireChat.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SentireChat.ViewModels
{
    public sealed class ConversationsViewModel : BaseViewModel
    {
        private readonly ApiClient _api;

        public ObservableCollection<ConversationSummaryDto> Items { get; } = new();

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public ICommand RefreshCommand { get; }
        public ICommand OpenCommand { get; }

        public ConversationsViewModel(ApiClient api)
        {
            _api = api;

            RefreshCommand = new Command(async () => await SafeLoadAsync());
            OpenCommand = new Command<ConversationSummaryDto>(async (c) =>
            {
                if (c is null) return;

                await Shell.Current.GoToAsync("messages",
                    new Dictionary<string, object> { ["ConversationId"] = c.Id });
            });
        }

        private bool _hasLoaded;
        public bool HasLoaded
        {
            get => _hasLoaded;
            set => SetProperty(ref _hasLoaded, value);
        }

        private async Task SafeLoadAsync()
        {
            try
            {
                await LoadAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CRASH SafeLoadAsync: " + ex);
                // aqui você pode setar um StatusText / toast
            }
        }

        public async Task LoadAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                IsRefreshing = true;

                var list = await _api.GetConversationsAsync();
                Items.Clear();
                if (list != null)
                {
                    foreach (var c in list.OrderByDescending(x => x.LastMessageAtUtc))
                    {
                        Items.Add(c);
                    }

                }
            }
            catch (Exception ex)
            {
                // coloque seu StatusText/toast
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
        }
    }
}
