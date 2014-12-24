JIRO-001: Fields should be readonly
Category: Immutability
====

##Cause
A field has a mutable reference.

##Rule Description
All fields should have immutable references. Fields are shared state among all members of a class. Shared mutable state should be avoided as it's susceptable to race conditions and makes the class more difficult to reason about.

##How to Fix Violations

To fix a violation of this rule, add the 'readonly' modifier to each field.

##When to Suppress Warnings

##Example

The following example shows a method that violates the rule.

'class Person
{ 
	private DateTime dateOfBirth; 
}'

The following example shows a method that satisfies the rule.

'class Person
{ 
	private readonly DateTime dateOfBirth;
}'