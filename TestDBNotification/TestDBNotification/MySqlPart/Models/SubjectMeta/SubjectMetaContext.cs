using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDBNotification.MySqlPart.Models.SubjectMeta
{
    public class SubjectMetaContext
    {
        private readonly string ConnectionString;

        /// <summary>
        /// <subject, >
        /// </summary>
        private Dictionary<string, Dictionary<string, SubjectMeta>> MetaDictionary = new Dictionary<string, Dictionary<string, SubjectMeta>>();

        //"server=35.236.136.171;port=3306;user id=nani-back-end;password=exbnXQy?D#h5DzHK;database=subjectMeta";
        public SubjectMetaContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        public IEnumerable<SubjectMeta> GetBySubject(string subject)
        {
            using var mySql = new MySqlConnection(ConnectionString);

            //connection string
            mySql.Open();

            var statement = "SELECT * FROM Meta" + subject;
            var cmd = new MySqlCommand(statement, mySql);

            var reader = cmd.ExecuteReader();

            var result = new List<SubjectMeta>();
            while (reader.Read())
            {
                result.Add(new SubjectMeta()
                {
                    UID = (string)reader["UID"],
                    MetaType = (string)reader["metaType"],
                    Code = (string)reader["code"],
                    Name = (string)reader["name"],
                    ParentUID =  TryGetDBValue<string>(reader["parentUID"]),
                    Curriculum = TryGetDBValue<string>(reader["curriculum"]),
                    Subject = subject,
                });
            }
            mySql.Close();

            return result;
        }

        private T TryGetDBValue<T>(object obj)
        {
            if(obj.GetType() == typeof(DBNull))
            {
                return default;
            }
            return (T)obj;
        }

        public Dictionary<string, SubjectMeta> GetDictionarySubject(string subject)
        {
            using var mySql = new MySqlConnection(ConnectionString);

            //connection string
            mySql.Open();

            var statement = "SELECT * FROM Meta" + subject;
            var cmd = new MySqlCommand(statement, mySql);

            var reader = cmd.ExecuteReader();

            var result = new List<SubjectMeta>();
            var result2 = new Dictionary<string, SubjectMeta>();
            while (reader.Read())
            {
                var uid = (string)reader["UID"];
                result2[uid] = new SubjectMeta()
                {
                    UID = uid,
                    MetaType = (string)reader["metaType"],
                    Code = (string)reader["code"],
                    Name = (string)reader["name"],
                    ParentUID = TryGetDBValue<string>(reader["parentUID"]),
                    Curriculum = TryGetDBValue<string>(reader["curriculum"]),
                    Subject = subject,
                };
            }
            mySql.Close();

            return result2;
        }

        /// <summary>
        /// 填充所有subjectMEta資料。約2.5秒
        /// </summary>
        /// <param name="subjects"></param>
        public void Populate(string[] subjects)
        {
            foreach (var subject in subjects)
            {
                MetaDictionary[subject] = GetDictionarySubject(subject);
            }
        }

        public SubjectMeta GetByUID(string subject, string uid)
        {
            if(MetaDictionary[subject].TryGetValue(uid, out var result))
            {
                return result;
            }
            return new SubjectMeta()
            {
                Code = default,
                Name = default
            };
        }
    }
}
