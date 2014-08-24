using System;

namespace CourseProject.ViewModels
{
    public class GetCommentViewModel
    {
        public String Text { get; set; }

        public String AuthorName { get; set; }

        public String AuthorId { get; set; }

        public String AuthorAvatar { get; set; }

        public double AuthorRating { get; set; }

        public int Id { get; set; }
    }
}