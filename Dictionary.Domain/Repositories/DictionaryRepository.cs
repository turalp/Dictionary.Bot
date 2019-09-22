using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dictionary.Domain.Models;
using Dictionary.Domain.Repositories.Abstract;

namespace Dictionary.Domain.Repositories
{
    public class DictionaryRepository : IDictionaryRepository, IDisposable
    {
        private bool _disposed = false;
        private readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public async Task<Word> GetByWordAsync(string word)
        {
            _connection.Open();
            
            var result = new Word();
            var command = new SqlCommand(string.Format(Queries.GetByWord, word), _connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                result.WordId = Guid.ParseExact(reader[0].ToString(), "N");
                result.Title = reader[1].ToString();
                result.Description = reader[2].ToString();
            }

            reader.Close();
            _connection.Close();

            return result;
        }

        public async Task InsertAsync(Word word)
        {
            var command = new SqlCommand(
                string.Format(Queries.InsertWord, word.Title, word.Description), 
                _connection);

            _connection.Open();
            await command.ExecuteNonQueryAsync();
            _connection.Close();
        }

        public async Task UpdateAsync(Word word)
        {
            var command = new SqlCommand(
                string.Format(Queries.UpdateWord, word.Title, word.Description, word.WordId),
                _connection);

            _connection.Open();
            await command.ExecuteNonQueryAsync();
            _connection.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _connection.Dispose();
            }

            _disposed = true;
        }
    }
}
