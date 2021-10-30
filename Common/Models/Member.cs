namespace AL.LeagueRoster.Common;

public class Member
{
    public int ClubId { get; init; }
    public Guid MemberId { get; init; }
    public string? FirstName { get; init; }
    public char? Initial { get; init; }
    public string? LastName { get; init; }
    public CountryCode CountryCode { get; init; }
    public string? Address1 { get; init; }
    public string? Address2 { get; init; }
    public string? Locality { get; init; }
    public string? Region { get; init; }
    public string? PostalCode { get; init; }
    public string? EmailAddress { get; init; }
    public Reflector Reflector { get; init; }
    public bool IsAlcor { get; init; }
    public List<Role>? Roles { get; init; }
    public bool NoAdvertising { get; init; }
    public DateTime UpdatedOn { get; init; }
}