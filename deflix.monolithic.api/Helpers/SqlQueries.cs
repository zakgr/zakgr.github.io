namespace deflix.monolithic.api.Helpers
{
    public static class SqlQueries
    {
        public static string GetCheckUserExistsQuery() =>
            "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";

        public static string GetInsertUserQuery() =>
            "INSERT INTO Users (Username, Password, Email, SubscriptionType, ExpirationDate, PaymentMethod) " +
            "VALUES (@Username, @Password, @Email, @SubscriptionType, @ExpirationDate, @PaymentMethod)";

        public static string GetAuthenticateUserQuery() =>
            "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

        public static string GetUserByIdQuery() =>
            "SELECT * FROM Users WHERE Id = @UserId";

        public static string GetUpdateUserProfileQuery() =>
            "UPDATE Users SET Email = @Email, SubscriptionType = @SubscriptionType, PaymentMethod = @PaymentMethod WHERE Id = @UserId";

        public static string GetInsertSessionTokenQuery() =>
            "INSERT INTO SessionTokens (UserId, Token, CreatedAt) VALUES (@UserId, @Token, @CreatedAt)";

        public static string GetSessionTokenQuery() =>
            "SELECT * FROM SessionTokens WHERE Token = @Token";
    }

}
