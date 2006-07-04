using System;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace FileHelpers
{
    public sealed class ClassBuilder
    {
		#region LoadFromString

		public static Type ClassFromString(string classStr)
		{
			return ClassFromString(classStr, string.Empty);
		}

		public static Type ClassFromString(string classStr, NetLenguage leng)
		{
			return ClassFromString(classStr, string.Empty, leng);
		}

		public static Type ClassFromString(string classStr, string className)
		{
			return ClassFromString(classStr, className, NetLenguage.CSharp);
		}
		
		public static Type ClassFromString(string classStr, string className, NetLenguage leng)
		{
			ICodeCompiler comp = null;

			switch(leng)
			{
				case NetLenguage.CSharp:
					comp = (new CSharpCodeProvider()).CreateCompiler();
					break;

				case NetLenguage.VbNet:
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
				case NetLenguage.CSharp:
					code.Append("using System; \n");
					code.Append("using FileHelpers; \n");
					code.Append("using System.Data; \n\n");
					break;

				case NetLenguage.VbNet:
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
					error.AppendFormat("{0}\n", err.ErrorText);
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
					return ts[0];
				else
					throw new BadUsageException("The Compiled assembly don´t have any Type inside.");
			}
		}

		#endregion

		#region CreateFromFile

		public static Type ClassFromFile(string filename, string className, NetLenguage leng)
		{
			StreamReader reader = new StreamReader(filename);
			string classDef = reader.ReadToEnd();
			reader.Close();

			return ClassFromString(classDef, className, leng);
		}

		public static Type ClassFromFile(string filename, string className)
		{
			return ClassFromFile(filename, className, NetLenguage.CSharp);
		}

		public static Type ClassFromFile(string filename)
		{
			return ClassFromFile(filename, string.Empty);
		}

		public static Type ClassFromFile(string filename, NetLenguage leng)
		{
			return ClassFromFile(filename, string.Empty, leng);
		}


		public static Type ClassFromBinaryFile(string filename)
		{
			return ClassFromBinaryFile(filename, string.Empty, NetLenguage.CSharp);
		}

		public static Type ClassFromBinaryFile(string filename, NetLenguage leng)
		{
			return ClassFromBinaryFile(filename, string.Empty, leng);
		}
    	
		public static Type ClassFromBinaryFile(string filename, string className, NetLenguage leng)
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

    	#region TrimMode

		private TrimMode mTrimMode = TrimMode.None;

		public TrimMode TrimMode
		{
			get { return mTrimMode; }
			set { mTrimMode = value; }
		}
		#endregion

    	private string mClassName;
    	
    	public ClassBuilder(string className)
		{
    		mClassName = className;
		}
    	
    	public Type CreateType()
    	{
    		StringBuilder sb = new StringBuilder();
    		return ClassFromString(sb.ToString());
    	}
    	
    	
		#region "  EncDec  "	
		
    	private class EncDec 
		{
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
    
			public static byte[] Encrypt(byte[] clearData, string Password) 
			{ 
				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
					new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
								   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

				return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16)); 

			}

			public static void Encrypt(string fileIn, 
				string fileOut, string Password) 
			{ 

				FileStream fsIn = new FileStream(fileIn, 
					FileMode.Open, FileAccess.Read); 
				FileStream fsOut = new FileStream(fileOut, 
					FileMode.OpenOrCreate, FileAccess.Write); 

				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
					new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
								   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

				Rijndael alg = Rijndael.Create(); 
				alg.Key = pdb.GetBytes(32); 
				alg.IV = pdb.GetBytes(16); 

				CryptoStream cs = new CryptoStream(fsOut, 
					alg.CreateEncryptor(), CryptoStreamMode.Write); 
				int bufferLen = 4096; 
				byte[] buffer = new byte[bufferLen]; 
				int bytesRead; 

				do 
				{ 
					// read a chunk of data from the input file 
					bytesRead = fsIn.Read(buffer, 0, bufferLen); 

					// encrypt it 
					cs.Write(buffer, 0, bytesRead); 
				} while(bytesRead != 0); 

				// close everything 

				// this will also close the unrelying fsOut stream
				cs.Close(); 
				fsIn.Close();     
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
				return System.Text.Encoding.Unicode.GetString(decryptedData); 
			}

			public static byte[] Decrypt(byte[] cipherData, string Password) 
			{ 
				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
					new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
								   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 
				return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16)); 
			}

			// Decrypt a file into another file using a password 
			public static void Decrypt(string fileIn, 
				string fileOut, string Password) 
			{ 
    
				// First we are going to open the file streams 
				FileStream fsIn = new FileStream(fileIn,
					FileMode.Open, FileAccess.Read); 
				FileStream fsOut = new FileStream(fileOut,
					FileMode.OpenOrCreate, FileAccess.Write); 

				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
					new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
								   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 
				Rijndael alg = Rijndael.Create(); 

				alg.Key = pdb.GetBytes(32); 
				alg.IV = pdb.GetBytes(16); 

				CryptoStream cs = new CryptoStream(fsOut, 
					alg.CreateDecryptor(), CryptoStreamMode.Write); 

				int bufferLen = 4096; 
				byte[] buffer = new byte[bufferLen]; 
				int bytesRead; 

				do 
				{ 
					// read a chunk of data from the input file 
					bytesRead = fsIn.Read(buffer, 0, bufferLen); 

					// Decrypt it 
					cs.Write(buffer, 0, bytesRead); 

				} while(bytesRead != 0); 

				cs.Close(); // this will also close the unrelying fsOut stream 
				fsIn.Close();     
			}
		}
    	
		
    	#endregion
    }
}
