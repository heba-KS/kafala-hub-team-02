namespace DalilAcademy.Core.Exceptions;

// Lecture 6: thrown when a request breaks a uniqueness rule → 409
public class ConflictException(string message) : Exception(message);
