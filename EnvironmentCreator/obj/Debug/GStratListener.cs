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
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="GStratParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.2-SNAPSHOT")]
[System.CLSCompliant(false)]
public interface IGStratListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.effecte"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEffecte([NotNull] GStratParser.EffecteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.effecte"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEffecte([NotNull] GStratParser.EffecteContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.CallFn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCallFn([NotNull] GStratParser.CallFnContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.CallFn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCallFn([NotNull] GStratParser.CallFnContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.root"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRoot([NotNull] GStratParser.RootContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.root"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRoot([NotNull] GStratParser.RootContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.@int"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInt([NotNull] GStratParser.IntContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.@int"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInt([NotNull] GStratParser.IntContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.AddSub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddSub([NotNull] GStratParser.AddSubContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.AddSub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddSub([NotNull] GStratParser.AddSubContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.PrecondExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrecondExpr([NotNull] GStratParser.PrecondExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.PrecondExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrecondExpr([NotNull] GStratParser.PrecondExprContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.CallFnPrecond"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCallFnPrecond([NotNull] GStratParser.CallFnPrecondContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.CallFnPrecond"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCallFnPrecond([NotNull] GStratParser.CallFnPrecondContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.parenth"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenth([NotNull] GStratParser.ParenthContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.parenth"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenth([NotNull] GStratParser.ParenthContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterId([NotNull] GStratParser.IdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitId([NotNull] GStratParser.IdContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionCall([NotNull] GStratParser.FunctionCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionCall([NotNull] GStratParser.FunctionCallContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.NewType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNewType([NotNull] GStratParser.NewTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.NewType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNewType([NotNull] GStratParser.NewTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.AssignExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignExpr([NotNull] GStratParser.AssignExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.AssignExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignExpr([NotNull] GStratParser.AssignExprContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdent([NotNull] GStratParser.IdentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdent([NotNull] GStratParser.IdentContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAction([NotNull] GStratParser.ActionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAction([NotNull] GStratParser.ActionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.MulDivMod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMulDivMod([NotNull] GStratParser.MulDivModContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.MulDivMod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMulDivMod([NotNull] GStratParser.MulDivModContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.NewVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNewVariable([NotNull] GStratParser.NewVariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.NewVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNewVariable([NotNull] GStratParser.NewVariableContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GStratParser.NewInstances"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNewInstances([NotNull] GStratParser.NewInstancesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GStratParser.NewInstances"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNewInstances([NotNull] GStratParser.NewInstancesContext context);
}
} // namespace EnvironmentCreator.Gammars
