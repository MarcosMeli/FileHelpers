
${ClassName}[] record;

FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(${ClassName}));

engine.BeginReadFile(@"YourFile.txt");

while(engine.ReadNext() != null)
{
	record = (${ClassName}) engine.LastRecord;

	// Your Code Here
}

engine.EndsRead();
