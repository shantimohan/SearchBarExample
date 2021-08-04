using SearchBarCancelIssue.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SearchBarCancelIssue.ViewModels
{
    public class SearchVM
    {
        public ObservableCollection<Item> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public string title;
        public string searchFilter = "";

        public SearchVM(string titleIn)
        {
            title = titleIn;

            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());
        }

        private void ExecuteLoadItemsCommand()
        {
            Items.Clear();

            for (int i = 0; i < 100; i++)
            {
                Item item = new Item
                {
                    Name = $"{title} Item #{i}",
                    Desc = $"Description of Item #{i}"
                };

                searchFilter = searchFilter.Trim();

                if (string.IsNullOrWhiteSpace(searchFilter))
                {
                    Items.Add(item);
                }
                else
                {
                    if (item.Name.ToLower().Contains(searchFilter.ToLower()) ||
                        item.Desc.ToLower().Contains(searchFilter.ToLower()))
                    {
                        Items.Add(item);
                    }
                }
            }
        }
    }
}
