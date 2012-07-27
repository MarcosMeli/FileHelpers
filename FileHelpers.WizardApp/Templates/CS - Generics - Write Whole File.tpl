
var arr = new List<${ClassName}>();

${ClassName} record;

// record = new ${ClassName}();
// Assign Record Values
// arr.Field1 = value;

// Fill the array with your records
// arr.Add(record);

var engine = new FileHelperEngine<${ClassName}>();

engine.WriteFile(@"YourFile.txt", arr);
