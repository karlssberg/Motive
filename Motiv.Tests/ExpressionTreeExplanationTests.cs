namespace Motiv.Tests;

public class ExpressionTreeExplanationTests
{
    [Fact]
    public void Should_yield_true_assertion_when_overriding_assertion()
    {
        // Assemble
        var literal = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var modelCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue(_ => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var resultCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue((_, _) => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var multipleCallback = Spec
            .From((int n) => n > 0)
            .WhenTrueYield((_, _) => ["is positive"])
            .WhenFalse("is not positive")
            .Create("is-positive");

        var spec = literal | modelCallback | resultCallback | multipleCallback;

        // Act
        var act = spec.IsSatisfiedBy(1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("is positive");
    }

    [Fact]
    public void Should_yield_true_assertion_when_overriding_assertion_from_higher_order_proposition()
    {
        // Assemble
        var literal = Spec
            .From((int n) => n > 0)
            .AsAnySatisfied()
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var literalWithImpliedStatement = Spec
            .From((int n) => n > 0)
            .AsAnySatisfied()
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create();

        var modelCallback = Spec
            .From((int n) => n > 0)
            .AsAllSatisfied()
            .WhenTrue(_ => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");


        var modelCallbackWithImpliedStatement = Spec
            .From((int n) => n > 0)
            .AsAllSatisfied()
            .WhenTrue(_ => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var resultCallback = Spec
            .From((int n) => n > 0)
            .AsNSatisfied(1)
            .WhenTrue(_ => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var resultCallbackWithImpliedStatement = Spec
            .From((int n) => n > 0)
            .AsNSatisfied(1)
            .WhenTrue(_ => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var multipleCallback = Spec
            .From((int n) => n > 0)
            .AsAtLeastNSatisfied(1)
            .WhenTrueYield(_ => ["is positive"])
            .WhenFalse("is not positive")
            .Create("is-positive");

        var multipleCallbackWithImpliedStatement = Spec
            .From((int n) => n > 0)
            .AsAtLeastNSatisfied(1)
            .WhenTrueYield(_ => ["is positive"])
            .WhenFalse("is not positive")
            .Create("is-positive");

        var spec = literal
                   | literalWithImpliedStatement
                   | modelCallback
                   | modelCallbackWithImpliedStatement
                   | resultCallback
                   | resultCallbackWithImpliedStatement
                   | multipleCallback
                   | multipleCallbackWithImpliedStatement;

        // Act
        var act = spec.IsSatisfiedBy([1]);

        // Assert
        act.Assertions.Should().BeEquivalentTo("is positive");
    }

    [Fact]
    public void Should_yield_true_reason_when_not_overriding_assertion()
    {
        // Assemble
        var spec = Spec
            .From((int n) => n > 0)
            .Create("is-positive");

        // Act
        var act = spec.IsSatisfiedBy(1);

        // Assert
        act.Reason.Should().BeEquivalentTo("is-positive");
    }

    [Fact]
    public void Should_yield_true_reason_when_not_overriding_higher_order_assertion()
    {
        // Assemble
        var spec = Spec
            .From((int n) => n > 0)
            .Create("is-positive");

        // Act
        var act = spec.IsSatisfiedBy(1);

        // Assert
        act.Reason.Should().BeEquivalentTo("is-positive");
    }

    [Fact]
    public void Should_yield_true_reason_when_overriding_assertion()
    {
        // Assemble
        var literal = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");


        var literalWithoutStatement = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create();

        var modelCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue(_ => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var resultCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue((_, _) => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var multipleCallback = Spec
            .From((int n) => n > 0)
            .WhenTrueYield((_, _) => ["is positive"])
            .WhenFalse("is not positive")
            .Create("is-positive");

        var spec = literal | literalWithoutStatement | modelCallback | resultCallback | multipleCallback;

        // Act
        var act = spec.IsSatisfiedBy(1);

        // Assert
        act.Reason.Should().BeEquivalentTo("is positive | is positive | is positive | is positive | is-positive");
    }

    [Fact]
    public void Should_yield_true_justification_when_overriding_assertion()
    {
        // Assemble
        var literal = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var modelCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue(_ => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var resultCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue((_, _) => "is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var multipleCallback = Spec
            .From((int n) => n > 0)
            .WhenTrueYield((_, _) => ["is positive"])
            .WhenFalse("is not positive")
            .Create("is-positive");

        var spec = literal | modelCallback | resultCallback | multipleCallback;

        // Act
        var act = spec.IsSatisfiedBy(1);

        // Assert
        act.Justification.Should().BeEquivalentTo(
            """
            OR
                is positive
                    (int n) => n > 0 == true
                        n > 0
                is positive
                    (int n) => n > 0 == true
                        n > 0
                is positive
                    (int n) => n > 0 == true
                        n > 0
                is-positive
                    (int n) => n > 0 == true
                        n > 0
            """);
    }

    [Fact]
    public void Should_yield_false_assertion_when_overriding_assertion()
    {
        // Assemble
        var literal = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var modelCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse(_ => "is not positive")
            .Create("is-positive");

        var resultCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse((_, _) => "is not positive")
            .Create("is-positive");

        var multipleCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalseYield((_, _) => ["is not positive"])
            .Create("is-positive");

        var multipleCallbackWithImplicitStatement = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalseYield((_, _) => ["is not positive"])
            .Create();

        var spec = literal | modelCallback | resultCallback | multipleCallback | multipleCallbackWithImplicitStatement;

        // Act
        var act = spec.IsSatisfiedBy(-1);

        // Assert
        act.Assertions.Should().BeEquivalentTo("is not positive");
    }

    [Fact]
    public void Should_yield_false_assertion_when_overriding_assertion_from_higher_order_proposition()
    {
        // Assemble
        var literal = Spec
            .From((int n) => n > 0)
            .AsAnySatisfied()
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var literalWithImpliedStatement = Spec
            .From((int n) => n > 0)
            .AsAnySatisfied()
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create();

        var modelCallback = Spec
            .From((int n) => n > 0)
            .AsAllSatisfied()
            .WhenTrue("is positive")
            .WhenFalse(_ => "is not positive")
            .Create("is-positive");

        var resultCallback = Spec
            .From((int n) => n > 0)
            .AsNSatisfied(1)
            .WhenTrue("is positive")
            .WhenFalse(_ => "is not positive")
            .Create("is-positive");

        var multipleCallback = Spec
            .From((int n) => n > 0)
            .AsAtLeastNSatisfied(1)
            .WhenTrue("is positive")
            .WhenFalseYield(_ => ["is not positive"])
            .Create("is-positive");

        var multipleCallbackWithImplicitStatement = Spec
            .From((int n) => n > 0)
            .AsAtLeastNSatisfied(1)
            .WhenTrue("is positive")
            .WhenFalseYield(_ => ["is not positive"])
            .Create();

        var spec = literal | literalWithImpliedStatement | modelCallback | resultCallback | multipleCallback | multipleCallbackWithImplicitStatement;

        // Act
        var act = spec.IsSatisfiedBy([-1]);

        // Assert
        act.Assertions.Should().BeEquivalentTo("is not positive");
    }

    [Fact]
    public void Should_yield_false_reason_when_not_overriding_assertion()
    {
        // Assemble
        var spec = Spec
            .From((int n) => n > 0)
            .Create("is-positive");

        // Act
        var act = spec.IsSatisfiedBy(-1);

        // Assert
        act.Reason.Should().BeEquivalentTo("¬is-positive");
    }

    [Fact]
    public void Should_yield_false_reason_when_overriding_assertion()
    {
        // Assemble
        var literal = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var literalWithImpliedStatement = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create();

        var modelCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse(_ => "is not positive")
            .Create("is-positive");

        var resultCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse((_, _) => "is not positive")
            .Create("is-positive");

        var multipleCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalseYield((_, _) => ["is not positive"])
            .Create("is-positive");

        var multipleCallbackWithImplicitStatement = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalseYield((_, _) => ["is not positive"])
            .Create();

        var spec = literal | literalWithImpliedStatement | modelCallback | resultCallback | multipleCallback | multipleCallbackWithImplicitStatement;

        // Act
        var act = spec.IsSatisfiedBy(-1);

        // Assert
        act.Reason.Should().BeEquivalentTo("is not positive | is not positive | is not positive | is not positive | ¬is-positive | ¬is positive");
    }

    [Fact]
    public void Should_yield_false_justification_when_overriding_assertion()
    {
        // Assemble
        var literal = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create("is-positive");

        var literalWithImplicitStatement = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse("is not positive")
            .Create();

        var modelCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse(_ => "is not positive")
            .Create("is-positive");

        var resultCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalse((_, _) => "is not positive")
            .Create("is-positive");

        var multipleCallback = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalseYield((_, _) => ["is not positive"])
            .Create("is-positive");

        var multipleCallbackWithImplicitStatement = Spec
            .From((int n) => n > 0)
            .WhenTrue("is positive")
            .WhenFalseYield((_, _) => ["is not positive"])
            .Create();

        var spec = literal | literalWithImplicitStatement | modelCallback | resultCallback | multipleCallback | multipleCallbackWithImplicitStatement;

        // Act
        var act = spec.IsSatisfiedBy(-1);

        // Assert
        act.Justification.Should().Be(
            """
            OR
                is not positive
                    (int n) => n > 0 == false
                        n <= 0
                is not positive
                    (int n) => n > 0 == false
                        n <= 0
                is not positive
                    (int n) => n > 0 == false
                        n <= 0
                is not positive
                    (int n) => n > 0 == false
                        n <= 0
                ¬is-positive
                    (int n) => n > 0 == false
                        n <= 0
                ¬is positive
                    (int n) => n > 0 == false
                        n <= 0
            """);
    }

    [Fact]
    public void Should_assert_why_a_higher_order_predicate_was_unsatisfied()
    {
        var areAllInRange =
            Spec.From((ICollection<int> numbers) => numbers.All(n => n > 0 & n <= 10))
                .Create("all in range");

        var result = areAllInRange.IsSatisfiedBy([-1, 2, 3]);

        result.Assertions.Should().BeEquivalentTo("n <= 0");
    }
}
