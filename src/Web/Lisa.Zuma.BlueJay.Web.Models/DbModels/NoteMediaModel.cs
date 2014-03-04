using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Web.Models.DbModels
{
    public class NoteMediaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EncodedData { get; set; }
        public string Location { get; set; }
    }
}
