namespace Products.Application.Exceptions;

public class NotFoundException(string entity, Guid id) : Exception($"the {entity} with {id} not found");
