using System;
using System.Collections.Generic;

namespace ProyectoGrafo
{
    public class Grafo
    {
        public List<Vertice> ListaAdyc = new List<Vertice>();

        public string AgregarVertice(Ciudad nuevaCiudad)
        {
            ListaAdyc.Add(new Vertice(nuevaCiudad));
            return "Nuevo vértice creado";
        }

        public string AgregarArista(string CiudadOrigen, string CiudadDestino, int costo)
        {
            int indiceOrigen = ObtenerIndiceCiudad(CiudadOrigen);
            int indiceDestino = ObtenerIndiceCiudad(CiudadDestino);

            if (indiceOrigen != -1 && indiceDestino != -1)
            {
                ListaAdyc[indiceOrigen].AgregarArista(indiceDestino, costo);
                return "Arista agregada correctamente";
            }
            else
            {
                return "Las ciudades ingresadas no existen en el grafo";
            }
        }

        public string[] MostrarAristasVertice(int posiVertice, ref string msg)
        {
            string[] salida = null;

            if (posiVertice >= 0 && posiVertice <= (ListaAdyc.Count-1))
            {
                salida = ListaAdyc[posiVertice].MuesraAristas();
                msg = "Correcto";
            }
            else
            {
                msg = "La posición del vértice no existe en la Lista de Adyacencia";
            }

            return salida;
        }

        public List<string> MostrarAristasVertice2(int posiVertice, ref string mensaje)
        {
            List<string> salida = new List<string>();

            if (posiVertice >= 0 && posiVertice < ListaAdyc.Count)
            {
                NodoLista temp = ListaAdyc[posiVertice].ListaEnlaces.inicio;

                while (temp != null)
                {
                    salida.Add($"Vértice destino: {ListaAdyc[temp.vertexNum].info.NomCiudad} ,  posición enlace a:  [{temp.vertexNum}] , costo: {temp.costo}");
                    temp = temp.next;
                }

                mensaje = "Correcto";
            }
            else
            {
                mensaje = "La posición del vértice no existe en la Lista de Adyacencia";
            }

            return salida;
        }

        public string[] MostrarVertices()
        {
            string[] vertices = new string[ListaAdyc.Count];
            for (int i = 0; i < ListaAdyc.Count; i++)
            {
                vertices[i] = ListaAdyc[i].infoCiudad();
            }
            return vertices;
        }

        public int ObtenerIndiceCiudad(string nombreCiudad)
        {
            for (int i = 0; i < ListaAdyc.Count; i++)
            {
                if (ListaAdyc[i].info.NomCiudad.Equals(nombreCiudad, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1; // Retorna -1 si no se encuentra la ciudad
        }
    }
}
