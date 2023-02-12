namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Mapping;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public IEnumerable<T> GetAll<T>(string sort, int page, int itemsPerPage)
        {
            var query = this.postsRepository
                .AllAsNoTracking()
                .AsQueryable();

            SortPosts(ref sort, ref query);

            return query.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        private static void SortPosts(ref string sort, ref IQueryable<Post> query)
        {
            if (sort == null)
            {
                sort = "descending";
            }

            sort = sort.ToLower().Trim();

            switch (sort)
            {
                case "ascending":
                    query = query.OrderBy(x => x.CreatedOn);
                    break;
                case "descending":
                    query = query.OrderByDescending(x => x.CreatedOn);
                    break;
                case "popularity":
                    query = query.OrderByDescending(x => x.ViewsCount);
                    break;
                case "comments":
                    query = query.OrderByDescending(x => x.Comments.Count());
                    break;
                default:
                    sort = "descending";
                    query = query.OrderByDescending(x => x.CreatedOn);
                    break;
            }
        }
    }
}
