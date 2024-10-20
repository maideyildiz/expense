using ExpenseTracker.API.DTOs.User;
using ExpenseTracker.Core.Models;
using Mapster;

namespace ExpenseTracker.API.Mappings;

public class MapsterConfiguration
{
    public static void RegisterMappings()
    {
        RegisterUserListMapping();
        RegisterUserMapping();
        RegisterInsertUserRequestMapping();
        //RegisterInsertUserResultMapping();
        RegisterUpdateUserRequestMapping();
        RegisterUpdateUserMapping();
        // Diğer haritalama metotlarını burada çağır

        TypeAdapterConfig.GlobalSettings.Scan();
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
            .Map(dest => dest.Password, src => src.Password);
    }
    // private static void RegisterInsertUserResultMapping()
    // {
    //     TypeAdapterConfig<User, InsertUserCommandResult>
    //         .NewConfig()
    //         .Map(dest => dest.Token, src => src.JwtToken)
    //         .Map(dest => dest.Email, src => src.Email);
    // }

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