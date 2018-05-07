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
using System.Windows.Shapes;

namespace H2_Case_Bank
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Account acc = new Account();
        Account CreateAccount = new Account();
        Account SelectedAccount = new Account();
        Customer cus = new Customer();
        Transaction trans = new Transaction();
        public Window1()
        {
            InitializeComponent();
            KundeNavn_DataGrid.CanUserAddRows = false;
            Transaktion_DataGrid.CanUserAddRows = false;
            Rente();
            UdførButton_content();
        }

        private void Tilbage_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
           
            MW.Show();
            this.Close();
        }

        private void Beløb_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("Beløbet kan kun bestå af tal.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void KontoType_Combobox_DropDownClosed(object sender, EventArgs e)
        {
            Rente();
        }
        


        private void Udfør_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Aktion_ComboBox.SelectedIndex == 0)
            {
                //Indsæt
                SelectedAccount.Deposit(int.Parse(KontoNR_TextBox.Text), decimal.Parse(Beløb_TextBox.Text));
                Transaktion_DataGrid.ItemsSource = null;

                cus.UserID = int.Parse(UserID_TextBox.Text);
                KundeNavn_DataGrid.ItemsSource = SelectedAccount.getCustomerAccounts(cus);
            }

            else if (Aktion_ComboBox.SelectedIndex == 1)
            {
                //Udbetal
                SelectedAccount.Withdraw(int.Parse(KontoNR_TextBox.Text), decimal.Parse(Beløb_TextBox.Text));

                Transaktion_DataGrid.ItemsSource = null;

                cus.UserID = int.Parse(UserID_TextBox.Text);
                KundeNavn_DataGrid.ItemsSource = SelectedAccount.getCustomerAccounts(cus);

              
            }
            
        }

         private void Aktion_ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            UdførButton_content();
        }

        private void Rente()
        {
            if (KontoType_Combobox.SelectedIndex == 0)
            {
                Rente_TextBox.Text = "0.80";
            }
            else if (KontoType_Combobox.SelectedIndex == 1)
            {
                Rente_TextBox.Text = "1.12";
            }
            else if (KontoType_Combobox.SelectedIndex == 2)
            {
                Rente_TextBox.Text = "0.52";
            }
            else if (KontoType_Combobox.SelectedIndex == 3)
            {
                Rente_TextBox.Text = "2.07";
            }
        }
        private void UdførButton_content()
        {
            if (Aktion_ComboBox.SelectedIndex == 0)
            {
                Udfør_Button.Content = "Indsæt";
            }

            else if (Aktion_ComboBox.SelectedIndex == 1)
            {
                Udfør_Button.Content = "Udbetal";
            }
        }

        private void KundeNavn_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (KundeNavn_DataGrid.Items.Count != 0)
            {
                // IF Selected account is null select first item on list
                SelectedAccount = (Account)KundeNavn_DataGrid.SelectedItem;
                if (SelectedAccount == null)
                {
                    KundeNavn_DataGrid.SelectedIndex = 0;
                    SelectedAccount = (Account)KundeNavn_DataGrid.SelectedItem;
                }
            }

           
            //SelectedAccount = (Account)KundeNavn_DataGrid.SelectedItem;

            Transaktion_DataGrid.ItemsSource = trans.getTransactions(SelectedAccount);
            
            Transaktion_Label.Content = "Overførelser (Konto nr. " + SelectedAccount.Accountnumber + ")";

            KontoNR_TextBox.Text = SelectedAccount.Accountnumber.ToString();
           
        }

        private void Opret_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Rente_TextBox.Text.Equals(string.Empty) || Balance_TextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Rente og Balance skal udfuldes.");
            }
            else
            {
                CreateAccount.Accounttype = KontoType_Combobox.Text;
                CreateAccount.Interest = Convert.ToDecimal(Rente_TextBox.Text);
                CreateAccount.Balance = decimal.Parse(Balance_TextBox.Text);
                CreateAccount.FK_CustomerID = int.Parse(UserID_TextBox.Text);

                CreateAccount.CreateAccount(CreateAccount);

                cus.UserID = int.Parse(UserID_TextBox.Text);
                KundeNavn_DataGrid.ItemsSource = null;
                KundeNavn_DataGrid.ItemsSource = acc.getCustomerAccounts(cus);
            }
        }

        private void SletKonto_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccount.Accounttype == null)
            {
                MessageBox.Show("Vælg en konto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SelectedAccount.Accounttype != null)
            {
                MessageBoxResult result = MessageBox.Show("Er du sikker på du vil slette denne Konto: " + SelectedAccount.Accountnumber + "?", "Bekræft", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:

                        MessageBox.Show(SelectedAccount.Accountnumber + " er nu slettet fra databasen", "Succeded", MessageBoxButton.OK, MessageBoxImage.Information);

                        SelectedAccount = (Account)KundeNavn_DataGrid.SelectedItem;
                        SelectedAccount.deleteAccount(SelectedAccount);

                        cus.UserID = int.Parse(UserID_TextBox.Text);
                        //KundeNavn_DataGrid.ItemsSource = null;
                        KundeNavn_DataGrid.ItemsSource = acc.getCustomerAccounts(cus);
                        break;

                    case MessageBoxResult.No:

                        break;
                }

                
            }
            
        }

        private void Balance_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("Balancen kan kun bestå af tal.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
