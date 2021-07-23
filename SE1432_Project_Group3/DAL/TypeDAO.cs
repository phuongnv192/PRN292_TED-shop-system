﻿using PRN292_Project.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PRN292_Project.DAL
{
	class TypeDAO
	{
        public static IEnumerable<ProductType> getAllProductTypes()
        {
            var productTypes = new List<ProductType>();

            try
            {
                DataTable dt = getDataTable();
                foreach (DataRow row in dt.Rows)
                {
                    var productType = new ProductType
                    {
                        TypeID = int.Parse(row["TypeID"].ToString()),
                        Name = row["Type_name"].ToString()
                    };
                    productTypes.Add(productType);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return productTypes.AsEnumerable();

        }
        public static DataTable getDataTable()
        {
            string sql = "SELECT [TypeID],[Type_name] " +
                "FROM [ProductType]";
            return DAO.GetDataTable(sql);
        }

        public static bool insert(ProductType t)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [ProductType] ([Type_name]) VALUES (@[Type_name])");
            cmd.Parameters.AddWithValue("@Type_name", t.Name);
            return DAO.UpdateTable(cmd);

        }

        public static bool update(ProductType t)
        {
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[ProductType] SET [Type_name] = @Type_name WHERE [TypeID] = @TypeID");
            cmd.Parameters.AddWithValue("@Type_name", t.Name);
            cmd.Parameters.AddWithValue("@TypeID", t.TypeID);
            return DAO.UpdateTable(cmd);

        }

        public static bool delete(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM [ProductType] WHERE [TypeID] = @TypeID");
            cmd.Parameters.AddWithValue("@TypeID", id);
            return DAO.UpdateTable(cmd);

        }

        public static ProductType getTypeByID(int id)
        {
            ProductType type = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT [TypeID],[Type_name] " +
                "FROM [ProductType] " +
                "WHERE [TypeID] = @TypeID");
                cmd.Parameters.AddWithValue("@TypeID", id);
                DataTable dt = DAO.GetDataTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    product = new Product
                    {
                        ProductID = int.Parse(row["ProductID"].ToString()),
                        TypeID = int.Parse(row["TypeID"].ToString()),
                        Produce_country = row["Produce_country"].ToString(),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        User_guide = row["User_guide"].ToString(),
                        Price = double.Parse(row["Price"].ToString()),
                        Sell_price = double.Parse(row["Sell_price"].ToString()),
                        Quantity = int.Parse(row["Quantity"].ToString())

                    };

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }
    }
}