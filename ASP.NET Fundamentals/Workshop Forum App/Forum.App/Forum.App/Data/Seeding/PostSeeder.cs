using Forum.App.Data.Models;

namespace Forum.App.Data.Seeding
{
    internal class PostSeeder
    {
        internal Post[] GeneratePosts()
        {
            ICollection<Post> posts = new HashSet<Post>();

            Post currentPost;

            currentPost = new Post()
            {
                Title = "Whispers of the Enchanted Grove",
                Content = "Amidst the ancient trees, " +
                "where emerald leaves dance with the breeze," +
                " a mystical grove comes alive. Listen closely, " +
                "and you'll hear the whispers of the forest spirits " +
                "and the secrets hidden within the rustling leaves."
            };
            posts.Add(currentPost);
            currentPost = new Post()
            {
                Title = "Journey Through the Verdant Canopy",
                Content = "Embark on a journey through the lush," +
                " green canopy of the forest. High above the forest floor," +
                " the intertwining branches create a natural tapestry, allowing" +
                " you to traverse through a world where sunlight filters through" +
                " a million leaves, painting the ground below in a dappled mosaic."
            };
            posts.Add(currentPost);
            currentPost = new Post()
            {
                Title = "Eternal Wisdom of the Whispering Pines",
                Content = "The whispering pines stand tall," +
                " their needles holding the ancient wisdom of the forest." +
                " Each gust of wind carries tales of centuries past," +
                " tales of resilience and adaptation."
            };
            posts.Add(currentPost);

            return posts.ToArray();
        }
    }
}
