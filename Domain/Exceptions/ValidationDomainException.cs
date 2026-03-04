namespace Domain.Exceptions;

public sealed class ValidationDomainException(string message) : DomainException(message)
{

}
