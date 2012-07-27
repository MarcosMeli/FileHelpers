
var arr = new List<${ClassName}>();

${ClassName} record;

// record = new ${ClassName}();
// Assign Record Values
// record.Field1 = value;

// Fill the array with your records
// arr.Add(record);

var engine = new FileHelperEngine(typeof(${ClassName}));

engine.WriteFile(@"YourFile.txt", arr);
