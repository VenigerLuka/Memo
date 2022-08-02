namespace MemoProject.Models.Contracts
{
    public interface IResult<T>
    {
        string Message { get; set; }
        bool Succedded { get; set; }
        T Value { get; set; }
    }
}