
var engine = new FileHelperAsyncEngine(typeof(${ClassName}));

using(engine.BeginReadFile(@"YourFile.txt"))
{
	foreach(${ClassName} record in engine)
	{

		// Your Code Here

	}
}

