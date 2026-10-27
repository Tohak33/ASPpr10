namespace para.Models;

public enum StoreError { NotFound, Validation, Conflict }

public class StoreException(StoreError error, string message) : Exception(message)
{
    public StoreError Error { get; } = error;
}
