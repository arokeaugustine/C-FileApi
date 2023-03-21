using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using UserApp.Models;

namespace UserApp.Services;

public interface IUserService
{
    Task<List<UserModel>?> GetUsers();
    Task<ResponseModel> CreateUser(UserModel userModel);
    Task<ResponseModel> DeleteUser(int userid);
    Task<UserModel> UserDetail(int userid);
}

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<List<UserModel>?> GetUsers()
    {
        string? baseuri = _configuration.GetSection("APIUrls").GetSection("UserApiBaseUrl").Value;
        string? endpointuri = _configuration.GetSection("APIUrls").GetSection("GetUserUrl").Value;
        string requesturi = baseuri + endpointuri;
        RestClient client = new RestClient(requesturi);
        var request = new RestRequest(requesturi, Method.Get){RequestFormat = DataFormat.Json};
        
        try
        {
            var response = await client.ExecuteAsync(request).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<List<UserModel>>(response.Content);
            return result;
        }
        catch (Exception ex)
        {
            
            ex.ToString();
        }
        return null;
        
    }

    public async Task<ResponseModel> CreateUser(UserModel userModel)
    {
        string? baseuri = _configuration.GetSection("APIUrls").GetSection("UserApiBaseUrl").Value;
        string? endpointuri = _configuration.GetSection("APIUrls").GetSection("PostUserUrl").Value;
        string requesturi = baseuri + endpointuri;
        RestClient client = new RestClient(requesturi);
        var request = new RestRequest(requesturi, Method.Post);
        request.AddJsonBody(userModel);
        request.RequestFormat = DataFormat.Json;
        
        try
        {
            var response = await client.ExecuteAsync(request).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<ResponseModel?>(response.Content);
            return result;
        
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        return null;
    }
    public async Task<UserModel> UserDetail(int userid)
    {
        string? baseuri = _configuration.GetSection("APIUrls").GetSection("UserApiBaseUrl").Value;
        string? endpointuri = _configuration.GetSection("APIUrls").GetSection("GetUserDetailsUrl").Value;
        string requesturi = baseuri + endpointuri;
        requesturi = baseuri + endpointuri;
        RestClient client = new RestClient(requesturi);
        var request = new RestRequest(requesturi, Method.Get);
        request.AddParameter("userid", userid);
        request.RequestFormat = DataFormat.Json;
        try
        {
            var response = await client.ExecuteAsync(request).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<UserModel>(response.Content);
            return result;
        }
        catch (Exception ex)
        {
            
            ex.ToString();
        }
        return null;
        

    }

    public async Task<ResponseModel> DeleteUser(int userid)
    {
        string? baseuri = _configuration.GetSection("APIUrls").GetSection("UserApiBaseUrl").Value;
        string? endpointuri = _configuration.GetSection("APIUrls").GetSection("DeleteUsersDetailsUrl").Value;
        string requesturi = baseuri + endpointuri;
        RestClient client = new RestClient(requesturi);
        var request = new RestRequest(requesturi, Method.Delete);
        request.AddParameter("userid", userid);

        try
        {
            var response = await client.ExecuteAsync(request).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<ResponseModel>(response.Content);
            return result;
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        return null;
    }


}
