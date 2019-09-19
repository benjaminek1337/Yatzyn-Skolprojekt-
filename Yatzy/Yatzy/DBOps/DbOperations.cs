using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;
using System.Collections.ObjectModel;
using Yatzy.Models;
using Yatzy.GameEngine;
using System.Windows;

namespace Yatzy.DBOps
{
    class DbOperations
    {


        string Connect = ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString;
        int gameId;

        public object Messagebox { get; private set; }

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
                conn.Close();
                return players;
            }
        }

        /*public ObservableCollection<Player> GetMostWins()
        //{
        //    Player p;

        //    ObservableCollection<Player> players = new ObservableCollection<Player>();

        //    NpgsqlTransaction transaction = null;
        //    NpgsqlConnection conn = null;
        //    NpgsqlCommand cmd = null;
        //    try
        //    {
        //        string[] stmts = new string[1];
        //        stmts[0] = "SELECT * from player";
        //        stmts[1] = "SELECT * FROM game_player order by game_player.game_id desc";
        //        stmts[2] = "";

        //        conn = new NpgsqlConnection(Connect);
        //        conn.Open();
        //        transaction = conn.BeginTransaction();


        //            cmd = new NpgsqlCommand(stmts[0], conn);
        //            cmd.Transaction = transaction;
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    p = new Player()
        //                    {
        //                        Nickname = reader.GetString(2),
        //                        Firstname = reader.GetString(1),
        //                        Lastname = reader.GetString(3),
        //                        PlayerId = reader.GetInt32(0)
        //                    };

        //                players.Add(p);
        //                }
        //            }

        //        cmd = new NpgsqlCommand(stmts[0], conn);
        //        cmd.Transaction = transaction;
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                p = new Player()
        //                {
        //                    Nickname = reader.GetString(2),
        //                    Firstname = reader.GetString(1),
        //                    Lastname = reader.GetString(3),
        //                    PlayerId = reader.GetInt32(0)
        //                };

        //                players.Add(p);
        //            }
        //        }


        //        transaction.Commit();
        //        conn.Close();
        //        return players;
        //    }
        //    catch (Exception)
        //    {

        //        transaction.Rollback();
        //        conn.Close();
        //        return null;
        //    }





        //}*/

        public ObservableCollection<Player> GetAvaliablePlayers()
        {
            Player p;

            ObservableCollection<Player> players = new ObservableCollection<Player>();

            NpgsqlTransaction transaction = null;
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {
                
                string stmt = "SELECT * FROM player WHERE EXISTS ( SELECT score FROM game_player WHERE score IS NOT null)";

                conn = new NpgsqlConnection(Connect);
                conn.Open();
                transaction = conn.BeginTransaction();

                    cmd = new NpgsqlCommand(stmt, conn);
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
                                PlayerId = reader.GetInt32(0)
                            };

                            players.Add(p);

                        }
                    }
                transaction.Commit();
                conn.Close();
                return players;
            }
            catch (PostgresException ex)
            {
                transaction.Rollback();
                conn.Close();
                return null;
            }
        }

        public ObservableCollection<Player> GetHighScorePlayers(int gametype)
        {
            Player p;

            ObservableCollection<Player> players = new ObservableCollection<Player>();

            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {
                string stmt = "select nickname, firstname, lastname, sum (game_player.score) from((player " +
                                "inner join game_player on player.player_id = game_player.player_id) " +
                               "inner join game on game_player.game_id = game.game_id) " +
	                          "where game.ended_at >= current_timestamp - interval '7 days' and game.gametype_id = @gametype " +
                              "group by player.nickname, player.firstname, player.lastname "+
                               "order by sum desc ";
                conn = new NpgsqlConnection(Connect);
                conn.Open();
  
                    cmd = new NpgsqlCommand(stmt, conn);
                cmd.Parameters.AddWithValue("gametype", gametype);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p = new Player()
                            {
                                Nickname = reader.GetString(0),
                                Firstname = reader.GetString(1),
                                Lastname = reader.GetString(2),
                                HighScore = reader.GetInt32(3)
                            };

                            players.Add(p);

                        }
                    }
                conn.Close();
                return players;
            }
            catch (Exception)
            {
                conn.Close();
                return null;
            }
        }



        #endregion


        #region Metoder som skriver data

        public void StartGameTransaction(ObservableCollection<Player> players, int gameType)
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

                foreach (var item in players)
                {
                    cmd = new NpgsqlCommand(stmt3, conn);
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("player_id", item.PlayerId);
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


        public void SaveGameTransaction (ObservableCollection<Player> players)
        {
            

            NpgsqlTransaction transaction = null;
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {
                
                string stmt1 = "UPDATE game_player SET score = @totalscore WHERE game_id = @game_id AND player_id = @player_id";
                string stmt2 = "UPDATE game SET ended_at = @now WHERE game_id = @game_id";

                conn = new NpgsqlConnection(Connect);
                conn.Open();
                transaction = conn.BeginTransaction();

                foreach (var item in players)
                {
                    cmd = new NpgsqlCommand(stmt1, conn);
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("player_id", item.PlayerId);
                    cmd.Parameters.AddWithValue("game_id", gameId);
                    cmd.Parameters.AddWithValue("totalscore", item.TotalScore);
                    cmd.ExecuteNonQuery();
                }

                cmd = new NpgsqlCommand(stmt2, conn);
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("game_id", gameId);
                cmd.Parameters.AddWithValue("now", DateTime.Now);
                cmd.ExecuteNonQuery();


                transaction.Commit();
                conn.Close();

            }
            catch (Exception)
            {

                transaction.Rollback();
                conn.Close();

            }



        }


        public void RegisterPlayer(Player player)
        {
            
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {

                string stmts = "insert into player(firstname, nickname, lastname) values(@fname,@nname,@lname)";
                

                conn = new NpgsqlConnection(Connect);
                conn.Open();            

                for (int i = 0; i < stmts.Length; i++)
                {
                    cmd = new NpgsqlCommand(stmts, conn);
                    //cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("fname", player.Firstname);
                    cmd.Parameters.AddWithValue("nname", player.Nickname);
                    cmd.Parameters.AddWithValue("lname", player.Lastname);
                    cmd.ExecuteNonQuery();
                }               
                conn.Close();

            }
            catch (Exception)
            {              
                conn.Close();
            }
        }

        #endregion
    }
}
