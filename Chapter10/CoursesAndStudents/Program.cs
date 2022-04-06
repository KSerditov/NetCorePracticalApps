using Microsoft.EntityFrameworkCore;
using static System.Console;
using CoursesAndStudents;

using (Academy a = new())
{
    bool deleted = await a.Database.EnsureDeletedAsync();
    WriteLine(deleted);
    bool created = await a.Database.EnsureCreatedAsync();
    WriteLine(created);
    WriteLine(a.Database.GenerateCreateScript());
}
