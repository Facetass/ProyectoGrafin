using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ProyectoGrafo;
using Newtonsoft.Json;
using System.Web.UI;
using System.Linq;


namespace WebGrafo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Grafo graf1 = null;
        string mensaje = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Carga de la página por primera vez
                graf1 = new Grafo();
                Session["graf1"] = graf1;
            }
            else
            {
                // Recuperación del grafo desde la sesión en caso de postback
                graf1 = (Grafo)Session["graf1"];
            }

            string graphJson = ConvertGraphToJson();
            ClientScript.RegisterStartupScript(this.GetType(), "graphData", "var graphData = " + graphJson + ";", true);
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            // Agregar ciudad como vértice
            string nombreCiudad = TextBox1.Text;
            int habitantes = Convert.ToInt32(TextBox2.Text); 
            float superficie = Convert.ToSingle(TextBox3.Text); 

            Ciudad nuevaCiudad = new Ciudad(nombreCiudad, habitantes, superficie);
            graf1.AgregarVertice(nuevaCiudad);

            Session["graf1"] = graf1;
            ActualizarDropDownList();

            TextBox8.Text = "Ciudad Agregada";

            Llenar();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Agregar arista entre ciudades
            string CiudadOrigen = TextBox4.Text;
            string CiudadDestino = TextBox5.Text;
            int costo = Convert.ToInt32(TextBox6.Text); 

            int indiceOrigen = ObtenerIndiceCiudad(CiudadOrigen);
            int indiceDestino = ObtenerIndiceCiudad(CiudadDestino);

            if (indiceOrigen != -1 && indiceDestino != -1)
            {
                string mensaje = graf1.AgregarArista(CiudadOrigen, CiudadDestino, costo);

                TextBox7.Text = mensaje;
                Session["graf1"] = graf1;
                ActualizarDropDownListAristas();
            }
            else
            {
                TextBox7.Text = "Las ciudades ingresadas no existen en el grafo.";
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            // Mostrar aristas del vértice seleccionado
            int posicionVertice = DropDownList1.SelectedIndex;
            string[] aristas;

            if (posicionVertice != -1)
            {
                aristas = graf1.MostrarAristasVertice(posicionVertice, ref mensaje);
                DropDownList2.Items.Clear();
                foreach (string w in aristas)
                {
                    DropDownList2.Items.Add(w);
                }
                List<string> aristasConNombre = graf1.MostrarAristasVertice2(posicionVertice, ref mensaje);
                DropDownList3.Items.Clear();
                foreach (string w in aristasConNombre)
                {
                    DropDownList3.Items.Add(w);
                }
            }
            else
            {
                TextBox7.Text = "Debe elegir un vértice de la lista";
            }
        }

        
        protected int ObtenerIndiceCiudad(string nombreCiudad)
        {
            // Método para obtener el índice de una ciudad por su nombre
            List<Vertice> vertices = graf1.ListaAdyc;
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].info.NomCiudad == nombreCiudad)
                {
                    return i;
                }
            }
            return -1; // Retorna -1 si no se encuentra la ciudad
        }

        protected void ActualizarDropDownList()
        {
            DropDownList1.Items.Clear();
            List<Vertice> vertices = graf1.ListaAdyc;
            foreach (Vertice vertice in vertices)
            {
                DropDownList1.Items.Add(vertice.infoCiudad());
            }
        }

        protected void ActualizarDropDownListAristas()
        {
            int posicionVertice = DropDownList1.SelectedIndex;
            if (posicionVertice != -1)
            {
                string[] aristas = graf1.MostrarAristasVertice(posicionVertice, ref mensaje);
                DropDownList2.Items.Clear();
                foreach (string arista in aristas)
                {
                    DropDownList2.Items.Add(arista);
                }

                List<string> aristasConNombre = graf1.MostrarAristasVertice2(posicionVertice, ref mensaje);
                DropDownList3.Items.Clear();
                foreach (string arista in aristasConNombre)
                {
                    DropDownList3.Items.Add(arista);
                }
            }
            else
            {
                TextBox7.Text = "Debe elegir un vértice de la lista";
            }
        }


 


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void Llenar() 
        {
            DropDownList4.Items.Clear();
            DropDownList5.Items.Clear();
            foreach (var ciudades in graf1.MostrarVertices())
            {
                DropDownList4.Items.Add(new ListItem(ciudades));
                DropDownList5.Items.Add(new ListItem(ciudades));
            }
        }
    }
}
