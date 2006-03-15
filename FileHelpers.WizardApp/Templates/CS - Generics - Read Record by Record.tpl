
${ClassName}[] record;

FileHelperAsyncEngine<${ClassName}> engine = new FileHelperAsyncEngine<${ClassName}>();

engine.BeginReadFile(@"YourFile.txt");

while(engine.ReadNext() != null)
{
	record = engine.LastRecord;

	// Your Code Here
}

engine.EndsRead();
