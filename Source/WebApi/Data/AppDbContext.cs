using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Data
{
    public class Name : DbContext
    {
        public Name(DbContextOptions<Name> options) : base(options)
        { }
    }
}