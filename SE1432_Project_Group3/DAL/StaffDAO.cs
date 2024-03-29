﻿using PRN292_Project.DTL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PRN292_Project.DAL
{
    class StaffDAO
    {
        public static IEnumerable<Staff> getAllStaffs()
        {
            var Staffs = new List<Staff>();

            try
            {
                DataTable dt = getDataTable();
                foreach (DataRow row in dt.Rows)
                {
                    var Staff = new Staff
                    {
                        StaffID = row["StaffID"].ToString(),
                        Name = row["Name"].ToString(),
                        Address = row["Address"].ToString(),
                        Phone = row["Phone"].ToString(),
                        BankAccount = row["BankAccount"].ToString(),
                        Username = row["Username"].ToString(),
                        Password = row["Password"].ToString(),
                        Role = row["Role"].ToString()
                    };
                    Staffs.Add(Staff);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Staffs.AsEnumerable();

        }

        public static DataTable getDataTable()
        {
            string sql = "SELECT [StaffID],[Name],[Address],[Phone],[BankAccount],a.[Username],[Password],[Role]" +
                "FROM [Staff] s INNER JOIN [Account] a ON a.[Username] = s.[Username]";
            return DAO.GetDataTable(sql);
        }

        public static bool insertStaff(Staff s)
        {
            AccountDAO.insert(s);
            SqlCommand cmd = new SqlCommand("INSERT INTO [Staff] " +
                "([StaffID],[Name],[Address],[Phone],[BankAccount],[Username]) " +
                "VALUES(@StaffID,@Name,@Address,@Phone,@BankAccount, @Username)");
            cmd.Parameters.AddWithValue("@StaffID", s.StaffID);
            cmd.Parameters.AddWithValue("@Name", s.Name);
            cmd.Parameters.AddWithValue("@Address", s.Address);
            cmd.Parameters.AddWithValue("@Phone", s.Phone);
            cmd.Parameters.AddWithValue("@BankAccount", s.BankAccount);
            return DAO.UpdateTable(cmd);
        }

        /*        public static bool update(Staff p)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [Staff] " +
                        "SET [Name] = @Name," +
                        "[Address] = @Address,[Phone] = @Phone,[BankAccount] = @BankAccount WHERE [StaffID] = @StaffID");
                    cmd.Parameters.AddWithValue("@StaffID", p.StaffID);
                    cmd.Parameters.AddWithValue("@Name", p.Name);
                    cmd.Parameters.AddWithValue("@Address", p.Address);
                    cmd.Parameters.AddWithValue("@Phone", p.Phone);
                    cmd.Parameters.AddWithValue("@BankAccount", p.BankAccount);
                    return DAO.UpdateTable(cmd);

                }

                public static bool delete(string id)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [Staff] WHERE [StaffID]=@StaffID");
                    cmd.Parameters.AddWithValue("@StaffID", id);
                    return DAO.UpdateTable(cmd);

                }*/
    }
}
