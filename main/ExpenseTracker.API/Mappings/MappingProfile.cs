using ExpenseTracker.API.DTOs;
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
        // Diğer haritalama metotlarını burada çağır
    }

    private static void RegisterUserListMapping()
    {
        TypeAdapterConfig<User, GetUserListQueryResult>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Email, src => src.Email);
    }

    private static void RegisterUserMapping()
    {
        TypeAdapterConfig<User, GetUserByIdQueryResult>
            .NewConfig()
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

}