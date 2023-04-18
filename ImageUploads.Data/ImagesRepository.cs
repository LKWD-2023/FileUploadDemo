using System.Data.SqlClient;

namespace ImageUploads.Data
{
    public class ImagesRepository
    {
        private string _connectionString;

        public ImagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(string title, string imagePath)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Images (Title, ImagePath) VALUES (@title, @path)";
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@path", imagePath);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Image> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM  Images";
            conn.Open();
            List<Image> result = new();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Image
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    ImagePath = (string)reader["ImagePath"]
                });
            }

            return result;
        }
    }
}