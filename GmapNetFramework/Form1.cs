using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GmapNetFramework
{
    public partial class Form1 : Form
    {
        private GMapMarker selectedMarker;
        List<Item> items = new List<Item>();
        public Form1()
        {
            InitializeComponent();

            //подписка на события мыши
            gMapControl1.MouseUp += gMapControl1_MouseUp;
            gMapControl1.MouseDown += gMapControl1_MouseDown;
            Markers.ShowMap(gMapControl1, 55.4, 83.7);
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                Markers.ShowMarkers(textBox1, textBox2, gMapControl1, items);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void gMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            //находим тот маркер над которым нажали клавишу мыши
            selectedMarker = gMapControl1.Overlays
                .SelectMany(o => o.Markers)
                .FirstOrDefault(m => m.IsMouseOver == true);
        }

        private void gMapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedMarker is null)
                return;

            //переводим координаты курсора мыши в долготу и широту на карте
            var latlng = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            //присваиваем новую позицию для маркера
            selectedMarker.Position = latlng;
            Markers.NewMarkerPosition(selectedMarker, items);
            selectedMarker = null;
        }


    }
}
