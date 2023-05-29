using Lab10.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab10.API.DataAccess;

public class DataSeed
{
    public static async Task InitData(ApplicationContext context)
    {
        if (await context.Employees.AnyAsync())
        {
            return;
        }

        var employees = new List<Employee>
        {
            new()
            {
                FirstName = "FirstName 1",
                LastName = "LastName 1",
                Age = 18,
                Address = "Address 1"
            },
            new()
            {
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                Age = 22,
                Address = "Address 2"
            },
            new()
            {
                FirstName = "FirstName 3",
                LastName = "LastName 3",
                Age = 17,
                Address = "Address 3"
            }
        };

        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();
    }
}