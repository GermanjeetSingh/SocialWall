using System.Collections;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialWall.DAL;
using SocialWall.Models;

namespace SocialWall.Controllers;
[Authorize]
public class HomeController : Controller
{

    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly SocialWallContext context;
    public HomeController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager ,SocialWallContext context)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.context = context;
    }

    public IActionResult Index()
    {
        var posts = context.Posts.Include("ApplicationUser").Include("Likes").Include("Comments");
        return View(posts);
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Post post)
    {
        if (post.content != "" && post.content.Length <= 240)
        {
            post.ApplicationUser = await userManager.GetUserAsync(User);
            context.Posts.Add(post);
            context.SaveChanges();
            return StatusCode(201);
        }
        return StatusCode(400);
    }

    [HttpPost]
    public async Task<IActionResult> Like(int id)
    {
        var user = await userManager.GetUserAsync(User);
        var post = context.Posts.Include("Likes").SingleOrDefault(post => post.ID == id);
        if (post == null)
            return StatusCode(400);
        var like = post.Likes.SingleOrDefault(like => like.ApplicationUser == user);
        if( like == null )
        {
            like = new Like();
            like.ApplicationUser = user;
            post.Likes.Add(like);
        }else
        {
            context.Remove(like);
        }
        context.SaveChanges();
        return Content(post.Likes.Count.ToString());
    }

    [HttpGet]
    public IActionResult Comment(int id)
    {
        var post = context.Posts
            .Include(posts => posts.Comments)
            .ThenInclude(comment => comment.ApplicationUser)
            .SingleOrDefault(post => post.ID == id);
        if (post == null)
            return StatusCode(400);
        return Json(post.Comments);
    }

    [HttpPost]
    public async Task<IActionResult> Comment([FromRoute]int id ,[FromBody]Comment comment)
    {
        var user = await userManager.GetUserAsync(User);
        var post = context.Posts.Include("Comments").SingleOrDefault(post => post.ID == id);
        if (post == null || comment.content == null || comment.content == "")
            return StatusCode(400);
        comment.ApplicationUser = user;
        post.Comments.Add(comment);
        context.SaveChanges();
        return StatusCode(200);
    }

    [HttpDelete]
    public async Task<IActionResult> Post(int id)
    {
        var user = await userManager.GetUserAsync(User);
        var post = context.Posts
            .Include("Likes")
            .Include("Comments")
            .SingleOrDefault(post => post.ID == id);

        if (post == null || user != post.ApplicationUser)
            return StatusCode(400);
        context.RemoveRange(post.Likes);
        context.RemoveRange(post.Comments);
        context.Remove(post);
        context.SaveChanges();
        return StatusCode(200);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

