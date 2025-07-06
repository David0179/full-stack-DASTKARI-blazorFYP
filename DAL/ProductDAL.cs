using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
      public class ProductDAL
        {
            public void SaveData(ProductsModel obj)
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_SaveProduct", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Price", obj.Price);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@ImageUrl", obj.ImageUrl);
                    cmd.Parameters.AddWithValue("@ArtistName", obj.ArtistName);
                    cmd.Parameters.AddWithValue("@ArtistId", obj.ArtistId);
                    cmd.Parameters.AddWithValue("@ArtistProfileDescription",
                        (object?)obj.ArtistProfileDescription ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ArtistImageUrl",
                        (object?)obj.ArtistImageUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ArtistProfileLink",
                        (object?)obj.ArtistProfileLink ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@StockQuantity", obj.StockQuantity);
                    cmd.Parameters.AddWithValue("@IsAvailable", obj.IsAvailable);

                    cmd.ExecuteNonQuery();
                }
            }

            public List<ProductsModel> GetData()
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_GetProducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<ProductsModel> products = new List<ProductsModel>();

                    while (sdr.Read())
                    {
                        ProductsModel obj = new ProductsModel
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Name = sdr["Name"].ToString(),
                            Price = Convert.ToDecimal(sdr["Price"]),
                            Description = sdr["Description"].ToString(),
                            ImageUrl = sdr["ImageUrl"].ToString(),
                            ArtistName = sdr["ArtistName"].ToString(),
                            ArtistId = Convert.ToInt32(sdr["ArtistId"]),
                            ArtistProfileDescription = sdr["ArtistProfileDescription"]?.ToString(),
                            ArtistImageUrl = sdr["ArtistImageUrl"]?.ToString(),
                            ArtistProfileLink = sdr["ArtistProfileLink"]?.ToString(),
                            StockQuantity = Convert.ToInt32(sdr["StockQuantity"]),
                            IsAvailable = Convert.ToBoolean(sdr["IsAvailable"])
                        };

                        products.Add(obj);
                    }

                    return products;
                }
            }

            public void DeleteData(int id)
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_DeleteProduct", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            public void UpdateProduct(ProductsModel obj)
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_UpdateProduct", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Price", obj.Price);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@ImageUrl", obj.ImageUrl);
                    cmd.Parameters.AddWithValue("@ArtistName", obj.ArtistName);
                    cmd.Parameters.AddWithValue("@ArtistId", obj.ArtistId);
                    cmd.Parameters.AddWithValue("@ArtistProfileDescription",
                        (object?)obj.ArtistProfileDescription ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ArtistImageUrl",
                        (object?)obj.ArtistImageUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ArtistProfileLink",
                        (object?)obj.ArtistProfileLink ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@StockQuantity", obj.StockQuantity);
                    cmd.Parameters.AddWithValue("@IsAvailable", obj.IsAvailable);

                    cmd.ExecuteNonQuery();
                }
            }

            public List<ProductsModel> GetProductsBySearch(string search)
            {
                List<ProductsModel> products = new List<ProductsModel>();

                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string query = @"SELECT * FROM Products 
                     WHERE Id = @id OR Name LIKE @name OR ArtistName LIKE @artistName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        bool isIdSearch = int.TryParse(search, out int id);

                        cmd.Parameters.AddWithValue("@id", isIdSearch ? id : -1);
                        cmd.Parameters.AddWithValue("@name", $"%{search}%");
                        cmd.Parameters.AddWithValue("@artistName", $"%{search}%");

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductsModel p = new ProductsModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"]?.ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Description = reader["Description"]?.ToString(),
                                    ImageUrl = reader["ImageUrl"]?.ToString(),
                                    ArtistName = reader["ArtistName"]?.ToString(),
                                    ArtistId = Convert.ToInt32(reader["ArtistId"]),
                                    ArtistImageUrl = reader["ArtistImageUrl"]?.ToString(),
                                    ArtistProfileDescription = reader["ArtistProfileDescription"]?.ToString(),
                                    ArtistProfileLink = reader["ArtistProfileLink"]?.ToString(),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                                };
                                products.Add(p);
                            }
                        }
                    }
                }

                return products;
            }
      }
    
}