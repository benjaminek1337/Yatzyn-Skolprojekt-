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

            using (var conn = new
                NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {

                    NpgsqlTransaction transaction = null;
                    string [] stmts = new string [1];
                    stmts [0] = "SELECT * FROM player";
                    stmts [1] = "SELECT * FROM players";

                try 
	            {	        
		                conn.Open();

                 using (var cmd = new NpgsqlCommand())
                 {
                    
                    cmd.Connection = conn;
                    transaction = conn.BeginTransaction();

                    for (int i = 0; i < stmts.length; i++)
			        {

			        
                       cmd.CommandText = stmts[i];

                    

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
                    transaction.Commit();
                    return players;
                    
                 }
	            }
	            catch (Exception error)
	            {
	     	     transaction.Rollback();
	            }

                finally
                {
                    if (conn.State = System.Data.ConnectionState.Open)
	                {
                        conn.Close();
	                }


                }


            }

            

            

          }


        #endregion
    }
}
