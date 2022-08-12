using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;

namespace GmapNetFramework
{
    internal class Markers: Form1
    {
        public static void ShowMarkers(System.Windows.Forms.TextBox textBox1, System.Windows.Forms.TextBox textBox2, GMapControl gMapControl1, List<Item> items)
        {
            SQLConnection markerdb = new SQLConnection();
            markerdb.OpenConnection();
            markerdb.GetItems(items);
            gMapControl1.ShowCenter = false;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            double lat = Convert.ToDouble(textBox2.Text);
            double lng = Convert.ToDouble(textBox1.Text);
            gMapControl1.Position = new PointLatLng(lat, lng);

            gMapControl1.MaxZoom = 30;
            gMapControl1.MinZoom = 3;
            gMapControl1.Zoom = 4;

            //Вид маркера
            gMapControl1.Overlays.Clear();
            GMapOverlay markers = new GMapOverlay("markers");
            foreach (var item in items)
            {
                PointLatLng markerPoint = new PointLatLng(item.lat, item.lng);
                GMapMarker marker = new GMarkerGoogle(markerPoint, GMarkerGoogleType.purple);
                marker.Tag = item.Id;

                //Текст отображаемый при наведении на маркер.
                marker.ToolTipText = ($"{item.Name}\n{item.lng}  {item.lat}");
                markers.Markers.Add(marker);
            }
            gMapControl1.Overlays.Add(markers);
            gMapControl1.Zoom++;
            gMapControl1.Zoom--;
            markerdb.CloseConnection();
        }

        public static void ShowMap(GMapControl gMapControl1, double lat, double lng)
        {
            gMapControl1.ShowCenter = false;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(lat, lng);
            gMapControl1.MaxZoom = 30;
            gMapControl1.MinZoom = 1;
            gMapControl1.Zoom = 2;
        }

        public static void NewMarkerPosition(GMapMarker selectedMarker, List<Item> items)
        {
            Item item = items.Find(a => a.Id == (int)selectedMarker.Tag);
            item.lng = selectedMarker.Position.Lng; 
            item.lat = selectedMarker.Position.Lat;
            SQLConnection markerdb = new SQLConnection();
            markerdb.OpenConnection();
            markerdb.SaveChange(item);
            markerdb.CloseConnection();

            Console.WriteLine(($"{item.Name}\n{item.lng}  {item.lat}"));
            selectedMarker.ToolTipText = ($"{item.Name}\n{item.lng}  {item.lat}");
        }
    }
}
