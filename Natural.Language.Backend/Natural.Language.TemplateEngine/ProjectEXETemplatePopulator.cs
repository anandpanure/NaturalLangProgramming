using System;
using System.Collections.Generic;
using System.IO;

namespace Natural.Language.TemplateEngine
{
    public class ProjectEXETemplatePopulator : TemplatePopulator, IPopulateTemplate
    {
        public string PopulateTemplate(string templateName, Dictionary<string, string> replacementValues, int injectAtLine)
        {
            // A choice that will be called for creating a new project
            var projestName = replacementValues[Constants.KeyProjectName];
            var guid = Guid.NewGuid().ToString();
            replacementValues.Add(Constants.KeyProjectGuiID, guid);

            var projectPath = Path.Combine(@"C:\AdaptivDevelopment\RuntimeTerror", projestName);
            var projectAssemblyPath = Path.Combine(projectPath, "Properties");

            // Create project folders
            CreateFolders(projectPath, projectAssemblyPath);

            // Copy the template files
            CopyProjectFiles(projectPath, projectAssemblyPath);

            // Rename files
            RenameProjectFiles(projectPath, projectAssemblyPath, projestName);

            // Remove the template tags
            RemoveTemplateTags(projectPath, projectAssemblyPath, projestName);

            // Remove the template tags
            UpdateKeyValues(projectPath, projectAssemblyPath, projestName, replacementValues);

            return "success";
        }

        private static void CreateFolders(string projectPath, string projectAssemblyPath)
        {
            Directory.CreateDirectory(projectPath);
            Directory.CreateDirectory(projectAssemblyPath);
        }

        private static void CopyProjectFiles(string projectPath, string projectAssemblyPath)
        {
            Console.WriteLine(Path.Combine(Constants.TemplatePath, Constants.ProjectTemplate));

            // Copy Package File
            File.Copy(Path.Combine(Constants.TemplatePath, Constants.PackageConfig), Path.Combine(projectPath, Constants.PackageConfig));

            // Copy Project File
            File.Copy(Path.Combine(Constants.TemplatePath, Constants.ProjectTemplate), Path.Combine(projectPath, Constants.ProjectTemplate));

            // Copy Program Class file
            File.Copy(Path.Combine(Constants.TemplatePath, Constants.ProgramTemplate), Path.Combine(projectPath, Constants.ProgramTemplate));

            // Copy the App File
            File.Copy(Path.Combine(Constants.TemplatePath, Constants.APPConfig), Path.Combine(projectPath, Constants.APPConfig));

            // Copy Assembly File
            File.Copy(Path.Combine(Constants.TemplatePath, Constants.AssemblyTemplate), Path.Combine(projectAssemblyPath, Constants.AssemblyTemplate));
        }

        private static void RenameProjectFiles(string projectPath, string projectAssemblyPath, string projectName)
        {
            File.Move(Path.Combine(projectPath, Constants.ProjectTemplate),
                Path.Combine(projectPath, $"{projectName}{Constants.ProjectExtension}"));

            // Copy Program Class file
            File.Move(Path.Combine(projectPath, Constants.ProgramTemplate),
                Path.Combine(projectPath, Constants.ProgramFile));

            // Copy Program Class file
            File.Move(Path.Combine(projectPath, Constants.APPConfig),
                Path.Combine(projectPath, Constants.APPConfig));

            // Copy Assembly File
            File.Move(Path.Combine(projectAssemblyPath, Constants.AssemblyTemplate),
                Path.Combine(projectAssemblyPath, Constants.AssemblyFile));
        }

        private static void RemoveTemplateTags(string projectPath, string projectAssemblyPath, string projectName)
        {
            Path.Combine(projectPath, $"{projectName}{Constants.ProjectExtension}").RemoveDataTag();
            Path.Combine(projectPath, Constants.ProgramFile).RemoveDataTag();
            Path.Combine(projectAssemblyPath, Constants.AssemblyFile).RemoveDataTag();
        }

        private static void UpdateKeyValues(string projectPath, string projectAssemblyPath, string projectName, Dictionary<string, string> replacementValues)
        {
            Path.Combine(projectPath, $"{projectName}{Constants.ProjectExtension}").UpdateKeyValues(replacementValues);
            Path.Combine(projectPath, Constants.ProgramFile).UpdateKeyValues(replacementValues);
            Path.Combine(projectAssemblyPath, Constants.AssemblyFile).UpdateKeyValues(replacementValues);
        }
    }
}
