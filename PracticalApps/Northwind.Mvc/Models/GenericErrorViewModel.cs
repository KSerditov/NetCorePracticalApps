namespace Northwind.Mvc.Models;

public record GenericErrorViewModel<T>
(
    T entity,
    bool HasErrors,
    IEnumerable<string> ValidationErrors
);
