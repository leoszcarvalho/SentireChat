using SentireChat.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace SentireChat.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ConversationSummary>> GetConversationsAsync()
            => await _http.GetFromJsonAsync<List<ConversationSummary>>("api/conversations")
               ?? [];

        public async Task<List<MessageItem>> GetMessagesAsync(int conversationId)
            => await _http.GetFromJsonAsync<List<MessageItem>>(
                $"api/conversations/{conversationId}/messages") ?? [];

        public async Task ReplyAsync(int conversationId, string text)
        {
            var payload = new ReplyRequest
            {
                ConversationId = conversationId,
                Text = text
            };

            var resp = await _http.PostAsJsonAsync("api/conversationreply", payload);
            resp.EnsureSuccessStatusCode();
        }
    }
}
