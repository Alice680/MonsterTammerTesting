using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnKeeper
{
    private class Node
    {
        public Node next;

        public Entity entity;

        public int value;

        public Node(Entity ent)
        {
            entity = ent;

            value = -15;
        }
    }

    private Node head;

    public TurnKeeper()
    {
        head = null;
    }

    public void AddEntity(Entity ent)
    {
        if (ent == null)
            return;

        RemoveEntity(ent);

        if (head == null)
        {
            head = new Node(ent);
        }
        else
        {
            Node temp = new Node(ent);
            temp.next = head;
            head = temp;
        }

        Reclculate();
    }

    public void RemoveEntity(Entity ent)
    {
        return;

        if (ent == null)
            return;

        Node temp = head;

        while (temp != null)
        {
            if (temp.entity == ent)
            {

            }

            temp = temp.next;
        }
    }

    public Entity GetCurrent()
    {
        return head.entity;
    }

    public void Increment()
    {
        if (head == null)
            return;

        if(head.value >= 10)
        {
            head.value -= 10;
            Reclculate();
        }

        while (head.value < 10)
        {
            Node temp = head;

            while (temp != null)
            {
                temp.value += 5 + temp.entity.GetSpeed();

                temp = temp.next;
            }

            Reclculate();
        }
    }

    private void Reclculate()
    {
        if (head == null)
            return;

        Node temp_head = head.next;

        head.next = null;

        while (temp_head != null)
        {
            Node pointer = temp_head;
            temp_head = temp_head.next;
            pointer.next = null;

            if (head.value < pointer.value)
            {
                pointer.next = head;
                head = pointer;
            }
            else
            {
                Node temp = head;

                while (temp.next != null && temp.value > pointer.value)
                    temp = temp.next;

                pointer.next = temp.next;
                temp.next = pointer;
            }
        }
    }
}