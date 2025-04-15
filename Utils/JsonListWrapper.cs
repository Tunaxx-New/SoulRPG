using System;
using System.Collections.Generic;

[Serializable]
public class JsonListWrapper
{
    public List<JsonTemplate> jsonTemplates;

    public JsonListWrapper(List<JsonTemplate> templates)
    {
        jsonTemplates = templates;
    }
}