using L6.C2.Purity_And_Concurrency;

class ListFormatter
{
    int counter; // global state mutations
    string PrependCounter(string s) => $"{++counter}. {s}";  // impure
    
    public List<string> Format(List<string> list) //pure + impure
        => list
            .Select(StringExt.ToSentenceCase)
            .Select(PrependCounter)          
            .ToList();
}