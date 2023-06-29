﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace WindowsForms_ConsumeAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Credenciales
        {
            public string key { get; set; }
            public string secret { get; set; }

        }

        public class Respuesta 
        {
            public string token { get; set; }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var cliente = new HttpClient();

            Credenciales oUsuaario = new Credenciales() { key = "LKCQUXEZQV", secret = "KFQCXKNQPHWCEVZMJKAX" };

             //se serializa el objeto Usuario                    //contenido, encoding, tipo 
            var content = new StringContent(JsonConvert.SerializeObject(oUsuaario), Encoding.UTF8, "application/json");
            //aquí se  ejecuta la API
            var response = await cliente.PostAsync("https://apiv5.dev-ridivi.com/v5/auth/token", content);
            //SE OBTIENE LA RESPUESTA
            var json_respuesta = await response.Content.ReadAsStringAsync();
            //SE DESEREALIZA LA RESPUESTA Y SE CONVIERTE EN OBJETO
            var orespuesta = JsonConvert.DeserializeObject<Respuesta>(json_respuesta);

            var cliente2 = new HttpClient();
            //se la pasa el token al segundo cliente que va a consumir algún método del API
            cliente2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", orespuesta.token);

            var content2 = new StringContent(JsonConvert.SerializeObject(orespuesta), Encoding.UTF8, "application/json");

            var response2 = await cliente2.PostAsync("https://apiv5.dev-ridivi.com/v5/auth/checkToken", content2);

            var json_respuesta2 = await response2.Content.ReadAsStringAsync();

        }
    }
}
