using System.Collections.Generic;

namespace Natural.Language.TemplateEngine
{
    public interface IPopulateTemplate
    {
        /// <summary>
        /// The template population should be implemented in the method
        /// </summary>
        /// <param name="templateName">The template name</param>
        /// <param name="replacementValues">List of replacement values</param>
        /// <param name="injectAtLine">The line at which the injection should be set</param>
        /// <returns></returns>
        string PopulateTemplate(string templateName, Dictionary<string,string> replacementValues, int injectAtLine);
    }
}
