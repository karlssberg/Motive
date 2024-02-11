﻿using FluentAssertions;

namespace Karlssberg.Motiv.Tests;

public class ElseIfSpecTests
{
    [Theory]
    [InlineAutoData(true, "antecedent satisfied")]
    [InlineAutoData(false, "consequent satisfied")]
    public void Should_return_an_antecedent_result_when_it_is_satisfied(bool model, string expected)
    {
        var antecedent = Spec.Build<bool>(m => m)
            .WhenTrue("antecedent satisfied")
            .WhenFalse("antecedent not satisfied")
            .CreateSpec();

        var consequent = Spec.Build<bool>(m => !m)
            .WhenTrue("consequent satisfied")
            .WhenFalse("consequent not satisfied")
            .CreateSpec();

        var sut = antecedent.ElseIf(consequent);

        var result = sut.IsSatisfiedBy(model);

        result.Reasons.Should().BeEquivalentTo(expected);
    }


    [Fact]
    public void Should_describe_the_else_if_spec()
    {
        var antecedent = Spec.Build<bool>(m => m)
            .WhenTrue("when true")
            .WhenFalse("when false")
            .CreateSpec();

        var consequent = Spec.Build<bool>(m => !m)
            .WhenTrue("when false")
            .WhenFalse("when true")
            .CreateSpec();

        var sut = antecedent.ElseIf(consequent);

        sut.Description.Should().Be("(when true) => (when false)");
    }
}