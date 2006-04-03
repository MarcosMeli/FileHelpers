
FileHelperEngine engine = new FileHelperEngine(typeof(${ClassName}));

${ClassName}[] res = (${ClassName}[]) engine.ReadFile(@"YourFile.txt");

foreach(${ClassName} record in res)
{
	// Your Code Here
}