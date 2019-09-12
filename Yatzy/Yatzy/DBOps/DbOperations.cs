using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;
using System.Collections.ObjectModel;
using Yatzy.Models;

namespace Yatzy.DBOps
{
    class DbOperations
    {

        string Connect = ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString;

        #region Metoder som hämtar data
        public ObservableCollection<Player> GetPlayers()
        {
            Player p;

            ObservableCollection<Player> players = new ObservableCollection<Player>();

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM player";


                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            p = new Player()
                            {
                                Nickname = reader.GetString(2),
                                Firstname = reader.GetString(1),
                                Lastname = reader.GetString(3)

                            };

                            players.Add(p);
                        };
                    }
                }
                return players;

            }

        }


        public ObservableCollection<Player> GetPlayersTransaction()
        {
            Player p;

            ObservableCollection<Player> players = new ObservableCollection<Player>();

            NpgsqlTransaction transaction = null;
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;


            try
            {
                string[] stmts = new string[2];
                stmts[0] = "SELECT * FROM player";
                stmts[1] = "SELECT * FROM player";

                conn = new NpgsqlConnection(Connect);
                conn.Open();
                transaction = conn.BeginTransaction();

                for (int i = 0; i < stmts.Length; i++)                          
                {
                    cmd = new NpgsqlCommand(stmts[i], conn);
                    cmd.Transaction = transaction;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p = new Player()
                            {
                                Nickname = reader.GetString(2),
                                Firstname = reader.GetString(1),
                                Lastname = reader.GetString(3)
                            };

                            players.Add(p);

                        }
                    }
                }

                transaction.Commit();
                conn.Close();
                return players;



            }
            catch (Exception ex)
            {

                transaction.Rollback();
                conn.Close();
                return null;
            }




        }


         


        #endregion
    }
}
