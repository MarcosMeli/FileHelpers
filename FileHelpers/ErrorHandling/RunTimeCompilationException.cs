

using System;
using System.CodeDom.Compiler;

namespace FileHelpers.Dynamic
{
	/// <summary>
	/// Exception with error information of the run time compilation.
	/// </summary>
	[Serializable]
	public sealed class RunTimeCompilationException : FileHelpersException
	{
		internal RunTimeCompilationException(string message, string sourceCode, CompilerErrorCollection errors) : base(message)
		{
			mSourceCode = sourceCode;
			mCompilerErrors = errors;
		}

		private string mSourceCode;

		/// <summary>
		/// The source code that generates the Exception
		/// </summary>
		public string SourceCode
		{
			get { return mSourceCode; }
		}

		private CompilerErrorCollection mCompilerErrors;

		/// <summary>
		/// The errors returned from the compiler.
		/// </summary>
		public CompilerErrorCollection CompilerErrors
		{
			get { return mCompilerErrors; }
		}
	}
}