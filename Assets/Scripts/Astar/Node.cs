using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    public Vector2Int gridPosition;
    public Node parentNode;
    public Node(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        parentNode = null;
    }

    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    /// <summary>
    /// 创建比较函数，方便用于在列表中进行排序，进而找出fCost最小的节点
    /// </summary>
    public int CompareTo(Node nodeToCompare)
    {
        //当此节点的fCost < nodeToCompare节点的fCost时，result < 0
        //当此节点的fCost > nodeToCompare节点的fCost时，result > 0
        //当此节点的fCost = nodeToCompare节点的fCost时，result = 0

        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return compare;
    }
}
