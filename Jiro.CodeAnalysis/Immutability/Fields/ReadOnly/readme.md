JIRO-001: Fields should have immutable references
-------------------------------------------------

| Category      | Analyzer | Fixer
--- | --- | ---
Immutability	| [ReadOnlyFieldAnalyzer](ReadOnlyFieldAnalyzer.cs)| [ReadOnlyFieldFixer](ReadOnlyFieldFixer.cs)

###Cause
A field has a mutable reference.

###Rule Description
Fields should have immutable references. Fields are shared state among all members of a class. Shared mutable state should be avoided as it's susceptable to race conditions and makes the class more difficult to reason about.

###How to Fix Violations

The [ReadOnlyFieldFixer](ReadOnlyFieldFixer.cs) can automatically fix this violation by adding the [readonly modifier](http://msdn.microsoft.com/en-us/library/acdd6hb7.aspx) to the field.

###When to Suppress Warnings

Do not suppress a warning from this rule.

###Example

The following example shows a method that violates the rule.

```csharp
internal sealed class Person
{ 
	private DateTime dateOfBirth; 
}
```

The following example shows a method that satisfies the rule.

```csharp
internal sealed class Person
{ 
	private readonly DateTime dateOfBirth;
}
```
