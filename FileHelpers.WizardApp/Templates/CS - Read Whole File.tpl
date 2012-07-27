
var engine = new FileHelperEngine(typeof(${ClassName}));

var res = (${ClassName}[]) engine.ReadFile(@"YourFile.txt");

foreach(var record in res)
{
	// Your Code Here
}