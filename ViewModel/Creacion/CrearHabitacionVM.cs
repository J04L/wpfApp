using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace app_wpf
{ class CrearHabitacionVM: INotifyDataErrorInfo, IDataErrorInfo
    {
        
        private int _numero;
        private int _capacidadAdultos;
        private int _capacidadNinos;
        private string _descripcion;
        private int _foto;
        private int _camasDobles;
        private int _camasIndividuales;
        private string _tipoHabitacion;
        private double _precio;
        private int _piso;
        private int _dimesiones;

        public int Dimensiones
        {
            get
            {
                return _dimesiones;
            }
            set
            {
                _dimesiones = value;
                OnPropertyChanged(nameof(Dimensiones));
            }
        }

        public int Piso
        {
            get
            {
                return _piso;
            }
            set
            {
                _piso = value;
                OnPropertyChanged(nameof(Piso));
            }
        }
        
        public int Numero
        {
            get
            {
                return _numero;
            }
            set
            {
                _numero = value;
                OnPropertyChanged(nameof(Numero));
            }
        }
        public int CapacidadAdultos
        {
            get
            {
                return _capacidadAdultos;
            }
            set
            {
                _capacidadAdultos = value;
                OnPropertyChanged(nameof(CapacidadAdultos));
            }
        }
        public int CapacidadNinos
        {
            get
            {
                return _capacidadNinos;
            }
            set
            {
                _capacidadNinos = value;
                OnPropertyChanged(nameof(CapacidadNinos));
            }
        }
        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
                OnPropertyChanged(nameof(Descripcion));
            }
        }
        public int Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                _foto = value;
                OnPropertyChanged(nameof(Foto));
            }
        }
        public int CamasDobles
        {
            get
            {
                return _camasDobles;
            }
            set
            {
                _camasDobles = value;
                OnPropertyChanged(nameof(CamasDobles));
            }
        }
        public int CamasIndividuales
        {
            get
            {
                return _camasIndividuales;
            }
            set
            {
                _camasIndividuales = value;
                OnPropertyChanged(nameof(CamasIndividuales));
            }
        }
        public string TipoHabitacion
        {
            get
            {
                return _tipoHabitacion;
            }
            set
            {
                _tipoHabitacion = value;
                OnPropertyChanged(nameof(TipoHabitacion));
                Precio = CrearHabitacion.tipoHabitacionesDictionary[value];
            }
        }
        public double Precio
        {
            get
            {
                return _precio;
            }
            set
            {
                _precio = value;
                OnPropertyChanged(nameof(Precio));
            }
        }
        public string Error { get; }

        public string this[string name]
        {
            get
            {
                switch (name)
                {
                    case "Numero":
                        if (Numero > 500)
                        {
                            return "El numero de habitación máximo es de 500";
                        }
                        else if (Numero < 0)
                        {
                            return "El numero no puede ser menor que 0";
                        }
                        break;
                    case "Precio":
                        if (CrearHabitacion.tipoHabitacionesDictionary[TipoHabitacion] > Precio)
                        {
                            return $"El precio base de esta habitación es de {CrearHabitacion.tipoHabitacionesDictionary[TipoHabitacion]}";
                        }
                        break;
                    case "CapacidadAdultos":
                        if (CapacidadAdultos > 10)
                            return "La capacidad máxima de adultos es de 10";
                        else if (CapacidadAdultos < 0)
                            return "La capacidad de adultos no puede ser menor que 0";
                        break;

                    case "CapacidadNinos":
                        if (CapacidadNinos > 10)
                            return "La capacidad máxima de niños es de 10";
                        else if (CapacidadNinos < 0)
                            return "La capacidad de niños no puede ser menor que 0";
                        break;

                    case "Descripcion":
                        if (Descripcion == "") return "La descripción no puede estar vacía";// Descripción debería ser un string, no un int.
                        break;

                    case "CamasDobles":
                        if (CamasDobles > 5)
                            return "El número máximo de camas dobles es de 5";
                        else if (CamasDobles < 0)
                            return "El número de camas dobles no puede ser menor que 0";
                        break;

                    case "CamasIndividuales":
                        if (CamasIndividuales > 10)
                            return "El número máximo de camas individuales es de 10";
                        else if (CamasIndividuales < 0)
                            return "El número de camas individuales no puede ser menor que 0";
                        break;

                    case "Piso":
                        if (Piso > 20)
                            return "El piso máximo permitido es 20";
                        else if (Piso < 0)
                            return "El número de piso no puede ser negativo";
                        break;

                    case "Dimensiones":
                        if (Dimensiones > 100)
                            return "Las dimensiones máximas permitidas son de 100 m²";
                        else if (Dimensiones < 10)
                            return "Las dimensiones mínimas permitidas son de 10 m²";
                        break;

                    default:
                        return "Propiedad no reconocida";
                }

                return null;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            Console.WriteLine($"{propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            throw new NotImplementedException();
        }

        public bool HasErrors { get; }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    }
}
