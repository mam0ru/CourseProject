using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucene.Net.Documents;

namespace CourseProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public String Text { get; set; }

        public Exercise Target { get; set; }

        public ApplicationUser Author { get; set; }

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