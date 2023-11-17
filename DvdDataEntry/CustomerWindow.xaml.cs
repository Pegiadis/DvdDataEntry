using DvdDataEntry.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DvdDataEntry
{
    public partial class CustomerWindow : Window
    {
        private List<CustomerModel> entries = new List<CustomerModel>();

        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void ActorsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow actorWindow = new MainWindow();
            actorWindow.Show();
            this.Close();
        }

        private void MoviesButton_Click(object sender, RoutedEventArgs e)
        {
            MoviesWindow moviesWindow = new MoviesWindow();
            moviesWindow.Show();
            this.Close();
        }

        private void CustomersButton_Click(object sender, RoutedEventArgs e)
        {
            // Stay on the current page.
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(customerIdTextBox.Text, out int customerId) &&
                int.TryParse(customerIdTextBox.Text, out int storeId) &&
                int.TryParse(customerIdTextBox.Text, out int addressId))
            {
                var newEntry = new CustomerModel
                {
                    CustomerId = customerId,
                    StoreId = storeId,
                    FirstName = firstNameTextBox.Text,
                    LastName = lastNameTextBox.Text,
                    EmailAddress = emailTextBox.Text,
                    AddressId = addressId
                };

                entries.Add(newEntry);
                dataGrid.Items.Refresh();

                // Optionally, clear the text boxes after adding
                customerIdTextBox.Clear();
                storeIdTextBox.Clear();
                firstNameTextBox.Clear();
                lastNameTextBox.Clear();
                emailTextBox.Clear();
                addressIdTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please ensure all fields are correctly filled. Customer ID must be an integer.");
            }
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

        private void AddToDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(customerIdTextBox.Text, out int customerId) &&
                int.TryParse(customerIdTextBox.Text, out int storeId) &&
                int.TryParse(customerIdTextBox.Text, out int addressId))
            {
                var newEntry = new CustomerModel
                {
                    CustomerId = customerId,
                    StoreId = storeId,
                    FirstName = firstNameTextBox.Text,
                    LastName = lastNameTextBox.Text,
                    EmailAddress = emailTextBox.Text,
                    AddressId = addressId
                };


                try
                {
                    using (var conn = new NpgsqlConnection(ConfigurationManager
                               .ConnectionStrings["MyDBConnectionString"].ConnectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(
                                   "INSERT INTO customer (customer_id, store_id, first_name, last_name, email, address_id) VALUES (@customer_id, @store_id, @first_name," +
                                   "@last_name ,@email,@address_id)",
                                   conn))
                        {
                            cmd.Parameters.AddWithValue("customer_id", newEntry.CustomerId);
                            cmd.Parameters.AddWithValue("store_id", newEntry.StoreId);
                            cmd.Parameters.AddWithValue("first_name", newEntry.FirstName);
                            cmd.Parameters.AddWithValue("last_name", newEntry.LastName);
                            cmd.Parameters.AddWithValue("email", newEntry.EmailAddress);
                            cmd.Parameters.AddWithValue("address_id", newEntry.AddressId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    customerIdTextBox.Clear();
                    storeIdTextBox.Clear();
                    firstNameTextBox.Clear();
                    lastNameTextBox.Clear();
                    emailTextBox.Clear();
                    addressIdTextBox.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Invalid Data Insertion");
            }
        }
    }
}
