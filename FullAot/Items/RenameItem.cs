namespace FullAot.Items;

public class RenameItem
{
    public string InName { get; }
    public string OutName { get; }

    public RenameItem(string @in, string @out)
    {
        InName = @in;
        OutName = @out;
    }

    //So schreiben wir einfach einen Tuple aus strings und bekommen das Objekt 'Gratis'
    public static implicit operator RenameItem((string column, string alias) pair) =>
        new(pair.column, pair.alias);
    
}