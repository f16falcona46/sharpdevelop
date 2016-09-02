/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 9/1/2016
 * Time: 10:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using SBIL;

namespace SBILCompiler
{
	class Program
	{
		public static void Main(string[] args)
		{
			System.IO.File.WriteAllBytes(args[1], SBIL_Funcs.SBIL_Compile(System.IO.File.ReadAllBytes(args[0])));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}