using System;
using System.Collections.Generic;

namespace Problema_Coloreado_Grafos
{
    class Program
    {
        private static void Main(string[] args)
        {
            bool getOut = false;
            do
            {
                Console.Clear();
                switch (Menu())
                {
                    case 0:
                        getOut = true;
                        break;
                    case 1: // se realiza el algoritmo sobre una array [x,y]
                        var rellenar = new int[3, 3];
                        var filas = rellenar.GetLength(0);
                        var columnas = rellenar.GetLength(1);
                        for (var x = 0; x < filas; x++)
                        {
                            for (var y = 0; y < columnas; y++)
                            {
                                var adyacencias = EncontrarAdyacencias(new[] {x, y}, new[] {filas, columnas});
                                var colores = ColoresAdyacentes(rellenar, adyacencias);

                                for (var i = 0; i <= colores.Count; i++)
                                {
                                    if (colores.Contains(i + 1))
                                    {
                                        continue;
                                    }

                                    rellenar[x, y] = i + 1;
                                    break;
                                }

                            }
                        }

                        PrintGrafo(rellenar);
                        Console.ReadLine();
                        break;
                    case 2: // se realiza el algoritmo sobre un grafo con una matriz de adyacencia
                        var matrizAdyacencia = new int[9, 9]
                        {
                            {0, 1, 1, 1, 0, 0, 0, 0, 0}, // 1
                            {1, 0, 0, 0, 1, 0, 0, 0, 0}, // 2
                            {1, 0, 0, 0, 1, 0, 0, 0, 0}, // 3
                            {1, 0, 0, 0, 0, 1, 0, 0, 1}, // 4
                            {0, 1, 1, 0, 0, 0, 1, 0, 0}, // 5
                            {0, 0, 0, 1, 0, 0, 0, 1, 1}, // 6
                            {0, 0, 0, 0, 1, 0, 0, 0, 0}, // 7
                            {0, 0, 0, 0, 0, 1, 0, 0, 0}, // 8     
                            {0, 0, 0, 1, 0, 1, 0, 0, 0}  // 9    
                        }; //1  2  3  4  5  6  7  8  9
                        Console.WriteLine("\nRepresentación del grafo ejemplo");
                        Console.WriteLine("          1");
                        Console.WriteLine("       / |  \\ ");
                        Console.WriteLine("     2   3   4-------");
                        Console.WriteLine("      \\  |   |      |");
                        Console.WriteLine("        5     6     |");
                        Console.WriteLine("        |     | \\   |");
                        Console.WriteLine("        7     8  9 _|   ");
                        Console.WriteLine(" ");
                        int[] grafo = new int[9];
                        for (int i = 0; i < grafo.Length; i++)
                        {
                            if (grafo[i] == 0)
                            {
                                //Igual que en el anterior, se buscan las posiciones adyacentes y se mira que colores tienen
                                var adyacencias = EncontrarAdyacenciasMatriz(i + 1, matrizAdyacencia);
                                var colores = ColoresMatriz(adyacencias, grafo);
                                int j = 1; // esta variable representa el color que tendrá este nodo del grafo
                                bool salirBucle = false;
                                do
                                {
                                    if (!colores.Contains(j))
                                    {
                                        grafo[i] = j;
                                        salirBucle = true;
                                    }
                                    else
                                    {
                                        j++;
                                    }
                                } while (!salirBucle);
                            }
                        }

                        //Pinto el grafo:
                        int nodo = 1;
                        foreach (int color in grafo)
                        {
                            Console.WriteLine("Nodo {0} tiene el color: {1}", nodo, color);
                            nodo++;
                        }
                        Console.ReadLine();
                        break;
                }
            } while (!getOut);
        }

        private static List<int> ColoresAdyacentes(int[,] rellenar, List<int[]> adyacencias)
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
                    Console.Write(grafo[x, y] + "\t");
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
                    if (deltaX == 0 && deltaY == 0)
                    {
                        continue;
                    }

                    nuevaPos = new[] {posicion[0] + deltaX, posicion[1] + deltaY};

                    fuera_eje_x = nuevaPos[0] < 0 || nuevaPos[0] >= tamañoMatriz[0];
                    fuera_eje_y = nuevaPos[1] < 0 || nuevaPos[1] >= tamañoMatriz[1];

                    if (fuera_eje_x || fuera_eje_y || nuevaPos == posicion)
                    {
                        continue;
                    }

                    adyacentes.Add(nuevaPos);
                }
            }

            return adyacentes;
        }

        private static int Menu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1- Usar una array para calcular los colores en cada posición");
            Console.WriteLine("2- Usar una matriz de adyacencia para relacionar los nodos de un grafo");
            Console.WriteLine("Elige una opción: ");
            int opcion = Convert.ToInt16(Console.ReadLine());
            if (opcion == 1 || opcion == 2)
            {
                return opcion;
            }
            else
            {
                return 0;
            }
        }

        private static List<int> EncontrarAdyacenciasMatriz(int posicion, int[,] matrizAdyaciencia)
        {
            List<int> adyacientes = new List<int>();
            //busco en la matriz de adyacencia las posiciones que tienen 1 en la matriz de adyacencia

            for (int i = 0; i < matrizAdyaciencia.GetLength(0); i++)
            {
                if (matrizAdyaciencia[posicion-1, i] == 1)
                {
                    //introduzco la posición en la matriz del numero que tiene adyaciencia con la posición dada
                    adyacientes.Add(i);
                }
            }
            return adyacientes;
        }

        private static List<int> ColoresMatriz(List<int> posicionesAdy,int[] grafo)
        {
            List<int> colores = new List<int>();
            foreach (int posicion in posicionesAdy)
            {
                if (grafo[posicion] != 0)
                {
                    colores.Add(grafo[posicion]);
                }
            }
            return colores;
        }
    }
}