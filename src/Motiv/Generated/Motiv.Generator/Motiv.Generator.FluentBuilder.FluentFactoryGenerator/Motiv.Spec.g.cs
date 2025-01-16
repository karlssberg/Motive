using Motiv.BooleanPredicateProposition.PropositionBuilders;
using Motiv.BooleanPredicateProposition.PropositionBuilders.Explanation;
using Motiv.BooleanPredicateProposition.PropositionBuilders.Metadata;
using Motiv.BooleanResultPredicateProposition.PropositionBuilders;
using Motiv.BooleanResultPredicateProposition.PropositionBuilders.Explanation;
using Motiv.BooleanResultPredicateProposition.PropositionBuilders.Metadata;
using Motiv.ExpressionTreeProposition.PropositionBuilders;
using Motiv.ExpressionTreeProposition.PropositionBuilders.Explanation;
using Motiv.ExpressionTreeProposition.PropositionBuilders.Metadata;
using Motiv.HigherOrderProposition.PropositionBuilders;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicate;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Policy;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicateWithName;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Spec;
using Motiv.HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate;
using Motiv.HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate;
using Motiv.HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree;
using Motiv.HigherOrderProposition.PropositionBuilders.Metadata.Policy;
using Motiv.HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate;
using Motiv.HigherOrderProposition.PropositionBuilders.Metadata.Spec;
using Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation;
using Motiv.SpecDecoratorProposition.PropositionBuilders.Metadata;
using System;
using System.Linq.Expressions;

namespace Motiv.BooleanPredicateProposition.PropositionBuilders
{
    public partial struct BooleanPredicatePropositionFactory<TModel>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_1__Motiv_Spec<TModel> WhenTrue(in System.Func<TModel, string> whenTrue)
        {
            return new Step_1__Motiv_Spec<TModel>(predicate, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_2__Motiv_Spec<TModel> WhenTrue(in string trueBecause)
        {
            return new Step_2__Motiv_Spec<TModel>(predicate, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_3__Motiv_Spec<TModel> WhenTrueYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> whenTrue)
        {
            return new Step_3__Motiv_Spec<TModel>(predicate, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_3__Motiv_Spec<TModel> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_3__Motiv_Spec<TModel>(predicate, Shared.WhenYieldOverloads.Convert<TModel, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_4__Motiv_Spec<TModel, TMetadata> WhenTrue<TMetadata>(in System.Func<TModel, TMetadata> whenTrue)
        {
            return new Step_4__Motiv_Spec<TModel, TMetadata>(predicate, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_4__Motiv_Spec<TModel, TMetadata> WhenTrue<TMetadata>(in TMetadata value)
        {
            return new Step_4__Motiv_Spec<TModel, TMetadata>(predicate, Shared.WhenOverloads.Convert<TModel, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_5__Motiv_Spec<TModel, TMetadata> WhenTrueYield<TMetadata>(in System.Func<TModel, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            return new Step_5__Motiv_Spec<TModel, TMetadata>(predicate, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_5__Motiv_Spec<TModel, TMetadata> WhenTrueYield<TMetadata>(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new Step_5__Motiv_Spec<TModel, TMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel> As(in System.Func<System.Collections.Generic.IEnumerable<HigherOrderProposition.ModelResult<TModel>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<HigherOrderProposition.ModelResult<TModel>>, System.Collections.Generic.IEnumerable<HigherOrderProposition.ModelResult<TModel>>> causeSelector)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderBooleanPredicateSpecMethods.As<TModel>(higherOrderPredicate, causeSelector));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel> AsAllSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderBooleanPredicateSpecMethods.AsAllSatisfied<TModel>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel> AsAnySatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderBooleanPredicateSpecMethods.AsAnySatisfied<TModel>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel> AsAtLeastNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderBooleanPredicateSpecMethods.AsAtLeastNSatisfied<TModel>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel> AsAtMostNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderBooleanPredicateSpecMethods.AsAtMostNSatisfied<TModel>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel> AsNoneSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderBooleanPredicateSpecMethods.AsNoneSatisfied<TModel>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel> AsNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderBooleanPredicateSpecMethods.AsNSatisfied<TModel>(n));
        }
    }
}

namespace Motiv
{
    public static partial class Spec
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static BooleanPredicateProposition.PropositionBuilders.BooleanPredicatePropositionFactory<TModel> Build<TModel>(in System.Func<TModel, bool> predicate)
        {
            return new BooleanPredicateProposition.PropositionBuilders.BooleanPredicatePropositionFactory<TModel>(predicate);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static BooleanResultPredicateProposition.PropositionBuilders.BooleanResultPredicatePropositionFactory<TModel, TMetadata> Build<TMetadata, TModel>(in System.Func<TModel, BooleanResultBase<TMetadata>> predicate)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.BooleanResultPredicatePropositionFactory<TModel, TMetadata>(predicate);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static BooleanResultPredicateProposition.PropositionBuilders.PolicyResultPredicatePropositionFactory<TModel, TMetadata> Build<TMetadata, TModel>(in System.Func<TModel, PolicyResultBase<TMetadata>> predicate)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.PolicyResultPredicatePropositionFactory<TModel, TMetadata>(predicate);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static BooleanResultPredicateProposition.PropositionBuilders.PolicyResultPredicatePropositionFactory<TModel, TMetadata> Build<TMetadata, TModel>(in System.Func<System.Func<TModel, PolicyResultBase<TMetadata>>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.PolicyResultPredicatePropositionFactory<TModel, TMetadata>(Shared.BuildOverloads.Convert<System.Func<TModel, PolicyResultBase<TMetadata>>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ExpressionTreeProposition.PropositionBuilders.MinimalExpressionTreePropositionFactory<TModel, TPredicateResult> From<TModel, TPredicateResult>(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression)
        {
            return new ExpressionTreeProposition.PropositionBuilders.MinimalExpressionTreePropositionFactory<TModel, TPredicateResult>(expression);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static SpecDecoratorProposition.PropositionBuilders.Explanation.MinimalPolicyDecoratorFactory<TModel, TMetadata> Build<TMetadata, TModel>(in PolicyBase<TModel, TMetadata> policy)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MinimalPolicyDecoratorFactory<TModel, TMetadata>(policy);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static SpecDecoratorProposition.PropositionBuilders.Explanation.MinimalPolicyDecoratorFactory<TModel, TMetadata> Build<TMetadata, TModel>(in System.Func<PolicyBase<TModel, TMetadata>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MinimalPolicyDecoratorFactory<TModel, TMetadata>(Shared.BuildOverloads.Convert<PolicyBase<TModel, TMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static SpecDecoratorProposition.PropositionBuilders.Explanation.MinimalSpecDecoratorFactory<TModel, TMetadata> Build<TMetadata, TModel>(in SpecBase<TModel, TMetadata> spec)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MinimalSpecDecoratorFactory<TModel, TMetadata>(spec);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static SpecDecoratorProposition.PropositionBuilders.Explanation.MinimalSpecDecoratorFactory<TModel, TMetadata> Build<TMetadata, TModel>(in System.Func<SpecBase<TModel, TMetadata>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MinimalSpecDecoratorFactory<TModel, TMetadata>(Shared.BuildOverloads.Convert<SpecBase<TModel, TMetadata>>(function));
        }
    }

    public struct Step_1__Motiv_Spec<TModel>
    {
        private readonly System.Func<TModel, bool> _predicate__parameter;
        private readonly System.Func<TModel, string> _whenTrue__parameter;
        public Step_1__Motiv_Spec(in System.Func<TModel, bool> predicate, in System.Func<TModel, string> whenTrue)
        {
            this._predicate__parameter = predicate;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel> WhenFalse(in System.Func<TModel, string> whenFalse)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel>(this._predicate__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel> WhenFalse(in string value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel>(this._predicate__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> whenFalse)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel>(this._predicate__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel>(this._predicate__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_2__Motiv_Spec<TModel>
    {
        private readonly System.Func<TModel, bool> _predicate__parameter;
        private readonly string _trueBecause__parameter;
        public Step_2__Motiv_Spec(in System.Func<TModel, bool> predicate, in string trueBecause)
        {
            this._predicate__parameter = predicate;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel> WhenFalse(in System.Func<TModel, string> falseBecause)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel> WhenFalse(in string value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_3__Motiv_Spec<TModel>
    {
        private readonly System.Func<TModel, bool> _predicate__parameter;
        private readonly System.Func<TModel, System.Collections.Generic.IEnumerable<string>> _whenTrue__parameter;
        public Step_3__Motiv_Spec(in System.Func<TModel, bool> predicate, in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> whenTrue)
        {
            this._predicate__parameter = predicate;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> whenFalse)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel>(this._predicate__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel>(this._predicate__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel> WhenFalse(in System.Func<TModel, string> whenFalse)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel>(this._predicate__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel> WhenFalse(in string value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel>(this._predicate__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, string>(value));
        }
    }

    public struct Step_4__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, bool> _predicate__parameter;
        private readonly System.Func<TModel, TMetadata> _whenTrue__parameter;
        public Step_4__Motiv_Spec(in System.Func<TModel, bool> predicate, in System.Func<TModel, TMetadata> whenTrue)
        {
            this._predicate__parameter = predicate;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, TMetadata> whenFalse)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TMetadata> WhenFalse(in TMetadata value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }
    }

    public struct Step_5__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, bool> _predicate__parameter;
        private readonly System.Func<TModel, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        public Step_5__Motiv_Spec(in System.Func<TModel, bool> predicate, in System.Func<TModel, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            this._predicate__parameter = predicate;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, TMetadata> whenFalse)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata> WhenFalse(in TMetadata value)
        {
            return new BooleanPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, TMetadata>(value));
        }
    }

    public struct Step_7__Motiv_Spec<TModel>
    {
        private readonly System.Func<TModel, bool> _predicate__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecBooleanPredicateOperation<TModel> _higherOrderOperation__parameter;
        private readonly string _trueBecause__parameter;
        public Step_7__Motiv_Spec(in System.Func<TModel, bool> predicate, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecBooleanPredicateOperation<TModel> higherOrderOperation, in string trueBecause)
        {
            this._predicate__parameter = predicate;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName.ExplanationFromBooleanPredicateWithNameHigherOrderPropositionFactory<TModel> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName.ExplanationFromBooleanPredicateWithNameHigherOrderPropositionFactory<TModel>(this._predicate__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName.ExplanationFromBooleanPredicateWithNameHigherOrderPropositionFactory<TModel> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName.ExplanationFromBooleanPredicateWithNameHigherOrderPropositionFactory<TModel>(this._predicate__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName.MultiAssertionExplanationWithNameHigherOrderPropositionFactory<TModel> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName.MultiAssertionExplanationWithNameHigherOrderPropositionFactory<TModel>(this._predicate__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName.MultiAssertionExplanationWithNameHigherOrderPropositionFactory<TModel> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicateWithName.MultiAssertionExplanationWithNameHigherOrderPropositionFactory<TModel>(this._predicate__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_8__Motiv_Spec<TModel>
    {
        private readonly System.Func<TModel, bool> _predicate__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecBooleanPredicateOperation<TModel> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, string> _trueBecause__parameter;
        public Step_8__Motiv_Spec(in System.Func<TModel, bool> predicate, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecBooleanPredicateOperation<TModel> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, string> trueBecause)
        {
            this._predicate__parameter = predicate;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicate.ExplanationFromBooleanPredicateHigherOrderPropositionFactory<TModel> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicate.ExplanationFromBooleanPredicateHigherOrderPropositionFactory<TModel>(this._predicate__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicate.ExplanationFromBooleanPredicateHigherOrderPropositionFactory<TModel> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanPredicate.ExplanationFromBooleanPredicateHigherOrderPropositionFactory<TModel>(this._predicate__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, string>(value));
        }
    }

    public struct Step_9__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, bool> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecBooleanPredicateOperation<TModel> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, TMetadata> _whenTrue__parameter;
        public Step_9__Motiv_Spec(in System.Func<TModel, bool> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecBooleanPredicateOperation<TModel> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, TMetadata> whenTrue)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, TMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in TMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }
    }

    public struct Step_10__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, bool> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecBooleanPredicateOperation<TModel> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        public Step_10__Motiv_Spec(in System.Func<TModel, bool> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecBooleanPredicateOperation<TModel> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, TMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in TMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate.MultiMetadataFromBooleanHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, TMetadata>(value));
        }
    }

    public struct Step_12__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _predicate__parameter;
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>, string> _trueBecause__parameter;
        public Step_12__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> predicate, in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>, string> trueBecause)
        {
            this._predicate__parameter = predicate;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, string> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_13__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _predicate__parameter;
        private readonly string _trueBecause__parameter;
        public Step_13__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> predicate, in string trueBecause)
        {
            this._predicate__parameter = predicate;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, string> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_14__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _predicate__parameter;
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_14__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> predicate, in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            this._predicate__parameter = predicate;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, string> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }
    }

    public struct Step_15__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _spec__parameter;
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> _whenTrue__parameter;
        public Step_15__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> spec, in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>, TReplacementMetadata> whenTrue)
        {
            this._spec__parameter = spec;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }
    }

    public struct Step_16__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _spec__parameter;
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> _whenTrue__parameter;
        public Step_16__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> spec, in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            this._spec__parameter = spec;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(function));
        }
    }

    public struct Step_18__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly string _trueBecause__parameter;
        public Step_18__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation, in string trueBecause)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName.ExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName.ExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName.ExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName.ExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName.MultiAssertionExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName.MultiAssertionExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName.MultiAssertionExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicateWithName.MultiAssertionExplanationFromBooleanResultWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_19__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_19__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string>(value));
        }
    }

    public struct Step_20__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string> _trueBecause__parameter;
        public Step_20__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string> trueBecause)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.BooleanResultPredicate.MultiAssertionExplanationFromBooleanResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_21__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> _whenTrue__parameter;
        public Step_21__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }
    }

    public struct Step_22__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> _whenTrue__parameter;
        public Step_22__Motiv_Spec(in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.MultiMetadataFromBooleanResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }
    }

    public struct Step_24__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _predicate__parameter;
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>, string> _trueBecause__parameter;
        public Step_24__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> predicate, in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>, string> trueBecause)
        {
            this._predicate__parameter = predicate;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, string> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_25__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _predicate__parameter;
        private readonly string _trueBecause__parameter;
        public Step_25__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> predicate, in string trueBecause)
        {
            this._predicate__parameter = predicate;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, string> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.ExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyResultWithNamePropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_26__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _predicate__parameter;
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_26__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> predicate, in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            this._predicate__parameter = predicate;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, string> falseBecause)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._predicate__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(function));
        }
    }

    public struct Step_27__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _spec__parameter;
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata> _whenTrue__parameter;
        public Step_27__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> spec, in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>, TReplacementMetadata> whenTrue)
        {
            this._spec__parameter = spec;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }
    }

    public struct Step_28__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _spec__parameter;
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> _whenTrue__parameter;
        public Step_28__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> spec, in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            this._spec__parameter = spec;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new BooleanResultPredicateProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyResultPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(function));
        }
    }

    public struct Step_30__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly string _trueBecause__parameter;
        public Step_30__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in string trueBecause)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicateWithName.ExplanationFromPolicyResultWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicateWithName.ExplanationFromPolicyResultWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicateWithName.ExplanationFromPolicyResultWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicateWithName.ExplanationFromPolicyResultWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.MultiAssertionExplanationWithNameHigherOrderPolicyResultPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.MultiAssertionExplanationWithNameHigherOrderPolicyResultPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.MultiAssertionExplanationWithNameHigherOrderPolicyResultPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.MultiAssertionExplanationWithNameHigherOrderPolicyResultPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_31__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> _trueBecause__parameter;
        public Step_31__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> trueBecause)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.ExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.ExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.ExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.ExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_32__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_32__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_33__Motiv_Spec<TModel, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new Step_33__Motiv_Spec<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_33__Motiv_Spec<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_33__Motiv_Spec<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string>(value));
        }
    }

    public struct Step_33__Motiv_Spec<TModel, TMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> _falseBecause__parameter;
        public Step_33__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
            this._falseBecause__parameter = falseBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata> WithCauseSelector(in System.Func<bool, System.Collections.Generic.IEnumerable<HigherOrderProposition.PolicyResult<TModel, TMetadata>>, System.Collections.Generic.IEnumerable<HigherOrderProposition.PolicyResult<TModel, TMetadata>>> causeSelector)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.PolicyResultPredicate.MultiAssertionExplanationFromPolicyResultHigherOrderPropositionFactory<TModel, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, this._falseBecause__parameter, causeSelector);
        }
    }

    public struct Step_34__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> _whenTrue__parameter;
        public Step_34__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }
    }

    public struct Step_35__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>> _resultResolver__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> _whenTrue__parameter;
        public Step_35__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>> resultResolver, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            this._resultResolver__parameter = resultResolver;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.MultiMetadataFromPolicyResultHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._resultResolver__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }
    }

    public struct Step_37__Motiv_Spec<TModel, TPredicateResult>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly System.Func<TModel, BooleanResultBase<string>, string> _trueBecause__parameter;
        public Step_37__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in System.Func<TModel, Motiv.BooleanResultBase<string>, string> trueBecause)
        {
            this._expression__parameter = expression;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<TModel, BooleanResultBase<string>, string> falseBecause)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in string value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<TModel, string> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_38__Motiv_Spec<TModel, TPredicateResult>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly string _trueBecause__parameter;
        public Step_38__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in string trueBecause)
        {
            this._expression__parameter = expression;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<TModel, BooleanResultBase<string>, string> falseBecause)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in string value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<TModel, string> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.ExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNameExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_39__Motiv_Spec<TModel, TPredicateResult>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_39__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in System.Func<TModel, Motiv.BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            this._expression__parameter = expression;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<TModel, BooleanResultBase<string>, string> falseBecause)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in string value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<TModel, string> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Explanation.MultiAssertionExplanationExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, string>(function));
        }
    }

    public struct Step_40__Motiv_Spec<TModel, TPredicateResult, TMetadata>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly System.Func<TModel, BooleanResultBase<string>, TMetadata> _whenTrue__parameter;
        public Step_40__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in System.Func<TModel, Motiv.BooleanResultBase<string>, TMetadata> whenTrue)
        {
            this._expression__parameter = expression;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MetadataExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in System.Func<TModel, BooleanResultBase<string>, TMetadata> whenFalse)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MetadataExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MetadataExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in TMetadata value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MetadataExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MetadataExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in System.Func<TModel, TMetadata> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MetadataExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, TMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TMetadata>> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>>(function));
        }
    }

    public struct Step_41__Motiv_Spec<TModel, TPredicateResult, TMetadata>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        public Step_41__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in System.Func<TModel, Motiv.BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            this._expression__parameter = expression;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TMetadata>> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in System.Func<TModel, BooleanResultBase<string>, TMetadata> whenFalse)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in TMetadata value)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in System.Func<TModel, TMetadata> function)
        {
            return new ExpressionTreeProposition.PropositionBuilders.Metadata.MultiMetadataPropositionExpressionTreeFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, TMetadata>(function));
        }
    }

    public struct Step_43__Motiv_Spec<TModel, TPredicateResult>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string> _trueBecause__parameter;
        public Step_43__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string> trueBecause)
        {
            this._expression__parameter = expression;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.ExplanationHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.ExplanationHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.ExplanationHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.ExplanationHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_44__Motiv_Spec<TModel, TPredicateResult>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> _higherOrderOperation__parameter;
        private readonly string _trueBecause__parameter;
        public Step_44__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> higherOrderOperation, in string trueBecause)
        {
            this._expression__parameter = expression;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.ExplanationWithNameHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.ExplanationWithNameHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.ExplanationWithNameHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.ExplanationWithNameHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultWithNameHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultWithNameHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultWithNameHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultWithNameHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>>(value));
        }
    }

    public struct Step_45__Motiv_Spec<TModel, TPredicateResult>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_45__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            this._expression__parameter = expression;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.ExpressionTree.MultiAssertionExplanationFromBooleanResultHigherOrderExpressionTreePropositionFactory<TModel, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string>(value));
        }
    }

    public struct Step_46__Motiv_Spec<TModel, TPredicateResult, TMetadata>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, TMetadata> _whenTrue__parameter;
        public Step_46__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, TMetadata> whenTrue)
        {
            this._expression__parameter = expression;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MetadataHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, TMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MetadataHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MetadataHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in TMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MetadataHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }
    }

    public struct Step_47__Motiv_Spec<TModel, TPredicateResult, TMetadata>
    {
        private readonly System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> _expression__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        public Step_47__Motiv_Spec(in System.Linq.Expressions.Expression<System.Func<TModel, TPredicateResult>> expression, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, string> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            this._expression__parameter = expression;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalseYield(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, TMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult> WhenFalse(in TMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.MultiMetadataFromSpecHigherOrderExpressionTreePropositionFactory<TModel, TMetadata, TPredicateResult>(this._expression__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, TMetadata>(value));
        }
    }

    public struct Step_50__Motiv_Spec<TModel, TMetadata>
    {
        private readonly PolicyBase<TModel, TMetadata> _policy__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly string _trueBecause__parameter;
        public Step_50__Motiv_Spec(in Motiv.PolicyBase<TModel, TMetadata> policy, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in string trueBecause)
        {
            this._policy__parameter = policy;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.ExplanationFromPolicyWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.ExplanationFromPolicyWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.ExplanationFromPolicyWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.ExplanationFromPolicyWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string>(value));
        }
    }

    public struct Step_51__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly PolicyBase<TModel, TMetadata> _spec__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> _whenTrue__parameter;
        public Step_51__Motiv_Spec(in Motiv.PolicyBase<TModel, TMetadata> spec, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue)
        {
            this._spec__parameter = spec;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Policy.MetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Policy.MetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Policy.MetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Policy.MetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }
    }

    public struct Step_52__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly PolicyBase<TModel, TMetadata> _policy__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> _whenTrue__parameter;
        public Step_52__Motiv_Spec(in Motiv.PolicyBase<TModel, TMetadata> policy, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderPolicyPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            this._policy__parameter = policy;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._policy__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._policy__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._policy__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Policy.MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._policy__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }
    }

    public struct Step_53__Motiv_Spec<TModel, TMetadata>
    {
        private readonly PolicyBase<TModel, TMetadata> _policy__parameter;
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>, string> _trueBecause__parameter;
        public Step_53__Motiv_Spec(in Motiv.PolicyBase<TModel, TMetadata> policy, in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>, string> trueBecause)
        {
            this._policy__parameter = policy;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPolicyDecoratorFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, string> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPolicyDecoratorFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPolicyDecoratorFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPolicyDecoratorFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPolicyDecoratorFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPolicyDecoratorFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_54__Motiv_Spec<TModel, TMetadata>
    {
        private readonly PolicyBase<TModel, TMetadata> _policy__parameter;
        private readonly string _trueBecause__parameter;
        public Step_54__Motiv_Spec(in Motiv.PolicyBase<TModel, TMetadata> policy, in string trueBecause)
        {
            this._policy__parameter = policy;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, string> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }
    }

    public struct Step_55__Motiv_Spec<TModel, TMetadata>
    {
        private readonly PolicyBase<TModel, TMetadata> _policy__parameter;
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_55__Motiv_Spec(in Motiv.PolicyBase<TModel, TMetadata> policy, in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            this._policy__parameter = policy;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, string> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TMetadata>(this._policy__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(function));
        }
    }

    public struct Step_56__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly PolicyBase<TModel, TMetadata> _spec__parameter;
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata> _whenTrue__parameter;
        public Step_56__Motiv_Spec(in Motiv.PolicyBase<TModel, TMetadata> spec, in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>, TReplacementMetadata> whenTrue)
        {
            this._spec__parameter = spec;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPolicyFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPolicyFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPolicyFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPolicyFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPolicyFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPolicyFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }
    }

    public struct Step_57__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly PolicyBase<TModel, TMetadata> _spec__parameter;
        private readonly System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> _whenTrue__parameter;
        public Step_57__Motiv_Spec(in Motiv.PolicyBase<TModel, TMetadata> spec, in System.Func<TModel, Motiv.PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            this._spec__parameter = spec;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataFromPolicyPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(function));
        }
    }

    public struct Step_60__Motiv_Spec<TModel, TMetadata>
    {
        private readonly SpecBase<TModel, TMetadata> _spec__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly string _trueBecause__parameter;
        public Step_60__Motiv_Spec(in Motiv.SpecBase<TModel, TMetadata> spec, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation, in string trueBecause)
        {
            this._spec__parameter = spec;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.ExplanationWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string> falseBecause)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.ExplanationWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.ExplanationWithNameHigherOrderPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.ExplanationWithNameHigherOrderPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string>(value));
        }
    }

    public struct Step_61__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly SpecBase<TModel, TMetadata> _spec__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> _whenTrue__parameter;
        public Step_61__Motiv_Spec(in Motiv.SpecBase<TModel, TMetadata> spec, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue)
        {
            this._spec__parameter = spec;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Spec.MetadataHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Spec.MetadataHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Spec.MetadataHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Spec.MetadataHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }
    }

    public struct Step_62__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly SpecBase<TModel, TMetadata> _spec__parameter;
        private readonly HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> _higherOrderOperation__parameter;
        private readonly System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> _whenTrue__parameter;
        public Step_62__Motiv_Spec(in Motiv.SpecBase<TModel, TMetadata> spec, in Motiv.HigherOrderProposition.PropositionBuilders.HigherOrderSpecPredicateOperation<TModel, TMetadata> higherOrderOperation, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            this._spec__parameter = spec;
            this._higherOrderOperation__parameter = higherOrderOperation;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.Spec.MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._higherOrderOperation__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }
    }

    public struct Step_63__Motiv_Spec<TModel, TMetadata>
    {
        private readonly SpecBase<TModel, TMetadata> _spec__parameter;
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>, string> _trueBecause__parameter;
        public Step_63__Motiv_Spec(in Motiv.SpecBase<TModel, TMetadata> spec, in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>, string> trueBecause)
        {
            this._spec__parameter = spec;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, string> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_64__Motiv_Spec<TModel, TMetadata>
    {
        private readonly SpecBase<TModel, TMetadata> _spec__parameter;
        private readonly string _trueBecause__parameter;
        public Step_64__Motiv_Spec(in Motiv.SpecBase<TModel, TMetadata> spec, in string trueBecause)
        {
            this._spec__parameter = spec;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, string> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationWithNamePropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationWithNamePropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }
    }

    public struct Step_65__Motiv_Spec<TModel, TMetadata>
    {
        private readonly SpecBase<TModel, TMetadata> _spec__parameter;
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_65__Motiv_Spec(in Motiv.SpecBase<TModel, TMetadata> spec, in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            this._spec__parameter = spec;
            this._trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, string> falseBecause)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in string value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata> WhenFalse(in System.Func<TModel, string> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Explanation.MultiAssertionExplanationPropositionFactory<TModel, TMetadata>(this._spec__parameter, this._trueBecause__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }
    }

    public struct Step_66__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly SpecBase<TModel, TMetadata> _spec__parameter;
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> _whenTrue__parameter;
        public Step_66__Motiv_Spec(in Motiv.SpecBase<TModel, TMetadata> spec, in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>, TReplacementMetadata> whenTrue)
        {
            this._spec__parameter = spec;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }
    }

    public struct Step_67__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>
    {
        private readonly SpecBase<TModel, TMetadata> _spec__parameter;
        private readonly System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> _whenTrue__parameter;
        public Step_67__Motiv_Spec(in Motiv.SpecBase<TModel, TMetadata> spec, in System.Func<TModel, Motiv.BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            this._spec__parameter = spec;
            this._whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenFalse)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalseYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> whenFalse)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, whenFalse);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in TReplacementMetadata value)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata> WhenFalse(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new SpecDecoratorProposition.PropositionBuilders.Metadata.MultiMetadataPropositionFactory<TModel, TReplacementMetadata, TMetadata>(this._spec__parameter, this._whenTrue__parameter, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(function));
        }
    }
}

namespace Motiv.HigherOrderProposition.PropositionBuilders.Metadata.BooleanPredicate
{
    public partial struct TrueHigherOrderFromBooleanPredicatePropositionFactory<TModel>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_7__Motiv_Spec<TModel> WhenTrue(in string trueBecause)
        {
            return new Step_7__Motiv_Spec<TModel>(predicate, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_8__Motiv_Spec<TModel> WhenTrue(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, string> trueBecause)
        {
            return new Step_8__Motiv_Spec<TModel>(predicate, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_9__Motiv_Spec<TModel, TMetadata> WhenTrue<TMetadata>(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, TMetadata> whenTrue)
        {
            return new Step_9__Motiv_Spec<TModel, TMetadata>(predicate, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_9__Motiv_Spec<TModel, TMetadata> WhenTrue<TMetadata>(in TMetadata value)
        {
            return new Step_9__Motiv_Spec<TModel, TMetadata>(predicate, higherOrderOperation, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_10__Motiv_Spec<TModel, TMetadata> WhenTrueYield<TMetadata>(in System.Func<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            return new Step_10__Motiv_Spec<TModel, TMetadata>(predicate, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_10__Motiv_Spec<TModel, TMetadata> WhenTrueYield<TMetadata>(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new Step_10__Motiv_Spec<TModel, TMetadata>(predicate, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanEvaluation<TModel>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }
    }
}

namespace Motiv.BooleanResultPredicateProposition.PropositionBuilders
{
    public partial struct BooleanResultPredicatePropositionFactory<TModel, TMetadata>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_12__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<TModel, BooleanResultBase<TMetadata>, string> trueBecause)
        {
            return new Step_12__Motiv_Spec<TModel, TMetadata>(predicate, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_12__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<TModel, string> function)
        {
            return new Step_12__Motiv_Spec<TModel, TMetadata>(predicate, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_13__Motiv_Spec<TModel, TMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_13__Motiv_Spec<TModel, TMetadata>(predicate, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_14__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_14__Motiv_Spec<TModel, TMetadata>(predicate, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_14__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_14__Motiv_Spec<TModel, TMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_14__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new Step_14__Motiv_Spec<TModel, TMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_15__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> whenTrue)
        {
            return new Step_15__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_15__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in TReplacementMetadata value)
        {
            return new Step_15__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_15__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new Step_15__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_16__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            return new Step_16__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_16__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new Step_16__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_16__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new Step_16__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata> As(in System.Func<System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, TMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, TMetadata>>, System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, TMetadata>>> causeSelector)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.As<TModel, TMetadata>(higherOrderPredicate, causeSelector));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata> AsAllSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAllSatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata> AsAnySatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAnySatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata> AsAtLeastNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAtLeastNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata> AsAtMostNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAtMostNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata> AsNoneSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsNoneSatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata> AsNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate.TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsNSatisfied<TModel, TMetadata>(n));
        }
    }

    public partial struct PolicyResultPredicatePropositionFactory<TModel, TMetadata>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_24__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<TModel, PolicyResultBase<TMetadata>, string> trueBecause)
        {
            return new Step_24__Motiv_Spec<TModel, TMetadata>(predicate, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_24__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<TModel, string> function)
        {
            return new Step_24__Motiv_Spec<TModel, TMetadata>(predicate, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_25__Motiv_Spec<TModel, TMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_25__Motiv_Spec<TModel, TMetadata>(predicate, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_26__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_26__Motiv_Spec<TModel, TMetadata>(predicate, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_26__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_26__Motiv_Spec<TModel, TMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_26__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new Step_26__Motiv_Spec<TModel, TMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_27__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata> whenTrue)
        {
            return new Step_27__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_27__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in TReplacementMetadata value)
        {
            return new Step_27__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_27__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new Step_27__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_28__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            return new Step_28__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_28__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new Step_28__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_28__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new Step_28__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(predicate, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata> As(in System.Func<System.Collections.Generic.IEnumerable<HigherOrderProposition.PolicyResult<TModel, TMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<HigherOrderProposition.PolicyResult<TModel, TMetadata>>, System.Collections.Generic.IEnumerable<HigherOrderProposition.PolicyResult<TModel, TMetadata>>> causeSelector)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.As<TModel, TMetadata>(higherOrderPredicate, causeSelector));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata> AsAllSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsAllSatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata> AsAnySatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsAnySatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata> AsAtLeastNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsAtLeastNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata> AsAtMostNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsAtMostNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata> AsNoneSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsNoneSatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata> AsNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate.TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata>(predicate, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsNSatisfied<TModel, TMetadata>(n));
        }
    }
}

namespace Motiv.HigherOrderProposition.PropositionBuilders.Metadata.BooleanResultPredicate
{
    public partial struct TrueHigherOrderFromBooleanResultPredicatePropositionFactory<TModel, TMetadata>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_18__Motiv_Spec<TModel, TMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_18__Motiv_Spec<TModel, TMetadata>(resultResolver, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_19__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_19__Motiv_Spec<TModel, TMetadata>(resultResolver, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_19__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_19__Motiv_Spec<TModel, TMetadata>(resultResolver, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_20__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, string> trueBecause)
        {
            return new Step_20__Motiv_Spec<TModel, TMetadata>(resultResolver, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_21__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue)
        {
            return new Step_21__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(resultResolver, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_21__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in TReplacementMetadata value)
        {
            return new Step_21__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(resultResolver, higherOrderOperation, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_22__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            return new Step_22__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(resultResolver, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_22__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new Step_22__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(resultResolver, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }
    }
}

namespace Motiv.HigherOrderProposition.PropositionBuilders.Metadata.PolicyResultPredicate
{
    public partial struct TrueHigherOrderFromPolicyResultPredicatePropositionFactory<TModel, TMetadata>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_30__Motiv_Spec<TModel, TMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_30__Motiv_Spec<TModel, TMetadata>(resultResolver, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_31__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, string> trueBecause)
        {
            return new Step_31__Motiv_Spec<TModel, TMetadata>(resultResolver, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_32__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_32__Motiv_Spec<TModel, TMetadata>(resultResolver, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_32__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_32__Motiv_Spec<TModel, TMetadata>(resultResolver, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_34__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue)
        {
            return new Step_34__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(resultResolver, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_34__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in TReplacementMetadata value)
        {
            return new Step_34__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(resultResolver, higherOrderOperation, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_35__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            return new Step_35__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(resultResolver, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_35__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new Step_35__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(resultResolver, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }
    }
}

namespace Motiv.ExpressionTreeProposition.PropositionBuilders
{
    public partial struct MinimalExpressionTreePropositionFactory<TModel, TPredicateResult>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_37__Motiv_Spec<TModel, TPredicateResult> WhenTrue(in System.Func<TModel, BooleanResultBase<string>, string> trueBecause)
        {
            return new Step_37__Motiv_Spec<TModel, TPredicateResult>(expression, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_37__Motiv_Spec<TModel, TPredicateResult> WhenTrue(in System.Func<TModel, string> function)
        {
            return new Step_37__Motiv_Spec<TModel, TPredicateResult>(expression, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_38__Motiv_Spec<TModel, TPredicateResult> WhenTrue(in string trueBecause)
        {
            return new Step_38__Motiv_Spec<TModel, TPredicateResult>(expression, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_39__Motiv_Spec<TModel, TPredicateResult> WhenTrueYield(in System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_39__Motiv_Spec<TModel, TPredicateResult>(expression, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_39__Motiv_Spec<TModel, TPredicateResult> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_39__Motiv_Spec<TModel, TPredicateResult>(expression, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_39__Motiv_Spec<TModel, TPredicateResult> WhenTrueYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new Step_39__Motiv_Spec<TModel, TPredicateResult>(expression, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_40__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrue<TMetadata>(in System.Func<TModel, BooleanResultBase<string>, TMetadata> whenTrue)
        {
            return new Step_40__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_40__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrue<TMetadata>(in TMetadata value)
        {
            return new Step_40__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_40__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrue<TMetadata>(in System.Func<TModel, TMetadata> function)
        {
            return new Step_40__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<string>, TMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_41__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrueYield<TMetadata>(in System.Func<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            return new Step_41__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_41__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrueYield<TMetadata>(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new Step_41__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_41__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrueYield<TMetadata>(in System.Func<TModel, System.Collections.Generic.IEnumerable<TMetadata>> function)
        {
            return new Step_41__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<string>, System.Collections.Generic.IEnumerable<TMetadata>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult> As(in System.Func<System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, string>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, string>>, System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, string>>> causeSelector)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>(expression, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.As<TModel, string>(higherOrderPredicate, causeSelector));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult> AsAllSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>(expression, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAllSatisfied<TModel, string>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult> AsAnySatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>(expression, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAnySatisfied<TModel, string>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult> AsAtLeastNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>(expression, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAtLeastNSatisfied<TModel, string>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult> AsAtMostNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>(expression, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAtMostNSatisfied<TModel, string>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult> AsNoneSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>(expression, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsNoneSatisfied<TModel, string>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult> AsNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree.TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>(expression, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsNSatisfied<TModel, string>(n));
        }
    }
}

namespace Motiv.HigherOrderProposition.PropositionBuilders.Metadata.ExpressionTree
{
    public partial struct TrueExpressionTreeHigherOrderFromSpecPropositionFactory<TModel, TPredicateResult>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_43__Motiv_Spec<TModel, TPredicateResult> WhenTrue(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, string> trueBecause)
        {
            return new Step_43__Motiv_Spec<TModel, TPredicateResult>(expression, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_44__Motiv_Spec<TModel, TPredicateResult> WhenTrue(in string trueBecause)
        {
            return new Step_44__Motiv_Spec<TModel, TPredicateResult>(expression, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_45__Motiv_Spec<TModel, TPredicateResult> WhenTrueYield(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_45__Motiv_Spec<TModel, TPredicateResult>(expression, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_45__Motiv_Spec<TModel, TPredicateResult> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_45__Motiv_Spec<TModel, TPredicateResult>(expression, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_46__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrue<TMetadata>(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, TMetadata> whenTrue)
        {
            return new Step_46__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_46__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrue<TMetadata>(in TMetadata value)
        {
            return new Step_46__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, higherOrderOperation, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, TMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_47__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrueYield<TMetadata>(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            return new Step_47__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_47__Motiv_Spec<TModel, TPredicateResult, TMetadata> WhenTrueYield<TMetadata>(in System.Collections.Generic.IEnumerable<TMetadata> value)
        {
            return new Step_47__Motiv_Spec<TModel, TPredicateResult, TMetadata>(expression, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, string>, System.Collections.Generic.IEnumerable<TMetadata>>(value));
        }
    }
}

namespace Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation
{
    public partial struct MinimalPolicyDecoratorFactory<TModel, TMetadata>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata> As(in System.Func<System.Collections.Generic.IEnumerable<HigherOrderProposition.PolicyResult<TModel, TMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<HigherOrderProposition.PolicyResult<TModel, TMetadata>>, System.Collections.Generic.IEnumerable<HigherOrderProposition.PolicyResult<TModel, TMetadata>>> causeSelector)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata>(policy, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.As<TModel, TMetadata>(higherOrderPredicate, causeSelector));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata> AsAllSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata>(policy, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsAllSatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata> AsAnySatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata>(policy, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsAnySatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata> AsAtLeastNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata>(policy, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsAtLeastNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata> AsAtMostNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata>(policy, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsAtMostNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata> AsNoneSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata>(policy, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsNoneSatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata> AsNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Policy.HigherOrderFromPolicyPropositionFactory<TModel, TMetadata>(policy, HigherOrderProposition.PropositionBuilders.HigherOrderPredicatePolicyMethods.AsNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_53__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<TModel, PolicyResultBase<TMetadata>, string> trueBecause)
        {
            return new Step_53__Motiv_Spec<TModel, TMetadata>(policy, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_53__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<TModel, string> function)
        {
            return new Step_53__Motiv_Spec<TModel, TMetadata>(policy, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_54__Motiv_Spec<TModel, TMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_54__Motiv_Spec<TModel, TMetadata>(policy, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_55__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_55__Motiv_Spec<TModel, TMetadata>(policy, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_55__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_55__Motiv_Spec<TModel, TMetadata>(policy, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_55__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new Step_55__Motiv_Spec<TModel, TMetadata>(policy, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_56__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata> whenTrue)
        {
            return new Step_56__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_56__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in TReplacementMetadata value)
        {
            return new Step_56__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_56__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new Step_56__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, Shared.WhenOverloads.Convert<TModel, PolicyResultBase<TMetadata>, TReplacementMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_57__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            return new Step_57__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_57__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new Step_57__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_57__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new Step_57__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, Shared.WhenYieldOverloads.Convert<TModel, PolicyResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }
    }

    public partial struct MinimalSpecDecoratorFactory<TModel, TMetadata>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata> As(in System.Func<System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, TMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, TMetadata>>, System.Collections.Generic.IEnumerable<HigherOrderProposition.BooleanResult<TModel, TMetadata>>> causeSelector)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata>(spec, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.As<TModel, TMetadata>(higherOrderPredicate, causeSelector));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata> AsAllSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata>(spec, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAllSatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata> AsAnySatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata>(spec, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAnySatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata> AsAtLeastNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata>(spec, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAtLeastNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata> AsAtMostNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata>(spec, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsAtMostNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata> AsNoneSatisfied()
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata>(spec, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsNoneSatisfied<TModel, TMetadata>());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata> AsNSatisfied(in int n)
        {
            return new HigherOrderProposition.PropositionBuilders.Explanation.Spec.HigherOrderFromSpecPropositionFactory<TModel, TMetadata>(spec, HigherOrderProposition.PropositionBuilders.HigherOrderPredicateSpecMethods.AsNSatisfied<TModel, TMetadata>(n));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_63__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<TModel, BooleanResultBase<TMetadata>, string> trueBecause)
        {
            return new Step_63__Motiv_Spec<TModel, TMetadata>(spec, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_63__Motiv_Spec<TModel, TMetadata> WhenTrue(in System.Func<TModel, string> function)
        {
            return new Step_63__Motiv_Spec<TModel, TMetadata>(spec, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, string>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_64__Motiv_Spec<TModel, TMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_64__Motiv_Spec<TModel, TMetadata>(spec, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_65__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_65__Motiv_Spec<TModel, TMetadata>(spec, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_65__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Collections.Generic.IEnumerable<string> value)
        {
            return new Step_65__Motiv_Spec<TModel, TMetadata>(spec, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_65__Motiv_Spec<TModel, TMetadata> WhenTrueYield(in System.Func<TModel, System.Collections.Generic.IEnumerable<string>> function)
        {
            return new Step_65__Motiv_Spec<TModel, TMetadata>(spec, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<string>>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_66__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata> whenTrue)
        {
            return new Step_66__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_66__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in TReplacementMetadata value)
        {
            return new Step_66__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_66__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<TModel, TReplacementMetadata> function)
        {
            return new Step_66__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, Shared.WhenOverloads.Convert<TModel, BooleanResultBase<TMetadata>, TReplacementMetadata>(function));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_67__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            return new Step_67__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_67__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new Step_67__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_67__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<TModel, System.Collections.Generic.IEnumerable<TReplacementMetadata>> function)
        {
            return new Step_67__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, Shared.WhenYieldOverloads.Convert<TModel, BooleanResultBase<TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(function));
        }
    }
}

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Policy
{
    public partial struct HigherOrderFromPolicyPropositionFactory<TModel, TMetadata>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_50__Motiv_Spec<TModel, TMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_50__Motiv_Spec<TModel, TMetadata>(policy, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_51__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue)
        {
            return new Step_51__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_51__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in TReplacementMetadata value)
        {
            return new Step_51__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, higherOrderOperation, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_52__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            return new Step_52__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_52__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new Step_52__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(policy, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }
    }
}

namespace Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Spec
{
    public partial struct HigherOrderFromSpecPropositionFactory<TModel, TMetadata>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_60__Motiv_Spec<TModel, TMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_60__Motiv_Spec<TModel, TMetadata>(spec, higherOrderOperation, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_61__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata> whenTrue)
        {
            return new Step_61__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_61__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrue<TReplacementMetadata>(in TReplacementMetadata value)
        {
            return new Step_61__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, higherOrderOperation, Shared.WhenOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, TReplacementMetadata>(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_62__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Func<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>> whenTrue)
        {
            return new Step_62__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, higherOrderOperation, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_62__Motiv_Spec<TModel, TMetadata, TReplacementMetadata> WhenTrueYield<TReplacementMetadata>(in System.Collections.Generic.IEnumerable<TReplacementMetadata> value)
        {
            return new Step_62__Motiv_Spec<TModel, TMetadata, TReplacementMetadata>(spec, higherOrderOperation, Shared.WhenYieldOverloads.Convert<HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TMetadata>, System.Collections.Generic.IEnumerable<TReplacementMetadata>>(value));
        }
    }
}