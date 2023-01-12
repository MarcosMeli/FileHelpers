using System;
using System.IO;
using System.Linq;
using FileHelpers.Options;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class RecordOptionsTests
    {
        [Test]
        public void ReadFileNotifyRecord()
        {
            var engine = new DelimitedFileEngine<SampleTypeCustomAttribute>();

            engine.Options.ObjectToValuesHandler = x => Process(x, engine.Options);

            var res = engine.WriteString(new SampleTypeCustomAttribute[] {
                new SampleTypeCustomAttribute() { One = 100, Two = 200 },
                new SampleTypeCustomAttribute() { One = 300, Two = 400 },
            });

            var expected = "101,-201\r\n301,-401\r\n";
            Assert.AreEqual(expected, res);
        }

        private object[] Process(object obj, RecordOptions options) {
            var result = new object[options.FieldCount];

            for (var i = 0; i < options.FieldCount; i++) {
                result[i] = GetValueByAttributes(obj, options.Fields[i]);
			}

            return result;
		}

        private object GetValueByAttributes(object obj, FieldBase fieldBase) {
            var value = (int)fieldBase.FieldInfo.GetValue(obj);

            if (fieldBase.FieldType == typeof(int)) {
                var attribs = fieldBase.FieldInfo
                    .GetCustomAttributes(false)
                    .OfType<ICustomAttribute>();

                var number = (int)value;
                foreach (var attrib in attribs) {
                    if (attrib is AddOneAttribute) {
                        number++;
					}
                    else if (attrib is NegativeAttribute) {
                        number = -number;
					}
				}
                value = number;
			}

            return value;
		}
    }
}