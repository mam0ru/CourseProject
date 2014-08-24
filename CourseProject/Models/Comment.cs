using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using Lucene.Net.Documents;

namespace CourseProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public String Text { get; set; }

        public int TargetId { get; set; }

        public virtual Exercise Target { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public Document GetDocument()
        {
            Document document = new Document();
            document.Add(new Field("Text", this.Text, Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field("Id", this.Id.ToString(), Field.Store.YES, Field.Index.NO));
            document.Add(new Field("Author", this.Author.UserName, Field.Store.NO, Field.Index.ANALYZED));
            return document;
        }
    }
}