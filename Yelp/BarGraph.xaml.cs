using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.DataVisualization;
using Npgsql;

namespace Yelp
{
    using System.ComponentModel;
    using System.Xml.Serialization;
    /// <summary>
    /// Interaction logic for BarGraph.xaml
    /// </summary>
    public partial class BarGraph : INotifyPropertyChanged
    {
        Business business = new Business();

        GraphContents graphContents = new GraphContents();

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public BarGraph(Business business)
        {
            this.business = business;
            InitializeComponent();
            barGraph();
        }


        public void barGraph()
        {
            List<KeyValuePair<string, int>> graphData = new List<KeyValuePair<string, int>>();
            graphData = ExecuteQuery(business.businessID);
            checkinGraph.DataContext = graphData;
        }

        /// <summary>
        /// Connect to database
        /// </summary>
        /// <returns></returns>
        private string BuildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = 'PUT DB NAME HERE'; password = 'PUT PASSWORD HERE'";
        }


        /// <summary>
        /// Get the specified query from the database.
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="myf"></param>
        public List<KeyValuePair<string, int>> ExecuteQuery(string bid)
        {
            string checkinQuery = "SELECT month, count(checkin) FROM businesstable INNER JOIN checkin ON businesstable.businessid = checkin.businessid WHERE businesstable.businessid = '" + bid + "' GROUP BY month ORDER BY month ASC";
            List<KeyValuePair<string, int>> graphData = new List<KeyValuePair<string, int>>();

            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = checkinQuery;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())

                            graphData.Add(new KeyValuePair<string, int>(graphContents.GetMonth(reader.GetInt32(0)).ToString(), reader.GetInt32(1)));

                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return graphData;
        }

        /// <summary>
        /// Get the specified query from the database.
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="myf"></param>
        public void ExecuteUpdateQuery(string sqlstr)
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlstr;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

        private void checkIn_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            now.ToString("HH:mm:ss");
            string hour = "";
            string min = "";
            string sec = "";
            if (now.Hour < 10)
            {
                hour = "0" + now.Hour.ToString() + ":";
            }
            else
            {
                hour = now.Hour.ToString() + ":";
            }
            if (now.Minute < 10)
            {
                min = "0" + now.Minute.ToString() + ":";
            }
            else
            {
                min = now.Minute.ToString() + ":";
            }
            if (now.Second < 10)
            {
                sec = "0" + now.Second.ToString();
            }
            else
            {
                sec = now.Second.ToString();
            }
            string time = hour + min + sec;
            string sqlInsert = "INSERT INTO checkin VALUES('" + business.businessID + "', '" + Convert.ToInt32(now.Year) + "', '" + Convert.ToInt32(now.Month) + "', '" + Convert.ToInt32(now.Month) + "', '" + time + "')";
            ExecuteUpdateQuery(sqlInsert);
            barGraph();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs("BarGraphClosed"));
        }
    }
}
