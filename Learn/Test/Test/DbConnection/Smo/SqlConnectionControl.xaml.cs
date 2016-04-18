﻿using System.Data.SqlClient;
using System.Windows;
using Test.BaseUI;

namespace Test.DbConnection.Smo
{
    /// <summary>
    /// Interaction logic for SqlConnectionStringBuilder.xaml
    /// </summary>
    public partial class SqlConnectionControl
    {
        public static readonly DependencyProperty SqlConnectionStringProperty =
            DependencyProperty.Register("SqlConnectionString", typeof(SqlConnectionStringBuilder), typeof(SqlConnectionControl), 
            new FrameworkPropertyMetadata(SqlConnectionStringProperty, PropertyChangedCallback, CoerceValueCallback));

        private static object CoerceValueCallback(DependencyObject d, object basevalue)
        {
            var sqlConnectionControl = d as SqlConnectionControl;
            return sqlConnectionControl.ViewModel.ConnectionString;
        }

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sqlConnectionControl = d as SqlConnectionControl;
            sqlConnectionControl.ViewModel.ConnectionString = (SqlConnectionStringBuilder)e.NewValue;
        }

        public SqlConnectionStringBuilder SqlConnectionString
        {
            get { return (SqlConnectionStringBuilder)GetValue(SqlConnectionStringProperty); }
            set { SetValue(SqlConnectionStringProperty, value); }
        }

        public SqlConnectionControl()
        {
            InitializeComponent();
        }
    }

    public class SqlConnectionControlBase : AsUserControl<SqlConnectionControlViewModel> { }
}