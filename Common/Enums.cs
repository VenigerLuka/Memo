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

        public enum AuditEventEnum
        {
            ChangeDefaultSettings = 100,
            ChangeUserSettings = 101,
            DefaultSettings = 111,
            MemoCreated = 200,
            MemoEdited = 201,
            MemoDeleted = 202
        }



    }
}
