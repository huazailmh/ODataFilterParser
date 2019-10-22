grammar OData;

/*
 * Parser Rules
 */

program: expression;

expression:
	'(' expression ')' # Parenthesis
	| column=COLUMN_NAME compare=(
		Equal
		| NotEqual
		| GreaterThan
		| GreaterThanOrEqual
		| LessThan
		| LessThanOrEqual) value=TEXT # Compare
	| expression logic = (K_AND | K_OR) expression	# Logic
	;

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

COLUMN_NAME
   : VALID_ID_START VALID_ID_CHAR*
   | '[' COLUMN_NAME ']'
   ;

TEXT
	:'"' .*? '"'
	|'\'' .*? '\''
	;
   
fragment VALID_ID_START
   : LOWERCASE | UPERCASE | '_'
   ;

fragment VALID_ID_CHAR
   : VALID_ID_START | (LOWERCASE | UPERCASE | DIGIT )
   ;

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

SPACES: [ \u000B\t\r\n] -> channel(HIDDEN);
