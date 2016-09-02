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

namespace SBILTester
{
	class Program
	{
		public static void Main(string[] args)
		{
			SBIL_State state = new SBIL_State();
			byte[] romem = System.IO.File.ReadAllBytes(args[0]);
			byte[] dsmem = new byte[2048];
			byte[] rsmem = new byte[1024];
			SBIL_Funcs.SBIL_CreateState(state, romem, dsmem, rsmem);
			SBIL_ExitState result = SBIL_Funcs.SBIL_Run(state);
			
			if (result == SBIL_ExitState.YIELD) {
				int stack_size = SBIL_Funcs.SBIL_GetStackSize(state);
				Console.WriteLine("Stack Items: {0}", stack_size);
				SBIL_Type type = SBIL_Funcs.SBIL_GetItemType(state, stack_size - 1);
				Console.WriteLine("Item {0} type: {1}", stack_size - 1, type);
				if (type == SBIL_Type.INT) {
					Console.WriteLine("Item {0} value: {1}", stack_size - 1, (int)SBIL_Funcs.SBIL_GetItem(state, stack_size - 1));
				}
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}