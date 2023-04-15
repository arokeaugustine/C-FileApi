using Microsoft.AspNetCore.Mvc;
using UserApp.Models;
using UserApp.Services;

namespace UserApp.Controllers.Users;

public class UsersController:Controller
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Home()
    {
        return View();
    }
    
    public async Task<IActionResult> Report()
    {
        List<UserModel> users = await _userService.GetUsers().ConfigureAwait(false);
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> GetJson()
    {
        List<UserModel> users = await _userService.GetUsers().ConfigureAwait(false);
        List<UserModel> result = (from userModel in users
                                    select new UserModel{
                                        Id = userModel.Id,
                                        FirstName = userModel.FirstName,
                                        LastName = userModel.LastName,
                                        Email = userModel.Email,
                                        PhoneNumber = userModel.PhoneNumber
                                    }).ToList();
        return Json(result);
    }
    public async Task<IActionResult> ReportDetails(int id)
    {
        var user= await _userService.UserDetail(id).ConfigureAwait(false);
        return View(user);
    }
     public async Task<IActionResult> JsonReportDetails(int id)
    {
        var user= await _userService.UserDetail(id).ConfigureAwait(false);
        return Json(user);
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserModel userModel)
    {
        var response = await _userService.CreateUser(userModel).ConfigureAwait(false);
        return RedirectToAction("Report");
    }

     [HttpPost]
    public async Task<IActionResult> RegisterJson(UserModel userModel)
    {
        var response = await _userService.CreateUser(userModel).ConfigureAwait(false);
        return RedirectToAction("Report");
    }

    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUser(id).ConfigureAwait(false);
        if (result.ResponseStatus == true)
        {
            return RedirectToAction("Register");
        }
        return RedirectToAction("Home");
    }

    public async Task<IActionResult> UpdateUser(UserModel user)
    {
        var response = await _userService.UpdateUser(user).ConfigureAwait(false);

        return Json(response);
    }
}


