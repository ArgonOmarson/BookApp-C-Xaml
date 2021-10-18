using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace RESTWSCall2
{
    public sealed partial class AddPage : Page
    {
        private const string BASE_URL = @"http://193.10.202.70/BookApi/api/"; //"http://localhost:65511/api/"; 
        public AddPage()
        {
            this.InitializeComponent();
            LoadAuthors();
            LoadShelves();
        }

        private async void LoadShelves()
        {
            try
            {
                string URL = BASE_URL + "Shelves";
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var shelfList = JsonConvert.DeserializeObject<List<Shelf>>(content);

                    cmbShelf.ItemsSource = shelfList;
                    cmbShelf.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void LoadAuthors()
        {
            try
            {
                string URL = BASE_URL + "Authors";
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var authorList = JsonConvert.DeserializeObject<List<Author>>(content);

                    //Databind the ViewModel presentation list
                    cmbAuthor.ItemsSource = GetPresentationList(authorList);
                    cmbAuthor.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        //In this case we have created a presentation class to show both first and last name. Could be done in simpler ways, but with more complex data it is a good solution
        private List<AuthorPresentation> GetPresentationList(List<Author> authorList)
        {
            List<AuthorPresentation> returnList = new List<AuthorPresentation>();
            foreach (var author in authorList)
            {
                AuthorPresentation presentData = new AuthorPresentation { Id = author.Id, FirstName = author.FirstName , LastName= author.LastName };
                returnList.Add(presentData);
            }
            return returnList;
        }

        public async System.Threading.Tasks.Task AddBook()
        {
            try
            {
                HttpClient client = new HttpClient();

                //We need to convert back from ViewModel AuthorPresentation to Author
                AuthorPresentation selectedAuthor = (AuthorPresentation)cmbAuthor.SelectedItem;
                Author bookAuthor = new Author { Id = selectedAuthor.Id, FirstName = selectedAuthor.FirstName, LastName = selectedAuthor.LastName };

                Shelf selectedShelf = (Shelf)cmbShelf.SelectedItem;

                Book newBook = new Book();
                newBook.ISBN = txtISBN.Text;
                newBook.Title = txtTitle.Text;
                newBook.ShelfId = selectedShelf.Id;
                newBook.Author = new List<Author>();
                newBook.Author.Add(bookAuthor);

                string jsonString = JsonConvert.SerializeObject(newBook);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                string URL = BASE_URL + "Books";
                var response = await client.PostAsync(URL, content);
                var responseString = await response.Content.ReadAsStringAsync(); //ToDo: Handle an error response from web service
                System.Diagnostics.Debug.WriteLine(responseString); 
            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            await AddBook();
            var dialog = new MessageDialog("Your book is saved"); //ToDo: Handle an error response from web service
            await dialog.ShowAsync();
        }

        #region Navigation
        private void navMain_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void MnuHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void MnuCalendar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InfoPage));
        }
        #endregion

    }
}
