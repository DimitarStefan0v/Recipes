﻿namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Mapping;
    using Recipes.Web.ViewModels.Posts;

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

        public async Task CreateAsync(CreatePostInputModel input, string userId)
        {
            var post = new Post
            {
                AddedByUserId = userId,
                Content = input.Content,
                Name = input.Name,
                IsApproved = false,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            return this.postsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task DeleteAsync(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            if (post == null)
            {
                return;
            }

            this.postsRepository.Delete(post);
            await this.postsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllUnapproved<T>(string sort, int page, int itemsPerPage)
        {
            var query = this.postsRepository
                .AllAsNoTracking()
                .Where(x => x.IsApproved == false)
                .AsQueryable();

            SortPosts(ref sort, ref query);

            return query.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public async Task Approve(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            if (post == null)
            {
                return;
            }

            post.IsApproved = true;
            await this.postsRepository.SaveChangesAsync();
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
                    query = query.OrderBy(x => x.CreatedOn).ThenBy(x => x.Name);
                    break;
                case "descending":
                    query = query.OrderByDescending(x => x.CreatedOn).ThenBy(x => x.Name);
                    break;
                case "popularity":
                    query = query.OrderByDescending(x => x.ViewsCount).ThenBy(x => x.Name);
                    break;
                case "comments":
                    query = query.OrderByDescending(x => x.Comments.Count()).ThenBy(x => x.Name);
                    break;
                default:
                    sort = "descending";
                    query = query.OrderByDescending(x => x.CreatedOn).ThenBy(x => x.Name);
                    break;
            }
        }
    }
}
