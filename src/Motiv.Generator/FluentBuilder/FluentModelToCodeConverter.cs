namespace Motiv.Generator.FluentBuilder;

public static class CSharpFluentBuilderEmitter
{
    public string Emit(FluentBuilderContext context)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"namespace {context.NameSpace}");
        builder.AppendLine("{");
        builder.AppendLine($"    public static class {context.RootTypeName}FluentBuilder");
        builder.AppendLine("    {");
        builder.AppendLine($"        public static {context.RootTypeName} Create() => new();");
        builder.AppendLine();
        builder.AppendLine($"        public static {context.RootTypeName} With{context.Constructor.Parameters[0].Name.Capitalize()}(this {context.RootTypeName} instance, {context.Constructor.Parameters[0].Type} {context.Constructor.Parameters[0].Name})");
        builder.AppendLine("        {");
        builder.AppendLine($"            instance.{context.Constructor.Parameters[0].Name} = {context.Constructor.Parameters[0].Name};");
        builder.AppendLine("            return instance;");
        builder.AppendLine("        }");
        builder.AppendLine();
        builder.AppendLine($"        public static {context.RootTypeName} With{context.Constructor.Parameters[1].Name.Capitalize()}(this {context.RootTypeName} instance, {context.Constructor.Parameters[1].Type} {context.Constructor.Parameters[1].Name})");
        builder.AppendLine("        {");
        builder.AppendLine($"            instance.{context.Constructor.Parameters[1].Name} = {context.Constructor.Parameters[1].Name};");
        builder.AppendLine("            return instance;");
        builder.AppendLine("        }");
        builder.AppendLine("    }");
        builder.AppendLine("}");

        return builder.ToString();
    }
}
