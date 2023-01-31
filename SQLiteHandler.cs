﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Windows;
using System.Data;

namespace WPF_sql
{
    internal class SQLiteHandler
    {
        string? _database_name;
        string _connection_string;
        SQLiteConnection _connection; //kräver using System.Data.SQLite;
        SQLiteCommand _command;
        SQLiteDataAdapter dataAdp;
        DataTable dt;

        private void Open()
        {
            if (_connection != null)
            {
                _connection.Open();
            }
        }

        private void Close()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }

        public SQLiteHandler(string databaseName)
        {
            _database_name = databaseName;
            _connection_string = "URI=file:" + databaseName + ";foreign keys=true;";
            _connection = new SQLiteConnection(_connection_string);

            CheckSQLiteVersion();
            GetCurrentTime();
        }

        private void CheckSQLiteVersion()
        {
            Open();
            string stm = "SELECT SQLITE_VERSION();";
            _command = new SQLiteCommand(stm, _connection);
            string? version = _command.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");
            Close();
        }

        private void GetCurrentTime()
        {
            Open();
            string stm = "SELECT(datetime('now', 'localtime'));";
            _command = new SQLiteCommand(stm, _connection);
            string? date = _command.ExecuteScalar().ToString();
            Console.WriteLine($"SQLite dateNow: {date} \n");
            Close();
        }

        //Skapa en table named student
        public void CreateTable()
        {
            Open();
            _command.CommandText = "CREATE TABLE students(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, age INT);";
            _command.ExecuteNonQuery();

            Console.WriteLine("Table 'students' created\n");
            Close();
        }

        //Add student to student table
        public void AddStudentToStudentTable(string firstName, string lastName, int age)
        {
            Open();
            try
            {
                _command.CommandText = "INSERT INTO students (firstName, lastName, age) VALUES (@firstName, @lastName, @age);";


                SQLiteParameter firstNameParam = new SQLiteParameter("@firstName", System.Data.DbType.String);
                SQLiteParameter lastNameParam = new SQLiteParameter("@lastName", System.Data.DbType.String);
                SQLiteParameter ageParam = new SQLiteParameter("@age", System.Data.DbType.Int32);

                firstNameParam.Value = firstName;
                lastNameParam.Value = lastName;
                ageParam.Value = age;

                _command.Parameters.Add(firstNameParam);
                _command.Parameters.Add(lastNameParam);
                _command.Parameters.Add(ageParam);

                _command.Prepare();
                _command.ExecuteNonQuery();
                Close();
                MessageBox.Show("Student Added!");
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }

        }

        //Show all student in students table
        public void ShowAllStudent(DataGrid dataGrid1)
        {
            Open();
            try
            {
                _command.CommandText = "SELECT * FROM students;";
                _command.ExecuteNonQuery();

                dataAdp = new SQLiteDataAdapter(_command);
                dt = new DataTable();
                dataAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                Close();
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }
        }
        //Show all courses and which student belong to the course
        public void ShowAllStudentCourse(DataGrid dataGrid1)
        {
            Open();
            try
            {
                _command.CommandText = "SELECT courses.id AS \"Course ID\", courses.courseName AS \"Course Name\", students.firstName AS \"First Name\", students.lastName AS \"Last Name\", students.id AS \"Student ID\" FROM students INNER JOIN courses ON courses.id = student_course.courseID INNER JOIN student_course ON students.id = student_course.studentID ORDER BY courseName ASC;";
                _command.ExecuteNonQuery();

                dataAdp = new SQLiteDataAdapter(_command);
                dt = new DataTable();
                dataAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                Close();
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }
        }

        //Remove student using ID
        public void RemoveStudentByID(int id)
        {
            Open();
            try
            {
                _command.CommandText = "DELETE FROM students WHERE id = @id;";

                SQLiteParameter idParam = new SQLiteParameter("@id", System.Data.DbType.Int32);

                idParam.Value = id;

                _command.Parameters.Add(idParam);

                _command.Prepare();
                _command.ExecuteNonQuery();
                Close();
                MessageBox.Show("Student Removed!");
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }
        }

        //Add course to courses table
        public void AddCourse(string courseName)
        {
            Open();
            try
            {
                _command.CommandText = "INSERT INTO courses (courseName) VALUES (@courseName);";


                SQLiteParameter courseNameParam = new SQLiteParameter("@courseName", System.Data.DbType.String);

                courseNameParam.Value = courseName;

                _command.Parameters.Add(courseNameParam);

                _command.Prepare();
                _command.ExecuteNonQuery();
                MessageBox.Show("Course Added!");
                Close();
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }
        }

        //Add studentID and courseID to student_course Table
        public void AddStudentToCourse(int studentID, int courseID)
        {
            Open();
            try
            {
                _command.CommandText = "INSERT INTO student_course (studentID, courseID) VALUES (@studentID, @courseID);";

                SQLiteParameter studentIDPARAM = new SQLiteParameter("@studentID", System.Data.DbType.Int32);
                SQLiteParameter courseIDPARAM = new SQLiteParameter("@courseID", System.Data.DbType.Int32);

                studentIDPARAM.Value = studentID;
                courseIDPARAM.Value = courseID;

                _command.Parameters.Add(studentIDPARAM);
                _command.Parameters.Add(courseIDPARAM);


                _command.Prepare();
                _command.ExecuteNonQuery();
                MessageBox.Show("Student Added to course!");
                Close();
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }
        }

        //Show all course and courseID in courses table
        public void ShowCourses(DataGrid dataGrid1)
        {
            Open();
            try
            {
                _command.CommandText = "SELECT * FROM courses;";
                _command.ExecuteNonQuery();

                dataAdp = new SQLiteDataAdapter(_command);
                dt = new DataTable();
                dataAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                Close();
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }
        }

        //Find Student by firstname
        public void FindStudentByFirstName(string firstName, DataGrid dataGrid1)
        {
            Open();
            try
            {
                _command.CommandText = "SELECT * FROM students WHERE firstName = @firstName;";
                _command.Parameters.AddWithValue("@firstName", firstName);
                _command.ExecuteNonQuery();

                dataAdp = new SQLiteDataAdapter(_command);
                dt = new DataTable();
                dataAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                Close();
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }

        }

        public int FindStudentByID(int id, DataGrid dataGrid1)
        {
            Open();
            try
            {
                _command.CommandText = "SELECT * FROM students WHERE id = @id;";
                _command.Parameters.AddWithValue("@id", id);
                _command.ExecuteNonQuery();

                dataAdp = new SQLiteDataAdapter(_command);
                dt = new DataTable();
                dataAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                return id;
                Close();
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }
            return id;
        }

        public void UpdateStudentFirstName(int id, string firstName, string lastName, int age, DataGrid dataGrid1)
        {
            Open();
            try
            {
                FindStudentByID(id, dataGrid1);
                _command.CommandText = "UPDATE students SET firstName = @firstName, lastName = @lastName, age = @age WHERE id = @id;";
                _command.ExecuteNonQuery();

                Close();
            }
            catch (Exception expectation)
            {
                MessageBox.Show(expectation.Message);
            }

        }
    }

}

//To Add
// Update student information
// Remove course
// Find course by name
// Update course information