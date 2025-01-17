namespace MkApi.Domain.ValueObjects;

public class CatFact
{
    public required string Fact { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is CatFact cat)
        {
            return Fact == cat.Fact;
        }
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Fact);
}