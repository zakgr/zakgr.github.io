using deflix.monolithic.api.DTOs;
using deflix.monolithic.api.Helpers;
using deflix.monolithic.api.Interfaces;
using System.Data.SqlClient;

namespace deflix.monolithic.api.Services
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IDatabaseService _databaseService;

        public SubscriptionsService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IEnumerable<SubscriptionDto> GetAllSubscriptions()
        {
            var subscriptions = new List<SubscriptionDto>();

            using (var reader = _databaseService.ExecuteReader(SqlQueries.GetAllSubscriptionsQuery()))
            {
                while (reader.Read())
                {
                    subscriptions.Add(new SubscriptionDto
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"]
                    });
                }
            }

            return subscriptions;
        }

        public UserSubscriptionDto GetUserSubscription(int userId)
        {
            using (var reader = _databaseService.ExecuteReader(
                SqlQueries.GetUserSubscriptionQuery(),
                new SqlParameter("@UserId", userId)))
            {
                if (reader.Read())
                {
                    return new UserSubscriptionDto
                    {
                        SubscriptionId = (int)reader["SubscriptionId"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"],
                        ExpirationDate = (DateTime)reader["ExpirationDate"]
                    };
                }
            }

            return null;
        }

        public void SubscribeUser(int userId, int subscriptionId)
        {
            _databaseService.ExecuteNonQuery(
                SqlQueries.InsertOrUpdateUserSubscriptionQuery(),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@SubscriptionId", subscriptionId),
                new SqlParameter("@ExpirationDate", DateTime.UtcNow.AddMonths(1))  // Example: 1 month subscription
            );
        }
    }
}
