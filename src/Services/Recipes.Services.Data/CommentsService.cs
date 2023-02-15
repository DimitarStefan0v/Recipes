namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Mapping;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task AddCommentAsync(int id, string content, string userId, bool postComment)
        {
            var comment = new Comment
            {
                Content = content,
                AddedByUserId = userId,
                IsApproved = false,
            };

            if (postComment)
            {
                comment.PostId = id;
            }
            else
            {
                comment.RecipeId = id;
            }

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task Approve(int id)
        {
            var comment = this.commentsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            if (comment == null)
            {
                return;
            }

            comment.IsApproved = true;
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
             var comment = this.commentsRepository.All().Where(x => x.Id == id).FirstOrDefault();

             if (comment == null)
            {
                return;
            }

             this.commentsRepository.Delete(comment);
             await this.commentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllPostComments<T>(int id)
        {
            return this.commentsRepository
                .AllAsNoTracking()
                .Where(x => x.PostId == id)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllUnapproved<T>(int page, int itemsPerPage)
        {
            return this.commentsRepository
                .AllAsNoTracking()
                .Where(x => x.IsApproved == false && (x.Post != null || x.Recipe != null))
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }
    }
}
