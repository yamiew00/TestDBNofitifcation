using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDBNotification.QuestionBank.Models;

namespace TestDBNotification.QuestionBank.Collections
{
    public class QuestionProvider
    {
        public Dictionary<string, QuestionCollection> QuestionCollections { get; private set; }

        public QuestionCollection this[string subject]
        {
            get => QuestionCollections[subject];
        }

        public QuestionProvider(IEnumerable<string> subjects, MongoDataStream<Question> mongoDataStream)
        {
            QuestionCollections = new Dictionary<string, QuestionCollection>();

            foreach (var subject in subjects)
            {
                var questionCollection = new QuestionCollection(subject, 
                                                                mongoDataStream.GetCollection(subject));

                //存下實體，並進行與dataStream的訂閱
                QuestionCollections[subject] = questionCollection;
                mongoDataStream.SubscribeInsert(subject, questionCollection.OnInsert());
            }
        }


        public async Task<int> CountAll()
        {
            var sum = 0;
            foreach (var questionCollection in QuestionCollections.Values)
            {
                sum += await questionCollection.GetQuestionAmount();
            }
            return sum;
        }

        public async Task IsComplete()
        {
            await Task.WhenAll(QuestionCollections.Values.Select(x => x.IsComplete()));
        }
    }
}
