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
    public partial class SearchPage : ContentPage
    {
        Item item;
        SearchVM itemsVM;

        public SearchPage(string pageTitle, SearchVM itemsVMIn)
        {
            InitializeComponent();

            sbSearchData.Placeholder = $"Search {pageTitle}";

            itemsVM = new SearchVM(pageTitle);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            itemsVM.LoadItemsCommand.Execute(null);
            BindingContext = itemsVM;
        }

        private void lvSearch_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            item = (Item)e.Item;
        }

        private async void Accept_Clicked(object sender, EventArgs e)
        {
            Page1.searchedItem = item;
            await Navigation.PopModalAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Page1.searchedItem = null;
            await Navigation.PopModalAsync();
        }

        private void sbSearchData_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar sb = (SearchBar)sender;

            itemsVM.searchFilter = sb.Text.Trim().ToLower();
            itemsVM.LoadItemsCommand.Execute(null);
        }
    }
}