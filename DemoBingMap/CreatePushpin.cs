using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingMapsRESTToolkit;
using BingMapsRESTToolkit.Extensions;
using Microsoft.Maps.MapControl.WPF;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace BingMap
{
    public class CreatePushpin
    {
        public List<string> addressList { get; }
        public List<string> toolTip { get; }

        public CreatePushpin()
        {
            this.addressList = new List<string>();
            this.toolTip = new List<string>();
        }

        /// <summary>
        /// Gets the name of the business to use as a tool tip on the map.
        /// </summary>
        /// <param name="obj"></param>
        public void GetToolTipText(List<object> obj)
        {
            for (int i = 0; i < obj.Count();)
            {
                string tempString = obj[i].ToString();
                toolTip.Add(tempString);
                i = i + 8;
            }
        }

        /// <summary>
        /// Breaks up the list of objects to get the addresses.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<string> GetAddresses(List<object> obj)
        {
            for (int i = 0; i < obj.Count();)
            {
                string tempString = obj[i + 1].ToString() + ", " + obj[i + 2].ToString() + ", " + obj[i + 3].ToString();
                addressList.Add(tempString);
                i = i + 8;
            }

            return addressList;
        }

        public void EmptyLists()
        {
            addressList.Clear();
            toolTip.Clear();
        }

    }
}
