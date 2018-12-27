namespace LearningSystem.Web.Areas.Admin.Models
{
    using System.Collections.Generic;
    using Blog.Models;

    public class AdminHomeViewModel
    {
        public IEnumerable<BlogArticleViewModel> Articles { get; set; } 
            = new List<BlogArticleViewModel>();
    }
}