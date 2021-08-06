using SearchBarCancelIssue.Models;
using SearchBarCancelIssue.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchBarCancelIssue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private bool ReturningFromSearch = false;
        public static Item searchedItem;

        public SearchVM page1VM;

        public Page1()
        {
            InitializeComponent();

            page1VM = new SearchVM(this.Title);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (page1VM.Items.Count == 0)
            {
                page1VM.LoadItemsCommand.Execute(null);
                BindingContext = page1VM;
                ItemsListView.ItemsSource = page1VM.Items;
            }

            if (ReturningFromSearch &&
                searchedItem != null)
            {
                Item item = page1VM.Items.Where(x => x.Name == searchedItem.Name).FirstOrDefault();
                ItemsListView.ScrollTo(searchedItem, ScrollToPosition.Center, true);
                ItemsListView.SelectedItem = item;
                ReturningFromSearch = false;
            }
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            ReturningFromSearch = false;
            searchedItem = null;
            await Navigation.PushModalAsync(new NavigationPage(new SearchPage(this.Title, page1VM)));
            ReturningFromSearch = true;
        }

        private async void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Item item = (Item)e.Item;
            await DisplayAlert("Item Details", $"Name: {item.Name}{Environment.NewLine}Desc: {item.Desc}", "Ok");
        }
    }
}