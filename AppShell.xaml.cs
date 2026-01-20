namespace SentireChat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("messages", typeof(Pages.MessagesPage));

        }
    }
}
