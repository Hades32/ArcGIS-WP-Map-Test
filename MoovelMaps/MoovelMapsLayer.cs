using Esri.ArcGISRuntime.Layers;

namespace MoovelMaps
{
    public sealed class MoovelMapsLayer : WebTiledLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Esri.ArcGISRuntime.Layers.OpenStreetMapLayer" /> class.
        /// </summary>
        public MoovelMapsLayer()
        {
            // http://tiles.maps.moovel.com/tiles/00112233445566778899aabbccddeeff/2/13/4300/2822
            // base.TemplateUri = "http://{subDomain}.tile.openstreetmap.org/{level}/{col}/{row}";
            // base.CopyrightText = "Map data © OpenStreetMap contributors, CC-BY-SA";
            // base.SubDomains = new String[] { "a", "b", "c" };
            base.TemplateUri = "http://tiles.maps.moovel.com/tiles/00112233445566778899aabbccddeeff/2/{level}/{col}/{row}";
        }
    }
}
