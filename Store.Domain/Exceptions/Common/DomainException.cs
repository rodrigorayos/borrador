﻿namespace Store.Domain.Exceptions.Common;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}