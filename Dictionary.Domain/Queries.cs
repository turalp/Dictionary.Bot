namespace Dictionary.Domain
{
    public static class Queries
    {
        public const string InsertWord = "INSERT INTO Words ([Title], [Description]) VALUES ({0}, {1})";

        public const string UpdateWord = "UPDATE Words SET [Title] = {0}, [Description] = {1} WHERE [WordId] = {2}";

        public const string GetByWord = "SELECT [WordId], [Title], [Description] FROM Words WHERE [Title] = {0}";
    }
}
