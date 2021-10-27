using System;
using System.Collections.Generic;

namespace Problema_Coloreado_Grafos
{
    class Program
    {
        private static void Main(string[] args)
        {
            var rellenar = new int[10, 10];
            var filas = rellenar.GetLength(0);
            var columnas = rellenar.GetLength(1);
            for (var x = 0; x < filas; x++)
            {
                for (var y = 0; y < columnas; y++)
                {
                    var adyacencias = EncontrarAdyacencias(new []{x, y}, new []{filas, columnas});
                    var colores = ColoresAdyacentes(rellenar, adyacencias);
                    
                    for (var i = 1; i <= adyacencias.Count; i++)
                    {
                        if (colores.Contains(i))
                        {
                            continue;
                        }

                        rellenar[x, y] = i;
                        break;
                    }
                    
                }
            }
            
            PrintGrafo(rellenar);
        }

        private static List<int> ColoresAdyacentes(int[,]rellenar, List<int[]> adyacencias)
        {
            var colores = new List<int>();
            
            // Comprueba los colores de las casillas adyacentes a la posición evaluada
            // Si hay un color (!= 0) lo guarda en 'colores'
            foreach (var adyacente in adyacencias)
            {
                var colorAdyacente = rellenar[adyacente[0], adyacente[1]];
                if (colorAdyacente != 0)
                {
                    colores.Add(colorAdyacente);
                }
            }

            return colores;
        }

        private static void PrintGrafo(int[,] grafo)
        {
            for (var x = 0; x < grafo.GetLength(0); x++)
            {
                for (var y = 0; y < grafo.GetLength(1); y++)
                {
                    Console.Write(grafo[x, y]+"\t");
                }
                Console.WriteLine("");
            }
        }

        private static List<int[]> EncontrarAdyacencias(int[] posicion, int[] tamañoMatriz)
        {
            // Devuelve una lista de las posiciones adyacentes a la posición dada
            // Si la posible adyacencia cae fuera de los bordes de la matriz, no se devuelve
            List<int[]> adyacentes = new List<int[]>();
            bool fuera_eje_x, fuera_eje_y;
            int[] nuevaPos;

            for (var deltaX = -1; deltaX < 2; deltaX++)
            {
                for (var deltaY = -1; deltaY < 2; deltaY++)
                {
                    // No considera la posición misma para las adyacencias
                    if (deltaX == 0 && deltaY == 0) { continue; }
                    
                    nuevaPos = new[] {posicion[0] + deltaX, posicion[1] + deltaY};
                    
                    fuera_eje_x = nuevaPos[0] < 0 || nuevaPos[0] >= tamañoMatriz[0];
                    fuera_eje_y = nuevaPos[1] < 0 || nuevaPos[1] >= tamañoMatriz[1];

                    if (fuera_eje_x || fuera_eje_y || nuevaPos == posicion) { continue; }
                    
                    adyacentes.Add(nuevaPos);
                }
            }

            return adyacentes;
        }
    }
}