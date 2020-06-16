using Natural.Language.TemplateEngine;

using System;
using System.Collections.Generic;

namespace Natural.Language.Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                var line = Console.ReadLine();
                // Example of creating a new EXE based project
                if (line == "1")
                {
                    ProjectEXETemplatePopulator populator = new ProjectEXETemplatePopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                    keyValuePairs.Add(populator.ProjectName, "Demo.Project");
                    Console.WriteLine(populator.PopulateTemplate(string.Empty, keyValuePairs, 0));
                }
                // Adding function
                if (line == "2")
                {
                    FunctionTemplatePopulator populator = new FunctionTemplatePopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    { populator.ClassName , "Test" },
                    { populator.Name , "HelloWorld" },
                    { populator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };

                    Console.WriteLine(populator.PopulateTemplate("FunctionTemplate", keyValuePairs, -1));
                }
                // Adding class
                if (line == "3")
                {
                    ClassTemplatePopulator populator = new ClassTemplatePopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    { populator.ProjectName , "Demo.Project"},
                    { populator.ClassName , "Employee" },
                    { populator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };

                    Console.WriteLine(populator.PopulateTemplate("ClassTemplate", keyValuePairs, -1));
                }
                // Adding Property to class
                if (line == "4")
                {
                    FunctionTemplatePopulator populator = new FunctionTemplatePopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    { populator.ProjectName , "Demo.Project"},
                    { populator.ClassName , "Test" },
                    { populator.ReturnType, "string" },
                    { populator.Name , "HelloWorld" },
                    { populator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };

                    Console.WriteLine(populator.PopulateTemplate("PropertyTemplate", keyValuePairs, -1));
                }
                // Support storage
                if (line == "5")
                {
                    FunctionTemplatePopulator functionPopulator = new FunctionTemplatePopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    { functionPopulator.ProjectName , "Demo.Project" },
                    { functionPopulator.ClassName , "Employee" },
                    { functionPopulator.Name , "HelloWorld" },
                    { functionPopulator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };

                    Console.WriteLine(functionPopulator.PopulateTemplate("PersistTemplate", keyValuePairs, -1));

                    // Example of creating a new EXE based project
                    IntentPopulator populator = new IntentPopulator();
                    keyValuePairs.Clear();
                    keyValuePairs = new Dictionary<string, string>()
                {
                    { populator.VariableName, "EmployeeList" },
                    { populator.ProjectName , "Demo.Project" },
                    { populator.ClassName , "Employee" },
                    { populator.Name , "HelloWorld" },
                    { populator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };
                    Console.WriteLine(populator.PopulateTemplate("FetchObjectFromDatabaseTemplate", keyValuePairs, -1));
                }
                // Reading from Console
                if (line == "6")
                {
                    FunctionTemplatePopulator functionPopulator = new FunctionTemplatePopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    {"@VARIABLE_NAME@", "EmployeeList" },
                    { functionPopulator.ProjectName , "Demo.Project" },
                    { functionPopulator.ClassName , "Program" },
                    { functionPopulator.Name , "HelloWorld" },
                    { functionPopulator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };

                    Console.WriteLine(functionPopulator.PopulateTemplate("FetchObjectFromConsoleFunctionTemplate", keyValuePairs, -1));

                    // Example of creating a new EXE based project
                    IntentPopulator populator = new IntentPopulator();
                    keyValuePairs.Clear();
                    keyValuePairs = new Dictionary<string, string>()
                {
                    {"@VARIABLE_NAME@", "EmployeeList" },
                    { functionPopulator.ProjectName , "Demo.Project" },
                    { populator.ClassName , "Employee" },
                    { populator.Name , "HelloWorld" },
                    { populator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };
                    Console.WriteLine(populator.PopulateTemplate("FetchObjectFromConsoleIntentTemplate", keyValuePairs, -1));
                }
                // Display on console
                if (line == "7")
                {
                    FunctionTemplatePopulator functionPopulator = new FunctionTemplatePopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    {"@VARIABLE_NAME@", "EmployeeList" },
                    { functionPopulator.ProjectName , "Demo.Project" },
                    { functionPopulator.ClassName , "Program" },
                    { functionPopulator.Name , "HelloWorld" },
                    { functionPopulator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };

                    Console.WriteLine(functionPopulator.PopulateTemplate("DisplayObjectContentFunctionTemplate", keyValuePairs, -1));

                    // Example of creating a new EXE based project
                    IntentPopulator populator = new IntentPopulator();
                    keyValuePairs.Clear();
                    keyValuePairs = new Dictionary<string, string>()
                {
                    {"@VARIABLE_NAME@", "EmployeeList" },
                    { functionPopulator.ProjectName , "Demo.Project" },
                    { populator.ClassName , "Program" },
                    { populator.Name , "HelloWorld" },
                    { populator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };
                    Console.WriteLine(populator.PopulateTemplate("DisplayObjectContentIntentTemplate", keyValuePairs, -1));
                }
                // Storing to DB
                if (line == "8")
                {
                    // Example of creating a new EXE based project
                    IntentPopulator populator = new IntentPopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    { populator.ClassName , "Program" },
                    { populator.Name , "HelloWorld" },
                    { populator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };
                    Console.WriteLine(populator.PopulateTemplate("DisplayObjectContentTemplate", keyValuePairs, -1));
                }

                // Adding Filter
                if (line == "9")
                {
                    // Add filter logic 
                    IntentPopulator populator = new IntentPopulator();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    { populator.Name , "HelloWorld" },
                    { populator.ClassName , "Employee" },
                    { populator.ProjectName , "Demo.Project" },
                    { populator.PropertyName , "EmployeeName" },
                    { populator.PropertyValue , "Abk" },
                    { populator.SolutionPath, @"C:\AdaptivDevelopment\Projects\RuntimeTError\NaturalLanguageProgramming\Natural.Language.Backend\Natural.Language.Backend\" }
                };
                    System.Diagnostics.Debugger.Launch();
                    Console.WriteLine(populator.PopulateTemplate("LINQWhereIntentTemplate", keyValuePairs, -1));
                }

                if (line == "-1")
                {
                    break;
                }
            } while (true);
        }
    }
}
