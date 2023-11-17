using System;
using System.Collections.Generic;
using System.Configuration;
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
using Npgsql;

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
            if (int.TryParse(actorIdTextBox.Text, out int actorId))
            {

                var newEntry = new ActorModel
                {
                    ActorId = actorId,
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
            else
            {
                MessageBox.Show("Actor ID must be an integer.");
            }
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


        private void AddToDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(actorIdTextBox.Text, out int actorId))
            {
                var newEntry = new ActorModel
                {
                    ActorId = actorId,
                    FirstName = firstNameTextBox.Text,
                    LastName = lastNameTextBox.Text
                };

                try
                {
                    using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand("INSERT INTO actor (actor_id, first_name, last_name) VALUES (@actor_id, @first_name, @last_name)", conn))
                        {
                            cmd.Parameters.AddWithValue("actor_id", newEntry.ActorId);
                            cmd.Parameters.AddWithValue("first_name", newEntry.FirstName);
                            cmd.Parameters.AddWithValue("last_name", newEntry.LastName);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    actorIdTextBox.Clear();
                    firstNameTextBox.Clear();
                    lastNameTextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Actor ID must be an integer.");
            }
        }


        private void ActorsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementation for when the Actors button is clicked
            // For example, navigate to the Actors view or page
        }

        private void MoviesButton_Click(object sender, RoutedEventArgs e)
        {
     
        }

        private void CustomersButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.Show();
            this.Close();
        }
    }
}
