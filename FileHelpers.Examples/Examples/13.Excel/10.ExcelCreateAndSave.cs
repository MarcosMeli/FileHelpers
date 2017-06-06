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

            students[0] = new Student();
            students[0].StudentNumber = 0;
            students[0].FullName = "Chuck Norris";
            students[0].Course = "Karate";

            students[1] = new Student();
            students[1].StudentNumber = 1;
            students[1].FullName = "Steven Seagal";
            students[1].Course = "Aikido";

            students[2] = new Student();
            students[2].StudentNumber = 2;
            students[2].FullName = "Dennis Ritchie";
            students[2].Course = "Programming";

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

        //-> File:ExcelTest.xlsx
        //-> /File
    }
}
