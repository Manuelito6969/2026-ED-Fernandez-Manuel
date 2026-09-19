using System;

namespace ArbolBinarioBusqueda
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree bst = new BinarySearchTree();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("          MENÚ BST (ÁRBOL)           ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Insertar valor");
                Console.WriteLine("2. Buscar valor");
                Console.WriteLine("3. Eliminar valor");
                Console.WriteLine("4. Mostrar recorrido Preorden");
                Console.WriteLine("5. Mostrar recorrido Inorden");
                Console.WriteLine("6. Mostrar recorrido Postorden");
                Console.WriteLine("7. Mostrar valor mínimo");
                Console.WriteLine("8. Mostrar valor máximo");
                Console.WriteLine("9. Mostrar altura del árbol");
                Console.WriteLine("10. Limpiar árbol");
                Console.WriteLine("0. Salir");
                Console.WriteLine("=====================================");
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine();
                    switch (opcion)
                    {
                        case 1:
                            Console.Write("Ingrese el valor a insertar: ");
                            if (int.TryParse(Console.ReadLine(), out int valIns))
                            {
                                bst.Insert(valIns);
                                Console.WriteLine($"¡Valor {valIns} insertado correctamente!");
                            }
                            else { Console.WriteLine("Número no válido."); }
                            break;

                        case 2:
                            Console.Write("Ingrese el valor a buscar: ");
                            if (int.TryParse(Console.ReadLine(), out int valSch))
                            {
                                bool encontrado = bst.Search(valSch);
                                if (encontrado) Console.WriteLine($"¡El valor {valSch} SÍ se encuentra en el árbol!");
                                else Console.WriteLine($"El valor {valSch} NO está en el árbol.");
                            }
                            else { Console.WriteLine("Número no válido."); }
                            break;

                        case 3:
                            Console.Write("Ingrese el valor a eliminar: ");
                            if (int.TryParse(Console.ReadLine(), out int valDel))
                            {
                                bst.Delete(valDel);
                                Console.WriteLine($"Proceso de eliminación para {valDel} finalizado.");
                            }
                            else { Console.WriteLine("Número no válido."); }
                            break;

                        case 4:
                            Console.WriteLine("Recorrido Preorden:");
                            bst.PreOrderTraversal();
                            break;

                        case 5:
                            Console.WriteLine("Recorrido Inorden:");
                            bst.InOrderTraversal();
                            break;

                        case 6:
                            Console.WriteLine("Recorrido Postorden:");
                            bst.PostOrderTraversal();
                            break;

                        case 7:
                            int? min = bst.GetMin();
                            if (min.HasValue) Console.WriteLine($"El valor mínimo es: {min.Value}");
                            else Console.WriteLine("El árbol está vacío.");
                            break;

                        case 8:
                            int? max = bst.GetMax();
                            if (max.HasValue) Console.WriteLine($"El valor máximo es: {max.Value}");
                            else Console.WriteLine("El árbol está vacío.");
                            break;

                        case 9:
                            int altura = bst.GetHeight();
                            Console.WriteLine($"La altura del árbol es: {altura}");
                            break;

                        case 10:
                            bst.Clear();
                            Console.WriteLine("¡El árbol ha sido limpiado con éxito!");
                            break;

                        case 0:
                            Console.WriteLine("Saliendo del programa...");
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Intente de nuevo.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada no válida.");
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }
    }
}