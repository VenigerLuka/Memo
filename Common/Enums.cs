namespace MemoProject.Common
{
    public static class Enums
    {
        public enum Errors
        {
            SomethingWentWrong,
            WrongUserOrPassword,
            NotFound,
            Unauthorized

        }
        public enum StatusEnum
        {
            Active = 1,
            Suspended = 2,
            Deleted = 3
        }

        public enum Roles
        {
            Admin,
            Standard
        }


    }
}
