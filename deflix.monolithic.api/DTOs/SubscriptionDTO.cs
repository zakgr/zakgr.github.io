﻿namespace deflix.monolithic.api.DTOs
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class UserSubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

}
