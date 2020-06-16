namespace Natural.Language.TemplateEngine
{
    public static class Constants
    {
        public const string TemplatePath = @"C:\Alexa\netcoreapp3.1\Templates";
        public const string ProgramTemplate = "ProgramFileTemplate.xml";
        public const string AssemblyTemplate = "AssemblyFileTemplate.xml";
        public const string ProjectTemplate = "ProjectEXETemplate.xml";
        public const string PackageConfig = "packages.config";
        public const string LINQWhereTemplate = "PersistTemplate.xml";
        public const string LINQHolder = "LINQHolder.xml";
        public const string APPConfig = "App.config";



        public const string ProgramFile = "Program.cs";
        public const string AssemblyFile = "AssemblyInfo.cs";
        public const string ProjectExtension = ".csproj";

        // Template tags
        public const string DataTagStart = "<Data>";
        public const string DataTagEnd = "</Data>";

        // Dictionary keys
        public const string KeyProjectGuiID = "@PROJECTGUID@";
        public const string KeyProjectName = "@PROJECT_NAME@";
    }
}
