using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
	public class SorterTests
	{
        private const int MegaByte = 1024 *1024;


        [Test]
        public void Sort6xhalf()
        {
            SortMb(6 * MegaByte, (int) (0.2 * MegaByte));
        }

        [Test]
        public void Sort6x1()
        {
            SortMb(6 * MegaByte, 1 * MegaByte);
        }

        [Test]
        public void Sort6x2()
        {
            SortMb(6 * MegaByte, 2 * MegaByte);
        }


        [Test]
        public void Sort6x6()
        {
            SortMb(6 * MegaByte, 6 * MegaByte);
        }

        [Test]
        public void Sort6x7()
        {
            SortMb(6 * MegaByte, 7 * MegaByte);
        }

        [Test]
        public void Sort6x20()
        {
            SortMb(6 * MegaByte, 20 * MegaByte);
        }

        [Test]
        public void Sort6x2Reverse()
        {
            SortMb(6 * MegaByte, 20 * MegaByte, false);
        }

        private void SortMb(int totalSize, int blockSize, bool ascending)
        {
            using (var temp = new TempFileFactory())
            {
                using (var temp2 = new TempFileFactory())
                {
                    using (var temp3 = new TempFileFactory())
                    {
                        CreateTempFile(totalSize, temp, ascending);

                        var sorter = new BigFileSorter(blockSize);
                        sorter.Sort(temp, temp2);

                        var sorter2 = new BigFileSorter<OrdersTab>();
                        sorter2.Sort(temp, temp3);

                        if (!ascending)
                            ReverseFile(temp);

                            AssertSameFile(temp, temp2);
                            AssertSameFile(temp, temp3);

                    }
                }
            }
        }

        private void ReverseFile(string temp)
        {
            var data = File.ReadAllLines(temp);
            Array.Sort(data);
            File.WriteAllLines(temp, data);
        }

        public void SortMb(int totalSize, int blockSize)
		{
		   SortMb(totalSize, blockSize, true);
		}


        private void AssertSameFile(string temp, string temp2)
        {
            var fi1 = new FileInfo(temp);
            var fi2 = new FileInfo(temp2);

            fi1.Length.AssertEqualTo(fi2.Length);

            using (var sr1 = new StreamReader(temp))
            {
                using (var sr2 = new StreamReader(temp))
                {
                    
                    while (true)
                    {
                        var line1 = sr1.ReadLine();
                        var line2 = sr2.ReadLine();

                        line1.AssertEqualTo(line2);

                        if (line1 == null)
                            break;
                    }
                }
            }
        }

        private void CreateTempFile(int sizeOfFile, string fileName, bool ascending)
	    {
	        int size = 0;
	        var engine = new FileHelperAsyncEngine<OrdersTab>();
	        engine.AfterWriteRecord += (sender, e) => size += e.RecordLine.Length; 

            using(engine.BeginWriteFile(fileName))
            {
                var i = 1;
                while (size < sizeOfFile)
                {
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

	}
}