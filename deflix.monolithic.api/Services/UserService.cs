namespace deflix.monolithic.api.Services
{
    using System;
    using System.Data.SqlClient;
    using deflix.monolithic.api.DTOs;
    using deflix.monolithic.api.Helpers;
    using deflix.monolithic.api.Interfaces;
    using deflix.monolithic.api.Types;

    public class UserService : IUserService
    {
        private readonly IDatabaseService _databaseService;

        public UserService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public bool Register(UserRegisterDto userDto)
        {
            var existingUserCount = _databaseService.ExecuteScalar<int>(
                SqlQueries.GetCheckUserExistsQuery(),
                new SqlParameter("@Username", userDto.Username),
                new SqlParameter("@Email", userDto.Email)
            );

            if (existingUserCount > 0)
            {
                return false;
            }

            _databaseService.ExecuteNonQuery(
                SqlQueries.GetInsertUserQuery(),
                new SqlParameter("@Username", userDto.Username),
                new SqlParameter("@Password", userDto.Password),  // Hash passwords in production
                new SqlParameter("@Email", userDto.Email),
                new SqlParameter("@SubscriptionType", "Free"),
                new SqlParameter("@ExpirationDate", DateTime.UtcNow.AddMonths(1)),
                new SqlParameter("@PaymentMethod", "None")
            );

            return true;
        }

        public User? Authenticate(string username, string password)
        {
            using var reader = _databaseService.ExecuteReader(
                SqlQueries.GetAuthenticateUserQuery(),
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password));
            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    Username = reader["Username"].ToString(),
                    Password = reader["Password"].ToString(),  // Avoid returning passwords in real apps
                    Email = reader["Email"].ToString(),
                    SubscriptionType = reader["SubscriptionType"].ToString(),
                    ExpirationDate = (DateTime)reader["ExpirationDate"],
                    PaymentMethod = reader["PaymentMethod"].ToString()
                };
            }

            return null;
        }

        public UserDto? GetUserProfile(int userId)
        {
            using var reader = _databaseService.ExecuteReader(
                SqlQueries.GetUserByIdQuery(),
                new SqlParameter("@UserId", userId));
            if (reader.Read())
            {
                return new UserDto
                {
                    Id = (int)reader["Id"],
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    SubscriptionType = reader["SubscriptionType"].ToString(),
                    ExpirationDate = (DateTime)reader["ExpirationDate"],
                    PaymentMethod = reader["PaymentMethod"].ToString()
                };
            }

            return null;
        }

        public bool UpdateUserProfile(int userId, UserProfileUpdateDto profileUpdateDto)
        {
            int rowsAffected = _databaseService.ExecuteNonQuery(
                SqlQueries.GetUpdateUserProfileQuery(),
                new SqlParameter("@Email", profileUpdateDto.Email ?? (object)DBNull.Value),
                new SqlParameter("@SubscriptionType", profileUpdateDto.SubscriptionType ?? (object)DBNull.Value),
                new SqlParameter("@PaymentMethod", profileUpdateDto.PaymentMethod ?? (object)DBNull.Value),
                new SqlParameter("@UserId", userId)
            );

            return rowsAffected > 0;
        }

        public string CreateSessionToken(int userId)
        {
            var plainToken = $"{userId}:{Guid.NewGuid()}";
            var encodedToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainToken));

            _databaseService.ExecuteNonQuery(
                SqlQueries.GetInsertSessionTokenQuery(),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Token", encodedToken),
                new SqlParameter("@CreatedAt", DateTime.UtcNow)
            );

            return encodedToken;
        }

        public User? GetUserBySessionToken(string encodedToken)
        {
            using var reader = _databaseService.ExecuteReader(
                SqlQueries.GetSessionTokenQuery(),
                new SqlParameter("@Token", encodedToken));
            if (reader.Read())
            {
                int userId = (int)reader["UserId"];
                return GetUserById(userId);
            }

            return null;
        }

        private User? GetUserById(int userId)
        {
            using var reader = _databaseService.ExecuteReader(
                SqlQueries.GetUserByIdQuery(),
                new SqlParameter("@UserId", userId));
            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    SubscriptionType = reader["SubscriptionType"].ToString(),
                    ExpirationDate = (DateTime)reader["ExpirationDate"],
                    PaymentMethod = reader["PaymentMethod"].ToString()
                };
            }

            return null;
        }
    }

}
