namespace Domain.Exceptions;

public sealed class ConflictDomainException(string message) : DomainException(message)
{

}