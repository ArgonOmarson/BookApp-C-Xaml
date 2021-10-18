using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;


namespace RESTWSCall2
{
    public sealed partial class MainPage : Page
    {
        private const string BASE_URL = @"http://193.10.202.70/BookApi/api/"; //"http://localhost:65511/api/"; Local adress for testing
        public MainPage()
        {
            this.InitializeComponent();
          
        }
        //OnNavigatedTo will execute every time the user goes to the page. The constructor normally only the first time
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadBooks();
        }
        public async void LoadBooks()
        {
            try
            {
                prgMain.IsActive = true; //Progressbar shown while waiting for webservice to answer
                string URL = BASE_URL + "Books";
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var bookList = JsonConvert.DeserializeObject<List<Book>>(content);

                  //Databind the list
                  lstBooks.ItemsSource = bookList;
                  prgMain.IsActive = false;
                }
            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        #region Navigation
        private void MnuAdd_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddPage));

        }

        private void MnuCalendar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InfoPage));
        }
        #endregion
    }
}

