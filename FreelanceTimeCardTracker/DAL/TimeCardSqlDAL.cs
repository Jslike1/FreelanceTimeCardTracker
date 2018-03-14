using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreelanceTimeCardTracker.DAL;
using FreelanceTimeCardTracker.Models;
using System.Data.SqlClient;

namespace FreelanceTimeCardTracker.DAL
{
    public class TimeCardSqlDAL : ITimeCardDAL
    {

        public string ConnectionString { get; set; }

        public double ServerTimeZoneModifier = 60;

        public TimeCardSqlDAL(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public TimeCard GetTimeCard(string username)
        {
            TimeCard timeCard = new TimeCard();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT TOP 1 timecard.id, timecard.project, timecard.start_datetime, timecard.end_datetime, timecard.notes FROM timecard WHERE user_name = @username ORDER BY id DESC;", conn);
                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        timeCard.ID = Convert.ToInt32(reader["id"]);
                        timeCard.Username = username;
                        timeCard.Project = Convert.ToString(reader["project"]);
                        timeCard.Start_DateTime = Convert.ToDateTime(reader["start_datetime"]);
                        if (reader["end_datetime"] == DBNull.Value)
                        {
                            timeCard.End_DateTime = null;
                        }
                        else
                        {
                            timeCard.End_DateTime = Convert.ToDateTime(reader["end_datetime"]);
                        }
                        timeCard.Notes = Convert.ToString(reader["notes"]);
                    }
                }
            }

            if (timeCard.End_DateTime != null)
            {
                return new TimeCard();
            }

            return timeCard;
        }

        public List<TimeCard> GetUserTimeCards(string username)
        {
            List<TimeCard> result = new List<TimeCard>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM timecard WHERE user_name = @username ORDER BY id DESC", conn);

                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    TimeCard temp = new TimeCard();

                    temp.ID = Convert.ToInt32(reader["id"]);
                    temp.Username = Convert.ToString(reader["user_name"]);
                    temp.Project = Convert.ToString(reader["project"]);
                    temp.Start_DateTime = Convert.ToDateTime(reader["start_datetime"]);
                    if (reader["end_datetime"] == DBNull.Value)
                    {
                        temp.End_DateTime = null;
                    }
                    else
                    {
                        temp.End_DateTime = Convert.ToDateTime(reader["end_datetime"]);
                    }
                    temp.Notes = Convert.ToString(reader["notes"]);

                    result.Add(temp);
                }
            }

            return result;
        }


        public void PunchIn(TimeCard timeCard)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"insert into TIMECARD (user_name, start_datetime) values (@username, @start_datetime);", conn);
                cmd.Parameters.AddWithValue("@username", timeCard.Username);
                cmd.Parameters.AddWithValue("@start_datetime", DateTime.Now.ToUniversalTime());

                cmd.ExecuteNonQuery();
            }
        }

        public void PunchOut(TimeCard timeCard)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"UPDATE timecard SET project = @project, end_datetime = @endtime, notes = @notes WHERE id = @id;", conn);

                cmd.Parameters.AddWithValue("@id", timeCard.ID);
                cmd.Parameters.AddWithValue("@project", timeCard.Project);
                cmd.Parameters.AddWithValue("@endtime", DateTime.Now.ToUniversalTime());
                cmd.Parameters.AddWithValue("@notes", timeCard.Notes);

                cmd.ExecuteNonQuery();
            }
        }
    }
}