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

            Console.WriteLine("\t\tStudents from file:\n");
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }

            students[0].StudentNumber = 420;
            Console.WriteLine("\nStudent {0} edited.", students[0].FullName);
            students[1].Course = "Jiu-Jitsu";
            Console.WriteLine("\nStudent {0} edited.", students[0].FullName);

            Console.WriteLine("\t\tEdited students:\n");
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }

            // Insert students to excel storage
            // This method will save out excel file
            storage.InsertRecords(students);
            Console.WriteLine("Changes saved.");

            //-> /File
        }


        //-> File: Customer.cs
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
