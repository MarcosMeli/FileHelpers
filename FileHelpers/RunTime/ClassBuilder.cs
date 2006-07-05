using System;
using System.Collections;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace FileHelpers
{
	
	//-> ADD: Sealed !!!
	//-> ADD: Visibility !!!
	
    public abstract class ClassBuilder
    {
		//---------------------
		//->  STATIC METHODS

    	#region LoadFromString

		public static Type ClassFromString(string classStr)
		{
			return ClassFromString(classStr, string.Empty);
		}

		public static Type ClassFromString(string classStr, NetLanguage leng)
		{
			return ClassFromString(classStr, string.Empty, leng);
		}

		public static Type ClassFromString(string classStr, string className)
		{
			return ClassFromString(classStr, className, NetLanguage.CSharp);
		}
		
		public static Type ClassFromString(string classStr, string className, NetLanguage leng)
		{
			ICodeCompiler comp = null;

			switch(leng)
			{
				case NetLanguage.CSharp:
					comp = (new CSharpCodeProvider()).CreateCompiler();
					break;

				case NetLanguage.VbNet:
					comp = (new VBCodeProvider()).CreateCompiler();
					break;
			}

			CompilerParameters cp = new CompilerParameters();
			cp.ReferencedAssemblies.Add("system.dll");
			cp.ReferencedAssemblies.Add("system.data.dll");
			cp.ReferencedAssemblies.Add("filehelpers.dll");
			cp.GenerateExecutable = false;
			cp.GenerateInMemory = true;

			StringBuilder code = new StringBuilder();

			switch(leng)
			{
				case NetLanguage.CSharp:
					code.Append("using System; \n");
					code.Append("using FileHelpers; \n");
					code.Append("using System.Data; \n\n");
					break;

				case NetLanguage.VbNet:
					code.Append("Imports System \n");
					code.Append("Imports FileHelpers \n");
					code.Append("Imports System.Data \n\n");
					break;
			}

			code.Append(classStr);

			CompilerResults cr = comp.CompileAssemblyFromSource(cp, code.ToString());

			if (cr.Errors.HasErrors)
			{
				StringBuilder error = new StringBuilder();
				error.Append("Error Compiling Expression: ");
				foreach (CompilerError err in cr.Errors)
				{
					error.AppendFormat("Line {0}: {1}\n", err.Line, err.ErrorText);
				}
				throw new Exception("Error Compiling Expression: " + error.ToString());
			}
            
			//            Assembly.Load(cr.CompiledAssembly.);
			if (className != string.Empty)
				return cr.CompiledAssembly.GetType(className, true, true);
			else
			{
				Type[] ts = cr.CompiledAssembly.GetTypes();
				if (ts.Length > 0)
					foreach (Type t in ts)
					{
						if (t.FullName.StartsWith("My.My") == false)
							return t;
					}

				throw new BadUsageException("The Compiled assembly don´t have any Type inside.");
			}
		}

		#endregion

		#region CreateFromFile

		public static Type ClassFromSourceFile(string filename, string className, NetLanguage leng)
		{
			StreamReader reader = new StreamReader(filename);
			string classDef = reader.ReadToEnd();
			reader.Close();

			return ClassFromString(classDef, className, leng);
		}

		public static Type ClassFromSourceFile(string filename, string className)
		{
			return ClassFromSourceFile(filename, className, NetLanguage.CSharp);
		}

		public static Type ClassFromSourceFile(string filename)
		{
			return ClassFromSourceFile(filename, string.Empty);
		}

		public static Type ClassFromSourceFile(string filename, NetLanguage leng)
		{
			return ClassFromSourceFile(filename, string.Empty, leng);
		}


		public static Type ClassFromBinaryFile(string filename)
		{
			return ClassFromBinaryFile(filename, string.Empty, NetLanguage.CSharp);
		}

		public static Type ClassFromBinaryFile(string filename, NetLanguage leng)
		{
			return ClassFromBinaryFile(filename, string.Empty, leng);
		}
    	
		public static Type ClassFromBinaryFile(string filename, string className, NetLanguage leng)
		{
			
			StreamReader reader = new StreamReader(filename);
			string classDef = reader.ReadToEnd();
			reader.Close();
			
			classDef = EncDec.Decrypt(classDef, "withthefilehelpers1.0.0youcancodewithoutproblems1.5.0");

			return ClassFromString(classDef, className, leng);
		}

		public static void ClassToBinaryFile(string filename, string classDef)
		{

			classDef = EncDec.Encrypt(classDef, "withthefilehelpers1.0.0youcancodewithoutproblems1.5.0");
			
			StreamWriter writer = new StreamWriter(filename);
			writer.Write(classDef);
			writer.Close();
		}

		#endregion

		public void SaveToSourceFile(string filename)
		{
			SaveToSourceFile(filename, NetLanguage.CSharp);
		}

    	public void SaveToSourceFile(string filename, NetLanguage leng)
    	{
    		StreamWriter writer = new StreamWriter(filename);
    		writer.Write(GetClassCode(leng));
    		writer.Close();
    	}

		public void SaveToBinaryFile(string filename)
		{
			SaveToBinaryFile(filename, NetLanguage.CSharp);
		}

		public void SaveToBinaryFile(string filename, NetLanguage leng)
		{
			StreamWriter writer = new StreamWriter(filename);

			string classDef = GetClassCode(leng);
			classDef = EncDec.Encrypt(classDef, "withthefilehelpers1.0.0youcancodewithoutproblems1.5.0");
			writer.Write(classDef);
			writer.Close();
		}

    	internal ClassBuilder(string className)
		{
    		mClassName = className;
		}
    	
    	public Type CreateType()
    	{
    		string classCode = GetClassCode(NetLanguage.CSharp);
    		return ClassFromString(classCode, NetLanguage.CSharp);
    	}

    	   	
		//--------------
		//->  Fields 

		#region Fiields
    	
		protected ArrayList mFields = new ArrayList();

		protected void AddFieldInternal(FieldBuilder field)
		{
			field.mFieldIndex = mFields.Add(field);
		}

		public FieldBuilder[] Fields
		{
			get { return (FieldBuilder[]) mFields.ToArray(typeof(FieldBuilder)); }
		}

		public int FieldCount
		{
			get
			{
				return mFields.Count;
			}
		}


		public FieldBuilder FieldByIndex(int index)
		{
			return (FieldBuilder) mFields[index];
		}

		#endregion
    	
		#region ClassName
		
    	private string mClassName;
		public string ClassName
		{
			get { return mClassName; }
		}
		
    	#endregion

   	
    	//----------------------------
    	//->  ATTRIBUTE MAPPING
    	
		#region IgnoreFirstLines
    	
		private int mIgnoreFirstLines = 0;

		public int IgnoreFirstLines
		{
			get { return mIgnoreFirstLines; }
			set { mIgnoreFirstLines = value; }
		}
		
    	#endregion

		#region IgnoreLastLines
    	
		private int mIgnoreLastLines = 0;

		public int IgnoreLastLines
		{
			get { return mIgnoreLastLines; }
			set { mIgnoreLastLines = value; }
		}

		#endregion
    	
		#region IgnoreEmptyLines
    	
		private bool mIgnoreEmptyLines = false;

		public bool IgnoreEmptyLines
		{
			get { return mIgnoreEmptyLines; }
			set { mIgnoreEmptyLines = value; }
		}

    	#endregion

    	
    	#region "  EncDec  "	
		
    	private sealed class EncDec 
		{
    		private EncDec()
    		{}
    		
			public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV) 
			{ 
				MemoryStream ms = new MemoryStream(); 
				Rijndael alg = Rijndael.Create(); 
				alg.Key = Key; 
				alg.IV = IV; 
				CryptoStream cs = new CryptoStream(ms, 
					alg.CreateEncryptor(), CryptoStreamMode.Write); 
				cs.Write(clearData, 0, clearData.Length); 
				cs.Close(); 
				byte[] encryptedData = ms.ToArray();
				return encryptedData; 
			} 

			public static string Encrypt(string clearText, string Password) 
			{ 
				byte[] clearBytes = Encoding.Unicode.GetBytes(clearText); 

				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
					new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
								   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 
				byte[] encryptedData = Encrypt(clearBytes, 
					pdb.GetBytes(32), pdb.GetBytes(16)); 
				return Convert.ToBase64String(encryptedData); 
			}
    

			// Decrypt a byte array into a byte array using a key and an IV 
			public static byte[] Decrypt(byte[] cipherData, 
				byte[] Key, byte[] IV) 
			{ 
				MemoryStream ms = new MemoryStream(); 
				Rijndael alg = Rijndael.Create(); 
				alg.Key = Key; 
				alg.IV = IV; 

				CryptoStream cs = new CryptoStream(ms, 
					alg.CreateDecryptor(), CryptoStreamMode.Write); 

				cs.Write(cipherData, 0, cipherData.Length); 
				cs.Close(); 

				byte[] decryptedData = ms.ToArray(); 

				return decryptedData; 
			}

			public static string Decrypt(string cipherText, string Password) 
			{ 
				byte[] cipherBytes = Convert.FromBase64String(cipherText); 
				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
					new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
								   0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 
				byte[] decryptedData = Decrypt(cipherBytes, 
					pdb.GetBytes(32), pdb.GetBytes(16)); 
				return Encoding.Unicode.GetString(decryptedData); 
			}

		}
    	
		
    	#endregion
    	
		internal string GetClassCode(NetLanguage leng)
		{
			StringBuilder sb = new StringBuilder(100);
			
			AttributesBuilder attbs = new AttributesBuilder(leng);
			
			AddAttributesInternal(attbs, leng);
			AddAttributesCode(attbs, leng);
			
			sb.Append(attbs.GetAttributesCode());
			
			switch (leng)
			{
				case NetLanguage.VbNet:
					sb.Append("Public NotInheritable Class " + mClassName);
					sb.Append(StringHelper.NewLine);
					break;
				case NetLanguage.CSharp:
					sb.Append("public sealed class " + mClassName);
					sb.Append(StringHelper.NewLine);
					sb.Append("{");
					break;
			}

			sb.Append(StringHelper.NewLine);


			foreach (FieldBuilder field in mFields)
			{
				sb.Append(field.GetFieldCode(leng));
			}
			
			
			sb.Append(StringHelper.NewLine);
			
			switch (leng)
			{
				case NetLanguage.VbNet:
					sb.Append("End Class");
					break;
				case NetLanguage.CSharp:
					sb.Append("}");
					break;
			}

			sb.Append(StringHelper.NewLine);
			return sb.ToString();
			
		}
    	
		internal abstract void AddAttributesCode(AttributesBuilder attbs, NetLanguage leng);

		private void AddAttributesInternal(AttributesBuilder attbs, NetLanguage leng)
		{

			if (mIgnoreFirstLines != 0)
				attbs.AddAttribute("IgnoreFirst("+ mIgnoreFirstLines.ToString() +")");

			if (mIgnoreFirstLines != 0)
				attbs.AddAttribute("IgnoreLast("+ mIgnoreLastLines.ToString() +")");

			if (mIgnoreEmptyLines == true)
				attbs.AddAttribute("IgnoreEmptyLines()");

		
		}
    	
		
    	
    }
}
