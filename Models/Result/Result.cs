using MemoProject.Models.Contracts;

namespace MemoProject.Models.Result
{
    public class Result<T> : IResult<T>
    {
        public bool Succedded { get; set; }
        public T Value { get; set; }
        public string Message { get; set; }
    }

    public class NoValue
    {

    }
}
