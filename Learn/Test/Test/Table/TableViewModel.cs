using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodeFirst;
using Test.DbConnection.Smo;
using Test.ViewModel;

namespace Test.Table
{
    public class TableViewModel : ViewModelBase
    {
        private ICommand okCommand;
        public ICommand OkCommand
        {
            get { return GetDelegateCommand<object>(ref okCommand, x => OnOk()); }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return GetDelegateCommand<object>(ref cancelCommand, x => OnCancel()); }
        }

        public DataGrid Grid { get; set; }

        private List<Row> dataGrid;
        public List<Row> DataGrid
        {
            get { return dataGrid; }
            set
            {
                dataGrid = value;
                OnPropertyChanged(() => DataGrid);
            }
        }

        public string ConnectionString { get; set; }


        public void Refresh()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    var context = new SampleContext(connection);
                    List<Client> clients = context.Clients.ToList();
                    DataGrid = clients.Select(ConvertToRow).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Exception stacktrace", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private void OnOk()
        {
            CloseView(true);
        }

        private void OnCancel()
        {
            CloseView(false);
        }

        private Row ConvertToRow(Client client)
        {
            return Row.ConvertToRow(client);
        }
        
    }

    public class Row
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Sex { get; set; }

        public string HomePhone { get; set; } //mask
        public string MobilePhone { get; set; } //mask
        public string Email { get; set; }

        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; } //mask
        public string IdentNumber { get; set; } //mask
        public string IssuedBy { get; set; }
        public DateTime IssueDate { get; set; }

        public string RegistrationCity { get; set; }
        public string RegistrationAdress { get; set; }
        public string ResidenseCity { get; set; }
        public string ResidenseAdress { get; set; }

        public string Disability { get; set; }
        public string Nationality { get; set; }
        public string FamilyStatus { get; set; }

        public bool IsPensioner { get; set; }
        public bool IsReservist { get; set; }

        public Money MonthlyIncome { get; set; }

        public static Row ConvertToRow(Client client)
        {
            return new Row
            {
                Surname = client.Surname,
                Name = client.Name,
                MiddleName = client.MiddleName,
                BirthDate = client.BirthDate,
                BirthPlace = client.BirthPlace,
                Sex = client.Sex.GetDescription(),

                HomePhone = client.HomePhone,
                MobilePhone = client.MobilePhone,
                Email = client.Email,

                PassportSeries = client.Passport.PassportSeries,
                PassportNumber = client.Passport.PassportNumber,
                IdentNumber = client.Passport.IdentNumber,
                IssuedBy = client.Passport.IssuedBy,
                IssueDate = client.Passport.IssueDate,

                RegistrationCity = client.Registration.City.CityId,
                RegistrationAdress = client.Registration.Adress,
                ResidenseCity = client.Residense.City.CityId,
                ResidenseAdress = client.Residense.Adress,

                Disability = client.Disability.DisabilityId,
                Nationality = client.Nationality.NationalityId,
                FamilyStatus = client.FamilyStatus.FamilyStatusId,

                IsPensioner = client.IsPensioner,
                IsReservist = client.IsReservist,
                MonthlyIncome = new Money
                {
                    Value = client.MonthlyIncome,
                    Currency = client.Currency.CurrencyId
                },
            };
        }
    }

    public class Money
    {
        public double? Value { get; set; }
        public string Currency { get; set; }
    }
}
