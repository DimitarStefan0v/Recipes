namespace Recipes.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;

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
    }
}
