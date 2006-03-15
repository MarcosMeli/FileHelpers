
FileHelperEngine<${ClassName}> engine = new FileHelperEngine<${ClassName}>();

${ClassName}[] res = engine.ReadFile(@"YourFile.txt");

foreach(${ClassName} record in res)
{
	// Your Code Here
}