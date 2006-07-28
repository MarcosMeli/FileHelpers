using System;
using System.CodeDom.Compiler;

#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

namespace FileHelpers.RunTime
{
	public class RunTimeCompilationException : FileHelperException
	{

		protected internal RunTimeCompilationException(string message, string sourceCode, CompilerErrorCollection errors) : base(message)
		{
			mSourceCode = sourceCode;
			mCompilerErrors = errors;
		}

		private string mSourceCode;

		public string SourceCode
		{
			get { return mSourceCode; }
		}

		private CompilerErrorCollection mCompilerErrors;

		public CompilerErrorCollection CompilerErrors
		{
			get { return mCompilerErrors; }
		}


	}
}