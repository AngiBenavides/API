using API.Controller;
using API.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Toast;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace API.ViewModels
{
    public class PersonaViewModels
    {
        RESTListView apirest = new RESTListView();

        public IList PersonasList { get; set; }

        public String ID { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Anio { get; set; }
        public String Celular { get; set; }
        public String Direccion { get; set; }

        public ICommand CreateCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var response = await apirest.CreatePersona(Nombres, Apellidos, Anio, Celular, Direccion);

                    if (response.IsSuccessStatusCode)
                    {
                        CrossToastPopUp.Current.ShowToastSuccess("Persona Insertada");
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastError("No se ha insertado la persona");
                    }
                });
            }

        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var response = await apirest.GetPersonas();
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        JObject data = (JObject)JsonConvert.DeserializeObject<object>(content);
                        var personaCadena = data.Value<object>("data");
                        var personas = JsonConvert.DeserializeObject<Persona[]>(personaCadena.ToString());
                        PersonasList.Clear();
                        foreach(var item in personas)
                        {
                            PersonasList.Add(new Persona
                            {
                                Nombres = item.Nombres,
                                Apellidos = item.Apellidos,
                                Anio = item.Anio,
                                Celular = item.Celular,
                                Direccion = item.Direccion
                            });
                        }
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastError("No hay personas");
                    }
                });
            }

        }

        public PersonaViewModels()
        {
            PersonasList = new ObservableCollection<Persona>();
        }
    }
}
