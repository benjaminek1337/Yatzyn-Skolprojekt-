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
                    cmd.CommandText = "SELECT * FROM player WHERE firstname is not null ORDER BY firstname ASC ";


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

        public IEnumerable<Player> GetWinStreak()
        {
            Player p;
            Game g;

            ObservableCollection<Player> allplayers = new ObservableCollection<Player>();
            ObservableCollection<Player> pointplayers = new ObservableCollection<Player>();
            ObservableCollection<Game> games = new ObservableCollection<Game>();

            allplayers = GetPlayers();

            NpgsqlTransaction transaction = null;
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {
                string[] stmts = new string[2];
                stmts[0] = "SELECT game_id, MAX (score) FROM game_player WHERE score is not null GROUP BY game_id order by game_id desc";
                stmts[1] = "SELECT gp.game_id, p.nickname, gp.score FROM game_player gp " +
                           "inner join player p " +
                           "on p.player_id = gp.player_id  WHERE gp.score is not null " +
                           "order by gp.game_id desc";

                conn = new NpgsqlConnection(Connect);
                conn.Open();
                transaction = conn.BeginTransaction();

                cmd = new NpgsqlCommand(stmts[0], conn);
                cmd.Transaction = transaction;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        g = new Game()
                        {
                            GameId = reader.GetInt32(0),
                            HighestScore = reader.GetInt32(1)

                        };

                        games.Add(g);

                    }
                }

                cmd = new NpgsqlCommand(stmts[1], conn);
                cmd.Transaction = transaction;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {


                        p = new Player()
                        {
                            GameId = reader.GetInt32(0),
                            Nickname = reader.GetString(1),
                            HighScore = reader.GetInt32(2)

                        };

                        pointplayers.Add(p);

                    }
                }

                foreach (var player in allplayers)
                {
                    player.GamesInARow = 0;
                    foreach (var game in games)
                    {
                        bool flag = false;
                        foreach (var pointplayer in pointplayers)
                        {

                            if (pointplayer.Nickname == player.Nickname)
                            {
                                if (game.GameId == pointplayer.GameId)
                                {
                                    if (pointplayer.HighScore == game.HighestScore)
                                    {

                                        player.GamesInARow++;
                                    }
                                    else
                                    {
                                        flag = true;
                                        break;
                                    }

                                }
                            }

                        }
                        if (flag == true)
                        {
                            break;
                        }

                    }
                }


                transaction.Commit();
                conn.Close();

                int rank = 1;

                var sortedList = allplayers.OrderByDescending(x => x.GamesInARow).Take(5);
                foreach (var sortedPlayer in sortedList)
                {
                    sortedPlayer.Rank = rank++;
                }


                return sortedList;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                transaction.Rollback();
                conn.Close();
                return null;
            }


        }

        private int? CheckInt (NpgsqlDataReader reader, int value)
        {
            if (reader[value] != DBNull.Value)
            {
                
                return reader.GetInt32(value);
            }
            else
            {
 
                return null;
            }

        }


        public ObservableCollection<Player> GetAvaliablePlayers()
        {
            Player p;
            Game g;

            ObservableCollection<Game> games = new ObservableCollection<Game>();
            ObservableCollection<Player> players = new ObservableCollection<Player>();
            ObservableCollection<Player> newPlayers = new ObservableCollection<Player>();

            NpgsqlTransaction transaction = null;
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {
                
                string stmt = "SELECT * FROM player ORDER BY firstname ASC";
                string stmt1 = "SELECT * FROM game_player ORDER BY game_id desc";
                conn = new NpgsqlConnection(Connect);
                conn.Open();
                transaction = conn.BeginTransaction();

                    cmd = new NpgsqlCommand(stmt, conn);
                    cmd.Transaction = transaction;
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
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

                cmd = new NpgsqlCommand(stmt1, conn);
                cmd.Transaction = transaction;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {


                        g = new Game()
                        {

                            GameId = reader.GetInt32(0),
                            Player_Id = reader.GetInt32(1),
                            HighestScore = CheckInt(reader, 2)
                        };
                        
                        games.Add(g);

                    }
                }

                for (int i = 0; i < players.Count; i++)
                {
                    bool flag = false;
                    foreach (var game in games)
                    {
                        if (players[i].PlayerId == game.Player_Id && game.HighestScore == null)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (flag == false)
                    {
                        newPlayers.Add(players[i]);
                    }

                }


                transaction.Commit();
                conn.Close();
                return newPlayers;
            }
            catch (PostgresException error)
            {
                MessageBox.Show(error.ToString());
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
                string stmt = " select rnk, nickname, firstname, lastname, s" +
                              " from(select nickname, firstname, lastname, sum(game_player.score) as s," +
                              " rank() over(order by sum(game_player.score) desc) as rnk" +
                              " from " +
                              " ((player inner join game_player on player.player_id = game_player.player_id)" +
                              " inner join game on game_player.game_id = game.game_id)" +
                              " where game.ended_at >= current_timestamp - interval '7 days' and game.gametype_id = @gametype and game_player.score is not null" +
                              " group by player.nickname, player.firstname, player.lastname" +
                              " order by s desc) t1" +
                              " where  rnk <= 5";
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
                                Rank = reader.GetInt32(0),
                                Nickname = reader.GetString(1),
                                Firstname = reader.GetString(2),
                                Lastname = reader.GetString(3),
                                HighScore = reader.GetInt32(4)
                            };

                            players.Add(p);

                        }
                    }
                conn.Close();
                
                return players;
            }
            catch (Exception error )
            {
                MessageBox.Show(error.ToString());
                conn.Close();
                return null;
            }
        }

        public ObservableCollection<Player> GetHighestGames(int gametype)
        {
            Player p;

            ObservableCollection<Player> players = new ObservableCollection<Player>();

           
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;
            try
            {

                string stmt = " select rnk, nickname, firstname, lastname, gamecount" +
                              " from(select nickname, firstname, lastname, count(game) as gamecount," +
                              " rank() over(order by count(game) desc) as rnk" +
                              " from((player" +
                              " inner join game_player on player.player_id = game_player.player_id)" +
                              " inner join game on game_player.game_id = game.game_id)" +
                              " group by player.nickname, player.firstname, player.lastname" +
                              " order by gamecount desc) as t1" +
                              " where rnk <= 5";
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
                            Rank = reader.GetInt32(0),
                            Nickname = reader.GetString(1),
                            Firstname = reader.GetString(2),
                            Lastname = reader.GetString(3),
                            HighScore = reader.GetInt32(4)
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
                string stmt3 = "INSERT INTO game_player (player_id, game_id) VALUES (@player_id , @game_id)";

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
                    item.GameId = gameId;
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


        public void AbortGameTransaction(int gameId)
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

            gameId = players[0].GameId;
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
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
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

                string stmt = "insert into player(firstname, nickname, lastname) values(@fname,@nname,@lname)";
                

                conn = new NpgsqlConnection(Connect);
                conn.Open();            

                    cmd = new NpgsqlCommand(stmt, conn);
                 
                    cmd.Parameters.AddWithValue("fname", player.Firstname);
                    cmd.Parameters.AddWithValue("nname", player.Nickname);
                    cmd.Parameters.AddWithValue("lname", player.Lastname);
                    cmd.ExecuteNonQuery();
                          
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
