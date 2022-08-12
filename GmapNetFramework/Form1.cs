using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using GmapNetFramework;


/// Marker
/// создать массив объектов класса Маркеры
/// В массиве изначально будут всё отношение таблицы (SELECT * FROM TABLE)
/// И после этого пока что не касаемся таблицы
/// при инициализации программы выводит карту в каком-нибудь масштабе
/// и расставляем нужные маркеры в зависимости от его имени (пример с оружием сквидварда)
///
/// После этого должна быть возможность передвинуть маркер D&D
/// по события Drag мы должны взять объект и передвинуть его
/// по событиию Drop получить новые координаты маркера, определить его id и выполнить функцию (UPDATE WHERE id=this.id)
/// и вроде всё
/// а
/// подключение к базе данных
/// string connectionString = "Server=DESKTOP-U4MN4UD\SQLEXPRESS;Database=TestTask_Gmap;Trusted_Connection=True;";
/// SqlConnectionStringBuilder


namespace GmapNetFramework
{
    public partial class Form1 : Form
    {
        private GMapMarker selectedMarker;
        List<Item> items = new List<Item>();
        // SqlConne public List<item> items = new List<item>();ctionStringBuilder connectionString = "Server=DESKTOP-U4MN4UD\SQLEXPRESS;Database=TestTask_Gmap;Trusted_Connection=True;";
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
            //Console.WriteLine((int)selectedMarker.Tag);
            Markers.NewMarkerPosition(selectedMarker, items);
            selectedMarker = null;
        }


    }
}
