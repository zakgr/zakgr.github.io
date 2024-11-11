namespace deflix.monolithic.api.Helpers
{
    public static class SqlQueries
    {
        // Users Queries
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

        // Movies Queries
        public static string GetAllMoviesQuery() =>
        "SELECT * FROM Movies";

        public static string GetMovieByIdQuery() =>
            "SELECT * FROM Movies WHERE Id = @Id";

        public static string InsertMovieQuery() =>
            "INSERT INTO Movies (Title, Description, Poster, Genre, YoutubeKey, UsersRating, UsersComment, CriticsRating, PlanType) " +
            "VALUES (@Title, @Description, @Poster, @Genre, @YoutubeKey, @UsersRating, @UsersComment, @CriticsRating, @PlanType)";

        public static string GetMoviesByGenreQuery() =>
            "SELECT * FROM Movies WHERE Genre = @Genre";

        // Recommendations Queries
        public static string GetRecommendationsForUserQuery() =>
            @"
            SELECT m.Id AS MovieId, m.Title, m.Description, m.Poster, m.Genre, m.UsersRating, m.PlanType,
                   'Based on your favorites' AS Reason  -- You can modify the reason logic as needed
            FROM Movies m
            WHERE m.Genre IN (
                SELECT DISTINCT Genre
                FROM Movies
                JOIN Favorites f ON f.MovieId = Movies.Id
                WHERE f.UserId = @UserId
            )
            AND m.Id NOT IN (
                SELECT MovieId FROM Favorites WHERE UserId = @UserId
            )
            ORDER BY m.UsersRating DESC
            ";

        // Favorites Queries
        public static string GetFavoritesForUserQuery() =>
            @"
            SELECT m.Id AS MovieId, m.Title, m.Description, m.Poster, m.Genre, m.UsersRating, m.PlanType
            FROM Favorites f
            JOIN Movies m ON f.MovieId = m.Id
            WHERE f.UserId = @UserId
            ";

        public static string InsertFavoriteQuery() =>
            "INSERT INTO Favorites (UserId, MovieId) VALUES (@UserId, @MovieId)";

        public static string DeleteFavoriteQuery() =>
            "DELETE FROM Favorites WHERE UserId = @UserId AND MovieId = @MovieId";

        // Watchlists Queries
        public static string GetWatchlistForUserQuery() =>
            @"
            SELECT m.Id AS MovieId, m.Title, m.Description, m.Poster, m.Genre, m.UsersRating, m.PlanType
            FROM Watchlist w
            JOIN Movies m ON w.MovieId = m.Id
            WHERE w.UserId = @UserId
            ";

        public static string InsertWatchlistItemQuery() =>
            "INSERT INTO Watchlist (UserId, MovieId) VALUES (@UserId, @MovieId)";

        public static string DeleteWatchlistItemQuery() =>
            "DELETE FROM Watchlist WHERE UserId = @UserId AND MovieId = @MovieId";

        // Subscriptions Queries
        public static string GetAllSubscriptionsQuery() =>
            "SELECT * FROM Subscriptions";

        public static string GetUserSubscriptionQuery() =>
            @"
            SELECT s.Id AS SubscriptionId, s.Name, s.Description, s.Price, us.ExpirationDate
            FROM UserSubscriptions us
            JOIN Subscriptions s ON us.SubscriptionId = s.Id
            WHERE us.UserId = @UserId
            ";

        public static string InsertOrUpdateUserSubscriptionQuery() =>
            @"
            MERGE INTO UserSubscriptions AS target
            USING (SELECT @UserId AS UserId, @SubscriptionId AS SubscriptionId) AS source
            ON target.UserId = source.UserId
            WHEN MATCHED THEN
                UPDATE SET SubscriptionId = source.SubscriptionId, ExpirationDate = @ExpirationDate
            WHEN NOT MATCHED THEN
                INSERT (UserId, SubscriptionId, ExpirationDate)
                VALUES (@UserId, @SubscriptionId, @ExpirationDate);
            ";

    }

}
