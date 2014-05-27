// Define a grammar called Hello
grammar Hello;
r  : 'hello' ID r | ID ;       // match keyword hello followed by an identifier
   
ID : [a-z]+ ;             // match lower-case identifiers
WS : [ \t\r\n]+ -> skip ; // skip spaces, tabs, newlines