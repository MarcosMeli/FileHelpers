using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    [Category("BigSorter")]
    public class SorterTests
    {
        private const int MegaByte = 1024*1024;


        [Test]
        public void Sort6xhalf()
        {
            SortMb<OrdersTab>(4*MegaByte, (int) (0.5*MegaByte));
        }

        [Test]
        public void Sort6x1()
        {
            SortMb<OrdersTab>(4*MegaByte, 1*MegaByte);
        }

        [Test]
        public void Sort6x2()
        {
            SortMb<OrdersTab>(4*MegaByte, 2*MegaByte);
        }

        [Test]
        public void Sort6x35()
        {
            SortMb<OrdersTab>(4*MegaByte, (int) (3.5*MegaByte));
        }

        [Test]
        public void Sort6x6()
        {
            SortMb<OrdersTab>(4*MegaByte, 4*MegaByte);
        }

        [Test]
        public void Sort6x7()
        {
            SortMb<OrdersTab>(4*MegaByte, 7*MegaByte);
        }

        [Test]
        public void Sort6x20()
        {
            SortMb<OrdersTab>(4*MegaByte, 20*MegaByte);
        }

        [Test]
        public void Sort6x2Reverse()
        {
            SortMb<OrdersTab>(4*MegaByte, 20*MegaByte, false);
        }


        [Test]
        public void Sort6xhalfHeaderFooter()
        {
            SortMb<OrdersTabHeaderFooter>(4*MegaByte, (int) (0.5*MegaByte));
        }


        [Test]
        public void Sort6x1HeaderFooter()
        {
            SortMb<OrdersTabHeaderFooter>(4*MegaByte, 1*MegaByte);
        }

        [Test]
        public void Sort6x2HeaderFooter()
        {
            SortMb<OrdersTabHeaderFooter>(4*MegaByte, 2*MegaByte);
        }

        [Test]
        public void Sort6x35HeaderFooter()
        {
            SortMb<OrdersTabHeaderFooter>(4*MegaByte, (int) (3.5*MegaByte));
        }

        [Test]
        public void Sort6x6HeaderFooter()
        {
            SortMb<OrdersTabHeaderFooter>(4*MegaByte, 4*MegaByte);
        }

        [Test]
        public void Sort6x7HeaderFooter()
        {
            SortMb<OrdersTabHeaderFooter>(4*MegaByte, 4*MegaByte);
        }

        [Test]
        public void Sort6x20HeaderFooter()
        {
            SortMb<OrdersTabHeaderFooter>(4*MegaByte, 20*MegaByte);
        }

        [Test]
        public void Sort6x2ReverseHeaderFooter()
        {
            SortMb<OrdersTabHeaderFooter>(4*MegaByte, 20*MegaByte, false);
        }


        private void SortMb<T>(int totalSize, int blockSize, bool ascending)
            where T : class, IComparable<T>
        {
            using (var temp = new TempFileFactory())
            using (var temp2 = new TempFileFactory())
            using (var temp3 = new TempFileFactory()) {
                if (typeof (T) == typeof (OrdersTab))
                    CreateTempFile(totalSize, temp, ascending);
                else
                    CreateTempFileHeaderFooter(totalSize, temp, ascending);

                var sorter = new BigFileSorter(blockSize);
                sorter.Sort(temp, temp2);

                var sorter2 = new BigFileSorter<T>(blockSize);
                sorter2.Sort(temp, temp3);

                if (!ascending)
                    ReverseFile(temp);

                AssertSameFile(temp, temp2);
                AssertSameFile(temp, temp3);
            }
        }

        private void ReverseFile(string temp)
        {
            var data = File.ReadAllLines(temp);
            Array.Sort(data);
            File.WriteAllLines(temp, data);
        }

        public void SortMb<T>(int totalSize, int blockSize) where T : class, IComparable<T>
        {
            SortMb<T>(totalSize, blockSize, true);
        }


        private void AssertSameFile(string temp, string temp2)
        {
            var fi1 = new FileInfo(temp);
            var fi2 = new FileInfo(temp2);

            Check.That(fi1.Length).IsEqualTo(fi2.Length);

            using (var sr1 = new StreamReader(temp))
            using (var sr2 = new StreamReader(temp)) {
                while (true) {
                    var line1 = sr1.ReadLine();
                    var line2 = sr2.ReadLine();

                    Check.That(line1).IsEqualTo(line2);

                    if (line1 == null)
                        break;
                }
            }
        }

        private void CreateTempFile(int sizeOfFile, string fileName, bool ascending)
        {
            int size = 0;
            var engine = new FileHelperAsyncEngine<OrdersTab>();
            engine.AfterWriteRecord += (sender, e) => size += e.RecordLine.Length;

            using (engine.BeginWriteFile(fileName)) {
                var i = 1;
                while (size < sizeOfFile) {
                    var order = new OrdersTab();
                    order.CustomerID = "sdads";
                    order.EmployeeID = 123;
                    order.Freight = 123;
                    order.OrderDate = new DateTime(2009, 10, 23);
                    if (ascending)
                        order.OrderID = 1000000 + i;
                    else
                        order.OrderID = 1000000 - i;
                    order.RequiredDate = new DateTime(2009, 10, 20);
                    order.ShippedDate = new DateTime(2009, 10, 21);
                    order.ShipVia = 123;

                    engine.WriteNext(order);
                    i++;
                }
            }
        }


        private void CreateTempFileHeaderFooter(int sizeOfFile, string fileName, bool ascending)
        {
            int size = 0;
            var engine = new FileHelperAsyncEngine<OrdersTabHeaderFooter>();
            engine.AfterWriteRecord += (sender, e) => size += e.RecordLine.Length;

            engine.HeaderText = "Test Header, Test Header \r\nAnotherLine";
            engine.FooterText = "Footer Text";
            using (engine.BeginWriteFile(fileName)) {
                var i = 1;
                while (size < sizeOfFile) {
                    var order = new OrdersTabHeaderFooter();
                    order.CustomerID = "sdads";
                    order.EmployeeID = 123;
                    order.Freight = 123;
                    order.OrderDate = new DateTime(2009, 10, 23);
                    if (ascending)
                        order.OrderID = 1000000 + i;
                    else
                        order.OrderID = 1000000 - i;
                    order.RequiredDate = new DateTime(2009, 10, 20);
                    order.ShippedDate = new DateTime(2009, 10, 21);
                    order.ShipVia = 123;

                    engine.WriteNext(order);
                    i++;
                }
            }
        }

        [DelimitedRecord("\t")]
        [IgnoreFirst(2)]
        [IgnoreLast(2)]
        public class OrdersTabHeaderFooter
            : IComparable<OrdersTabHeaderFooter>
        {
            public int OrderID;

            public string CustomerID;

            public int EmployeeID;

            public DateTime OrderDate;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime RequiredDate;

            [FieldNullValue(typeof (DateTime), "2005-1-1")]
            public DateTime ShippedDate;

            public int ShipVia;

            public decimal Freight;

            public int CompareTo(OrdersTabHeaderFooter other)
            {
                return this.OrderID.CompareTo(other.OrderID);
            }
        }
    }
}