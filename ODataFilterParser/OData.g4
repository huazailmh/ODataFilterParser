grammar OData;

/*
 * Parser Rules
 */

program: expression;

expression:
	'(' expression ')' # Parenthesis
	| expression operate = (
		Equal
		| NotEqual
		| GreaterThan
		| GreaterThanOrEqual
		| LessThan
		| LessThanOrEqual
	) expression										# Equals
	| expression keyword = (K_AND | K_OR) expression	# Logic
	| TEXT												# Text;

/*
 * Lexer Rules
 */

K_AND: A N D;
K_OR: O R;

Equal: E Q;
NotEqual: N E;
GreaterThan: G T;
GreaterThanOrEqual: G E;
LessThan: L T;
LessThanOrEqual: L E;

TEXT: (LOWERCASE | UPERCASE | DIGIT)+;

SPACES: [ \u000B\t\r\n] -> channel(HIDDEN);

fragment LOWERCASE: [a-z];
fragment UPERCASE: [A-Z];

fragment DIGIT: [0-9];
fragment DIGITS: DIGIT+;
fragment A: [aA];
fragment B: [bB];
fragment C: [cC];
fragment D: [dD];
fragment E: [eE];
fragment F: [fF];
fragment G: [gG];
fragment H: [hH];
fragment I: [iI];
fragment J: [jJ];
fragment K: [kK];
fragment L: [lL];
fragment M: [mM];
fragment N: [nN];
fragment O: [oO];
fragment P: [pP];
fragment Q: [qQ];
fragment R: [rR];
fragment S: [sS];
fragment T: [tT];
fragment U: [uU];
fragment V: [vV];
fragment W: [wW];
fragment X: [xX];
fragment Y: [yY];
fragment Z: [zZ];
