# Motiv

![Build Status](https://github.com/karlssberg/Motiv/actions/workflows/dotnet.yml/badge.svg) [![NuGet](https://img.shields.io/nuget/v/Motiv.svg)](https://www.nuget.org/packages/Motiv/) [![codecov](https://codecov.io/gh/karlssberg/Motiv/graph/badge.svg?token=XNN34D2JIP)](https://codecov.io/gh/karlssberg/Motiv)
### Quick Links

- [Documentation](https://karlssberg.github.io/Motiv/)
- [Try Motiv Online](https://dotnetfiddle.net/knykpD)
- [NuGet Package](https://www.nuget.org/packages/Motiv/)
- [Official GitHub Repository](https://github.com/karlssberg/Motiv)

## Decisions Made Clear

Motiv is a pragmatic solution to the _[Boolean Blindness](https://existentialtype.wordpress.com/2011/03/15/boolean-blindness/)_
problem (which is the loss of information resulting from the evaluation of logic to a single true or false value).
It achieves this by decomposing logical expressions into individual atomic [propositions](https://en.wikipedia.org/wiki/Proposition),
so that during evaluation, the specific causes of a decision can be preserved, and then put to use.
In most cases this will be a human-readable explanation of the decision, but it could equally be used to surface state.

```csharp
// Define the proposition
var isInRangeAndEven = Spec.From((int n) => n >= 1 & n <= 10 & n % 2 == 0)
                           .Create("in range and even");

// Evaluate proposition (typically elsewhere in your code)
var result = isInRangeAndEven.IsSatisfiedBy(11);

result.Satisfied;  // false
result.Assertions; // ["n > 10", "n % 2 != 0"]
result.Reason;     // "¬in range and even"
```

## Why Use Motiv?

Motiv primarily gives you visibility into your application's decision-making process.
It can also be used to dynamically surface state objects based on results of the underlying logic.

Consider using Motiv if your project requires two or more of the following:

1. **Visibility**: Obtain concise explanations about the outcome of complex logic.
2. **Decomposition**: Un-obfuscate boolean logic into self-explanatory proposition.
3. **Reusability**: Reuse highly composable logic across multiple locations.
4. **Modeling**: Explicitly model the logic in your domain as propositions.
5. **Testing**: Test your application's boolean logic in isolation.

## Use Cases

Motiv can be applied in various scenarios, including:

* **User Feedback**: Give detailed explanations about decisions.
* **Debugging**: Quickly find out the causes from complex logic.
* **Multilingual Support**: Offer explanations in different languages.
* **Validation**: Ensure user input meets specific criteria and provide detailed feedback.
* **Dynamic Logic**: Compose logic at runtime based on user input.
* **Rules Processing**: Declaratively define and compose complex _if-then_ rules.
* **Conditional State**: Yield different states based on complex criteria.
* **Auditing**: Log _why_ it happened, instead of _what_.

## Installation

Install Motiv via NuGet Package Manager Console:
```bash
Install-Package Motiv
```
Or using the .NET CLI:
```bash
dotnet add package Motiv
```

## Usage

### From Lambda Expression Tree to Propositions

The simplest way to use Motiv is with an `Expression<Func<T, bool>>`,
Motiv will recompose it into a multiple individual propositions,
so that each underlying logical sub-expression asserts its contribution to the final decision.

```csharp
```csharp
Expression<Func<Customer, bool>> ;

var isEligibleForLoan = Spec.From((Customer customer) =>
                                customer.CreditScore > 600
                                & customer.Income > 100000)
                            .Create("eligible for loan");

var result = isEligibleForLoan.IsSatisfiedBy(eligibleCustomer);

result.Satisfied;  // true
result.Reason;     // "eligible for loan"
result.Assertions; // ["customer.CreditScore > 600", "customer.Income > 100000"]
```

### From Lambda Expression to Propositions

Create and evaluate a `Func<T, bool>` proposition:

```csharp
Func<Customer, bool> expression = customer =>
    customer.CreditScore > 600 & customer.Income > 100000;

var isEligibleForLoan = Spec.Build(expression)
                            .Create("eligible for loan");

var result = isEligibleForLoan.IsSatisfiedBy(eligibleCustomer);

result.Satisfied;  // true
result.Reason;     // "eligible for loan"
result.Assertions; // ["eligible for loan"]
```

### Propositions with Custom Assertions

Use `WhenTrue()` and `WhenFalse()` for user-friendly explanations:

```csharp
var isEligibleForLoan = Spec.Build((Customer customer) =>
                                     customer is { CreditScore: > 600, Income: > 100000 })
                            .WhenTrue("eligible for a loan")
                            .WhenFalse("not eligible for a loan")
                            .Create();

var result = isEligibleForLoanPolicy.IsSatisfiedBy(ineligibleCustomer);

result.Satisfied;  // false
result.Reason;     // "not eligible for a loan"
```

### Propositions with Custom Metadata

Use `WhenTrue()` and `WhenFalse()` with types other than `string`:

```csharp
var isEligibleForLoanPolicy =  Spec.Build((Customer customer) =>
                                     customer is { CreditScore: > 600, Income: > 100000 })
                                   .WhenTrue(MyEnum.EligibleForLoan)
                                   .WhenFalse(MyEnum.NotEligibleForLoan)
                                   .Create("eligible for a loan");

var result = isEligibleForLoanPolicy.IsSatisfiedBy(eligibleCustomer);

result.Satisfied;  // true
result.Value;      // MyEnum.EligibleForLoan
result.Reason;     // "eligible for a loan"
```

### Composing Propositions

Combine propositions using boolean operators:

```csharp
var hasGoodCreditScore = Spec.Build((Customer customer) => customer.CreditScore > 600)
                             .WhenTrue("good credit score")
                             .WhenFalse("inadequate credit score")
                             .Create();

var hasSufficientIncome = Spec.Build((Customer customer) => customer.Income > 100000)
                              .WhenTrue("sufficient income")
                              .WhenFalse("insufficient income")
                              .Create();

var isEligibleForLoan = hasGoodCreditScore & hasSufficientIncome;

var result = isEligibleForLoan.IsSatisfiedBy(eligibleCustomer);

result.Satisfied;  // true
result.Reason;     // "good credit score & sufficient income"
result.Assertions; // ["good credit score", "sufficient income"]
```

### Higher Order Logic

Provide facts about collections:

```csharp
var allNegative = Spec.Build((int n) => n < 0)
                      .AsAllSatisfied()
                      .WhenTrue("all are negative")
                      .WhenFalseYield(eval => eval.FalseModels.Select(n => $"{n} is not negative"))
                      .Create();

var result = allNegative.IsSatisfiedBy([-1, 2, 3]);

result.Satisfied;  // false
result.Reason;     // "¬all are negative"
result.Assertions; // ["2 is not negative", "3 is not negative"]
```

## Tradeoffs

Consider these potential tradeoffs when using Motiv:

1. **Performance**: Motiv isn't optimized for high-performance scenarios where nanoseconds matter.
    Instead, it lazily evaluates results to ensure that extraneous computation is avoided.
2. **Dependency**: Once integrated, Motiv becomes a dependency in your codebase.
    It does not, however, introduce any additional dependencies.
3. **Learning Curve**: While Motiv introduces a new approach, it's designed to be intuitive and straightforward to use.

## License

Motiv is released under the MIT License. See the [LICENSE](./LICENSE) file for details.
