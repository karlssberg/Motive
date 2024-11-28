using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Motiv.Generator.Attributes;
using Motiv.Generator.FluentBuilder.FluentModel;

namespace Motiv.Generator.FluentBuilder;

[Generator]
public class FluentBuilderGenerator : IIncrementalGenerator
{
    private readonly FluentModelFactory _fluentModelFactory = new();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
// #if DEBUG
//         if (!Debugger.IsAttached)
//         {
//              Debugger.Launch();
//         }
// #endif
        // Step 1: Find all FluentConstructors
        var handlerDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                nameof(GenerateFluentBuilderAttribute),
                predicate: (node, _) => node is ConstructorDeclarationSyntax,
                transform: _fluentModelFactory.CreateIntermediateRepresentation)
            .Where(handler => handler is not null);

        // Step 2: Collect and consolidate all FluentConstructors
        var consolidated = handlerDeclarations
            .Collect()
            .Select((fluentConstructors, _) => ConsolidateFluentConstructors(fluentConstructors))
            .WithComparer(new ConsolidatedFluentConstructorsComparer());

        // Step 3: Generate based on consolidated view
        context.RegisterSourceOutput(consolidated, GenerateDispatcher);
    }

    private static ConsolidatedFluentConstructors ConsolidateFluentConstructors(
        ImmutableArray<FluentBuilderContext?> fluentConstructors)
    {
        var builder = ImmutableDictionary.CreateBuilder<string, ImmutableArray<FluentBuilderContext>>();

        // Group FluentConstructors by message type
        var groups = fluentConstructors
            .OfType<FluentBuilderContext>()
            .GroupBy(constructor => (constructor.NameSpace));

        foreach (var group in groups)
        {
            builder.Add(group.Key, [..group]);
        }

        return new ConsolidatedFluentConstructors(builder.ToImmutable());
    }

    private void GenerateDispatcher(SourceProductionContext context,
        ConsolidatedFluentConstructors consolidated)
    {
        // Generate the dispatcher ref struct that uses the consolidated data
        var source = $$"""

                       using System;

                       public readonly struct {{}}
                       {
                           private readonly object _message;

                           public MessageDispatcher(object message)
                           {
                               _message = message;
                           }

                           public void Dispatch()
                           {
                               switch (_message)
                               {
                                   {{string.Join("\n", GenerateCases(consolidated))}}
                                   default:
                                       throw new ArgumentException("Unknown message type");
                               }
                           }
                       }
                       """;

        context.AddSource("MessageDispatcher.g.cs", source);
    }

    private string CreateFluentStep(
        string stepName,
        string priorStepName,
        IEnumerable<FluentMethod> fluentMethods)
    {
        var sb = new StringBuilder(
          $$"""
            public struct {{stepName}}
            {
                private readonly {{priorStepName}} _previousStep;

                public {{stepName}}(ref {{priorStepName}} previousStep)
                {
                    _previousStep = previousStep;
                }
            """);

        foreach (var fluentMethod in fluentMethods)
        {
            sb.Append(
              $$"""

                """);
        }


        return sb.Append("}").ToString();
    }

//     private string[] GenerateCases(ConsolidatedFluentConstructors consolidated)
//     {
//         var cases = new List<string>();
//         foreach (var (messageType, FluentConstructors) in consolidated.FluentConstructorsByMessageType)
//         {
//             cases.Add($"""
//
//                                    case {messageType} msg:
//                                        {string.Join("\n                ",
//                                            FluentConstructors.Select(h => $"new {h.HandlerTypeName}().{h.MethodName}(msg);"))}
//                                        break;
//                        """);
//         }
//         return cases.ToArray();
//     }
}
public class ConsolidatedFluentConstructorsComparer : IEqualityComparer<ConsolidatedFluentConstructors>
{
    public bool Equals(ConsolidatedFluentConstructors x, ConsolidatedFluentConstructors y)
    {
        return x.FluentBuilderContexts.Equals(y.FluentBuilderContexts);
    }

    public int GetHashCode(ConsolidatedFluentConstructors obj)
    {
        return obj.FluentBuilderContexts.GetHashCode();
    }
}
