using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CourseProject.Repository;
using CourseProject.Repository.Implementation;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.Ajax.Utilities;
using Version = Lucene.Net.Util.Version;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace CourseProject.Models
{
    public class LuceneSearchUserName
    {
        private ApplicationUserManager userManager;

        private static string _luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "lucene_index");

        private static FSDirectory _directoryTemp;

        public LuceneSearchUserName(ApplicationUserManager userManager)
        {
            _directoryTemp = FSDirectory.Open(_luceneDir);
            UserManager = userManager;
            AddLuceneIndex(userManager.Users.ToList());
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager;
            }
            private set
            {
                userManager = value;
            }
        }

        private static void _addToLuceneIndex(ApplicationUser userData, IndexWriter writer)
        {
            var doc = new Document();
            // add lucene fields mapped to db fields
            doc.Add(new Field("Id", userData.Id, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("UserName", userData.UserName, Field.Store.YES, Field.Index.ANALYZED));
            // add entry to index
            writer.AddDocument(doc);
        }

        public static void AddLuceneIndex(List<ApplicationUser> userDatas)
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(_directoryTemp, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var userData in userDatas)
                {
                    _addToLuceneIndex(userData, writer);
                }
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }

        public List<ApplicationUser> SearchUser(String search)
        {
            IndexReader reader = IndexReader.Open(_directoryTemp, true);
            Searcher searcher = new IndexSearcher(reader);
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "UserName", analyzer);
            var query = new FuzzyQuery(new Term("UserName", search), 0.45f);//Query query = parser.Parse(search);            
            TopScoreDocCollector collector = TopScoreDocCollector.Create(100, true);
            searcher.Search(query, collector);
            ScoreDoc[] hits = collector.TopDocs().ScoreDocs;
            List<String> userIds = new List<String>();
            foreach (ScoreDoc scoreDoc in hits)
            {
                //Get the document that represents the search result.
                Document document = searcher.Doc(scoreDoc.Doc);
                String userId = document.Get("Id");
                //The same document can be returned multiple times within the search results.
                if (!userIds.Contains(userId))
                {
                    userIds.Add(userId);
                }
            }

            var users = new List<ApplicationUser>();
            for (int i = 0; i < userIds.Count; i++)
            {
                users.Add(userManager.FindById(userIds[i]));
            }

            reader.Dispose();
            searcher.Dispose();
            analyzer.Close();

            return users;
        }
    }
}