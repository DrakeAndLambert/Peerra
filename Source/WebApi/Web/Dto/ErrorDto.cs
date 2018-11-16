using DrakeLambert.Peerra.WebApi.SharedKernel.Dto;

namespace DrakeLambert.Peerra.WebApi.Web.Dto
{
    public class ErrorDto
    {
        public string Error { get; set; }

        public ErrorDto(string error)
        {
            Error = error;
        }

        public ErrorDto(Result result) : this(result.Error)
        { }
    }
}
