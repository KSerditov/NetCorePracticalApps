namespace Northwind.Mvc.Models;

public record HomeModelBindingViewModel
(
    Thing thing,
    bool HasErrors,
    IEnumerable<string> ValidationErrors
);
