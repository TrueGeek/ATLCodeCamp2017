using BringMeSoup.mobile.Pages;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BringMeSoup.mobile.ViewModels
{

    class SickViewModel : BaseViewModel
    {

        public string SickCode { get; set; }

        private bool _requestIsPending;
        public bool RequestIsPending
        {
            get => _requestIsPending;
            set => SetProperty<bool>(ref _requestIsPending, value);
        }
        public string CurrentUserEmail { get; set; }

        public SickViewModel()
        {
            SendRequestCanExecute = true;
            SickCode = Helpers.Settings.SickCode;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => GetRequests());
            CurrentUserEmail = Helpers.Settings.EmailAddress;
        }

        public bool SendRequestCanExecute { get; set; }
        public ICommand SendRequest
        {
            get
            {
                return new Command(async () => await SendRequestExecutor(), () => SendRequestCanExecute);
            }
        }
        public async Task SendRequestExecutor()
        {
            SendRequestCanExecute = false;

            var content = new StringContent("");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.AccessToken);

            var url = "http://bringmesoupapi.azurewebsites.net/api/requests";

            var response = await client.PostAsync(url, content);

            SendRequestCanExecute = true;
        }

        public ICommand LogOff
        {
            get
            {
                return new Command(async () =>
                {

                    Helpers.Settings.ClearAllValues();

                    await Navigation.PushAsync(new LogonPage());

                });
            }
        }

        private bool GetRequests()
        {

            if (string.IsNullOrEmpty(Helpers.Settings.AccessToken))
            {
                return false;   // stop the timer, the user has logged off
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.AccessToken);

            var url = "http://bringmesoupapi.azurewebsites.net/api/requests";

            var response = client.GetStringAsync(url).Result;
            var requests = JsonConvert.DeserializeObject<List<Request>>(response);

            if (requests.Any())
            {
                RequestIsPending = true;
            }
            else
            {
                RequestIsPending = false;
            }

            return true;    // keep the timer going

        }

    }

}
