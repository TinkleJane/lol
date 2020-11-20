﻿namespace ShaunaVayne.Infrastructure.Security
{
    public interface IPasswordHasher
    {
        byte[] Hash(string password, byte[] salt);
    }
}