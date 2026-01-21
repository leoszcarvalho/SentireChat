using SentireChat.Pages;

namespace SentireChat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("messages", typeof(Pages.MessagesPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));

        }
    }
}
