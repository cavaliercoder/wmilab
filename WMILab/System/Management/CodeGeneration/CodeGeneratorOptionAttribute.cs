namespace System.Management.CodeGeneration
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CodeGeneratorOptionAttribute : Attribute
    {
        public CodeGeneratorOptionAttribute(String name, String tooltip)
        {
            Name = name;
            Tooltip = tooltip;
        }

        public CodeGeneratorOptionAttribute(String name)
            : this(name, String.Empty)
        { }

        public String Name { get; set; }

        public String Tooltip { get; set; }
    }
}
