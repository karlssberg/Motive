﻿using FluentAssertions;
using Karlssberg.Motive.And;
using NSubstitute;

namespace Karlssberg.Motive.Tests;

public class AndSpecificationTests
{
    [Theory]
    [AutoParams(true, true, true)]
    [AutoParams(true, false, false)]
    [AutoParams(false, true, false)]
    [AutoParams(false, false, false)]
    public void Should_perform_logical_and(
        bool leftResult,
        bool rightResult,
        bool expected,
        object model)
    {
        var left = new Specification<object, bool>(
            $"left",
            _ => leftResult,
            true,
            false);
        var right = new Specification<object, bool>(
            $"right",
            _ => rightResult,
            true,
            false);
        
        var sut = left & right;

        var result = sut.Evaluate(model);

        result.IsSatisfied.Should().Be(expected);
        result.GetInsights().Should().AllBeEquivalentTo(expected);
    }
    
    [Theory]
    [AutoParams(true, true, "(left:True) AND:True (right:True)")]
    [AutoParams(true, false, "(left:True) AND:False (right:False)")]
    [AutoParams(false, true, "(left:False) AND:False (right:True)")]
    [AutoParams(false, false, "(left:False) AND:False (right:False)")]
    public void Should_serialize_the_result_of_the_and_operation(
        bool leftResult,
        bool rightResult,
        string expected,
        object model)
    {
        var left = new Specification<object, bool>(
            $"left",
            _ => leftResult,
            true,
            false);
        var right = new Specification<object, bool>(
            $"right",
            _ => rightResult,
            true,
            false);
        
        var sut = left & right;

        var result = sut.Evaluate(model);

        
    }
    
    [Theory]
    [AutoParams(true, true, "(True) AND:True (True)")]
    [AutoParams(true, false, "(True) AND:False (False)")]
    [AutoParams(false, true, "(False) AND:False (True)")]
    [AutoParams(false, false, "(False) AND:False (False)")]
    public void Should_serialize_the_result_of_the_and_operation_when_metadata_is_a_string(
        bool leftResult,
        bool rightResult,
        string expected,
        object model)
    {
        var left = new Specification<object, string>(
            $"left",
            _ => leftResult,
            true.ToString(),
            false.ToString());
        var right = new Specification<object, string>(
            $"right",
            _ => rightResult,
            true.ToString(),
            false.ToString());
        
        var sut = left & right;

        var result = sut.Evaluate(model);

        
    }
    
    [Theory]
    [AutoParams(true, true, "(True) AND:True (True)")]
    [AutoParams(true, false, "(True) AND:False (False)")]
    [AutoParams(false, true, "(False) AND:False (True)")]
    [AutoParams(false, false, "(False) AND:False (False)")]
    public void Should_serialize_the_result_of_the_and_operation_when_metadata_is_a_string_when_using_the_single_generic_specification_type(
        bool leftResult,
        bool rightResult,
        string expected,
        object model)
    {
        var left = new Specification<object>(
            _ => leftResult,
            true.ToString(),
            false.ToString());
        var right = new Specification<object>(
            _ => rightResult,
            true.ToString(),
            false.ToString());
        
        var sut = left & right;

        var result = sut.Evaluate(model);
        
        result.Description.Should().Be(expected);
        
    }
    
    [Theory]
    [AutoParams(true, true)]
    [AutoParams(true, false)]
    [AutoParams(false, true)]
    [AutoParams(false, false)]
    public void Should_provide_a_description_of_the_specification(bool leftResult, bool rightResult)
    {
        var left = new Specification<object, bool>(
            $"left",
            _ => leftResult,
            true,
            false);
        var right = new Specification<object, bool>(
            $"right",
            _ => rightResult,
            true,
            false);
        var expected = $"({left.Description}) AND ({right.Description})";
        
        var sut = left & right;

        sut.Description.Should().Be(expected);
        sut.ToString().Should().Be(expected);
    }
    
    [Theory]
    [AutoParams(true, true)]
    [AutoParams(true, false)]
    [AutoParams(false, true)]
    [AutoParams(false, false)]
    public void Should_provide_a_description_of_the_specification_when_using_convenience_specification(bool leftResult, bool rightResult)
    {
        var left = new Specification<object>(
            _ => leftResult,
            true.ToString(),
            false.ToString());
        var right = new Specification<object>(
            _ => rightResult,
            true.ToString(),
            false.ToString());
        var expected = $"({left.Description}) AND ({right.Description})";
        
        var sut = left & right;

        sut.Description.Should().Be(expected);
        sut.ToString().Should().Be(expected);
    }
    
    [Theory]
    [AutoParams("true",  null)]
    [AutoParams(null, "false")]
    public void Should_not_throw_if_null_metadata_supplied(
        string? trueMetadata, 
        string? falseMetadata,
        string? model)
    {
        var spec = new Specification<string?, string?>(
            "is null",
            m => m is null,
            trueMetadata,
            falseMetadata);
        
        
        var act = () =>
        {
            var sut = spec & spec;
            sut.Evaluate(model);
        };

        act.Should().NotThrow();
    }
    
    [Theory]
    [AutoParams]
    public void Should_wrap_thrown_exceptions_in_a_specification_exception(
        string model)
    {
        var normalSpec = new Specification<object>(
            m => true,
            "true",
            "false");
        var throwingSpec = new ThrowingSpecification<object, string>(
            "should always throw",
            new Exception("should be wrapped"));
        
        var sut = throwingSpec & normalSpec;
        
        var act = () => sut.Evaluate(model);
        
        act.Should().Throw<SpecificationException>().Where(ex => ex.Message.Contains(throwingSpec.Description));
        act.Should().Throw<SpecificationException>().Where(ex => ex.Message.Contains(sut.Description));
        act.Should().Throw<SpecificationException>().Where(ex => ex.Message.Contains(nameof(AndSpecification<object, string>)));
        act.Should().Throw<SpecificationException>().WithInnerExceptionExactly<Exception>().Where(ex => ex.Message.Contains("should be wrapped"));
    }
}