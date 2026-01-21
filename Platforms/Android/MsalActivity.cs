using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace SentireChat;

[Activity(Exported = true)]
[IntentFilter(
    new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msaldce1a0f1-2736-4697-9db3-20ee0de5d62d")]
public class MsalActivity : BrowserTabActivity
{
}