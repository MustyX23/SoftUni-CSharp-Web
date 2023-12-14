using Forum.App.Data;
using Forum.App.Data.Models;
using Forum.Services.Interfaces;
using Forum.ViewModels.Post;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext dbContext;

        public PostService(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddPostAsync(PostFormModel model)
        {
            Post post = new Post() 
            {
                Title = model.Title,
                Content = model.Content,
            };

            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            Post post = await dbContext
                .Posts
                .FirstAsync(p => p.Id.ToString() == id);

            dbContext.Posts.Remove(post);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditByIdAsync(string id, PostFormModel postEditedModel)
        {
            Post postToEdit = await this.dbContext.Posts.FirstAsync(p => p.Id.ToString() == id);

            postToEdit.Title = postEditedModel.Title;
            postToEdit.Content = postEditedModel.Content;

            await dbContext.SaveChangesAsync();
        }

        public async Task<PostFormModel> GetPostById(string id)
        {
            Post post = await dbContext
                .Posts
                .FirstAsync(p => p.Id.ToString() == id);

            return new PostFormModel()
            {
                Title = post.Title,
                Content = post.Content,
            };
        }

        public async Task<IEnumerable<PostListViewModel>> ListAllAsync()
        {
            IEnumerable<PostListViewModel> allPosts = await dbContext
                .Posts
                .Select(p => new PostListViewModel()
                {
                    Id = p.Id.ToString(),
                    Content = p.Content,
                    Title = p.Title
                })
                .ToArrayAsync();

            return allPosts;
        }
    }
}
