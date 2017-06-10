using FileHelpers;
using FileHelpers.ExcelNPOIStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamplesFx
{
    //-> Name: Open excel file, edit and save it
    //-> Runnable: true
    //-> Description: Shows how to load excel file to storage, edit it and save again

    public class ExportAndEdit :
    ExampleBase
    {
        public override void Run()
        {
            //-> File: ExcelExample.cs

            // Create an excel storage for specific class
            // startRow = 2 & startColumn = 1 -> for skipping column header names
            ExcelNPOIStorage storage = new ExcelNPOIStorage(typeof(Student), 2, 1);

            // Set storage file name -> represents the excel file name we want to read
            storage.FileName = "Students.xlsx";

            // Read from excel file
            Student[] students = storage.ExtractRecords() as Student[];

            Console.WriteLine("\t\tStudents from file:");
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }

            // Make some changes
            students[0].StudentNumber = 420;
            Console.WriteLine(Environment.NewLine + "Student {0} edited.", students[0].FullName);
            students[1].Course = "Jiu-Jitsu";
            Console.WriteLine("Student {0} edited.", students[1].FullName);

            Console.WriteLine(Environment.NewLine + "\t\tEdited students:");
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }

            // Insert students to excel storage
            // This method will save out excel file
            storage.InsertRecords(students);
            Console.WriteLine(Environment.NewLine + "Changes saved.");

            //-> /File
        }


        //-> File: Student.cs
        [DelimitedRecord("")]
        public class Student
        {
            public int StudentNumber { get; set; }

            public string FullName { get; set; }

            public string Course { get; set; }

            public override string ToString()
            {
                return $"{StudentNumber}: {FullName} is on course: {Course}";
            }
        }
        //-> /File
    }
}
