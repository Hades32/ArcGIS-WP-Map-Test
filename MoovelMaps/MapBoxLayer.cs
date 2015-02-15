using Esri.ArcGISRuntime.Layers;

namespace MoovelMaps
{
    public sealed class MapBoxLayer : WebTiledLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Esri.ArcGISRuntime.Layers.OpenStreetMapLayer" /> class.
        /// </summary>
        public MapBoxLayer()
        {
            // https://a.tiles.mapbox.com/v4/hades32.l753beip/attribution,zoompan,zoomwheel,geocoder,share.html?access_token=pk.eyJ1IjoiaGFkZXMzMiIsImEiOiJPaF9PZ1pZIn0.qPU97amCdbA6vwvyOl962w
            // http://api.tiles.mapbox.com/v4/{mapid}/{z}/{x}/{y}.{format}?access_token=<your access token>
            // base.TemplateUri = "http://{subDomain}.tile.openstreetmap.org/{level}/{col}/{row}";
            // base.CopyrightText = "Map data © OpenStreetMap contributors, CC-BY-SA";
            // base.SubDomains = new String[] { "a", "b", "c" };
            base.TemplateUri = "http://api.tiles.mapbox.com/v4/hades32.l753beip/{level}/{col}/{row}.png?access_token=pk.eyJ1IjoiaGFkZXMzMiIsImEiOiJPaF9PZ1pZIn0.qPU97amCdbA6vwvyOl962w";
        }
    }
}
