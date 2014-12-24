Jiro
====

Jiro is a thoughtful C# analyzer and fixer. 

####Immutability Rules
* [Fields should have immutable references](Jiro.CodeAnalysis/Immutability/Fields/ReadOnly/readme.md)
* Fields in structs should be private
* Fields should use [immutable collections](https://www.nuget.org/packages/System.Collections.Immutable) instead of arrays or mutable collections
* Properties should not have setters
