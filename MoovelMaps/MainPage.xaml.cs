using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using System;
using System.Linq;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace MoovelMaps
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const string MOOVEL_ID = "MoovelLayer";
        const string MAPBOX_ID = "MapBoxLayer";
        private GraphicsLayer _layer;
        private PictureMarkerSymbol _c2gSymbol;
        private GraphicCollection _graphics;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (_layer == null)
            {
                _c2gSymbol = new PictureMarkerSymbol();
                var symbolStreamRef = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/ic_map_pin.png"));
                await _c2gSymbol.SetSourceAsync(await symbolStreamRef.OpenReadAsync());
                _c2gSymbol.Width = 64;
                _c2gSymbol.Height = 64;
                _c2gSymbol.YOffset = 32;

                _layer = new GraphicsLayer();
                _graphics = new GraphicCollection();
                //AddPins(_graphics, _c2gSymbol, 48.399121, 9.992409);
                _layer.GraphicsSource = _graphics;
                MyMap.Layers.Add(_layer);
            }
        }

        private static void AddPins(GraphicCollection graphics, PictureMarkerSymbol c2gSymbol, double latCenter, double lonCenter)
        {
            var rnd = new Random();
            for (int i = 0; i < 250; i++)
            {
                double rndLat = (rnd.NextDouble() - 0.5) / 10.0;
                double rndLon = (rnd.NextDouble() - 0.5) / 10.0;
                double lat = latCenter + rndLat;
                double lon = lonCenter + rndLon;
                var c2gGraphic = new Graphic(new MapPoint(lon, lat, SpatialReferences.Wgs84), c2gSymbol);
                graphics.Add(c2gGraphic);
            }
        }

        private async void MapView1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var hit = await _layer.HitTestAsync(MapView1, e.GetPosition(MapView1));
            if (hit != null)
                await new MessageDialog("hit at " + hit.Geometry.Extent).ShowAsync();
        }

        private void AddBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var center = MapView1.Extent.GetCenter();
            center = (MapPoint)GeometryEngine.Project(center, SpatialReferences.Wgs84);
            AddPins(_graphics, _c2gSymbol, center.Y, center.X);
        }

        private void MapTypeToggleBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var webLayer = MyMap.Layers.OfType<WebTiledLayer>().First();
            if (webLayer is MoovelMapsLayer)
            {
                MyMap.Layers.Remove(webLayer);
                MyMap.Layers.Insert(0, new MapBoxLayer() { ID = MAPBOX_ID });
            }
            if (webLayer is MapBoxLayer)
            {
                MyMap.Layers.Remove(webLayer);
                MyMap.Layers.Insert(0, new MoovelMapsLayer() { ID = MOOVEL_ID });
            }
        }
    }
}
