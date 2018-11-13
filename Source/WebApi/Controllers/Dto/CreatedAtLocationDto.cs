namespace DrakeLambert.Peerra.WebApi.Controllers.Dto
{
    /// <summary>
    /// A result containing the location of the created resource.
    /// </summary>
    public class CreatedAtLocationDto
    {
        /// <summary>
        /// The location of the created resource.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="location">The resource location.</param>
        public CreatedAtLocationDto(string location)
        {
            Location = location;
        }
    }

    /// <summary>
    /// A result containing the location of the created resource and the created resource.
    /// </summary>
    /// <typeparam name="T">The type of created resource.</typeparam>
    public class CreatedAtLocationDto<T> : CreatedAtLocationDto
    {
        /// <summary>
        /// The created resource.
        /// </summary>
        public T CreatedResource { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="location">The resource location.</param>
        /// <param name="createdResource">The resource.</param>
        public CreatedAtLocationDto(string location, T createdResource) : base(location)
        {
            CreatedResource = createdResource;
        }
    }
}
