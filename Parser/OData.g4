grammar OData;

/*
 * Parser Rules
 */

program: expression;

expression:
	LP expression RP # Parenthesis
	| K_STARTSWITH LP column=column_name ',' value=TEXT RP # StartsWith
	| K_ENDSWITH LP column=column_name ',' value=TEXT RP # EndsWith
	| K_CONTAINS LP column=column_name ',' value=TEXT RP # Contains
	| column=column_name K_IN LP value=decimal_array RP # InDecimal
	| column=column_name K_IN LP value=string_array RP # InText
	| column=column_name compare=(
		Equal
		| NotEqual
		| GreaterThan
		| GreaterThanOrEqual
		| LessThan
		| LessThanOrEqual) value=decimal # CompareDecimal
	| column=column_name compare=(
		Equal
		| NotEqual
		| GreaterThan
		| GreaterThanOrEqual
		| LessThan
		| LessThanOrEqual) value=TEXT # CompareText
	| expression logic = (K_AND | K_OR) expression	# Logic
	;
	
column_name
   : COLUMN_NAME
   | '[' column_name ']'
   ;

string_array
	: TEXT (',' TEXT)*
	;

decimal_array
	: NUMBER (',' NUMBER)*
	;

text: TEXT;

decimal 
	: NUMBER
	;

/*
 * Lexer Rules
 */

K_IN: I N;
K_AND: A N D;
K_OR: O R;
K_STARTSWITH: S T A R T S W I T H;
K_ENDSWITH: E N D S W I T H;
K_CONTAINS: C O N T A I N S;
LP : '(';
RP : ')';

Equal: E Q;
NotEqual: N E;
GreaterThan: G T;
GreaterThanOrEqual: G E;
LessThan: L T;
LessThanOrEqual: L E;

COLUMN_NAME
   : VALID_ID_START VALID_ID_CHAR*
   ;

TEXT
	:'"' .*? '"'
	|'\'' .*? '\''
	;

NUMBER
   : (SIGN)? UNSIGNED_INTEGER+
   | (SIGN)? UNSIGNED_INTEGER+ ('.' UNSIGNED_INTEGER+)?
   ;

fragment UNSIGNED_INTEGER
   : ('0' .. '9')
   ;

fragment SIGN
   : ('+' | '-')
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
