namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}