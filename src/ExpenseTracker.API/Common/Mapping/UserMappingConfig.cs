using ExpenseTracker.Application.UserOperations.Commands;
using ExpenseTracker.Application.UserOperations.Common;
using ExpenseTracker.Contracts.UserOperations;

using Mapster;

namespace ExpenseTracker.API.Common.Mapping;
public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserRequest, UpdateUserProfileCommand>();
        config.NewConfig<UserResponse, UserResult>();
    }
}