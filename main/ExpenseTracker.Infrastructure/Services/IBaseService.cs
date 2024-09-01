using ExpenseTracker.Core.Models;
using ExpenseTracker.Core.Repositories;
namespace ExpenseTracker.Infrastructure.Services;

public interface IBaseService<T> : IBaseRepository<T> where T : Base
{
}

