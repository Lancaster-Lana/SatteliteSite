namespace Sattelite.EntityFramework.ViewModels.Admin.Persistences
{
    using System.Data;
    using Sattelite.Framework.Extensions;
    using Sattelite.EntityFramework.Repository;

    public class NewsDeletingPersistence : INewsDeletingPersistence
    {
        private readonly INewsRepository _newsRepository;

        public NewsDeletingPersistence(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public bool DeleteNews(int id)
        {
            var article = _newsRepository.GetById(id);

            if (article == null)
                throw new NoNullAllowedException(string.Format("News with id={0}", id).ToNotNullErrorMessage());

            return _newsRepository.DeleteNews(article);
        }
    }
}