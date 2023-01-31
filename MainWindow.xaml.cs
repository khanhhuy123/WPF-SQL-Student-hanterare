using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_sql;

//To add:
//Find student by id
//Update student information
//Remove course
//Find courseByName
//Update course information

namespace WpfApp_SQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLiteHandler handler = new SQLiteHandler("myDB.db");
        int doneNum;
        bool check;
        public MainWindow()
        {
            InitializeComponent();
            //Hide visibility of adding student CheckBox and Label
            firstNameLabel.Visibility = Visibility.Hidden;
            firstTextBox.Visibility = Visibility.Hidden;

            lastNameLabel.Visibility = Visibility.Hidden;
            secondTextBox.Visibility = Visibility.Hidden;

            ageLabel.Visibility = Visibility.Hidden;
            thirdTextBox.Visibility = Visibility.Hidden;

            IDLabel.Visibility = Visibility.Hidden;

            CourseNameLabel.Visibility = Visibility.Hidden;

            StudentID.Visibility = Visibility.Hidden;
            CourseID.Visibility = Visibility.Hidden;

            DoneBtn.Visibility = Visibility.Hidden;
            cancelButton.Visibility = Visibility.Hidden;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            //Hide visibility of adding student CheckBox and Label
            firstNameLabel.Visibility = Visibility.Hidden;
            CourseNameLabel.Visibility = Visibility.Hidden;
            IDLabel.Visibility = Visibility.Hidden;
            StudentID.Visibility = Visibility.Hidden;
            firstTextBox.Visibility = Visibility.Hidden;

            lastNameLabel.Visibility = Visibility.Hidden;
            CourseID.Visibility = Visibility.Hidden;
            secondTextBox.Visibility = Visibility.Hidden;

            ageLabel.Visibility = Visibility.Hidden;
            thirdTextBox.Visibility = Visibility.Hidden;

            DoneBtn.Visibility = Visibility.Hidden;
            cancelButton.Visibility = Visibility.Hidden;

            firstTextBox.Clear();
            secondTextBox.Clear();
            thirdTextBox.Clear();
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            //Show visibility of adding student CheckBox and Label
            firstNameLabel.Visibility = Visibility.Visible;
            CourseNameLabel.Visibility = Visibility.Hidden;
            IDLabel.Visibility = Visibility.Hidden;
            StudentID.Visibility = Visibility.Hidden;
            CourseID.Visibility = Visibility.Hidden;
            firstTextBox.Visibility = Visibility.Visible;

            lastNameLabel.Visibility = Visibility.Visible;
            secondTextBox.Visibility = Visibility.Visible;

            ageLabel.Visibility = Visibility.Visible;
            thirdTextBox.Visibility = Visibility.Visible;

            DoneBtn.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            doneNum = 1;
        }

        private void RemoveStudentByID(object sender, RoutedEventArgs e)
        {

            IDLabel.Visibility = Visibility.Visible;
            firstNameLabel.Visibility = Visibility.Hidden;
            CourseNameLabel.Visibility = Visibility.Hidden;
            StudentID.Visibility = Visibility.Hidden;
            firstTextBox.Visibility = Visibility.Visible;

            lastNameLabel.Visibility = Visibility.Hidden;
            CourseID.Visibility = Visibility.Hidden;
            secondTextBox.Visibility = Visibility.Hidden;

            ageLabel.Visibility = Visibility.Hidden;
            thirdTextBox.Visibility = Visibility.Hidden;

            DoneBtn.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;

            doneNum = 2;
        }
        private void AddCourseBtn(object sender, RoutedEventArgs e)
        {
            firstNameLabel.Visibility = Visibility.Hidden;
            CourseNameLabel.Visibility = Visibility.Visible;
            IDLabel.Visibility = Visibility.Hidden;
            StudentID.Visibility = Visibility.Hidden;
            firstTextBox.Visibility = Visibility.Visible;

            lastNameLabel.Visibility = Visibility.Hidden;
            CourseID.Visibility = Visibility.Hidden;
            secondTextBox.Visibility = Visibility.Hidden;

            ageLabel.Visibility = Visibility.Hidden;
            thirdTextBox.Visibility = Visibility.Hidden;

            DoneBtn.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            doneNum = 3;
        }
        private void AddStudentToCourse(object sender, RoutedEventArgs e)
        {
            firstNameLabel.Visibility = Visibility.Hidden;
            CourseNameLabel.Visibility = Visibility.Hidden;
            IDLabel.Visibility = Visibility.Hidden;
            StudentID.Visibility = Visibility.Visible;
            firstTextBox.Visibility = Visibility.Visible;

            lastNameLabel.Visibility = Visibility.Hidden;
            CourseID.Visibility = Visibility.Visible;
            secondTextBox.Visibility = Visibility.Visible;

            ageLabel.Visibility = Visibility.Hidden;
            thirdTextBox.Visibility = Visibility.Hidden;


            DoneBtn.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            doneNum = 4;
        }
        private void FindStudentByFirstName(object sender, RoutedEventArgs e)
        {
            firstNameLabel.Visibility = Visibility.Visible;
            CourseNameLabel.Visibility = Visibility.Hidden;
            IDLabel.Visibility = Visibility.Hidden;
            StudentID.Visibility = Visibility.Hidden;
            firstTextBox.Visibility = Visibility.Visible;

            lastNameLabel.Visibility = Visibility.Hidden;
            CourseID.Visibility = Visibility.Hidden;
            secondTextBox.Visibility = Visibility.Hidden;

            ageLabel.Visibility = Visibility.Hidden;
            thirdTextBox.Visibility = Visibility.Hidden;


            DoneBtn.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            doneNum = 5;
        }

        private void DoneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (doneNum == 1)
            {
                check = handler.AddStudentToStudentTable(firstTextBox.Text, secondTextBox.Text, Int32.Parse(thirdTextBox.Text));
                if (check == true)
                {
                    MessageBox.Show("Data Added!");
                    firstNameLabel.Visibility = Visibility.Hidden;
                    firstTextBox.Visibility = Visibility.Hidden;

                    lastNameLabel.Visibility = Visibility.Hidden;
                    secondTextBox.Visibility = Visibility.Hidden;

                    ageLabel.Visibility = Visibility.Hidden;
                    thirdTextBox.Visibility = Visibility.Hidden;

                    DoneBtn.Visibility = Visibility.Hidden;
                    cancelButton.Visibility = Visibility.Hidden;
                }
            }
            else if (doneNum == 2)
            {
                try
                {
                    handler.RemoveStudentByID(Int32.Parse(firstTextBox.Text));
                    MessageBox.Show("Data Removed!");
                    IDLabel.Visibility = Visibility.Hidden;
                    firstTextBox.Visibility = Visibility.Hidden;
                    DoneBtn.Visibility = Visibility.Hidden;
                    cancelButton.Visibility = Visibility.Hidden;
                }
                catch (Exception expectation)
                {
                    MessageBox.Show(expectation.Message);
                }
            }
            else if (doneNum == 3)
            {
                try
                {
                    check = handler.AddCourse(firstTextBox.Text);
                    if (check == true)
                    {
                        MessageBox.Show("Data Added!");
                        CourseNameLabel.Visibility = Visibility.Hidden;
                        firstTextBox.Visibility = Visibility.Hidden;

                        DoneBtn.Visibility = Visibility.Hidden;
                        cancelButton.Visibility = Visibility.Hidden;
                    }
                }
                catch (Exception expectation)
                {
                    MessageBox.Show(expectation.Message);
                }
            }
            else if (doneNum == 4)
            {
                try
                {
                    handler.AddStudentToCourse(Int32.Parse(firstTextBox.Text), Int32.Parse(secondTextBox.Text));
                    MessageBox.Show("Data Added!");
                    StudentID.Visibility = Visibility.Hidden;
                    CourseID.Visibility = Visibility.Hidden;
                    firstTextBox.Visibility = Visibility.Hidden;
                    secondTextBox.Visibility = Visibility.Hidden;

                    DoneBtn.Visibility = Visibility.Hidden;
                    cancelButton.Visibility = Visibility.Hidden;
                }
                catch (Exception expectation)
                {
                    MessageBox.Show(expectation.Message);
                }

            }
            else if (doneNum == 5)
            {
                handler.FindStudentByFirstName(firstTextBox.Text, dataGrid1);
                firstNameLabel.Visibility = Visibility.Hidden;
                firstTextBox.Visibility = Visibility.Hidden;

                DoneBtn.Visibility = Visibility.Hidden;
                cancelButton.Visibility = Visibility.Hidden;
            }
            firstTextBox.Text = String.Empty;
            secondTextBox.Text = String.Empty;
            thirdTextBox.Text = String.Empty;
        }

        private void ShowAllStudent(object sender, RoutedEventArgs e)
        {
            handler.ShowAllStudent(dataGrid1);
        }

        private void ShowAllStudentCourse(object sender, RoutedEventArgs e)
        {
            handler.ShowAllStudentCourse(dataGrid1);
        }

        private void ShowCourses(object sender, RoutedEventArgs e)
        {
            handler.ShowCourses(dataGrid1);
        }
    }
}
