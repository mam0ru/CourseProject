using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucene.Net.Documents;

namespace CourseProject.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public String Text { get; set; }

        public ApplicationUser Author { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }

        public int TriesOfAnswers { get; set; }

        public ICollection<ApplicationUser> RightAnsweredUsers { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Video> Videos { get; set; }

        public ICollection<Picture> Pictures { get; set; }

        public ICollection<Formula> Formulas { get; set; }

        public ICollection<Graph> Graphs { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public Boolean Active { get; set; } 

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