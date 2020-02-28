using System;
using System.Collections.Generic;

namespace WassonTreesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree myTree = new BinaryTree(1);


            Console.WriteLine("Use this program to create and manipulate a binary tree." +
                "\nCommands:" +
                "\nR: Read out the tree" +
                "\nI: Insert node with value x" +
                "\nD: Delete node with value x" +
                "\nX: Stop" +
                "\n\nYou may begin issuing commands...");

            Char command = ' ';

            while(command != 'X')
            {
                command = Console.ReadLine().ToUpper()[0];

                switch (command)
                {
                    case 'R':
                        if(myTree.root == null)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("The following nodes are in the tree...\n");
                            myTree.ReadTree(myTree.root);
                            break;
                        }
                        
                    case 'I':
                        Console.WriteLine("Value of new node (#): ");
                        String newEntry = Console.ReadLine();
                        int newKey = Int32.Parse(newEntry);

                        myTree.Insert(myTree.root, newKey);
                        break;

                    case 'D':
                        Console.WriteLine("Value node to delete (#): ");
                        
                        int deleteKey = Int32.Parse(Console.ReadLine());
                        
                        if(myTree.root.left != null || myTree.root.right != null)
                        {
                            String replaceNode = myTree.Delete(myTree.root, null, null, deleteKey);
                            if(replaceNode != "")
                            {
                                if(deleteKey == Int32.Parse(replaceNode))
                                {
                                    Console.WriteLine("Node " + replaceNode + " was removed.");
                                }
                                else
                                {
                                    Console.WriteLine("Node " + deleteKey + " was removed and replaced with node " + replaceNode + ".");
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("Key value not found.");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Only the root node exists.");
                        }
                        
                        break;

                    case 'X':
                        Console.WriteLine("Stopping...");
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }


            }


        }
    }






    public class Node
    {
        //key value, l/r children
        public int key;
        public Node left, right;
        
        public Node(int item)
        {
            key = item;
            left = null;
            right = null;
        }
    }

    public class BinaryTree
    {
        public Node root;

        public BinaryTree(int key)
        {
            root = new Node(key);
        }

        BinaryTree()
        {
            root = null;
        }

        public void ReadTree(Node rootNode)
        {
            if(rootNode != null)
            {
                Console.WriteLine(rootNode.key + " ");
                
                if(rootNode.left != null)
                {
                    ReadTree(rootNode.left);
                }
                if(rootNode.right != null)
                {
                    ReadTree(rootNode.right);
                }
            }
        }
        
        public void Insert(Node temp, int key) //insert element into tree via inorder traversal
        {
            Queue<Node> nodeQ = new Queue<Node>(); //create queue to store nodes to process
            nodeQ.Enqueue(temp); //add root note to the queue

            while(nodeQ.Count != 0) //while something is in the queue do...
            {

                temp = nodeQ.Peek(); //make sure we are processing the node at the front of the list
                nodeQ.Dequeue(); //remove the node we're processing (the first one) from the list

                if(temp.left == null) //check the left child
                {
                    temp.left = new Node(key);
                    break;
                }
                else
                {
                    nodeQ.Enqueue(temp.left); //if left child exists, add to queue
                }

                if(temp.right == null) //check the right child
                {
                    temp.right = new Node(key);
                    break;
                }
                else
                {
                    nodeQ.Enqueue(temp.right); //if right child exists, add to queue
                }
                



            }



        }

        public String Delete(Node rootNode, Node prevNode, Node keyNode, int key)
        {
            String oldKey = "";
            if(rootNode.key == key)
            {
                keyNode = rootNode;
            }

            if(rootNode.left == null && rootNode.right == null && keyNode != null && prevNode != null)
            {
                keyNode.key = rootNode.key;
                oldKey = rootNode.key.ToString();
                if(prevNode.left == rootNode)
                {
                    prevNode.left = null;
                }
                else
                {
                    prevNode.right = null;
                }
                return oldKey;
            }
            else
            {
                if(rootNode.left != null && oldKey == "") //if the root key is blank, we haven't yet found our requirements, if it has contents, then don't bother, we've accomplished our deletion
                {
                   oldKey = Delete(rootNode.left, rootNode, keyNode, key); //pass along the new node, the previous node, the keyed node (if found) and the key
                }
                if(rootNode.right != null && oldKey == "")
                {
                    oldKey = Delete(rootNode.right, rootNode, keyNode, key);
                }

                if(oldKey != "")
                {
                    return oldKey;
                }
                else
                {
                    return "";
                }
                
            }


            
        }





    }

}
