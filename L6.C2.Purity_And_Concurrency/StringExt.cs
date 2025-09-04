namespace L6.C2.Purity_And_Concurrency;

public static class StringExt
{
    public static string ToSentenceCase(this string s) // pure
        => s.ToUpper()[0] + s.ToLower().Substring(1);
}