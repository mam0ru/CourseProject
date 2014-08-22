using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CourseProject.Repository;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace CourseProject.Models
{
    public class LuceneSearch
    {

        private readonly IExerciseRepository exerciseRepository;

        private static string _luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath,
            "lucene_index");

        private static FSDirectory _directoryTemp;

        public LuceneSearch(IExerciseRepository exerciseRepository)
        {
            _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
            this.exerciseRepository = exerciseRepository;
            AddLuceneIndex(exerciseRepository.Get());
        }

        private static void _addToLuceneIndex(Exercise exerciseData, IndexWriter writer)
        {
            var doc = new Document();
            var commentsArray = String.Join(" ", exerciseData.Comments.Select(x => x.Text).ToArray());
            var tagsArray = String.Join(" ", exerciseData.Comments.Select(x => x.Text).ToArray());
            // add lucene fields mapped to db fields
            doc.Add(new Field("Id", exerciseData.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("ExerciseName", exerciseData.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ExerciseText", exerciseData.Text, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("UserName", exerciseData.Author.UserName, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Category", exerciseData.Category.Text, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Comments", commentsArray, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Tags", tagsArray, Field.Store.YES, Field.Index.ANALYZED));
            // add entry to index
            writer.AddDocument(doc);
        }

        public static void AddLuceneIndex(IEnumerable<Exercise> exerciseDatas)
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(_directoryTemp, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var exerciseData in exerciseDatas)
                {
                    _addToLuceneIndex(exerciseData, writer);
                }
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }

        public List<Exercise> SearchExerciseField(String field, String search, List<Exercise> exercises)
        {
            IndexReader reader = IndexReader.Open(_directoryTemp, true);
            Searcher searcher = new IndexSearcher(reader);
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, field, analyzer);
            var query = new FuzzyQuery(new Term(field, search), 0.45f);//Query query = parser.Parse(search);            
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
            for (int i = 0; i < exerciseIds.Count; i++)
            {
                exercises.Add(exerciseRepository.GetByID(exerciseIds[i]));
            }

            reader.Dispose();
            searcher.Dispose();
            analyzer.Close();

            return exercises;
        }

        public List<Exercise> SearchExercise(String search)
        {
            var exercises = new List<Exercise>();
            SearchExerciseField("ExerciseName",search, exercises);
            SearchExerciseField("ExerciseText", search, exercises);
            SearchExerciseField("UserName", search, exercises);
            SearchExerciseField("Category", search, exercises);
            SearchExerciseField("Comments", search, exercises);
            SearchExerciseField("Tags", search, exercises);
            return exercises;
        }

     
}
}  