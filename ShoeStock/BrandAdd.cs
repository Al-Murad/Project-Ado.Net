﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoeStock
{
    public partial class BrandAdd : Form
    {
        public BrandAdd()
        {
            InitializeComponent();
        }

        private void BrandAdd_Load(object sender, EventArgs e)
        {
            SetNewId(textBox1);
        }
        private void SetNewId(TextBox t)
        {
            using (SqlConnection con = new SqlConnection(DbConnectionUtil.ConString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(BrandId), 0) FROM Brands", con))
                {
                    con.Open();
                    int id = (int)cmd.ExecuteScalar();
                    con.Close();
                    t.Text = $"{id + 1}";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(DbConnectionUtil.ConString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Brands VALUES(@i, @n)", con))
                {
                    cmd.Parameters.AddWithValue("@i", textBox1.Text);
                    cmd.Parameters.AddWithValue("@n", textBox2.Text);
                    con.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        textBox2.Clear();
                        SetNewId(textBox1);
                    }
                    else
                    {
                        MessageBox.Show("Data Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
            }
        }
    }
}
