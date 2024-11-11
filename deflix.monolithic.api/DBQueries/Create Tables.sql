CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,  -- Consider a larger size for hashed passwords
    Email NVARCHAR(255) NOT NULL UNIQUE,
    SubscriptionType NVARCHAR(50) NOT NULL,
    ExpirationDate DATETIME NOT NULL,
    PaymentMethod NVARCHAR(50) NULL
);

CREATE TABLE SessionTokens (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    Token NVARCHAR(255) NOT NULL UNIQUE,
    CreatedAt DATETIME NOT NULL
);

CREATE TABLE Movies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Poster NVARCHAR(255) NULL,  -- URL or path to the movie poster
    Genre NVARCHAR(50) NULL,
    YoutubeKey NVARCHAR(50) NULL,  -- Key to identify the related YouTube video
    UsersRating FLOAT DEFAULT 0,  -- Average user rating
    UsersComment NVARCHAR(MAX) NULL,
    CriticsRating FLOAT DEFAULT 0,
    PlanType NVARCHAR(50) NOT NULL  -- e.g., "Free", "Basic", "Premium"
);
