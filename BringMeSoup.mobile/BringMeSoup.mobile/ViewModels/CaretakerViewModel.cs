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
    class CaretakerViewModel : BaseViewModel
    {

        public Guid? LatestRequestId { get; set; }
        public string AssociatedUserEmail { get; set; }
        public string CurrentUserEmail { get; set; }

        private bool _requestIsPending;
        public bool RequestIsPending
        {
            get => _requestIsPending;
            set => SetProperty<bool>(ref _requestIsPending, value);
        }

        public CaretakerViewModel()
        {
            FullfillRequestCanExecute = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => GetRequests());
            AssociatedUserEmail = Helpers.Settings.AssociatedSickUserEmail;
            CurrentUserEmail = Helpers.Settings.EmailAddress;
        }

        private bool GetRequests()
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.AccessToken);

            var url = "http://bringmesoupapi.azurewebsites.net/api/requests";

            var response = client.GetStringAsync(url).Result;
            var requests = JsonConvert.DeserializeObject<List<Request>>(response);

            if (requests.Any())
            {
                LatestRequestId = requests.First().Id;
                RequestIsPending = true;
            }
            else
            {
                LatestRequestId = null;
                RequestIsPending = false;
            }

            return true;    // keep the timer going

        }

        public bool FullfillRequestCanExecute { get; set; }
        public ICommand FullfillRequest
        {
            get
            {
                return new Command(async () => await FullfillRequestExecutor(), () => FullfillRequestCanExecute);
            }
        }
        public async Task FullfillRequestExecutor()
        {
            FullfillRequestCanExecute = false;

            var content = new StringContent("");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.AccessToken);

            var url = "http://bringmesoupapi.azurewebsites.net/api/fullfillrequest/" + LatestRequestId;

            var response = await client.PostAsync(url, content);

            FullfillRequestCanExecute = true;
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

    }
}
