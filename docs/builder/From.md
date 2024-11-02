﻿---
title: From()
---
# From()

The `Spec.From()` method is used to create a proposition from a lambda expression trees.
This is the easiest way to use Motiv, as it allows you to create multiple propositions from a single lambda expression.

For example, the following lambda expression:

```csharp
Spec.From((int n) => n > 0 & n <= 10)
    .Create("n is between 1 and 10 (inclusive)");
```

Internally, this example is transformed into two propositions: `n > 0` and `n <= 10`,
and is functionally equivalent to:

```csharp
var greaterThanOne = Spec.From((int n) => n > 0)
                         .Create("n is greater than 0");

var lessThanTen = Spec.From((int n) => n <= 10)
                      .Create("n is less than or equal to 10");

var isBetweenOneAndTen = greaterThanOne & lessThanTen;
```

Depending on their contribution to the final result, the sub-expressions could be filtered and/or changed to create
a meaningful explanation of the outcome.

So, with the expression `n > 0`, if `n` were `0` - and therefore unsatisfied - then the yielded assertions would be
`n <= 0`, which is the negation of the original expression.

For example:

```csharp
var inRange = Spec.From((int n) => n > 0 & n <= 10)
                  .Create("n is between 1 and 10 (inclusive)");

var result = inRange.IsSatisfiedBy(0);

result.Satisfied;  // false
result.Assertions; // ["n <= 0"]
```

## Displaying Values instead of Identifiers

The `Display.AsValue()` method can be used to replace identifiers with their actual values.
This allows for more informative assertions that are easier to understand.
In fact, the `Display.AsValue()` method can be used with any sub-expression (or the entire expression), if you so wish.

For example:

```csharp
var members = new[] { "Alice", "Ben", "Claudia" };
var isMember = Spec.From((string name) => members.Contains(Display.AsValue(name)))
                   .Create("is a member");

var result = isMember.IsSatisfiedBy("Ben");

result.Satisfied;  // true
result.Assertions; // ["members.Contains(\"Ben\") == true"]
```

## Partial customization

## Customizing the assertions/metadata

Like the `Spec.Build()` method, the `Spec.From()` method can also be used with the `WhenTrue()` and `WhenFalse()`
methods.

For example:

```csharp
var isEven = Spec.From((int n) => n % 2 == 0)
                 .WhenTrue("is even")
                 .WhenFalse("is odd")
                 .Create();

var result = isEven.IsSatisfiedBy(3);

result.Satisfied;     // false
result.Assertions;    // ["is odd"]
result.Justification; // is odd
                      //     n % 2 != 0
```

## Customizing individual clauses of an expression.

Motiv allows you to inline propositions within a boolean lambda expression tree.
This gives you the ability to customize the assertions and metadata of the underlying sub-expression without having to
rewrite the entire lambda expression.

For example:

```csharp
var isEven = Spec.From((int n) => n % 2 == 0)
                 .WhenTrue("n is even")
                 .WhenFalse("n is odd")
                 .Create("is even");

var isEvenAndPositive = Spec.From((int n) => isEven.IsSatisfied(n) & n > 0)
                            .Create("is even and positive");

var result = isEvenAndPositive.IsSatisfiedBy(-3);

result.Satisfied;     // false
result.Assertions;    // ["n is odd", "n <= 0"]
```

## Inlining Any and All LINQ methods

Expression can inline the `All` and `Any` methods from LINQ, which will be transformed into their Motiv equivalents.

For example:

```csharp
var areAnyEvenAndAllPositive = Spec.From((IEnumerable<int> numbers) =>
                                       numbers.Any(n => n % 2 == 0)
                                       & numbers.All(n => n > 0))
                                   .Create("all positive numbers amd some are even");

var result = areAnyEvenAndAllPositive.IsSatisfiedBy([-1, 2, 3]);

result.Satisfied;     // false
result.Assertions;    // ["n <= 0"]
result.Justification; // ¬all positive numbers amd some are even
                      //     AND
                      //         numbers.All((int n) => n > 0) == false
                      //             n <= 0
```

## Using Inline Propositions

Propositions can be used within the lambda expression of the `Spec.From()` method, and have their assertions and
metadata automatically incorporated into the final result.
This allows you to customize the assertions and metadata of underlying sub-expression without having to break apart
the lambda expression into individual propositions.

```csharp
var isAdmin =
    Spec.Build((string role) => role == "admin")
        .WhenTrue("is admin")
        .WhenFalse("is not admin")
        .Create();

var hasAccess = Spec
    .From((User user) => user.IsActive & user.Roles.Any(isAdmin))
    .Create("has admin access");
```

This also works with the results from evaluating propositions.
