using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Nodes
{
    class NodeLinkedList
    {
        private Node head;

        public NodeLinkedList() { this.head = null; }

        public void addFirst(Node node)
        {
            if (this.head == null) { this.head = node; }
            else
            {
                node.setNext(head);
                head = node;
            }
        }

        public Node getFirst() { return this.head; }

        public void addLast(Node node)
        {
            if (head == null) { this.addFirst(node); }
            else
            {
                Node lastNode = this.getLast();

                lastNode.setNext(node);
            }
        }

        public Node getLast()
        {
            Node node = head;

            while (node.hasNext()) { node = node.getNext(); }

            return node;
        }

        public Node get(int index)
        {
            Node node = head;
            int count = 0;

            while (node != null && node.hasNext() && count++ < index) { node = node.getNext(); }

            if (count < index) { return null; }
            else { return node; }
        }

        public void insertBefore(Node keyNode, Node insertNode)
        {
            if (keyNode == null) { addLast(insertNode); }
            else if (this.head.Equals(keyNode)) { addFirst(insertNode); }
            else
            {
                Node previous = null;
                Node current = head;

                while (current != null && !current.Equals(keyNode))
                {
                    previous = current;
                    current = current.getNext();
                }

                if (current != null)
                {
                    if (previous != null) { previous.setNext(insertNode); }

                    while (insertNode.hasNext()) { insertNode = insertNode.getNext(); }

                    insertNode.setNext(current);
                }
            }
        }
    }
}
