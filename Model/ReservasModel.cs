using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_wpf.Model
{
    public class ReservasModel
    {
        public string _id { get; set; }  // Para el campo _id de MongoDB (aunque no lo estés usando en tu aplicación, puedes ignorarlo si no lo necesitas)

        public string n_habitacion { get; set; }
        public string tipo_habitacion { get; set; }

        public DateTime f_Inicio { get; set; }
        public DateTime f_Final { get; set; }

        public int totalDias { get; set; }

        public string huespedEmail { get; set; }
        public string huespedNombre { get; set; }
        public string huespedApellidos { get; set; }
        public string huespedDni { get; set; }

        public string trabajadorEmail { get; set; }

        public int numeroHuespedes { get; set; }
        public decimal precio_noche { get; set; }
        public decimal precio_total { get; set; }

        public bool cuna { get; set; }
        public bool camaExtra { get; set; }
        public bool notificar { get; set; }
    }
}
