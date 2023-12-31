﻿using static Karlssberg.Motive.SpecificationException;

namespace Karlssberg.Motive.And;

internal class AndSpecification<TModel, TMetadata> : SpecificationBase<TModel, TMetadata>
{
    internal AndSpecification(
        SpecificationBase<TModel, TMetadata> leftOperand,
        SpecificationBase<TModel, TMetadata> rightOperand)
    {
        LeftOperand = Throw.IfNull(leftOperand, nameof(leftOperand));
        RightOperand = Throw.IfNull(rightOperand, nameof(rightOperand));
    }

    public SpecificationBase<TModel, TMetadata> LeftOperand { get; }
    public SpecificationBase<TModel, TMetadata> RightOperand { get; }

    public override string Description => $"({LeftOperand}) AND ({RightOperand})";

    public override BooleanResultBase<TMetadata> IsSatisfiedBy(TModel model)
    {
        var leftResult = WrapThrownExceptions(
            this,
            LeftOperand,
            () => LeftOperand.IsSatisfiedBy(model));

        var rightResult = WrapThrownExceptions(
            this,
            RightOperand,
            () => RightOperand.IsSatisfiedBy(model));

        return leftResult.And(rightResult);
    }
}