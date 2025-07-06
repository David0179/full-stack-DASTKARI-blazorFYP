using Microsoft.Data.SqlClient;
using System.Data;
using Entities;

namespace DAL
{
    public class ArtistDAL
    {
        public void SubmitApplication(ArtistApplicationModel obj)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_SubmitArtistApplication", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", obj.UserId);
                cmd.Parameters.AddWithValue("@ArtistName", obj.ArtistName);
                cmd.Parameters.AddWithValue("@ProfileDescription", obj.ProfileDescription);
                cmd.Parameters.AddWithValue("@Location", obj.Location);
                cmd.Parameters.AddWithValue("@YearsOfExperience", obj.YearsOfExperience);
                cmd.Parameters.AddWithValue("@ArtistImageUrl", obj.ArtistImageUrl);
                cmd.Parameters.AddWithValue("@WorkImage", obj.WorkImage);

                cmd.ExecuteNonQuery();
            }
        }

        public List<ArtistApplicationModel> GetPendingApplications()
        {
            List<ArtistApplicationModel> list = new List<ArtistApplicationModel>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ArtistApplications WHERE StatusId = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new ArtistApplicationModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        ArtistName = dr["ArtistName"].ToString(),
                        ProfileDescription = dr["ProfileDescription"].ToString(),
                        Location = dr["Location"].ToString(),
                        ProfileLink = dr["ProfileLink"].ToString(),
                        YearsOfExperience = dr["YearsOfExperience"] != DBNull.Value ? Convert.ToInt32(dr["YearsOfExperience"]) : 0,
                        AppliedDate = Convert.ToDateTime(dr["AppliedDate"]),
                        ArtistImageUrl = dr["ArtistImageUrl"].ToString(),
                        WorkImage = dr["WorkImage"].ToString()
                    });
                }
            }

            return list;
        }

        public void ApproveApplication(int appId, int processedBy)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_ApproveArtistApplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppId", appId);
                cmd.Parameters.AddWithValue("@ProcessedBy", processedBy);
                cmd.ExecuteNonQuery();
            }
        }

        public void RejectApplication(int appId, int processedBy, string reason)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_RejectArtistApplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppId", appId);
                cmd.Parameters.AddWithValue("@ProcessedBy", processedBy);
                cmd.Parameters.AddWithValue("@Reason", reason);
                cmd.ExecuteNonQuery();
            }
        }

        public async Task<List<ArtistDropdownModel>> GetApprovedArtists()
        {
            List<ArtistDropdownModel> list = new List<ArtistDropdownModel>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand(@"
            SELECT 
    A.Id AS ApplicationId,  
    A.ArtistName, 
    A.ProfileLink, 
    A.ProfileDescription,
  A.Location, 
    A.ArtistImageUrl,
    A.WorkImage
FROM Users U
INNER JOIN ArtistApplications A ON A.UserId = U.Id
WHERE U.IsArtist = 1 AND A.StatusId = 2
", con);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        list.Add(new ArtistDropdownModel
                        {
                            Id = Convert.ToInt32(dr["ApplicationId"]),
                            Name = dr["ArtistName"].ToString(),
                            ProfileLink = dr["ProfileLink"].ToString(),
                            Description = dr["ProfileDescription"].ToString(),
                            Location = dr["Location"].ToString(),
                            ImageUrl = dr["ArtistImageUrl"].ToString(),
                            workImage = dr["WorkImage"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public async Task UpdateArtistAsync(ArtistDropdownModel artist)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();

                var query = @"
    UPDATE ArtistApplications
    SET ArtistName = @Name,
        ProfileDescription = @Description,
        Location = @Location,
        ProfileLink = @ProfileLink,
        ArtistImageUrl = @ImageUrl,
        WorkImage = @WorkImage,
        ModifiedDate = GETDATE()
    WHERE Id = @Id";


                using (var command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Name", artist.Name);
                    command.Parameters.AddWithValue("@Description", artist.Description);
                    command.Parameters.AddWithValue("@Location", artist.Location);
                    command.Parameters.AddWithValue("@ProfileLink", artist.ProfileLink);
                    command.Parameters.AddWithValue("@ImageUrl", artist.ImageUrl ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@WorkImage", artist.workImage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Id", artist.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task RemoveArtistAsync(int applicationId)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                await conn.OpenAsync();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int userId;

                        // Step 1: Get UserId from ArtistApplications.Id
                        string getUserIdQuery = "SELECT UserId FROM ArtistApplications WHERE Id = @AppId";
                        using (SqlCommand getCmd = new SqlCommand(getUserIdQuery, conn, transaction))
                        {
                            getCmd.Parameters.AddWithValue("@AppId", applicationId);
                            object result = await getCmd.ExecuteScalarAsync();
                            if (result == null)
                                throw new Exception("Artist application not found.");

                            userId = Convert.ToInt32(result);
                        }

                        // Step 2: Set IsArtist = 0 in Users
                        string updateUserQuery = "UPDATE Users SET IsArtist = 0 WHERE Id = @UserId";
                        using (SqlCommand cmd = new SqlCommand(updateUserQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        // Step 3: Set StatusId = 3 in ArtistApplications
                        string updateAppQuery = "UPDATE ArtistApplications SET StatusId = 3 WHERE Id = @AppId";
                        using (SqlCommand cmd = new SqlCommand(updateAppQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@AppId", applicationId);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<ArtistDropdownModel?> GetArtistByIdAsync(int id)
        {
            ArtistDropdownModel? artist = null;

            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();

                var query = @"
            SELECT 
    Id,
    ArtistName,
    ProfileLink,
    ProfileDescription,
    Location,
    ArtistImageUrl,
    WorkImage,
    YearsOfExperience,
    About
FROM ArtistApplications
WHERE Id = @Id AND StatusId = 2"; //2 = approved

                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                        artist = new ArtistDropdownModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["ArtistName"].ToString(),
                            ProfileLink = reader["ProfileLink"].ToString(),
                            Description = reader["ProfileDescription"].ToString(),
                            Location = reader["Location"].ToString(),
                            ImageUrl = reader["ArtistImageUrl"].ToString(),
                            workImage = reader["WorkImage"].ToString(),
                            About = reader["About"].ToString() 
                        };

                    }
                }
                }
            }

            return artist;
        }


       public async Task UpdateArtistProfileAsync(ArtistDropdownModel artist)
{
    using (SqlConnection con = DBHelper.GetConnection())
    {
        await con.OpenAsync();

        var query = @"
        UPDATE ArtistApplications
        SET 
            About = @About,
            ArtistImageUrl = @ImageUrl,
            WorkImage = @WorkImage,
            ModifiedDate = GETDATE()
        WHERE Id = @ArtistId";

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@About", artist.About ?? (object)DBNull.Value); // Corrected
            cmd.Parameters.AddWithValue("@ImageUrl", artist.ImageUrl ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@WorkImage", artist.workImage ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ArtistId", artist.Id); // Corrected

            await cmd.ExecuteNonQueryAsync();
        }
    }
}


        public async Task<List<int>> GetApprovedArtistApplicationsByUserIdAsync(int userId)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                await con.OpenAsync();

                string query = @"SELECT Id FROM ArtistApplications 
                         WHERE UserId = @UserId AND StatusId = 2";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    var ids = new List<int>();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ids.Add(Convert.ToInt32(reader["Id"]));
                        }
                    }
                    return ids;
                }
            }
        }
    }
    }
