namespace System.Management.CodeGeneration
{
    public interface ICodeGenerator
    {
        String Name { get; }

        String Language { get; }

        String FileExtension { get; }

        String GetScript(ManagementClass c, String query);
    }
}
