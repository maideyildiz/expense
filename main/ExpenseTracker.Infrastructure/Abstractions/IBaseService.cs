using ExpenseTracker.Core.Models;
using ExpenseTracker.Core.Repositories.Abstractions;
namespace ExpenseTracker.Infrastructure.Abstractions;

public interface IBaseService<T> : IBaseRepository<T> where T : Base
{
}

