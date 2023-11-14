using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DvdDataEntry.Models;

namespace DvdDataEntry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CustomerModel> entries = new List<CustomerModel>();

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = entries;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Your logic for adding data goes here
            // For example, retrieving data from text boxes and adding it to a list or database

            var newEntry = new CustomerModel
            {
                Title = titleTextBox.Text,
                Genre = genreTextBox.Text,
                Company = companyTextBox.Text
            };

            entries.Add(newEntry);
            dataGrid.Items.Refresh();

            // Optionally, clear the text boxes after adding
            titleTextBox.Clear();
            genreTextBox.Clear();
            companyTextBox.Clear();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dataGrid.SelectedItems.Cast<CustomerModel>().ToList();
            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    entries.Remove(item);
                }

                dataGrid.Items.Refresh();
            }
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "Title" || textBox.Text == "Genre")
                {
                    textBox.Text = "";
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = textBox.Name == "titleTextBox" ? "Title" : "Genre";
                }
            }
        }

        private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
