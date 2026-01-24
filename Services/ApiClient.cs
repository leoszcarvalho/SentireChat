using System.Net.Http.Json;
using SentireChat.Models;

namespace SentireChat.Services;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public Task<List<ConversationSummaryDto>?> GetConversationsAsync()
        => _http.GetFromJsonAsync<List<ConversationSummaryDto>>("api/conversations");

    public Task<List<MessageItemDto>?> GetMessagesAsync(int conversationId)
        => _http.GetFromJsonAsync<List<MessageItemDto>>($"api/conversations/{conversationId}/messages");

    public async Task SendReplyAsync(int conversationId, string text)
    {
        var body = new SendReplyRequest { Text = text };
        var resp = await _http.PostAsJsonAsync($"api/conversationreply/{conversationId}/reply", body);
        resp.EnsureSuccessStatusCode();
    }
}