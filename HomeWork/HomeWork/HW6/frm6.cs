﻿using HomeWork.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork.HW6
{
    public partial class frm6 : Form
    {
        string connString = Settings.Default.NorthwindConnectionString;

        public frm6()
        {
            InitializeComponent();
            LoadCountry();
            LoadColumns();
        }
        private void LoadCountry()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("SELECT DISTINCT Country FROM Customers",conn);
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        comboBox1.Items.Add(dataReader["Country"]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            this.listView1.View = View.Details;
            this.comboBox1.SelectedIndex = 0;
        }
        private void LoadColumns()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Customers", conn);
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    DataTable dataTable = dataReader.GetSchemaTable();
                    //this.dataGridView1.DataSource = dataTable;  //觀察GetSchemaTable會拿到怎樣的資料表
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        listView1.Columns.Add(dataTable.Rows[i][0].ToString());
                    }
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//自動調整欄位寬度
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand($"SELECT * FROM Customers WHERE Country = '{comboBox1.Text}'", conn);
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ListViewItem lvi =  this.listView1.Items.Add(dataReader["CustomerID"].ToString());
                        for (int i = 1; i <= dataReader.FieldCount - 1; i++)
                        {
                            if (dataReader.IsDBNull(i))
                            {
                                lvi.SubItems.Add("空值");
                            }
                            else
                            {
                                lvi.SubItems.Add(dataReader[i].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.LargeIcon;
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.SmallIcon;
        }
        private void detailViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.Details;
        }
        private void customerIDAscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand($"SELECT * FROM Customers WHERE Country = '{comboBox1.Text}'ORDER BY CustomerID ASC", conn);
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ListViewItem lvi = this.listView1.Items.Add(dataReader["CustomerID"].ToString());
                        for (int i = 1; i <= dataReader.FieldCount - 1; i++)
                        {
                            if (dataReader.IsDBNull(i))
                            {
                                lvi.SubItems.Add("空值");
                            }
                            else
                            {
                                lvi.SubItems.Add(dataReader[i].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("123" + ex.Message);
            }
        }
        private void customerIDDescToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand($"SELECT * FROM Customers WHERE Country = '{comboBox1.Text}'ORDER BY CustomerID  DESC", conn);
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ListViewItem lvi = this.listView1.Items.Add(dataReader["CustomerID"].ToString());
                        for (int i = 1; i <= dataReader.FieldCount - 1; i++)
                        {
                            if (dataReader.IsDBNull(i))
                            {
                                lvi.SubItems.Add("空值");
                            }
                            else
                            {
                                lvi.SubItems.Add(dataReader[i].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("123" + ex.Message);
            }
        }
        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            ListViewGroup[] viewGroups = CreatLisrViewGroup();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand($"SELECT * FROM Customers", conn);
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ListViewItem lvi = this.listView1.Items.Add(dataReader["CustomerID"].ToString());
                        for (int i = 0; i < viewGroups.Length; i++)
                        {
                            if(viewGroups[i].Name == dataReader["Country"].ToString())
                            {
                                lvi.Group = viewGroups[i];
                            }
                        }
                        for (int i = 1; i <= dataReader.FieldCount - 1; i++)
                        {
                            if (dataReader.IsDBNull(i))
                            {
                                lvi.SubItems.Add("空值");
                            }
                            else
                            {
                                lvi.SubItems.Add(dataReader[i].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("123" + ex.Message);
            }
        }
        private ListViewGroup[] CreatLisrViewGroup()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand($"SELECT DISTINCT Country FROM Customers", conn);
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    ListViewGroup[]  viewGroups = new ListViewGroup[dataTable.Rows.Count];
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        ListViewGroup lvg = new ListViewGroup();
                        lvg.Header = dataTable.Rows[i][0].ToString();
                        lvg.Name = dataTable.Rows[i][0].ToString();
                        listView1.Groups.Add(lvg);
                        viewGroups[i] = lvg;
                    }
                    return viewGroups;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
