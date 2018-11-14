using DrakeLambert.Peerra.WebApi2.SharedKernel.Dto;

namespace DrakeLambert.Peerra.WebApi2.Web.Dto
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
