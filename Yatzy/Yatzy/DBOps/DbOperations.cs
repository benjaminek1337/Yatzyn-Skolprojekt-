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
        int gameId;

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
                string[] stmts = new string[1];
                stmts[0] = "SELECT * FROM player";

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
                                Lastname = reader.GetString(3),
                                PlayerId = reader.GetString(0)
                            };

                            players.Add(p);

                        }
                    }
                }

                transaction.Commit();
                conn.Close();
                return players;
            }
            catch (Exception)
            {

                transaction.Rollback();
                conn.Close();
                return null;
            }
        }





        #endregion


        #region Metoder som skriver data

        //Tar just nu emot en array med player ID, detta skall bytas ut mot lista med active players
        public void StartGameTransaction(int [] playerId, int gameType)
        {
            

            NpgsqlTransaction transaction = null;
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {

                string stmt1 = "INSERT INTO game (gametype_id) VALUES (@gametype_id)";
                string stmt2 = "SELECT game_id FROM game ORDER BY game_id DESC LIMIT 1";
                string stmt3 = "INSERT INTO game_player (player_id, game_id) VALUES (@player_id, @game_id)";

                //cmd.Parameters.AddWithValue("gametype", gameType);
                //cmd.Parameters.AddWithValue("game_id", gameId);

                conn = new NpgsqlConnection(Connect);
                conn.Open();
                transaction = conn.BeginTransaction();

                cmd = new NpgsqlCommand(stmt1, conn);
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("gametype_id", gameType);
                cmd.ExecuteNonQuery();

                cmd = new NpgsqlCommand(stmt2, conn);
                cmd.Parameters.AddWithValue("game_id", gameId);
                cmd.Transaction = transaction;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameId = reader.GetInt32(0);

                    }
                }

                foreach (var item in playerId)
                {
                    cmd = new NpgsqlCommand(stmt3, conn);
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("player_id", item);
                    cmd.Parameters.AddWithValue("game_id", gameId);
                    cmd.ExecuteNonQuery();
                }



                transaction.Commit();
                conn.Close();
                
            }
            catch (Exception)
            {
                
                transaction.Rollback();
                conn.Close();
                
            }
        }

        public void AbortGameTransaction()
        {
            NpgsqlTransaction transaction = null;
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {

                string[] stmts = new string[2];
                stmts[0] = "DELETE FROM game_player WHERE game_id = @game_id";
                stmts[1] = "DELETE FROM game WHERE game_id = @game_id";

                conn = new NpgsqlConnection(Connect);
                conn.Open();
                transaction = conn.BeginTransaction();

                for (int i = 0; i < stmts.Length; i++)
                {
                    cmd = new NpgsqlCommand(stmts[i], conn);
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("game_id", gameId);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                conn.Close();

            }
            catch (Exception)
            {

                transaction.Rollback();
                conn.Close();

            }
        }


        #endregion
    }
}
