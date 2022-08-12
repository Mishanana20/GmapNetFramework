using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmapNetFramework
{
    /// <summary>
    /// Id/name/lng/lat
    /// </summary>
    internal class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
    }
}
