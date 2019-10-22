// Generated from .\OData.g4 by ANTLR 4.7.2
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link ODataParser}.
 */
public interface ODataListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link ODataParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(ODataParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link ODataParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(ODataParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Parenthesis}
	 * labeled alternative in {@link ODataParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterParenthesis(ODataParser.ParenthesisContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Parenthesis}
	 * labeled alternative in {@link ODataParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitParenthesis(ODataParser.ParenthesisContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Equals}
	 * labeled alternative in {@link ODataParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterEquals(ODataParser.EqualsContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Equals}
	 * labeled alternative in {@link ODataParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitEquals(ODataParser.EqualsContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Text}
	 * labeled alternative in {@link ODataParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterText(ODataParser.TextContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Text}
	 * labeled alternative in {@link ODataParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitText(ODataParser.TextContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Logic}
	 * labeled alternative in {@link ODataParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterLogic(ODataParser.LogicContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Logic}
	 * labeled alternative in {@link ODataParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitLogic(ODataParser.LogicContext ctx);
}