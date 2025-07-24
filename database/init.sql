-- This file will be automatically executed when the database container starts

CREATE TABLE IF NOT EXISTS "Tasks" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "Title" VARCHAR(255) NOT NULL,
    "Description" TEXT,
    "IsCompleted" BOOLEAN DEFAULT FALSE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

