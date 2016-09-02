/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 9/1/2016
 * Time: 11:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace SBIL
{
	public static class SBIL_Funcs
	{
		public static void SBIL_CreateState(SBIL_State state, byte[] romem, byte[] dsmem, byte[] rsmem)
		{
			state.pc = 0;
			state.dsp = 0;
			state.rsp = 0;
			state.stack_size = 0;
			state.dsmem = dsmem;
			state.rsmem = rsmem;
			state.romem = romem;
		}
		
		public static SBIL_ExitState SBIL_Run(SBIL_State state)
		{
			SBIL_Internal_ExitState exitstate;
			while ((exitstate = SBIL_Internal_RunStep(state)) == SBIL_Internal_ExitState.CONTINUE) {}
			switch (exitstate)
			{
				case SBIL_Internal_ExitState.DONE:
					return SBIL_ExitState.DONE;
				case SBIL_Internal_ExitState.YIELD:
					return SBIL_ExitState.YIELD;
				case SBIL_Internal_ExitState.EXCEPTION:
				default:
					return SBIL_ExitState.UNHANDLED_EXCEPT;
			}
		}
		
		public static int SBIL_GetStackSize(SBIL_State state)
		{
			return state.stack_size;
		}
		
		public static SBIL_Type SBIL_GetItemType(SBIL_State state, uint index)
		{
			throw new System.NotImplementedException();
		}
		
		public static object SBIL_GetItem(SBIL_State state, uint index)
		{
			throw new System.NotImplementedException();
		}
		
		public static byte[] SBIL_Compile(byte[] input)
		{
			throw new System.NotImplementedException();
		}
		
		private static SBIL_Internal_ExitState SBIL_Internal_RunStep(SBIL_State state)
		{
			throw new System.NotImplementedException();
		}
	}
}