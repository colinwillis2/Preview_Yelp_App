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
    class FriendsTips
    {
        public string name { get; set; }
        public string businessName { get; set; }
        public string businessCity { get; set; }
        public string tipText { get; set; }
        public string tipDate { get; set; }

        public void Test()
        {

        }
        public void AddFriendsLastestTips(DataGrid dataGrid)
        {
            
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "User Name";
            dataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("businessName");
            col2.Header = "Business Name";
            dataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("businessCity");
            col3.Header = "City";
            dataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("tipText");
            col4.Header = "Text";
            dataGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("tipDate");
            col5.Header = "Tip Date";
            dataGrid.Columns.Add(col5);

        }
    }
}
