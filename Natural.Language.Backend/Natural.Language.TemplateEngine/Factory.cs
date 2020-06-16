using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Language.TemplateEngine
{
    public class Factory
    {
        public IPopulateTemplate PopulateTemplate(string ActionType)
        {
            switch (ActionType)
            {
                case "GenerateProject":
                    return new ProjectEXETemplatePopulator();
                case "GenerateFunction":
                    return new FunctionTemplatePopulator();
                case "GenerateClass":
                    return new ClassTemplatePopulator();
                case "GenerateSQLCommand":
                    return new ClassTemplatePopulator();
                case "GenerateProperty":
                    return new PropertyTemplatePopulator();
                case "GenerateIntent":
                    return new IntentPopulator();
                default:
                    return new DefaultTemplate();
            }
        }

    }
}
