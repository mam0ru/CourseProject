using System;
using System.Collections.Generic;
using Lucene.Net.Documents;

namespace CourseProject.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public String Text { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }

        public String Name { get; set; }

        public int TriesOfAnswers { get; set; }

        public virtual ICollection<ApplicationUser> RightAnsweredUsers { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Video> Videos { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Equation> Equations { get; set; }

        public virtual ICollection<Graph> Graphs { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public Boolean Active { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public Document GetDocument()
        {
            Document document = new Document();
            document.Add(new Field("Text", this.Text, Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field("Id", this.Id.ToString(), Field.Store.YES, Field.Index.NO));
            foreach (var tag in Tags)
            {
                document.Add(new Field("Tag", tag.Text, Field.Store.NO, Field.Index.ANALYZED));
            }
            document.Add(new Field("Author", this.Author.UserName, Field.Store.NO, Field.Index.ANALYZED));
            return document;
        }
    }
}