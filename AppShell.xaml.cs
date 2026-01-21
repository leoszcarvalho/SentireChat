using SentireChat.Pages;

namespace SentireChat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("conversations", typeof(ConversationsPage));
            Routing.RegisterRoute("messages", typeof(MessagesPage));

        }
    }
}
