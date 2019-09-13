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

        //public ObservableCollection<Game> GetGame()
        //{
        //    Game g;
        //    ObservableCollection<Game> games = new ObservableCollection<Game>();

        //    NpgsqlConnection conn = null;
        //    NpgsqlCommand cmd = null;


        //    string stmt = "SELECT game_id FROM game order by game_id asc";

        //        conn = new NpgsqlConnection(Connect);
        //        conn.Open();

        //        cmd = new NpgsqlCommand(stmt, conn);
                
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                g = new Game()
        //                {
        //                    GameId = reader.GetInt32(0)
        //                };

        //                games.Add(g);

        //            }
        //        }
        //    conn.Close();
        //    return games;
        //}


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
        public void StartGameTransaction(List<Player> playerId, int gameType)
        {
            

            NpgsqlTransaction transaction = null;
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {

                string stmt1 = "INSERT INTO game (gametype) VALUES (@gametype)";
                string stmt2 = "SELECT game_id FROM game ORDER BY game_id DESC LIMIT 1";
                string stmt3 = "INSERT INTO game_player (player_id, game_id) VALUES (@player_id, @game_id)";

                cmd.Parameters.AddWithValue("gametype", gameType);
                cmd.Parameters.AddWithValue("game_id", gameId);

                conn = new NpgsqlConnection(Connect);
                conn.Open();
                transaction = conn.BeginTransaction();

                cmd = new NpgsqlCommand(stmt1, conn);
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();

                cmd = new NpgsqlCommand(stmt2, conn);
                cmd.Transaction = transaction;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameId = reader.GetInt32(0);

                    }
                }


                for (int i = 0; i < playerId.Count; i++)
                {
                    cmd.Parameters.AddWithValue("player_id", playerId[i]);
                    cmd = new NpgsqlCommand(stmt3, conn);
                    cmd.Transaction = transaction;
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
