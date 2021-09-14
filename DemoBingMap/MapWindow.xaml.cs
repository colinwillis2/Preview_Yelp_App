using BingMapsRESTToolkit;
using BingMapsRESTToolkit.Extensions;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Location = Microsoft.Maps.MapControl.WPF.Location;


namespace BingMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        private string BingMapsKey = System.Configuration.ConfigurationManager.AppSettings.Get("BingMapsKey");
        public string SessionKey;
        private List<object> Obj = new List<object>();
        BuildMapLocations mapLocations = new BuildMapLocations();
        CreatePushpin createPushpin = new CreatePushpin();
        private int i;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MapWindow()
        {
            InitializeComponent();
            GetSessionKey();
        }

        /// <summary>
        /// Contructor for the Yelp project. Accepts a list of objects.
        /// </summary>
        /// <param name="obj"></param>
        public MapWindow(List<object> obj)
        {
            this.Obj = obj;
            InitializeComponent();
            GetSessionKey();
        }

        /// <summary>
        /// Creates a session key for all transactions.
        /// </summary>
        private void GetSessionKey()
        {
            MyMap.CredentialsProvider = new ApplicationIdCredentialsProvider("INSERT BING MAPS API KEY HERE");
            MyMap.CredentialsProvider.GetCredentials((c) =>
            {
                this.SessionKey = c.ApplicationId;
            });

        }

        /// <summary>
        /// Builds the maps bases on a list of objects or the search box.
        /// </summary>
        public async Task BuildMap()
        {
            MyMap.Children.Clear();
            GetSessionKey();
            createPushpin.EmptyLists();
            mapLocations.EmptyLists();
            i = 1;

            
            if (SearchBox.Text != "")
            {
                string tempString = SearchBox.Text;
                createPushpin.addressList.Add(tempString);
                createPushpin.toolTip.Add(tempString);
                var geoRequest = mapLocations.GetGeocode(SessionKey, tempString);
                await ProcessRequest(geoRequest);
            }
            else if (this.Obj.Count() != 0)
            {
                var addList = createPushpin.GetAddresses(this.Obj);
                createPushpin.GetToolTipText(this.Obj);

                foreach (var item in addList)
                {
                    var geoRequest = mapLocations.GetGeocode(SessionKey, item);
                    await ProcessRequest(geoRequest);
                }
            }

        }

        /// <summary>
        /// Process the Geocode Request. Adds locations to map with pushpins.
        /// </summary>
        /// <param name="geocodeRequest"></param>
        public async Task ProcessRequest(GeocodeRequest geocodeRequest)
        {
            var response = await geocodeRequest.Execute();
            var loc = mapLocations.GetLocation(response);

            SetMapView(loc);

        }

        /// <summary>
        /// Adds the pushpin to the map and sets the view to the location rectangle.
        /// </summary>
        /// <param name="loc"></param>
        public void SetMapView(Location loc)
        {
                MyMap.Children.Add(new Pushpin() { Location = loc, Content = i, ToolTip = createPushpin.toolTip.ElementAt(i - 1) + " - " + createPushpin.addressList.ElementAt(i - 1)});
                MyMap.SetView(mapLocations.BuildRectangle());
                i++;

        }

        /// <summary>
        /// Puts location that was entered in the search bar on the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            BuildMap();
        }

        /// <summary>
        /// Zooms map in.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            MyMap.ZoomLevel += .5;
        }

        /// <summary>
        /// Zooms map out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            MyMap.ZoomLevel -= .5;
        }
    }
}
