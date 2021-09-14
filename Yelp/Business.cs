using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;

namespace Yelp
{
    public class Business
    {
        public List<string> tempList = new List<string>();

        public Business()
        {
            
        }


        public string businessID { get; set; }
        public string businessName { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public Int64 zipcode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public double stars { get; set; }
        public double numCheckins { get; set; }
        public double numTips { get; set; }
        public string isOpen { get; set; }

        public string open { get; set; }
        public string close { get; set; }
        public string dayOfWeek { get; set; }
        public double distance { get; set; }

        public List<string> categories = new List<string>();

        public string search { get; set; }

        /// <summary>
        /// Create business for search
        /// </summary>
        /// <param name="obj"></param>
        public void createBusiness(List<object> obj)
        {
            businessName = (string)obj[0];
            address = (string)obj[1];
            dayOfWeek = (string)obj[2];
            open =  (string)obj[3];
            close = (string)obj[4];
            businessID = (string)obj[5];
            distance = (double)obj[6];
        }

        /// <summary>
        /// Create business for owner
        /// </summary>
        /// <param name="obj"></param>
        public void createBusinessOwner(List<object> obj)
        {
            businessID = (string)obj[0];
            businessName = (string)obj[1];
            address = (string)obj[2];
            state = (string)obj[3];
            city = (string)obj[4];
            zipcode = (Int64)obj[5];
            stars = (Int64)obj[6];
            numCheckins = (Int64)obj[7];
            numTips = (Int64)obj[8];
            open = (string)obj[9];
            close = (string)obj[10];

            //dayOfWeek = (string)obj[2];
            //open = (string)obj[3];
            //close = (string)obj[4];
            //distance = (double)obj[6];
        }

        /// <summary>
        /// Add columns to 
        /// </summary>
        public void AddBusinessColumns(DataGrid dataGrid)
        {
            

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("businessName");
            col1.Header = "BusinessName";
            dataGrid.Columns.Add(col1);
            
            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("address");
            col2.Header = "Address";
            dataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            dataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("state");
            col4.Header = "State";
            dataGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("stars");
            col5.Header = "Stars";
            dataGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Binding = new Binding("numCheckins");
            col6.Header = "Total Checkins";
            dataGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Binding = new Binding("numTips");
            col7.Header = "Total Tips";
            dataGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Binding = new Binding("distance");
            col8.Header = "Distance";
            dataGrid.Columns.Add(col8);

        }

    }
}
