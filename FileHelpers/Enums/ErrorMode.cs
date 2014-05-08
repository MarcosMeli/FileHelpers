using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
	/// <summary>Indicates the behavior when the engine classes like <see cref="FileHelperEngine"/> class found an error.</summary>
	public enum ErrorMode
	{
		/// <summary>Default value, this simply Rethrow the original exception.</summary>
		ThrowException = 0,
		/// <summary>Add an <see cref="ErrorInfo"/> to the array of <see cref="ErrorManager.Errors"/>.</summary>
		SaveAndContinue,
		/// <summary>Simply ignores the exception and continues processing the file.</summary>
		IgnoreAndContinue,
        /// <summary>Add an <see cref="ErrorInfo"/> to the array of <see cref="ErrorManager.Errors"/> and continues processing the current line.</summary>
        SaveAndContinueLine,
	}
}