
namespace Natural.Language.TemplateEngine
{
    public enum TemplateEnum
    {
        ClassTemplate,
        FunctionTemplate,
        ProjectEXETemplate,
        PropertyTemplate,
        FetchObjectFromConsoleFunctionTemplate,
        FetchObjectFromConsoleIntentTemplate,
        DisplayObjectContentFunctionTemplate,
        DisplayObjectContentIntentTemplate,
        PersistTemplate,
        StoreToDbContentTemplate,
        LINQWhereIntentTemplate,
        FetchObjectFromDatabaseTemplate
    }

    public enum ActionType
    {
        GenerateProject,
        GenerateFunction,
        GenerateClass,
        GenerateSQLCommand,
        GenerateProperty,
        GenerateStorage,
        FetchDetails,
        GenerateIntent
    }
}
