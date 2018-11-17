using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;

namespace HelloXarmain
{
    [Activity(Label = "Hello Xarmain", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        HttpClient _client = new HttpClient();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { button.Text = $"{count++} clicks!"; };
            Button btnSend = FindViewById<Button>(Resource.Id.btnSend);

            btnSend.Click += delegate {
                this.SendRequest();
            };
        }

        private async void SendRequest()
        {
            EditText etUrl = FindViewById<EditText>(Resource.Id.editTextUrl);
            if (string.IsNullOrEmpty(etUrl.Text.Trim()))
            {
                etUrl.Text = "https://google.com";
            }
            var resp = await _client.GetAsync(etUrl.Text);
            var message = string.Format("StatusCode is {0}", resp.StatusCode.ToString());
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }
    }
}

