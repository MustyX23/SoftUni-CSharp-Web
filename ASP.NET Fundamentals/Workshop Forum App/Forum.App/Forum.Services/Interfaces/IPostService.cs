using Forum.ViewModels.Post;

namespace Forum.Services.Interfaces
{
    internal interface IPostService
    {
        Task<IEnumerable<PostListViewModel>> ListAllAsync();

    }
}
