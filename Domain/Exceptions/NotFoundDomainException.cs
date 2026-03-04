namespace Domain.Exceptions;

public sealed class NotFoundDomainException(string message) : DomainException(message) 
{ 

}
