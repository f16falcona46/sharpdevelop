/*
 * Created by SharpDevelop.
 * User: Jason
 * Date: 9/1/2016
 * Time: 10:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace SBIL
{
	public enum SBIL_Words : byte
	{
		SWAP,
		SWAPN,
		DUP,
		DUPN,
		NDUP,
		NDUPN,
		DROP,
		DROPN,
		PICK,
		ROLL,
		ROLLD,
		ROT,
		UNROT,
		STO,
		PURGE,
		RCL,
		STOM,
		RCLM,
		IFT,
		IFTE,
		RPT,
		GET,
		EVAL,
		PUSH,
		POP,
		CONCT,
		SIZE,
		APPLY,
		THROW,
		SETHD,
		BAND,
		BOR,
		BNOT,
		BXOR,
		AND,
		OR,
		NOT,
		XOR,
		EQ,
		NEQ,
		GEQ,
		LEQ,
		GT,
		LT,
		ADD,
		SUB,
		MUL,
		DIV,
		MOD,
		POW,
		RSHFT,
		LSHFT,
		TYPCO,
		TYPOF,
		YIELD
	}
	
	public enum SBIL_Type : byte
	{
		INT,
		UNS,
		DEC,
		LST,
		PGM,
		NAM,
		STR
	}
	
	public enum SBIL_Internal_Types : byte
	{
		INT,
		UNS,
		DEC,
		LST,
		LST_PTR,
		PGM,
		PGM_PTR,
		NAM,
		HASH_NAM,
		STR,
		CONTINUED
	}
	
	public enum SBIL_ExitState : byte
	{
		NOERROR,
		YIELD,
		DONE,
		UNHANDLED_EXCEPT,
		
	}
	
	public enum SBIL_Internal_ExitState : byte
	{
		CONTINUE,
		YIELD,
		DONE,
		EXCEPTION
	}
	
	public class SBIL_State
	{
		public int pc;
		public int dsp;
		public int rsp;
		public int stack_size;
		public byte[] dsmem; //data stack
		public byte[] rsmem; //return stack
		public byte[] romem; //read only
	}
}