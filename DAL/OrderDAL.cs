using Microsoft.Data.SqlClient;
using System.Data;
using Entities;

namespace DAL
{
    public class OrderDAL
    {
        public void SaveOrder(OrderModel order)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Orders (ProductId, Quantity, PaymentMethod, OrderDate)
                    VALUES (@ProductId, @Quantity, @PaymentMethod, GETDATE())
                ", con);

                cmd.Parameters.AddWithValue("@ProductId", order.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                cmd.Parameters.AddWithValue("@PaymentMethod", order.PaymentMethod);

                cmd.ExecuteNonQuery();
            }
        }

        public List<ProductSalesData> GetProductSalesData()
        {
            var data = new List<ProductSalesData>();

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 
                p.Name AS ProductName,
                SUM(o.Quantity) AS TotalUnitsSold,
                SUM(o.Quantity * p.Price) AS TotalRevenue
            FROM Orders o
            JOIN Products p ON o.ProductId = p.Id
            GROUP BY p.Name
            ORDER BY TotalRevenue DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new ProductSalesData
                    {
                        ProductName = reader["ProductName"].ToString(),
                        TotalUnitsSold = Convert.ToInt32(reader["TotalUnitsSold"]),
                        TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                    });
                }
            }

            return data;
        }
    }
}
