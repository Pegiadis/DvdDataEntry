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
        private List<ActorModel> entries = new List<ActorModel>();

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = entries;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Your logic for adding data goes here
            // For example, retrieving data from text boxes and adding it to a list or database

            var newEntry = new ActorModel
            {
                ActorId = actorIdTextBox.Text,
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text
            };

            entries.Add(newEntry);
            dataGrid.Items.Refresh();

            // Optionally, clear the text boxes after adding
            actorIdTextBox.Clear();
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
        }

        // add a method to handle the remove button click event
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dataGrid.SelectedItems.Cast<ActorModel>().ToList();
            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    entries.Remove(item);
                }

                dataGrid.Items.Refresh();
            }
        }


        private void ActorsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementation for when the Actors button is clicked
            // For example, navigate to the Actors view or page
        }

        private void MoviesButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementation for when the Movies button is clicked
            // This could be the current view, so you might not need to do anything
        }

        private void CustomersButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementation for when the Customers button is clicked
            // For example, navigate to the Customers view or page
        }
    }
}
