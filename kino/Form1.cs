using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kino
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //adamickatomas
        }
            SQLiteConnection sqlite_c;

            private void Form1_Load(object sender, EventArgs e)
            {
                int i, j;
                for (i = 0; i < 3; i++)
                {
                    for (j = 0; j < 3; j++)
                    {
                        Button bt = new Button();
                        bt.Top = 50 * i;
                        bt.Left = 50 * j;
                        bt.Width = velikost;
                        bt.Name = i.ToString() + j.ToString();
                        bt.Height = velikost;
                        this.Controls.Add(bt);
                    }
                }
            }
            int velikost = 200;
            private SQLiteConnection CreateConnection()
            {
                SQLiteConnection sqlite_c;
                // Create a new database connection:
                sqlite_c = new SQLiteConnection("Data Source = database1.db; Version = 3; New = True; Compress = True; ");
                // Open the connection:
                try
                {
                    sqlite_c.Open();
                }
                catch (Exception ex)
                {

                }
                return sqlite_c;
            }
            private void CreateTable(SQLiteConnection conn)
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                string Createsql = @"CREATE TABLE IF NOT EXISTS automat
                   (id INT AUTO_INCREMENT PRIMARY KEY, kod VARCHAR(10), nazev VARCHAR(10), cena INT, pocet INT)";
                sqlite_cmd.CommandText = Createsql;
                sqlite_cmd.ExecuteNonQuery();
            }

            private void InsertData(SQLiteConnection conn, string kod, string nazev, int cena, int pocet)
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = @"INSERT INTO automat
                   (kod, nazev, cena, pocet) VALUES('" + kod + "','" + nazev + "','" + cena + "','" + pocet + "'); ";
                sqlite_cmd.ExecuteNonQuery();
            }

            private string ReadData(SQLiteConnection conn)
            {
                string text = "";
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT * FROM automat";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    text += sqlite_datareader.GetString(1) + "," + sqlite_datareader.GetString(2) + "," + sqlite_datareader.GetString(3) + "\n";
                }
                return (text);
                //conn.Close();
            }
        }
    }
