using ExpenseTracker.API.DTOs.User;
using ExpenseTracker.Core.Models;
using Mapster;

namespace ExpenseTracker.API.Mappings;

public class MappingProfile
{
    public static void RegisterMappings()
    {
        RegisterUserListMapping();
        RegisterUserMapping();
        RegisterInsertUserRequestMapping();
        RegisterInsertUserMapping();
        RegisterUpdateUserRequestMapping();
        RegisterUpdateUserMapping();
        // Diğer haritalama metotlarını burada çağır
    }

    private static void RegisterUserListMapping()
    {
        TypeAdapterConfig<User, GetUserListQueryResult>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Email, src => src.Email);
    }

    private static void RegisterUserMapping()
    {
        TypeAdapterConfig<User, GetUserByIdQueryResult>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Email, src => src.Email);
    }

    private static void RegisterInsertUserRequestMapping()
    {
        TypeAdapterConfig<InsertUserCommandRequest, User>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password)
        .Map(dest => dest.Id, src => Guid.NewGuid());
    }

    private static void RegisterInsertUserMapping()
    {
        TypeAdapterConfig<User, InsertUserCommandResult>
            .NewConfig()
            .Map(dest => dest.CreatedDate, src => src.CreatedDate)
            .Map(dest => dest.Email, src => src.Email);
    }

    private static void RegisterUpdateUserRequestMapping()
    {
        TypeAdapterConfig<UpdateUserCommandRequest, User>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password)
        .Map(dest => dest.Id, src => Guid.NewGuid());
    }

    private static void RegisterUpdateUserMapping()
    {
        TypeAdapterConfig<User, UpdateUserCommandResult>
            .NewConfig()
            .Map(dest => dest.UpdatedDate, src => src.UpdatedDate)
            .Map(dest => dest.Email, src => src.Email);
    }

}