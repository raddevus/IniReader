class IniFileReader
{
    private string FileName {get;set;}

    private Dictionary<string, Dictionary<string, string>> m_Sections = new Dictionary<string, Dictionary<string, string>>();

    public IniFileReader(string fileName)
    {
        FileName = fileName;
        ParseFile();
    }

    public void DisplayAllSections(){
        
        foreach (string sectionKey in m_Sections.Keys){
            Console.WriteLine($"[{sectionKey}]");
            Dictionary<string,string> keyValuePairs = null;
            m_Sections.TryGetValue(sectionKey, out keyValuePairs);
            Console.WriteLine($"Values in section: {keyValuePairs.Count}");
            foreach (string k in keyValuePairs.Keys){
                 string value = null;
                 keyValuePairs.TryGetValue(k,out value);
                 Console.WriteLine($"{k} : {value}");
            }
            Console.WriteLine();
        }
    }
    private string ParseSectionName(string Line)
    {
        if (!Line.StartsWith("[")) return null;
        if (!Line.EndsWith("]")) return null;
        if (Line.Length < 3) return null;
        return Line.Substring(1, Line.Length - 2);
    }

    private bool ParseKeyValuePair(string Line, ref string Key, ref string Value)
    {
        int i;
        if ((i = Line.IndexOf('=')) <= 0) return false;
        
        int j = Line.Length - i - 1;
        Key = Line.Substring(0, i).Trim();
        if (Key.Length <= 0) return false;

        Value = (j > 0) ? (Line.Substring(i + 1, j).Trim()) : ("");
        return true;
    }

    public string GetValue(string SectionName, string Key, string DefaultValue="")
    {
        // *** Check if the section exists ***
        Dictionary<string, string> Section;
        if (!m_Sections.TryGetValue(SectionName, out Section)) return DefaultValue;

        // *** Check if the key exists ***
        string Value;
        if (!Section.TryGetValue(Key, out Value)) return DefaultValue;
    
        // *** Return the found value ***
        return Value;
    }

    public void ParseFile(){
        StreamReader sr = null;
        try
        {
            // *** Clear local cache ***
            m_Sections.Clear();

            // *** Open the INI file ***
            try
            {
                sr = new StreamReader(FileName);
            }
            catch (FileNotFoundException)
            {
                return;
            }

            Dictionary<string, string> CurrentSection = null;
            string s;
                string SectionName;
                string Key = null;
                string Value = null;
            while ((s = sr.ReadLine()) != null)
            {
                s = s.Trim();

                    SectionName = ParseSectionName(s);
                    if (SectionName != null)
                {
                    // *** Only first occurrence of a section is loaded - duplicates ignored***
                    if (m_Sections.ContainsKey(SectionName))
                    {
                        CurrentSection = null;
                    }
                    else
                    {
                        CurrentSection = new Dictionary<string, string>();
                            m_Sections.Add(SectionName, CurrentSection);
                    }
                }
                else if (CurrentSection != null)
                {
                        // *** Check for key+value pair ***
                        if (ParseKeyValuePair(s, ref Key, ref Value))
                        {
                            // *** Only first occurrence of a key is loaded - duplicates ignored ***
                            if (!CurrentSection.ContainsKey(Key))
                            {
                                CurrentSection.Add(Key, Value);
                            }
                        }
                }
            }
        }
        finally
        {
            if (sr != null) sr.Close();
            sr = null;
        }
    }
}