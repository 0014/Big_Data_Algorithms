using System;

namespace _0014
{
    // The tree class
    public class Bst
    {
        public Bst()
        {
            Root = null;
            Count = 0;
        }

        public int Count { get; set; }
        public Node Root { get; set; }

        /// <summary>
        /// Searchs for a specific node in the tree. O(logn)
        /// </summary>
        /// <param name="element"> The key value to search </param>
        /// <returns> returns the found node </returns>
        public Node Search(int element)
        {
            var node = Root; // start searching from root

            while (Root != null)
            {
                if (element == node.Key) return node; // Console.Write(element + " FOUND.\n");

                if (element > node.Key && node.RightChild != null) // search for the right subtree
                    node = node.RightChild;

                else if (element < node.Key && node.LeftChild != null) // search for the left subtree
                    node = node.LeftChild;

                else return null; // Console.Write(element + " NOT FOUND.\n");
            }

            return null; // comes here if the there is no root in the tree
        }

        /// <summary>
        /// Inserts a single element to the tree. O(logn)
        /// </summary>
        /// <param name="element"> The key value to be insert </param>
        /// <returns> If its inserted successfull returns true, else false </returns>
        public bool Insert(int element)
        {
            var node = Root; // start searching from root

            while (Root != null)
            {
                if (element == node.Key) return false; // Console.Write("Insert " + element + " FAILED (alreadyin the list)\n");

                if (element > node.Key) // search for the right subtree
                {
                    if (node.RightChild != null) // continue searching
                        node = node.RightChild;
                    else // found the spot the insert the new node
                    {
                        node.RightChild = new Node(node, element); // insert the new node
                        Count ++;
                        return true;
                    }
                }

                else // search for the left subtree
                {
                    if (node.LeftChild != null) // continue searching
                        node = node.LeftChild;
                    else // found the spot the insert the new node
                    {
                        node.LeftChild = new Node(node, element); // insert the new node
                        Count ++;
                        return true;
                    }
                }
            }

            Root = new Node(element); // comes here if the there is no root in the tree
            Count ++;
            return true;
        }

        /// <summary>
        /// Takes a list of elements to insert without calling it several times.
        /// Overrides the insert method. O(elements.Length * logn)
        /// </summary>
        /// <param name="elements"> List of elements to insert </param>
        /// <returns> if all elements are successfully inserted return true, else false</returns>
        public bool Insert(int[] elements)
        {
            var success = true;

            for (int i = 0; i < elements.Length; i ++)
                success &= Insert(elements[i]);
            
            return success;
        }

        /// <summary>
        /// Deletes the wanted node from the tree. Average O(logn), Worst O(n)
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Delete(int element)
        {
            var nodeToDelete = Search(element);

            if (nodeToDelete == null) return false; // the node to be deleted, couldn`t be found

            if (nodeToDelete.RightChild == null && nodeToDelete.LeftChild == null) // there is no children simply delete
                nodeToDelete.DeleteNode();

            else if (nodeToDelete.RightChild != null && nodeToDelete.LeftChild != null) // both children exists, we have a problem here
            {
                var newChild = nodeToDelete.LeftChild;

                while (newChild.RightChild != null) // get the biggest of the smaller nodes
                    newChild = newChild.RightChild;
                
                var newKey = newChild.Key;

                Delete(newKey);

                nodeToDelete.Key = newKey; // swap the nodes
            }

            else
            {
                var parent = nodeToDelete.Parent;
                var child = nodeToDelete.RightChild ?? nodeToDelete.LeftChild;

                if (parent != null)
                {
                    if (parent.RightChild != null && nodeToDelete.Key == parent.RightChild.Key) // deleted node is right child
                        parent.RightChild = child;
                    else                                    // deleted node is left child
                        parent.LeftChild = child;
                }

                child.Parent = parent; // update childs parent info before deletion
            }

            Count --;
            return true;
        }

        /// <summary>
        /// Takes a list of elements to delete without calling it several times.
        /// Overrides the insert method. Average O(elements.Length * logn), Worst O(elements.Length * n)
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public bool Delete(int[] elements)
        {
            var success = true;

            for (int i = 0; i < elements.Length; i ++)
                success &= Delete(elements[i]);

            return success;
        }

        public void Print(Bst tree)
        {
            var tempBst = tree;

            var iterator = tempBst.Root;

            Console.Write("(");

            while (true)
            {

                if (iterator.LeftChild != null)
                {
                    Console.Write("("); // once we go deep we open pharantesis
                    iterator = iterator.LeftChild;
                }
                else
                {
                    var rightChild = iterator.RightChild;
                    var parent = iterator.Parent;

                    Console.Write(iterator.Key);

                    tempBst.Delete(iterator.Key);

                    if (rightChild != null)
                    {
                        Console.Write("("); // once we go deep we open pharantesis
                        iterator = rightChild;
                    }
                    else
                    {
                        Console.Write(")"); // we are getting closer to the root, close the pharantesis
                        iterator = parent;
                        if (iterator == null)
                        {
                            // if parent is null then it means we are back to root
                            Console.Write(")\n");
                            return; // end of printing
                        }
                    }
                }
            }
        }
    }

    public class Node
    {
        // this constructer function can be used to set the root node
        public Node(int key)
        {
            RightChild = null;
            LeftChild = null;
            Key = key;
            Parent = null;
        }

        // this constructer function can be used in general case
        public Node(Node parent, int key)
        {
            RightChild = null;
            LeftChild = null;
            Key = key;
            Parent = parent;
        }

        public int Key { get; set; }

        public Node Parent { get; set; }

        public Node RightChild { get; set; }

        public Node LeftChild { get; set; }

        public void DeleteNode()
        {
            var parent = Parent;

            // update parent`s information
            if (parent != null)
            {
                if (parent.LeftChild != null && parent.LeftChild.Key == Key)
                    parent.LeftChild = null;
                else parent.RightChild = null;
            }

            // remove this node`s child and parent information
            LeftChild = null;
            RightChild = null;
            Parent = null;
        }
    }
}
