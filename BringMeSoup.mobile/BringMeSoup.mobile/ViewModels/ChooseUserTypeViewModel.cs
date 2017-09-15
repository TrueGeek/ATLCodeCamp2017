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
    class ChooseUserTypeViewModel : BaseViewModel
    {

        public string SickCode { get; set; }

        public ICommand Register
        {
            get
            {
                return new Command(async (parameter) =>
                {

                    try
                    {

                        var isSick = bool.Parse(parameter.ToString());

                        var registerRequest = new RegisterUserTypeRequest()
                        {
                            UserType = isSick ? UserType.Sick : UserType.Caretaker,
                            SickCode = SickCode
                        };
                        var registerRequestAsJson = JsonConvert.SerializeObject(registerRequest);

                        var content = new StringContent(registerRequestAsJson);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Settings.AccessToken);

                        var url = "http://bringmesoupapi.azurewebsites.net/api/register";

                        var response = await client.PostAsync(url, content);
                        var responseContent = response.Content.ReadAsStringAsync().Result;

                        var user = JsonConvert.DeserializeObject<User>(responseContent);
                        Helpers.Settings.AssociatedSickUserId = user.AssociatedSickUserId;
                        Helpers.Settings.AssociatedSickUserEmail = user.AssociatedSickUserEmail;
                        Helpers.Settings.SickCode = user.SickCode;

                        if (isSick)
                        {
                            await Navigation.PushAsync(new SickPage());
                        }
                        else
                        {
                            await Navigation.PushAsync(new CaretakerPage());
                        }

                    }
                    catch (Exception ex)
                    {

                        var msg = ex.Message;

                        throw;
                    }


                });
            }
        }

    }
}
