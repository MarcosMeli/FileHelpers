using System;

namespace FileHelpers
{

	public interface INotifyRead
	{
		void AfterRead(EngineBase engine, string line);
	}

	public interface INotifyWrite
	{
		void BeforeWrite(EngineBase engine);
	}

}
