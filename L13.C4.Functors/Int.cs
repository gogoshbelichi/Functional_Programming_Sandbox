using Option;

public static class Int
{
    public static Option<int> Parse(string s)
    {
        int result;
        return int.TryParse(s, out result)
            ? new Some<int>(result) : None.Default;
    }
}