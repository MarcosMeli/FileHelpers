var engine = new FileHelperAsyncEngine<${ClassName}>();

using(engine.BeginReadFile(@"YourFile.txt"))
{
	foreach(var record in engine)
	{

		// Your Code Here
	}
}

