using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DogGo.Models;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;
        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public List<Walks> GetWalksByWalkerId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                List<Walks> walks = new List<Walks>();
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ws.Id, ws.Date, ws.Duration, ws.WalkerId, ws.DogId, wr.Name AS WalkerName, o.Name AS OwnerName 
                        FROM Walks ws
                        JOIN Walker wr ON wr.Id = ws.WalkerId
                        JOIN Dog d ON d.ID = ws.DogId
                        JOIN Owner o ON o.Id = d.OwnerId
                        Where wr.id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Walks walk = new Walks
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Owner = new Owner
                                {
                                    Name = reader.GetString(reader.GetOrdinal("OwnerName"))
                                },
                                Walker = new Walker
                                {
                                    Name = reader.GetString(reader.GetOrdinal("WalkerName"))
                                }
                            };
                            walks.Add(walk);
                        }

                    }
                    return walks;

                }
            }
        }
    }
}
