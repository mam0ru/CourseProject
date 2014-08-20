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

namespace CourseProject.Models
{
    //var query = new FuzzyQuery(new Term(field, searchString), 0.45f);

    public class LuceneSearch {

        private readonly IExerciseRepository exerciseRepository;
        private static string _luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "lucene_index");
        private static FSDirectory _directoryTemp;
        private static FSDirectory _directory
        {
            get
            {
                if (_directoryTemp == null) _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                if (IndexWriter.IsLocked(_directoryTemp)) IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }

        public LuceneSearch(IExerciseRepository exerciseRepository)
        {
            this.exerciseRepository = exerciseRepository;
        }

        private static void _addToLuceneIndex(Exercise exerciseData, IndexWriter writer)
        {
            // remove older index entry
            var searchQuery = new TermQuery(new Term("Id", exerciseData.Id.ToString()));
            writer.DeleteDocuments(searchQuery);

            // add new index entry
            var doc = new Document();
            var commentsArray = String.Join(" ", exerciseData.Comments.Select(x => x.Text).ToArray());
            var tagsArray = String.Join(" ", exerciseData.Comments.Select(x => x.Text).ToArray());
            // add lucene fields mapped to db fields
            doc.Add(new Field("Id", exerciseData.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));////////////?????
            doc.Add(new Field("ExerciseName", exerciseData.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ExerciseText", exerciseData.Text, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("UserName", exerciseData.Author.UserName, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Category", exerciseData.Category.Text, Field.Store.YES, Field.Index.ANALYZED));     
            doc.Add(new Field("Comments", commentsArray, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Tags", tagsArray, Field.Store.YES, Field.Index.ANALYZED));
            // add entry to index
            writer.AddDocument(doc);
        }

        public static void AddUpdateLuceneIndex(IEnumerable<Exercise> exerciseDatas)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entry if any)
                foreach (var exerciseData in exerciseDatas)
                {
                    _addToLuceneIndex(exerciseData, writer);
                }

                // close handles
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }

        public List<Exercise> SearchExerciseField(String field, String search)
        {

            IndexReader reader = IndexReader.Open(_directory, true);

            Searcher searcher = new IndexSearcher(reader);

            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, field, analyzer);
            
            Query query = parser.Parse(search);
            
            TopScoreDocCollector collector = TopScoreDocCollector.Create(100, true);

            searcher.Search(query, collector);

            ScoreDoc[] hits = collector.TopDocs().ScoreDocs;

            List<int> exerciseIds = new List<int>();

            foreach (ScoreDoc scoreDoc in hits)
            {

                //Get the document that represents the search result.

                Document document = searcher.Doc(scoreDoc.Doc);

                int exerciseId = int.Parse(document.Get("Id"));

                //The same document can be returned multiple times within the search results.

                if (!exerciseIds.Contains(exerciseId))
                {
                    exerciseIds.Add(exerciseId);
                }
            }

            //Now that we have the product Ids representing our search results, retrieve the products from the database.
            List<Exercise> exercises = new List<Exercise>();
            for (int i = 0; i < exerciseIds.Count; i++)
            {
                 exercises.Add(exerciseRepository.GetByID(exerciseIds[i]));
            }

            reader.Close();
            searcher.Close();
            analyzer.Close();

            return exercises;
        }




    }
}  