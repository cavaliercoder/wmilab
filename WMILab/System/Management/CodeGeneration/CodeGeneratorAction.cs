namespace System.Management.CodeGeneration
{
    using System;
    using System.Drawing;

    public class CodeGeneratorAction
    {
        public String Name { get; set; }
        
        public String ToolTip { get; set; }

        public Image Image { get; set; }

        public Object Tag { get; set; }
    }
}
