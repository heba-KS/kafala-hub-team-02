namespace DalilAcademy.Core.Exceptions;

// Lecture 6: thrown when a requested resource does not exist → 404
public class NotFoundException(string message) : Exception(message);
