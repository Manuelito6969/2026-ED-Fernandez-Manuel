using System;

namespace ArbolBinarioBusqueda
{
    public class BinarySearchTree
    {
        public Node? Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        // 1. Insertar valor
        public void Insert(int value)
        {
            Root = InsertRec(Root, value);
        }

        private Node InsertRec(Node? root, int value)
        {
            if (root == null) return new Node(value);

            if (value < root.Value)
                root.Left = InsertRec(root.Left, value);
            else if (value > root.Value)
                root.Right = InsertRec(root.Right, value);

            return root;
        }

        // 2. Buscar valor
        public bool Search(int value)
        {
            return SearchRec(Root, value);
        }

        private bool SearchRec(Node? root, int value)
        {
            if (root == null) return false;
            if (root.Value == value) return true;

            return value < root.Value ? SearchRec(root.Left, value) : SearchRec(root.Right, value);
        }

        // 3. Eliminar valor
        public void Delete(int value)
        {
            Root = DeleteRec(Root, value);
        }

        private Node? DeleteRec(Node? root, int value)
        {
            if (root == null) return null;

            if (value < root.Value)
            {
                root.Left = DeleteRec(root.Left, value);
            }
            else if (value > root.Value)
            {
                root.Right = DeleteRec(root.Right, value);
            }
            else
            {
                // Nodo sin hijos o con un solo hijo
                if (root.Left == null) return root.Right;
                if (root.Right == null) return root.Left;

                // Nodo con dos hijos: obtener el sucesor inorden (el menor del subárbol derecho)
                root.Value = MinValue(root.Right);
                root.Right = DeleteRec(root.Right, root.Value);
            }
            return root;
        }

        private int MinValue(Node root)
        {
            int minv = root.Value;
            while (root.Left != null)
            {
                minv = root.Left.Value;
                root = root.Left;
            }
            return minv;
        }

        // 4. Recorrido Preorden (Raíz - Izquierda - Derecha)
        public void PreOrderTraversal()
        {
            PreOrderRec(Root);
            Console.WriteLine();
        }

        private void PreOrderRec(Node? root)
        {
            if (root != null)
            {
                Console.Write(root.Value + " ");
                PreOrderRec(root.Left);
                PreOrderRec(root.Right);
            }
        }

        // 5. Recorrido Inorden (Izquierda - Raíz - Derecha)
        public void InOrderTraversal()
        {
            InOrderRec(Root);
            Console.WriteLine();
        }

        private void InOrderRec(Node? root)
        {
            if (root != null)
            {
                InOrderRec(root.Left);
                Console.Write(root.Value + " ");
                InOrderRec(root.Right);
            }
        }

        // 6. Recorrido Postorden (Izquierda - Derecha - Raíz)
        public void PostOrderTraversal()
        {
            PostOrderRec(Root);
            Console.WriteLine();
        }

        private void PostOrderRec(Node? root)
        {
            if (root != null)
            {
                PostOrderRec(root.Left);
                PostOrderRec(root.Right);
                Console.Write(root.Value + " ");
            }
        }

        // 7. Mostrar valor mínimo
        public int? GetMin()
        {
            if (Root == null) return null;
            Node current = Root;
            while (current.Left != null) current = current.Left;
            return current.Value;
        }

        // 8. Mostrar valor máximo
        public int? GetMax()
        {
            if (Root == null) return null;
            Node current = Root;
            while (current.Right != null) current = current.Right;
            return current.Value;
        }

        // 9. Mostrar altura del árbol
        public int GetHeight()
        {
            return CalculateHeight(Root);
        }

        private int CalculateHeight(Node? root)
        {
            if (root == null) return 0; // Árbol vacío altura 0 (o -1 según criterio)
            int leftHeight = CalculateHeight(root.Left);
            int rightHeight = CalculateHeight(root.Right);
            return Math.Max(leftHeight, rightHeight) + 1;
        }

        // 10. Limpiar árbol
        public void Clear()
        {
            Root = null;
        }
    }
}