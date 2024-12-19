using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Policy;
using Motiv.HigherOrderProposition.PropositionBuilders.Explanation.Spec;
using Motiv.HigherOrderProposition.PropositionBuilders.Metadata.Policy;
using Motiv.HigherOrderProposition.PropositionBuilders.Metadata.Spec;
using Motiv.SpecDecoratorProposition.PropositionBuilders;
using Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation;
using Motiv.SpecDecoratorProposition.PropositionBuilders.Metadata;
using System;

namespace Motiv
{
    public static partial class Spec
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Step_0__Motiv_Spec<TModel, TUnderlyingMetadata> Build<TModel, TUnderlyingMetadata>(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy)
        {
            return new Step_0__Motiv_Spec<TModel, TUnderlyingMetadata>(policy);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Step_1__Motiv_Spec<TModel, TUnderlyingMetadata> Build<TModel, TUnderlyingMetadata>(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> resultResolver)
        {
            return new Step_1__Motiv_Spec<TModel, TUnderlyingMetadata>(resultResolver);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Step_2__Motiv_Spec<TModel, TUnderlyingMetadata> Build<TModel, TUnderlyingMetadata>(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec)
        {
            return new Step_2__Motiv_Spec<TModel, TUnderlyingMetadata>(spec);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Step_3__Motiv_Spec<TModel, TUnderlyingMetadata> Build<TModel, TUnderlyingMetadata>(in Motiv.SpecDecoratorProposition.PropositionBuilders.TruePropositionBuilder<TModel, TUnderlyingMetadata> builder)
        {
            return new Step_3__Motiv_Spec<TModel, TUnderlyingMetadata>(builder);
        }
    }

    public struct Step_0__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _policy__parameter;
        public Step_0__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy)
        {
            _policy__parameter = policy;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_4__Motiv_Spec<TModel, TUnderlyingMetadata> As(in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate)
        {
            return new Step_4__Motiv_Spec<TModel, TUnderlyingMetadata>(_policy__parameter, higherOrderPredicate);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_5__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, string> trueBecause)
        {
            return new Step_5__Motiv_Spec<TModel, TUnderlyingMetadata>(_policy__parameter, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_5__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in string value)
        {
            return new Step_5__Motiv_Spec<TModel, TUnderlyingMetadata>(Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationConverter.Convert(_policy__parameter, value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_5__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue<T1>(in System.Func<T1, string> value)
        {
            return new Step_5__Motiv_Spec<TModel, TUnderlyingMetadata>(Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationConverter.Convert(_policy__parameter, value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_6__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_6__Motiv_Spec<TModel, TUnderlyingMetadata>(_policy__parameter, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_7__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenTrue<TMetadata>(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, TMetadata> whenTrue)
        {
            return new Step_7__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_policy__parameter, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_8__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenTrue<TMetadata>(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            return new Step_8__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_policy__parameter, whenTrue);
        }
    }

    public struct Step_1__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> _resultResolver__parameter;
        public Step_1__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> resultResolver)
        {
            _resultResolver__parameter = resultResolver;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_16__Motiv_Spec<TModel, TUnderlyingMetadata> As(in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate)
        {
            return new Step_16__Motiv_Spec<TModel, TUnderlyingMetadata>(_resultResolver__parameter, higherOrderPredicate);
        }
    }

    public struct Step_2__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        public Step_2__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec)
        {
            _spec__parameter = spec;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_20__Motiv_Spec<TModel, TUnderlyingMetadata> As(in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate)
        {
            return new Step_20__Motiv_Spec<TModel, TUnderlyingMetadata>(_spec__parameter, higherOrderPredicate);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_21__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, string> trueBecause)
        {
            return new Step_21__Motiv_Spec<TModel, TUnderlyingMetadata>(_spec__parameter, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_22__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_22__Motiv_Spec<TModel, TUnderlyingMetadata>(_spec__parameter, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_23__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            return new Step_23__Motiv_Spec<TModel, TUnderlyingMetadata>(_spec__parameter, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_24__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenTrue<TMetadata>(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, TMetadata> whenTrue)
        {
            return new Step_24__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_spec__parameter, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_25__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenTrue<TMetadata>(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            return new Step_25__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_spec__parameter, whenTrue);
        }
    }

    public struct Step_3__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecDecoratorProposition.PropositionBuilders.TruePropositionBuilder<TModel, TUnderlyingMetadata> _builder__parameter;
        public Step_3__Motiv_Spec(in Motiv.SpecDecoratorProposition.PropositionBuilders.TruePropositionBuilder<TModel, TUnderlyingMetadata> builder)
        {
            _builder__parameter = builder;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TruePropositionBuilder<TModel, TUnderlyingMetadata> Create()
        {
            return new TruePropositionBuilder<TModel, TUnderlyingMetadata>(_builder__parameter);
        }
    }

    public struct Step_4__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _policy__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        public Step_4__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate)
        {
            _policy__parameter = policy;
            _higherOrderPredicate__parameter = higherOrderPredicate;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_9__Motiv_Spec<TModel, TUnderlyingMetadata> WithCauses(in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector)
        {
            return new Step_9__Motiv_Spec<TModel, TUnderlyingMetadata>(_policy__parameter, _higherOrderPredicate__parameter, causeSelector);
        }
    }

    public struct Step_5__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, string> _trueBecause__parameter;
        public Step_5__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> spec, in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, string> trueBecause)
        {
            _spec__parameter = spec;
            _trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExplanationPolicyFactory<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, string> falseBecause)
        {
            return new ExplanationPolicyFactory<TModel, TUnderlyingMetadata>(_spec__parameter, _trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExplanationPolicyFactory<TModel, TUnderlyingMetadata> WhenFalse(in string value)
        {
            return new ExplanationPolicyFactory<TModel, TUnderlyingMetadata>(_spec__parameter, _trueBecause__parameter, Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationConverter.Convert(value));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExplanationPolicyFactory<TModel, TUnderlyingMetadata> WhenFalse<T1>(in System.Func<T1, string> value)
        {
            return new ExplanationPolicyFactory<TModel, TUnderlyingMetadata>(_spec__parameter, _trueBecause__parameter, Motiv.SpecDecoratorProposition.PropositionBuilders.Explanation.ExplanationConverter.Convert(value));
        }
    }

    public struct Step_6__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _policy__parameter;
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_6__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy, in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            _policy__parameter = policy;
            _trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new MultiAssertionExplanationFromPolicyPropositionFactory<TModel, TUnderlyingMetadata>(_policy__parameter, _trueBecause__parameter, falseBecause);
        }
    }

    public struct Step_7__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, TMetadata> _whenTrue__parameter;
        public Step_7__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> spec, in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, TMetadata> whenTrue)
        {
            _spec__parameter = spec;
            _whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MetadataPolicyFactory<TModel, TMetadata, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, TMetadata> whenFalse)
        {
            return new MetadataPolicyFactory<TModel, TMetadata, TUnderlyingMetadata>(_spec__parameter, _whenTrue__parameter, whenFalse);
        }
    }

    public struct Step_8__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        public Step_8__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> spec, in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            _spec__parameter = spec;
            _whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MultiMetadataFromPolicyPropositionFactory<TModel, TMetadata, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new MultiMetadataFromPolicyPropositionFactory<TModel, TMetadata, TUnderlyingMetadata>(_spec__parameter, _whenTrue__parameter, whenFalse);
        }
    }

    public struct Step_9__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _policy__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        public Step_9__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector)
        {
            _policy__parameter = policy;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_10__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_10__Motiv_Spec<TModel, TUnderlyingMetadata>(_policy__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_11__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenTrue<TMetadata>(in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenTrue)
        {
            return new Step_11__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_policy__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_12__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenTrue<TMetadata>(in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            return new Step_12__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_policy__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, whenTrue);
        }
    }

    public struct Step_10__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _policy__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly string _trueBecause__parameter;
        public Step_10__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector, in string trueBecause)
        {
            _policy__parameter = policy;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_13__Motiv_Spec<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, string> falseBecause)
        {
            return new Step_13__Motiv_Spec<TModel, TUnderlyingMetadata>(_policy__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _trueBecause__parameter, falseBecause);
        }
    }

    public struct Step_11__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> _whenTrue__parameter;
        public Step_11__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenTrue)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_14__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenFalse(in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenFalse)
        {
            return new Step_14__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _whenTrue__parameter, whenFalse);
        }
    }

    public struct Step_12__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _policy__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        public Step_12__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            _policy__parameter = policy;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_15__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenFalse(in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new Step_15__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_policy__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _whenTrue__parameter, whenFalse);
        }
    }

    public struct Step_13__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _policy__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly string _trueBecause__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, string> _falseBecause__parameter;
        public Step_13__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector, in string trueBecause, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, string> falseBecause)
        {
            _policy__parameter = policy;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _trueBecause__parameter = trueBecause;
            _falseBecause__parameter = falseBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExplanationFromPolicyWithNameHigherOrderPropositionFactory<TModel, TUnderlyingMetadata> Create()
        {
            return new ExplanationFromPolicyWithNameHigherOrderPropositionFactory<TModel, TUnderlyingMetadata>(_policy__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _trueBecause__parameter, _falseBecause__parameter);
        }
    }

    public struct Step_14__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> _whenTrue__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> _whenFalse__parameter;
        public Step_14__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenTrue, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenFalse)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _whenTrue__parameter = whenTrue;
            _whenFalse__parameter = whenFalse;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MetadataFromPolicyHigherOrderPropositionFactory<TModel, TMetadata, TUnderlyingMetadata> Create()
        {
            return new MetadataFromPolicyHigherOrderPropositionFactory<TModel, TMetadata, TUnderlyingMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _whenTrue__parameter, _whenFalse__parameter);
        }
    }

    public struct Step_15__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.PolicyBase<TModel, TUnderlyingMetadata> _policy__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> _whenFalse__parameter;
        public Step_15__Motiv_Spec(in Motiv.PolicyBase<TModel, TUnderlyingMetadata> policy, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            _policy__parameter = policy;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _whenTrue__parameter = whenTrue;
            _whenFalse__parameter = whenFalse;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TMetadata, TUnderlyingMetadata> Create()
        {
            return new MultiMetadataFromPolicyHigherOrderPropositionFactory<TModel, TMetadata, TUnderlyingMetadata>(_policy__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _whenTrue__parameter, _whenFalse__parameter);
        }
    }

    public struct Step_16__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> _resultResolver__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        public Step_16__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> resultResolver, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate)
        {
            _resultResolver__parameter = resultResolver;
            _higherOrderPredicate__parameter = higherOrderPredicate;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_17__Motiv_Spec<TModel, TUnderlyingMetadata> WithCauses(in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector)
        {
            return new Step_17__Motiv_Spec<TModel, TUnderlyingMetadata>(_resultResolver__parameter, _higherOrderPredicate__parameter, causeSelector);
        }
    }

    public struct Step_17__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> _resultResolver__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        public Step_17__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> resultResolver, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector)
        {
            _resultResolver__parameter = resultResolver;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_18__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_18__Motiv_Spec<TModel, TUnderlyingMetadata>(_resultResolver__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, trueBecause);
        }
    }

    public struct Step_18__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> _resultResolver__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly string _trueBecause__parameter;
        public Step_18__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> resultResolver, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector, in string trueBecause)
        {
            _resultResolver__parameter = resultResolver;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_19__Motiv_Spec<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new Step_19__Motiv_Spec<TModel, TUnderlyingMetadata>(_resultResolver__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _trueBecause__parameter, falseBecause);
        }
    }

    public struct Step_19__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> _resultResolver__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly string _trueBecause__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> _falseBecause__parameter;
        public Step_19__Motiv_Spec(in System.Func<TModel, Motiv.PolicyResultBase<TUnderlyingMetadata>> resultResolver, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.PolicyResult<TModel, TUnderlyingMetadata>>> causeSelector, in string trueBecause, in System.Func<Motiv.HigherOrderProposition.HigherOrderPolicyResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            _resultResolver__parameter = resultResolver;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _trueBecause__parameter = trueBecause;
            _falseBecause__parameter = falseBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MultiAssertionExplanationWithNameHigherOrderPolicyResultPropositionFactory<TModel, TUnderlyingMetadata> Create()
        {
            return new MultiAssertionExplanationWithNameHigherOrderPolicyResultPropositionFactory<TModel, TUnderlyingMetadata>(_resultResolver__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _trueBecause__parameter, _falseBecause__parameter);
        }
    }

    public struct Step_20__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        public Step_20__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_26__Motiv_Spec<TModel, TUnderlyingMetadata> WithCauses(in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> causeSelector)
        {
            return new Step_26__Motiv_Spec<TModel, TUnderlyingMetadata>(_spec__parameter, _higherOrderPredicate__parameter, causeSelector);
        }
    }

    public struct Step_21__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, string> _trueBecause__parameter;
        public Step_21__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, string> trueBecause)
        {
            _spec__parameter = spec;
            _trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExplanationPropositionFactory<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, string> falseBecause)
        {
            return new ExplanationPropositionFactory<TModel, TUnderlyingMetadata>(_spec__parameter, _trueBecause__parameter, falseBecause);
        }
    }

    public struct Step_22__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly string _trueBecause__parameter;
        public Step_22__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in string trueBecause)
        {
            _spec__parameter = spec;
            _trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExplanationWithNamePropositionFactory<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, string> falseBecause)
        {
            return new ExplanationWithNamePropositionFactory<TModel, TUnderlyingMetadata>(_spec__parameter, _trueBecause__parameter, falseBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MultiAssertionExplanationWithNamePropositionFactory<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new MultiAssertionExplanationWithNamePropositionFactory<TModel, TUnderlyingMetadata>(_spec__parameter, _trueBecause__parameter, falseBecause);
        }
    }

    public struct Step_23__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> _trueBecause__parameter;
        public Step_23__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> trueBecause)
        {
            _spec__parameter = spec;
            _trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MultiAssertionExplanationPropositionFactory<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<string>> falseBecause)
        {
            return new MultiAssertionExplanationPropositionFactory<TModel, TUnderlyingMetadata>(_spec__parameter, _trueBecause__parameter, falseBecause);
        }
    }

    public struct Step_24__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, TMetadata> _whenTrue__parameter;
        public Step_24__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, TMetadata> whenTrue)
        {
            _spec__parameter = spec;
            _whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MetadataPropositionFactory<TModel, TMetadata, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, TMetadata> whenFalse)
        {
            return new MetadataPropositionFactory<TModel, TMetadata, TUnderlyingMetadata>(_spec__parameter, _whenTrue__parameter, whenFalse);
        }
    }

    public struct Step_25__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        public Step_25__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            _spec__parameter = spec;
            _whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MultiMetadataPropositionFactory<TModel, TMetadata, TUnderlyingMetadata> WhenFalse(in System.Func<TModel, Motiv.BooleanResultBase<TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new MultiMetadataPropositionFactory<TModel, TMetadata, TUnderlyingMetadata>(_spec__parameter, _whenTrue__parameter, whenFalse);
        }
    }

    public struct Step_26__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        public Step_26__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> causeSelector)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_27__Motiv_Spec<TModel, TUnderlyingMetadata> WhenTrue(in string trueBecause)
        {
            return new Step_27__Motiv_Spec<TModel, TUnderlyingMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, trueBecause);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_28__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenTrue<TMetadata>(in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenTrue)
        {
            return new Step_28__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, whenTrue);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_29__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenTrue<TMetadata>(in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            return new Step_29__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, whenTrue);
        }
    }

    public struct Step_27__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly string _trueBecause__parameter;
        public Step_27__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> causeSelector, in string trueBecause)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _trueBecause__parameter = trueBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_30__Motiv_Spec<TModel, TUnderlyingMetadata> WhenFalse(in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, string> falseBecause)
        {
            return new Step_30__Motiv_Spec<TModel, TUnderlyingMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _trueBecause__parameter, falseBecause);
        }
    }

    public struct Step_28__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> _whenTrue__parameter;
        public Step_28__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> causeSelector, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenTrue)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_31__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenFalse(in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenFalse)
        {
            return new Step_31__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _whenTrue__parameter, whenFalse);
        }
    }

    public struct Step_29__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        public Step_29__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> causeSelector, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _whenTrue__parameter = whenTrue;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_32__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata> WhenFalse(in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            return new Step_32__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _whenTrue__parameter, whenFalse);
        }
    }

    public struct Step_30__Motiv_Spec<TModel, TUnderlyingMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly string _trueBecause__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, string> _falseBecause__parameter;
        public Step_30__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> causeSelector, in string trueBecause, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, string> falseBecause)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _trueBecause__parameter = trueBecause;
            _falseBecause__parameter = falseBecause;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ExplanationWithNameHigherOrderPropositionFactory<TModel, TUnderlyingMetadata> Create()
        {
            return new ExplanationWithNameHigherOrderPropositionFactory<TModel, TUnderlyingMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _trueBecause__parameter, _falseBecause__parameter);
        }
    }

    public struct Step_31__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> _whenTrue__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> _whenFalse__parameter;
        public Step_31__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> causeSelector, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenTrue, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, TMetadata> whenFalse)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _whenTrue__parameter = whenTrue;
            _whenFalse__parameter = whenFalse;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MetadataHigherOrderPropositionFactory<TModel, TMetadata, TUnderlyingMetadata> Create()
        {
            return new MetadataHigherOrderPropositionFactory<TModel, TMetadata, TUnderlyingMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _whenTrue__parameter, _whenFalse__parameter);
        }
    }

    public struct Step_32__Motiv_Spec<TModel, TUnderlyingMetadata, TMetadata>
    {
        private readonly Motiv.SpecBase<TModel, TUnderlyingMetadata> _spec__parameter;
        private readonly System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> _higherOrderPredicate__parameter;
        private readonly System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> _causeSelector__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> _whenTrue__parameter;
        private readonly System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> _whenFalse__parameter;
        public Step_32__Motiv_Spec(in Motiv.SpecBase<TModel, TUnderlyingMetadata> spec, in System.Func<System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, bool> higherOrderPredicate, in System.Func<bool, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>, System.Collections.Generic.IEnumerable<Motiv.HigherOrderProposition.BooleanResult<TModel, TUnderlyingMetadata>>> causeSelector, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenTrue, in System.Func<Motiv.HigherOrderProposition.HigherOrderBooleanResultEvaluation<TModel, TUnderlyingMetadata>, System.Collections.Generic.IEnumerable<TMetadata>> whenFalse)
        {
            _spec__parameter = spec;
            _higherOrderPredicate__parameter = higherOrderPredicate;
            _causeSelector__parameter = causeSelector;
            _whenTrue__parameter = whenTrue;
            _whenFalse__parameter = whenFalse;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TMetadata, TUnderlyingMetadata> Create()
        {
            return new MultiMetadataFromSpecHigherOrderPropositionFactory<TModel, TMetadata, TUnderlyingMetadata>(_spec__parameter, _higherOrderPredicate__parameter, _causeSelector__parameter, _whenTrue__parameter, _whenFalse__parameter);
        }
    }
}