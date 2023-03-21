using FileApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FileApi.Services;
public interface IUserService
{
    Task<ResponseModel> CreateUser(UserModel user);
    Task<List<UserModel>?> GetUsers();
    UserModel? GetUserDetails(int userId);
    Task<ResponseModel> UpdateUser(UserModel user);
    Task<ResponseModel> DeleteUser(int userId);

}

public class UserService:IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostingEnv;
    private readonly DatabaseContext _dbContext;

    public UserService(IConfiguration configuration, IWebHostEnvironment hostingEnv, DatabaseContext dbContext)
    {
        _configuration = configuration;
        _hostingEnv = hostingEnv;
        _dbContext = dbContext;
    }

    public async Task<List<UserModel>?> GetUsers()
    {
        try
        {
            List<UserModel> users = await _dbContext.User.Where(model => model.status == true).Select(user => new UserModel{
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                status = user.status,
                DateCreated = user.DateCreated
            }).ToListAsync().ConfigureAwait(false);
            return users;
        }
        catch (Exception ex) 
        {
            
            Console.WriteLine(ex);
        }
        return null;
    }
    
    public UserModel? GetUserDetails(int userId)
    {
        try
        {
            var user = _dbContext.User.Where(model => model.Id == userId)
            .Select(user => new UserModel{
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                status = user.status,
                DateCreated = user.DateCreated
            }).FirstOrDefault();
            return user;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return null;
    }
    
    public async Task<ResponseModel> CreateUser(UserModel user)
    {
        if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            return new ResponseModel{ResponseMessage="There was no First Name or Last Name"};
        User userData = MapUserData(user);
        if (string.IsNullOrEmpty(userData.FirstName) || string.IsNullOrEmpty(userData.LastName))
            return new ResponseModel{ResponseMessage="This user does not have an Id"};
        ResponseModel response = await SaveUser(userData).ConfigureAwait(false);
        return response;
    }
    private User MapUserData(UserModel user)
    {
        return new User{
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            status = true,
            DateCreated = DateTime.Now
        };
    }
    
    private async Task<ResponseModel> SaveUser(User user)
    {
        int entryId = 0;
        try
        {
            await _dbContext.User.AddAsync(user).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync();
            entryId = user.Id;
            if (entryId <= 0)
                return new ResponseModel{ ResponseMessage = "There was an error creating this user"};
        }
        catch (Exception ex)
        {
            
            Console.WriteLine(ex);
        }
        return new ResponseModel{ ResponseStatus = true, ResponseMessage = "User created successfully"};
    }

    public async Task<ResponseModel> UpdateUser(UserModel user)
    {
        if (user.Id == 0)
            return new ResponseModel{ ResponseMessage = "This user does not have an Id" };
        User UserData = MapUserData(user);
        if (string.IsNullOrEmpty(UserData.FirstName)|| string.IsNullOrEmpty(UserData.LastName))
            return new ResponseModel{ ResponseMessage = "This user does not have an Id" };
        ResponseModel response = await UpdateUser(UserData).ConfigureAwait(false);
        return response;
    }

    private async Task<ResponseModel> UpdateUser(User user)
    {
        int entryId = 0;
        try
        {
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();
            entryId = user.Id;
            if (entryId <= 0)
                return new ResponseModel{ResponseStatus = true, ResponseMessage = "User update successful"};
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return new ResponseModel{ResponseMessage = "There was an error updating this user's details"};
    }

    // public async Task<ResponseModel> DeleteUser(int userId)
    // {
    //     if (userId <= 0)
    //         return new ResponseModel{ResponseMessage = "Invalid user"};

    //     try
    //     {
    //         var user = _dbContext.User.Where(model=>model.Id == userId).FirstOrDefault();
    //         _dbContext.User.Remove(user);
    //         await _dbContext.SaveChangesAsync();
    //         return new ResponseModel{ResponseMessage = "User deleted successfully"};

    //     }
    //     catch (Exception ex)
    //     {
            
    //         Console.WriteLine(ex.Message);
    //     }
    //     return new ResponseModel{ResponseMessage = "There was an error deleting this user from the database"};

    // }
    public async Task<ResponseModel> DeleteUser(int userid)
    {

        try
        {
            var user =  _dbContext.User.Where(model=>model.Id == userid).FirstOrDefault();
            user.status = false;
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();
            return new ResponseModel{ResponseMessage = "User deleted successfully"};
        }       
        catch (Exception ex)
        {
            
            ex.ToString();
        }
        return null;
    }

}
