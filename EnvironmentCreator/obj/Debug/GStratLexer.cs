//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.2.2-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\ANTLR\Grammars\GStrat.g4 by ANTLR 4.2.2-SNAPSHOT

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace EnvironmentCreator.Gammars {
#pragma warning disable 3021
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.2-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class GStratLexer : Lexer {
	public const int
		T__14=1, T__13=2, T__12=3, T__11=4, T__10=5, T__9=6, T__8=7, T__7=8, T__6=9, 
		T__5=10, T__4=11, T__3=12, T__2=13, T__1=14, T__0=15, BOOL_ID=16, NUMBER_ID=17, 
		NAME=18, INT=19, MUL=20, DIV=21, MOD=22, ADD=23, SUB=24, EQUAL=25, NOT_EQUAL=26, 
		LESS_THEN=27, LESS__OR_EQ=28, MORE_THEN=29, MORE_OR_EQ=30, ASSIGN=31, 
		ASSIGN_MINUS=32, ASSIGN_ADD=33, ASSIGN_DIV=34, ASSIGN_MUL=35, ASSIGN_MOD=36, 
		WS=37;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"<INVALID>",
		"','", "'('", "'{'", "'action'", "'effe'", "'extends'", "'}'", "'effs'", 
		"'pre'", "')'", "'.'", "';'", "'duration'", "'type'", "'instance'", "'boolean'", 
		"'number'", "NAME", "INT", "'*'", "'/'", "'%'", "'+'", "'-'", "'=='", 
		"'!='", "'<'", "'<='", "'>'", "'>='", "'='", "'-='", "'+='", "'/='", "'*='", 
		"'%='", "WS"
	};
	public static readonly string[] ruleNames = {
		"T__14", "T__13", "T__12", "T__11", "T__10", "T__9", "T__8", "T__7", "T__6", 
		"T__5", "T__4", "T__3", "T__2", "T__1", "T__0", "BOOL_ID", "NUMBER_ID", 
		"NAME", "INT", "MUL", "DIV", "MOD", "ADD", "SUB", "EQUAL", "NOT_EQUAL", 
		"LESS_THEN", "LESS__OR_EQ", "MORE_THEN", "MORE_OR_EQ", "ASSIGN", "ASSIGN_MINUS", 
		"ASSIGN_ADD", "ASSIGN_DIV", "ASSIGN_MUL", "ASSIGN_MOD", "WS"
	};


	public GStratLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	public override string GrammarFileName { get { return "GStrat.g4"; } }

	public override string[] TokenNames { get { return tokenNames; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\'\xDD\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x3\x2\x3\x2\x3\x3\x3\x3\x3\x4"+
		"\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3"+
		"\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\t\x3\t"+
		"\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\f\x3\f\x3\r\x3\r\x3\xE"+
		"\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3"+
		"\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10"+
		"\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12"+
		"\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\a\x13\xA1\n\x13\f\x13"+
		"\xE\x13\xA4\v\x13\x3\x14\x3\x14\x3\x14\x6\x14\xA9\n\x14\r\x14\xE\x14\xAA"+
		"\x5\x14\xAD\n\x14\x3\x15\x3\x15\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3"+
		"\x18\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3"+
		"\x1C\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3\x1F\x3 \x3 \x3"+
		"!\x3!\x3!\x3\"\x3\"\x3\"\x3#\x3#\x3#\x3$\x3$\x3$\x3%\x3%\x3%\x3&\x3&\x3"+
		"&\x3&\x2\x2\x2\'\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2"+
		"\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10"+
		"\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/"+
		"\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F"+
		"=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'\x3\x2\a\x5\x2\x43"+
		"\\\x61\x61\x63|\x6\x2\x32;\x43\\\x61\x61\x63|\x3\x2\x32;\x3\x2\x33;\x5"+
		"\x2\v\f\xF\xF\"\"\xDF\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2"+
		"\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2"+
		"\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3"+
		"\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2"+
		"\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'"+
		"\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2"+
		"\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37"+
		"\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3"+
		"\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2"+
		"G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x3M\x3\x2\x2\x2\x5O\x3\x2"+
		"\x2\x2\aQ\x3\x2\x2\x2\tS\x3\x2\x2\x2\vZ\x3\x2\x2\x2\r_\x3\x2\x2\x2\xF"+
		"g\x3\x2\x2\x2\x11i\x3\x2\x2\x2\x13n\x3\x2\x2\x2\x15r\x3\x2\x2\x2\x17t"+
		"\x3\x2\x2\x2\x19v\x3\x2\x2\x2\x1Bx\x3\x2\x2\x2\x1D\x81\x3\x2\x2\x2\x1F"+
		"\x86\x3\x2\x2\x2!\x8F\x3\x2\x2\x2#\x97\x3\x2\x2\x2%\x9E\x3\x2\x2\x2\'"+
		"\xAC\x3\x2\x2\x2)\xAE\x3\x2\x2\x2+\xB0\x3\x2\x2\x2-\xB2\x3\x2\x2\x2/\xB4"+
		"\x3\x2\x2\x2\x31\xB6\x3\x2\x2\x2\x33\xB8\x3\x2\x2\x2\x35\xBB\x3\x2\x2"+
		"\x2\x37\xBE\x3\x2\x2\x2\x39\xC0\x3\x2\x2\x2;\xC3\x3\x2\x2\x2=\xC5\x3\x2"+
		"\x2\x2?\xC8\x3\x2\x2\x2\x41\xCA\x3\x2\x2\x2\x43\xCD\x3\x2\x2\x2\x45\xD0"+
		"\x3\x2\x2\x2G\xD3\x3\x2\x2\x2I\xD6\x3\x2\x2\x2K\xD9\x3\x2\x2\x2MN\a.\x2"+
		"\x2N\x4\x3\x2\x2\x2OP\a*\x2\x2P\x6\x3\x2\x2\x2QR\a}\x2\x2R\b\x3\x2\x2"+
		"\x2ST\a\x63\x2\x2TU\a\x65\x2\x2UV\av\x2\x2VW\ak\x2\x2WX\aq\x2\x2XY\ap"+
		"\x2\x2Y\n\x3\x2\x2\x2Z[\ag\x2\x2[\\\ah\x2\x2\\]\ah\x2\x2]^\ag\x2\x2^\f"+
		"\x3\x2\x2\x2_`\ag\x2\x2`\x61\az\x2\x2\x61\x62\av\x2\x2\x62\x63\ag\x2\x2"+
		"\x63\x64\ap\x2\x2\x64\x65\a\x66\x2\x2\x65\x66\au\x2\x2\x66\xE\x3\x2\x2"+
		"\x2gh\a\x7F\x2\x2h\x10\x3\x2\x2\x2ij\ag\x2\x2jk\ah\x2\x2kl\ah\x2\x2lm"+
		"\au\x2\x2m\x12\x3\x2\x2\x2no\ar\x2\x2op\at\x2\x2pq\ag\x2\x2q\x14\x3\x2"+
		"\x2\x2rs\a+\x2\x2s\x16\x3\x2\x2\x2tu\a\x30\x2\x2u\x18\x3\x2\x2\x2vw\a"+
		"=\x2\x2w\x1A\x3\x2\x2\x2xy\a\x66\x2\x2yz\aw\x2\x2z{\at\x2\x2{|\a\x63\x2"+
		"\x2|}\av\x2\x2}~\ak\x2\x2~\x7F\aq\x2\x2\x7F\x80\ap\x2\x2\x80\x1C\x3\x2"+
		"\x2\x2\x81\x82\av\x2\x2\x82\x83\a{\x2\x2\x83\x84\ar\x2\x2\x84\x85\ag\x2"+
		"\x2\x85\x1E\x3\x2\x2\x2\x86\x87\ak\x2\x2\x87\x88\ap\x2\x2\x88\x89\au\x2"+
		"\x2\x89\x8A\av\x2\x2\x8A\x8B\a\x63\x2\x2\x8B\x8C\ap\x2\x2\x8C\x8D\a\x65"+
		"\x2\x2\x8D\x8E\ag\x2\x2\x8E \x3\x2\x2\x2\x8F\x90\a\x64\x2\x2\x90\x91\a"+
		"q\x2\x2\x91\x92\aq\x2\x2\x92\x93\an\x2\x2\x93\x94\ag\x2\x2\x94\x95\a\x63"+
		"\x2\x2\x95\x96\ap\x2\x2\x96\"\x3\x2\x2\x2\x97\x98\ap\x2\x2\x98\x99\aw"+
		"\x2\x2\x99\x9A\ao\x2\x2\x9A\x9B\a\x64\x2\x2\x9B\x9C\ag\x2\x2\x9C\x9D\a"+
		"t\x2\x2\x9D$\x3\x2\x2\x2\x9E\xA2\t\x2\x2\x2\x9F\xA1\t\x3\x2\x2\xA0\x9F"+
		"\x3\x2\x2\x2\xA1\xA4\x3\x2\x2\x2\xA2\xA0\x3\x2\x2\x2\xA2\xA3\x3\x2\x2"+
		"\x2\xA3&\x3\x2\x2\x2\xA4\xA2\x3\x2\x2\x2\xA5\xAD\t\x4\x2\x2\xA6\xA8\t"+
		"\x5\x2\x2\xA7\xA9\t\x4\x2\x2\xA8\xA7\x3\x2\x2\x2\xA9\xAA\x3\x2\x2\x2\xAA"+
		"\xA8\x3\x2\x2\x2\xAA\xAB\x3\x2\x2\x2\xAB\xAD\x3\x2\x2\x2\xAC\xA5\x3\x2"+
		"\x2\x2\xAC\xA6\x3\x2\x2\x2\xAD(\x3\x2\x2\x2\xAE\xAF\a,\x2\x2\xAF*\x3\x2"+
		"\x2\x2\xB0\xB1\a\x31\x2\x2\xB1,\x3\x2\x2\x2\xB2\xB3\a\'\x2\x2\xB3.\x3"+
		"\x2\x2\x2\xB4\xB5\a-\x2\x2\xB5\x30\x3\x2\x2\x2\xB6\xB7\a/\x2\x2\xB7\x32"+
		"\x3\x2\x2\x2\xB8\xB9\a?\x2\x2\xB9\xBA\a?\x2\x2\xBA\x34\x3\x2\x2\x2\xBB"+
		"\xBC\a#\x2\x2\xBC\xBD\a?\x2\x2\xBD\x36\x3\x2\x2\x2\xBE\xBF\a>\x2\x2\xBF"+
		"\x38\x3\x2\x2\x2\xC0\xC1\a>\x2\x2\xC1\xC2\a?\x2\x2\xC2:\x3\x2\x2\x2\xC3"+
		"\xC4\a@\x2\x2\xC4<\x3\x2\x2\x2\xC5\xC6\a@\x2\x2\xC6\xC7\a?\x2\x2\xC7>"+
		"\x3\x2\x2\x2\xC8\xC9\a?\x2\x2\xC9@\x3\x2\x2\x2\xCA\xCB\a/\x2\x2\xCB\xCC"+
		"\a?\x2\x2\xCC\x42\x3\x2\x2\x2\xCD\xCE\a-\x2\x2\xCE\xCF\a?\x2\x2\xCF\x44"+
		"\x3\x2\x2\x2\xD0\xD1\a\x31\x2\x2\xD1\xD2\a?\x2\x2\xD2\x46\x3\x2\x2\x2"+
		"\xD3\xD4\a,\x2\x2\xD4\xD5\a?\x2\x2\xD5H\x3\x2\x2\x2\xD6\xD7\a\'\x2\x2"+
		"\xD7\xD8\a?\x2\x2\xD8J\x3\x2\x2\x2\xD9\xDA\t\x6\x2\x2\xDA\xDB\x3\x2\x2"+
		"\x2\xDB\xDC\b&\x2\x2\xDCL\x3\x2\x2\x2\x6\x2\xA2\xAA\xAC\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace EnvironmentCreator.Gammars
