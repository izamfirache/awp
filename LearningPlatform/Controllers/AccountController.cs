using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LearningPlatform.Models;
using LearningPlatform.Models.Users;
using LearningPlatform.Core.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Security;
using LearningPlatform.Http;
using System.IO;
using LearningPlatform.Helpers;
using System.Collections.Generic;

namespace LearningPlatform.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginUserModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var user = new User(model.Username, model.Password);
                
                var result = LoginUserInApi(user);

                if (result.IsSuccessStatusCode)
                {
                    var resultUserJson = await result.Content.ReadAsStringAsync();
                    var resultUser = JsonConvert.DeserializeObject<User>(resultUserJson);

                    FormsAuthentication.SetAuthCookie(resultUser.Username, false);
                    Session["LoggedInUserId"] = resultUser.Id;
                    Session["LoggedInUserName"] = resultUser.Username;

                    //Session["LoggedInUser"] = resultUser;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Wrong username or password";
                }
            }

            return View();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                string avatarData = string.Empty;

                if (model.Avatar != null)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    model.Avatar.InputStream.CopyTo(memoryStream);
                    avatarData = memoryStream.ToArray().ByteArrayToString();
                }

                var user = new User(model.Username, model.Password, model.Email, avatarData);

                var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, Request.GetBaseUrl() + "api/users");
                var serializedObject = JsonConvert.SerializeObject(user);
                request.Content = new StringContent(serializedObject, System.Text.Encoding.UTF8, "application/json");
                var result = await httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var resultUserJson = result.Content.ReadAsStringAsync().Result;
                    var resultUser = JsonConvert.DeserializeObject<User>(resultUserJson);

                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    Session["LoggedInUserId"] = resultUser.Id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var content = JsonConvert.DeserializeObject<BadRequestResponse>(result.Content.ReadAsStringAsync().Result);

                    if (content.Message.Contains("Username"))
                    {
                        ModelState.AddModelError("Username", content.Message);
                    }

                    if (content.Message.Contains("Email"))
                    {
                        ModelState.AddModelError("Email", content.Message);
                    }
                }
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MyDashBoard()
        {
            var model = new MyDashboardModel();

            var httpClient = new HttpClient();
            var pendingCourseRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + "api/users/" + Session["LoggedInUserId"] + "/enrollment?type=pending");
            var pendingCoursesResult = httpClient.SendAsync(pendingCourseRequest);

            var activeCoursesRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + "api/users/" + Session["LoggedInUserId"] + "/enrollment?type=active");
            var activeCoursesResult = httpClient.SendAsync(activeCoursesRequest);

            var completedCoursesRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + "api/users/" + Session["LoggedInUserId"] + "/enrollment?type=completed");
            var completedCoursesResult = httpClient.SendAsync(completedCoursesRequest);

            Task.WaitAll(new Task[] { pendingCoursesResult, activeCoursesResult, completedCoursesResult });

            model.PendingCourses = JsonConvert.DeserializeObject<List<Course>>(pendingCoursesResult.Result.Content.ReadAsStringAsync().Result);
            model.ActiveCourses = JsonConvert.DeserializeObject<List<Course>>(activeCoursesResult.Result.Content.ReadAsStringAsync().Result);
            model.CompletedCourses = JsonConvert.DeserializeObject<List<Course>>(completedCoursesResult.Result.Content.ReadAsStringAsync().Result);

            return View(model);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            var httpClient = new HttpClient();
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "api/users/" + Session["LoggedInUserId"]);
            var result = httpClient.SendAsync(request).Result;
            var user = JsonConvert.DeserializeObject<User>(result.Content.ReadAsStringAsync().Result);
            var editProfileModel = new EditProfileUserModel()
            {
                Email = user.Email,
                Username = user.Username
            };

            return View(editProfileModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(EditProfileUserModel editProfileModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editProfileModel);
            }
            else
            {
                var user = new User(Session["LoggedInUserName"].ToString(), editProfileModel.CurrentPassword);

                var result = LoginUserInApi(user);
                var resultUser = JsonConvert.DeserializeObject<User>(result.Content.ReadAsStringAsync().Result);

                if (result.IsSuccessStatusCode)
                {
                    // get new avatar
                    string avatarData = resultUser.Avatar;

                    if (editProfileModel.NewAvatar != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        editProfileModel.NewAvatar.InputStream.CopyTo(memoryStream);
                        avatarData = memoryStream.ToArray().ByteArrayToString();
                    }

                    var editedUser = new User(user.Username, editProfileModel.NewPassword, editProfileModel.Email, avatarData);

                    var httpClient = new HttpClient();

                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    var request = new HttpRequestMessage(HttpMethod.Put, baseUrl + "api/users/" + Session["LoggedInUserId"]);
                    var serializedObject = JsonConvert.SerializeObject(editedUser);
                    request.Content = new StringContent(serializedObject, System.Text.Encoding.UTF8, "application/json");
                    var editResult = await httpClient.SendAsync(request);

                    if (editResult.IsSuccessStatusCode)
                    {
                        //Session["LoggedInUserId"] ;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Error when editing the user profile";
                    }                    
                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", "Wrong password");
                }
            }

            return View();
        }

        private HttpResponseMessage LoginUserInApi(User user)
        {
            var httpClient = new HttpClient();
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            var request = new HttpRequestMessage(HttpMethod.Post, baseUrl + "api/users/token");
            var serializedObject = JsonConvert.SerializeObject(user);
            request.Content = new StringContent(serializedObject, System.Text.Encoding.UTF8, "application/json");
            var result = httpClient.SendAsync(request).Result;
            return result;
        }
    }
}