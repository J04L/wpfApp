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
using app_wpf.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace app_wpf
{ 
    class EditarHabitacionVM: INotifyPropertyChanged,INotifyDataErrorInfo, IDataErrorInfo
    {
        private int _numero;
        private int _capacidadAdultos;
        private int _capacidadNinos;
        private string _descripcion;
        private string _foto;
        private int _camasDobles;
        private int _camasIndividuales;
        private string _tipoHabitacion;
        private double _precio;
        private int _piso;
        private double _dimensiones;
        public List<TipoHabitacion> ListaTipoHabitaciones;
        
        public double Dimensiones
        {
            get
            {
                return _dimensiones;
            }
            set
            {
                _dimensiones = value;
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
                Console.WriteLine($"Nuevo valor de Numero: {_numero}");
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
        public string Foto
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
                Precio = ListaTipoHabitaciones[
                    ListaTipoHabitaciones.FindIndex(tipo => tipo.NombreTipoHabitacion == _tipoHabitacion)].PrecioBase;
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
                        if (ListaTipoHabitaciones[
                                ListaTipoHabitaciones.FindIndex(tipo => tipo.NombreTipoHabitacion == _tipoHabitacion)].PrecioBase > Precio)
                        {
                            return $"El precio base de esta habitación es de {ListaTipoHabitaciones[
                                ListaTipoHabitaciones.FindIndex(tipo => tipo.NombreTipoHabitacion == _tipoHabitacion)].PrecioBase}";
                        }
                        break;
                    case "CapacidadAdultos":
                        if (CapacidadAdultos <= 0)
                            return "La capacidad de adultos no puede ser menor o igual que 0";
                        break;

                    case "CapacidadNinos":
                        if (CapacidadNinos < 0)
                            return "La capacidad de niños no puede ser menor que 0";
                        break;

                    case "Descripcion":
                        if (Descripcion == "") return "La descripción no puede estar vacía";// Descripción debería ser un string, no un int.
                        break;

                    case "CamasDobles":
                        if (CamasDobles < 0)
                            return "El número de camas dobles no puede ser menor que 0";
                        break;

                    case "CamasIndividuales":
                        if (CamasIndividuales < 0)
                            return "El número de camas individuales no puede ser menor que 0";
                        break;

                    case "Piso":
                        if (Piso < 0)
                            return "El número de piso no puede ser negativo";
                        break;

                    case "Dimensiones":
                        if (Dimensiones < 0)
                            return "Las dimensiones no pueden ser menores que 0m²";
                        break;
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

        public Habitacion GetHabitacion()
        {
            return new Habitacion(
                _numero,
                ListaTipoHabitaciones.Find( tipo => tipo.NombreTipoHabitacion == _tipoHabitacion),
                new Capacidad(_capacidadAdultos, _capacidadNinos),
                _descripcion,
                _precio,
                MainWindow.Habitaciones.Find( habitacion => habitacion.NumeroHabitacion == Numero).Fotos,
                new Camas(_camasDobles, _camasIndividuales),
                _dimensiones,
                true,
                _piso
                );
        }
    }
}
