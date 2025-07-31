namespace ENCollect.Dyna.Seed;
/// <summary>
/// A small helper class to represent some “scope of work” 
/// that a Recommender can handle, identified by a City or other details.
/// </summary>
public class ScopeOfWork
{
    public string City { get; set; } = String.Empty;
    public string WorkDescription { get; set; } = String.Empty;
}