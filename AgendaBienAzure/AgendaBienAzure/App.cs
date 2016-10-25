using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace AgendaBienAzure
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MobileServiceClient client;

            IMobileServiceTable<AgendaAzure> tabla;

            client = new MobileServiceClient(Conexion.conexion);

            tabla = client.GetTable<AgendaAzure>();

            Label titulo = new Label()
            {
                Text = "Insertar datos:",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                TextColor = Color.Yellow,
                 HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };


            Entry nombre1 = new Entry { Placeholder = "NOMBRE DE USUARIO" };
            nombre1.TextColor = Color.Blue;
            nombre1.BackgroundColor = Color.FromHex("#FFEF00");
           
            Entry apellido1 = new Entry { Placeholder = "APELLIDO DE USUARIO" };
            apellido1.TextColor = Color.Blue;
            
            apellido1.BackgroundColor = Color.FromHex("#FFEF00");
            Entry telefono1 = new Entry { Placeholder = "TELEFONO DE USUARIO" };
            telefono1.TextColor = Color.Blue;
            

            telefono1.BackgroundColor = Color.FromHex("#FFEF00");
            Button enviar = new Button()
            {          
                Text = "   ENVIAR DATOS    ",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Button leer = new Button()
            {          
                Text = "   MOSTRAR DATOS   ",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            ListView lista = new ListView();
            ListView lista2 = new ListView();
            leer.Clicked += async (sender, args) =>
            {
                IEnumerable<AgendaAzure> items = await tabla
    .ToEnumerableAsync();
                string[] arreglo = new string[items.Count()];
                string[] arreglo2 = new string[items.Count()];
                int i = 0;
                foreach (var x in items)
                {
                    arreglo[i] = x.Name;
                    arreglo2[i] = x.Lastname;
                    i++;
                }
                lista.ItemsSource = arreglo;
                lista2.ItemsSource = arreglo2;
            };
            enviar.Clicked += async (sender, args) =>
            {
                var datos = new AgendaAzure { Name = nombre1.Text, Lastname = apellido1.Text, Cellphone = telefono1.Text };
                await tabla.InsertAsync(datos);
                IEnumerable<AgendaAzure> items = await tabla
    .ToEnumerableAsync();
                string[] arreglo = new string[items.Count()];
                string[] arreglo2 = new string[items.Count()];
                int i = 0;
                foreach (var x in items)
                {
                    arreglo[i] = x.Name;
                    arreglo2[i] = x.Lastname;
                    i++;
                }
                lista.ItemsSource = arreglo;
                lista2.ItemsSource = arreglo2;
            };
            Button eliminar = new Button()
            {
                      
                Text = "  ELIMINAR DATOS   ",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            eliminar.Clicked += async (sender, args) =>
            {
                IEnumerable<AgendaAzure> items = await tabla
    .ToEnumerableAsync();
                string[] arreglo = new string[items.Count()];
                string[] arreglo2 = new string[items.Count()];
                string[] ids = new string[items.Count()];
                string[] arreglo3 = new string[items.Count()];
                int i = 0;
                foreach (var x in items)
                {
                    arreglo[i] = x.Name;
                    arreglo2[i] = x.Lastname;
                    ids[i] = x.Id;
                    arreglo3[i] = x.Cellphone;
                    if (x.Cellphone == telefono1.Text)
                    {
                        if (x.Name != nombre1.Text)
                        {
                            x.Name = nombre1.Text;
                        }
                        if (x.Lastname != apellido1.Text)
                        {
                            x.Lastname = apellido1.Text;
                        }
                        await tabla.DeleteAsync(x);
                    }
                    i++;
                }
                lista.ItemsSource = arreglo;
                lista2.ItemsSource = arreglo2;
            };
            Button actualizar = new Button()
            {

                Text = "  ACTUALIZAR DATOS ",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            actualizar.Clicked += async (sender, args) =>
            {
                IEnumerable<AgendaAzure> items = await tabla
    .ToEnumerableAsync();
                string[] arreglo = new string[items.Count()];
                string[] arreglo2 = new string[items.Count()];
                string[] ids = new string[items.Count()];
                string[] arreglo3 = new string[items.Count()];
                int i = 0;
                foreach (var x in items)
                {
                    arreglo[i] = x.Name;
                    arreglo2[i] = x.Lastname;
                    ids[i] = x.Id;
                    arreglo3[i] = x.Cellphone;
                    if (x.Cellphone == telefono1.Text)
                    {
                        if (x.Name != nombre1.Text)
                        {
                            x.Name = nombre1.Text;
                        }
                        if (x.Lastname != apellido1.Text)
                        {
                            x.Lastname = apellido1.Text;
                        }
                        await tabla.UpdateAsync(x);
                    }
                    i++;
                }
                lista.ItemsSource = arreglo;
                lista2.ItemsSource = arreglo2;
            };
            var layout = new StackLayout();
            layout.Children.Add(titulo);
            layout.Children.Add(nombre1);
            layout.Children.Add(apellido1);
            layout.Children.Add(telefono1);
            layout.Children.Add(enviar);
            layout.Children.Add(leer);
            layout.Children.Add(eliminar);
            layout.Children.Add(actualizar);
            layout.Children.Add(lista);
            layout.Children.Add(lista2);
            MainPage = new ContentPage
            {
                Content = layout
            };

        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
