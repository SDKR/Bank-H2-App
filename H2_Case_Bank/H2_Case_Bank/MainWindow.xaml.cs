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

namespace H2_Case_Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window1 win1 = new Window1();
        Customer cus = new Customer();
        Account acc = new Account();
        Customer Selectedcustomer = new Customer();

        public MainWindow()
        {
            InitializeComponent();
            Kundeoversigt_DataGrid.CanUserAddRows = false;
            Kundeoversigt_DataGrid.ItemsSource = cus.ReturnCustomers();
        }

        private void Kundeoversigt_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow MW = new MainWindow();

            win1.KundeNavn_DataGrid.ItemsSource = acc.getCustomerAccounts(Selectedcustomer);

            win1.KundeNavn_Label.Content = Selectedcustomer.Firstname + " " + Selectedcustomer.Lastname + "'s Konti";
            win1.UserID_TextBox.Text = Selectedcustomer.UserID.ToString();

            win1.Show();
            this.Close();
            
        }

        private void Fornavn_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("Ingen tal i Fornavn", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Efternavn_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("Ingen tal i Fornavn", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Opret_Button_Click(object sender, RoutedEventArgs e)
        {
            cus.CreateCustomer(Fornavn_TextBox.Text, Efternavn_TextBox.Text);

            Kundeoversigt_DataGrid.ItemsSource = null;
            Kundeoversigt_DataGrid.ItemsSource = cus.ReturnCustomers();
        }

        private void Kundeoversigt_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selectedcustomer = (Customer)Kundeoversigt_DataGrid.SelectedItem;
            if (Selectedcustomer == null)
            {
                Kundeoversigt_DataGrid.SelectedIndex = 0;
                Selectedcustomer = (Customer)Kundeoversigt_DataGrid.SelectedItem;
            }

            
            
        }

        private void SletKunde_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Selectedcustomer.Firstname == null)
            {
                MessageBox.Show("Vælg en kunde du vil slette.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
               
                MessageBoxResult result = MessageBox.Show("Er du sikker på du vil slette " + Selectedcustomer.Firstname + " " + Selectedcustomer.Lastname + " som kunde?", "Bekræft", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:

                        MessageBox.Show(Selectedcustomer.Firstname + " " + Selectedcustomer.Lastname + " er nu slettet fra databasen", "Succeded", MessageBoxButton.OK, MessageBoxImage.Information);

                        Selectedcustomer.DeleteCustomer(Selectedcustomer.UserID);
                        Kundeoversigt_DataGrid.ItemsSource = cus.ReturnCustomers();
                        break;

                    case MessageBoxResult.No:

                        break;
                }



            }
        }

        private void Search_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string search = Search_TextBox.Text;

            List<Customer> Find = cus.ReturnCustomers().Where(Customer => Customer.Firstname.ToLower().StartsWith(Search_TextBox.Text.ToLower()) || Customer.Lastname.ToLower().StartsWith(Search_TextBox.Text.ToLower())).ToList();

            Kundeoversigt_DataGrid.ItemsSource = Find;

          
        }
    }
}
