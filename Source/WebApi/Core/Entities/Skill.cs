namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    using Ardalis.GuardClauses;

    public class Skill
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                Guard.Against.Null(value, nameof(Name));
                _name = value.ToLower();
            }
        }

        public Skill(string name)
        {
            Name = name;
        }
    }
}
