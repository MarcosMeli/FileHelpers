using FileHelpers;
using FileHelpers.DataLink;
using FileHelpers.ExcelNPOIStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamplesFx
{
    //-> Name: Create excel storage and save it.
    //-> Runnable: true
    //-> Description: Shows how to create excel storage, fill it with object data and save

    public class ExcelCreateAndSave :
    ExampleBase
    {
        public override void Run()
        {
            //-> File: ExcelExample.cs

            // Create an excel storage for specific class
            // By default start row/column is 2/B (index 1)
            ExcelNPOIStorage storage = new ExcelNPOIStorage(typeof(Student));

            // Set storage file name -> that will be excel output file name
            // Extension must be .xlsx or .xls
            storage.FileName = "Students.xlsx";

            // Sheet name is not required. By default sheet name will be "Sheet0"
            storage.SheetName = "Students";
            storage.ColumnsHeaders.Add("Student number");
            storage.ColumnsHeaders.Add("Student name");
            storage.ColumnsHeaders.Add("Course name");

            // Test data
            int count = 3;
            Student[] students = new Student[count];

            students[0] = CreateStudent(0, "Chuck Norris", "Karate");
            students[1] = CreateStudent(1, "Steven Seagal", "Aikido");
            students[2] = CreateStudent(2, "Dennis Ritchie", "Programming");

            // Insert students to excel storage
            // This method will save out excel file
            storage.InsertRecords(students);

            //-> /File
        }


        //-> File: Customer.cs
        [DelimitedRecord("")]
        public class Student
        {
            public int StudentNumber { get; set; }

            public string FullName { get; set; }

            public string Course { get; set; }
        }
        //-> /File

        
        //-> File: CreateStudent.cs
        /// <summary>
        /// Create new student
        /// </summary>
        /// <returns>Student object</returns>
        public static Student CreateStudent(int studentNumber, string fullName, string course)
            => new Student()
            {
                StudentNumber = studentNumber,
                FullName = fullName,
                Course = course
            };
        //-> /File

        //-> File:ExcelTest.xlsx
        //-> /File
    }
}
