using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Dynamic
{
	/// <summary>
	/// Exception with error information of the run time compilation.
	/// </summary>
	[Serializable]
	public sealed class RunTimeCompilationException : FileHelpersException
	{
        /// <summary>
        /// Compilation exception happen loading a dynamic class
        /// </summary>
        /// <param name="message">Message for the error</param>
        /// <param name="sourceCode">Source code reference???</param>
        /// <param name="errors">Errors from compiler</param>
        internal RunTimeCompilationException(string message, string sourceCode, CompilerErrorCollection errors) : base(message)
		{
			mSourceCode = sourceCode;
			mCompilerErrors = errors;
		}

		private readonly string mSourceCode;

		/// <summary>
		/// The source code that generates the Exception
		/// </summary>
		public string SourceCode
		{
			get { return mSourceCode; }
		}

		private readonly CompilerErrorCollection mCompilerErrors;

		/// <summary>
		/// The errors returned from the compiler.
		/// </summary>
		public CompilerErrorCollection CompilerErrors
		{
			get { return mCompilerErrors; }
		}
	}
}