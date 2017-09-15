using BringMeSoup.mobile.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BringMeSoup.mobile.ViewModels
{

    class LogonViewModel : BaseViewModel
    {

        public string Email { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var formValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username", Email),
                        new KeyValuePair<string, string>("password", Password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };

                    var url = "http://bringmesoupapi.azurewebsites.net/Token";
                    var request = new HttpRequestMessage(HttpMethod.Post, url)
                    {
                        Content = new FormUrlEncodedContent(formValues)
                    };

                    var client = new HttpClient();
                    var response = client.SendAsync(request).Result;

                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    JObject token = JsonConvert.DeserializeObject<dynamic>(responseContent);

                    Helpers.Settings.AccessToken = token.Value<string>("access_token");
                    Helpers.Settings.AccessTokenExpirationDate = token.Value<DateTime>(".expires");
                    Helpers.Settings.EmailAddress = token.Value<string>("userName");

                    await Navigation.PushAsync(new ChooseUserTypePage());

                });
            }
        }

    }
}
