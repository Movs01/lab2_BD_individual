using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

namespace lab2_BD_individual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VariantContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new VariantContext(); // Создайте экземпляр вашего контекста
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка данных из таблицы employee и отображение их в элементе управления
            var employee = _context.Employees.ToList();
            DataGridEmployee.ItemsSource = employee;

            // Загрузка данных из таблицы authors и отображение их в элементе управления
            var position = _context.Positions.ToList();
            DataGridPosition.ItemsSource = position;

            // Загрузка данных из таблицы authors и отображение их в элементе управления
            var borrowBook = _context.BorrowedBooks.ToList();
            DataGridBorrowedBooks.ItemsSource = borrowBook;
        }
        private bool IsValidFullName(string fullName)
        {
            string[] parts = fullName.Split(' ');
            return parts.Length == 3;
        }

        private string GetSelectedGender()
        {
            return MaleRadioButton.IsChecked == true ? "Мужчина" : (FemaleRadioButton.IsChecked == true ? "Женщина" : "Не указано");
        }

        private bool IsValidPassport(string passport)
        {
            string pattern = @"^\d{2}\s\d{2}\s\d{6}$";
            return Regex.IsMatch(passport, pattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{1}-\d{3}-\d{3}-\d{2}-\d{2}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        private void ButtonInsertEmployees_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;

            if (!IsValidFullName(fullName))
            {
                MessageBox.Show("Некорректное полное имя. Пожалуйста, введите фамилию, имя и отчество через пробел.");
                return;
            }

            string gender = GetSelectedGender();
            string passport = PassportDataTextBox.Text;

            if (!IsValidPassport(passport))
            {
                MessageBox.Show("Некорректные паспортные данные. Пожалуйста, введите в формате ** ** ******.");
                return;
            }

            string phoneNumber = PhoneNumberTextBox.Text;

            if (!IsValidPhoneNumber(phoneNumber))
            {
                MessageBox.Show("Некорректный номер телефона. Пожалуйста, введите в формате *-***-***-**-**.");
                return;
            }
            try
            {
                Employee employee = new Employee 
                {
                    EmployeeId = Convert.ToInt32(EmployeeIDTextBox.Text),
                    FullName = fullName,
                    Age = Convert.ToInt32(AgeTextBox.Text),
                    Gender = gender,
                    Address = AddressTextBox.Text,
                    PhoneNumber = phoneNumber,
                    PassportData = passport,
                    PositionCode = Convert.ToInt32(PositionCodeEmployeeTextBox.Text)
                };
                //_context.Employees.Add(new Employee { EmployeeId = Convert.ToInt32(EmployeeIDTextBox.Text), FullName = fullName, Age = Convert.ToInt32(AgeTextBox.Text), Gender = gender, Address = AddressTextBox.Text, PhoneNumber = phoneNumber, PassportData = passport, PositionCode = Convert.ToInt32(PositionCodeEmployeeTextBox.Text) });
                _context.Employees.Add(employee);
                _context.SaveChanges();
                LoadData(); // Обновление данных
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ButtonInsertPosition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Position position = new Position
                {
                    PositionsCode = Convert.ToInt32(PositionCodePositionsTextBox.Text),
                    PositionsName = PositionNameTextBox.Text,
                    Salary = Convert.ToDouble(SalaryTextBox.Text),
                    Responsibilites = ResponsibilitiesTextBox.Text,
                    Requirments = RequirementsTextBox.Text
                };
                //_context.Positions.Add(new Position { PositionsCode = Convert.ToInt32(PositionCodePositionsTextBox.Text), PositionsName = PositionNameTextBox.Text, Salary = Convert.ToDouble(SalaryTextBox.Text), Responsibilites = ResponsibilitiesTextBox.Text, Requirments = RequirementsTextBox.Text });
                _context.Positions.Add(position);
                _context.SaveChanges();
                LoadData(); // Обновление данных
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void ButtonInsertBorrowedBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string borrowDate = BorrowDateBorrowedBooksDatePicker.Text;
                string returnDate = ReturnDateBorrowedBooksDatePicker.Text;


                if (!string.IsNullOrEmpty(ReturnDateBorrowedBooksDatePicker.Text))
                {
                    DateTime parsedBorrowDate = DateTime.ParseExact(borrowDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    DateTime parsedReturnDate = DateTime.ParseExact(returnDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                    if (parsedBorrowDate > parsedReturnDate)
                    {
                        MessageBox.Show("Дата возврата должна быть после даты взятия книги.");
                        return;
                    }
                }


                string returnStatus = string.IsNullOrEmpty(ReturnDateBorrowedBooksDatePicker.Text) ? "Не вернул" : "Вернул";

                BorrowedBook borrowedBook = new BorrowedBook 
                {
                    BookCode = Convert.ToInt32(BookCodeBorrowedBooksTextBox.Text),
                    ReaderCode = Convert.ToInt32(ReaderCodeBorrowedBooksTextBox.Text),
                    BorrowDate = borrowDate,
                    ReturnDate = returnDate,
                    ReturnStatus = returnStatus,
                    EmployeeId = Convert.ToInt32(EmployeeIDBorrowedBooksTextBox.Text)
                };
                //_context.BorrowedBooks.Add(new BorrowedBook { BookCode = Convert.ToInt32(BookCodeBorrowedBooksTextBox.Text), ReaderCode = Convert.ToInt32(ReaderCodeBorrowedBooksTextBox.Text), BorrowDate = borrowDate, ReturnDate = returnDate, ReturnStatus = returnStatus, EmployeeId = Convert.ToInt32(EmployeeIDBorrowedBooksTextBox.Text) });
                var db = new VariantContext();
                _context.BorrowedBooks.Add(borrowedBook);
                _context.SaveChanges();
                LoadData(); // Обновление данных
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
