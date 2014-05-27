/*
 * Author:  Filip Dvoøák <filip.dvorak@runbox.com>
 *
 * Copyright (c) 2014 Filip Dvoøák <filip.dvorak@runbox.com>, all rights reserved
 *
 * Publishing, providing further or using this program is prohibited
 * without previous written permission of the author. Publishing or providing
 * further the contents of this file is prohibited without a previous written
 * permission of the author.
 */

/**
 * Examples:
 *  type Soldier {
 *    number health;
 *    number damage;
 *  }
 *  type SoldierWithJetPack extends Soldier {
 *    boolean flying;
 *  }
 *
 *  instance Rambo1, Rambo2;
 *
 *  action Init(){
 *    pre:{}
 *    effs{
 *    Rambo1.flying = false;
 *    Rambo1.health = 100;
 *    Rambo1.damage = 6;
 *    Rambo2.flying = false;
 *    Rambo2.health = 100;
 *    Rambo2.damage = 8;
 *    }
 *    effe{}
 *  }
 *
 *  action Shoot(Soldier shooter, Soldier target){
 *    duration = 10;
 *    pre:{ close_range(shooter, target),
 *          target.flying == false
 *        }
 *    effs:{}
 *    effe:{ target.health -= shooter.damage }
 *  }
 *
 *
 *
 *
 *
 *
 *
 *
 */
 


grammar GStrat;

/**
 * At the input we have either a type declaration, declaration of an instance of a particular type
 * or a declaration of an action.
 */
root
    :	
	(type|instance|action)*
    ;

/**
 * We declare the name of the type and the variables (entities) the type consists of.
 * We use single-inheritence of types
 */

type
    :
        'type' ID ('extends' ID) '{' variable* '}'
    ;    

/**
 * We may declare a boolean variable or a number variable - integer (TODO: choose what numbers to use across the system, 
 * sticking to floats might be better)
 */

variable
    :
        ('boolean'|'number') ID ';'
    ;

/**
 * First goes the name of type, then any number of comma-separated instances
 */

instance
    :
        'instance' ID ID (',' ID)+ ';'
    ;

/**
 * An action consists of set of preconditions (expressions we can evaluate) and effects,
 * we distinguish between effects that occour at the beginning and the end of the action.
 * The action has duration defined by an expression. The parameters of the action are the instances
 * of types.
 */
action
    :
        'action' ID '(' (ID ID)? (',' ID ID) ')'
        'duration' '=' expression
        'pre' '{' precondition* '}'
        'effs' '{' effect* '}'
        'effe' '{' effect* '}'
    ;

precondition
    :
        ID OPERATOR_COMPARE expression|
        functionCall
    ;

/**
 * Calling internal function with some parameters
 */

functionCall
    :
        ID '(' (ID|INT)? (',' ID|INT)* ')'
    ;

effect
    :
        ID OPERATOR_ASSIGN expression ';'|
        functionCall
    ;

expression
    :
        expression ('*'|'/'|'+'|'-') expression|
        ID|
        INT|
        '(' expression ')'
    ;


OPERATOR_COMPARE
    :
        '=='|'>='|'<='|'<'|'>'
    ;

OPERATOR_ASSIGN
    :
        '='|'-='|'+='|'/='|'*='
    ;

ID: [a-zA-Z.]+ ;
INT : [0-9]+ ;