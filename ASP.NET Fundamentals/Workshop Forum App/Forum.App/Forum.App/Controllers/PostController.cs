using Forum.Services.Interfaces;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;

namespace Forum.App.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<PostListViewModel> allPosts 
                = await this.postService.ListAllAsync();

            return View(allPosts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.postService.AddPostAsync(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected Error Occured :(");
                return View(model);
            }
            
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {            
            try
            {
                PostFormModel model = await this.postService.GetPostById(id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Post");                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.postService.EditByIdAsync(id, model);

            return RedirectToAction("All", "Post");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.postService.DeleteByIdAsync(id);
            return RedirectToAction("All", "Post");
        }
    }
}
