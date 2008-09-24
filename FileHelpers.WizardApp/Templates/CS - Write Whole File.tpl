
List<${ClassName}> arr = new List<${ClassName}>();

${ClassName} record;

// record = new ${ClassName}();
// Assign Record Values

// Fill the array with your records
// arr.Add(record);

FileHelperEngine engine = new FileHelperEngine(typeof(${ClassName}));

engine.WriteFile(@"YourFile.txt", arr);
