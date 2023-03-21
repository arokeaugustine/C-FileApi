using FileApi.Models;
using FileApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController:Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("User")]
    public async Task<ActionResult> Getusers()
    {
        List<UserModel>? users = await _userService.GetUsers().ConfigureAwait(false);
        return Ok(users);
    }

    [HttpGet("User/details")]
    public ActionResult GetUserDetails(int userId)
    {
        // var user = _userService.GetUserDetails(userId);

        return Ok(_userService.GetUserDetails(userId));
    }


    [HttpPost("User")]
    public async Task<ActionResult> CreateUser(UserModel user)
    {
        var result = await _userService.CreateUser(user).ConfigureAwait(false);   
        return Ok(result);
    }

    [HttpPost("User/update")]
    public async Task<ActionResult> UpdateUser(UserModel user)
    {
        return Ok(await _userService.UpdateUser(user).ConfigureAwait(false));
    }

    [HttpDelete("User/delete")]
    public async Task<ResponseModel> DeleteUser(int userId)
    {
        // var result = await _userService.DeleteUser(userId).ConfigureAwait(false);
        return await _userService.DeleteUser(userId).ConfigureAwait(false);
    }
}
