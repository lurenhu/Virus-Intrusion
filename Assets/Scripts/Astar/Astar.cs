using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Astar
{
    public static Stack<Vector3> BuildPath(Level level, Vector3Int startGridPosition, Vector3Int endGridPosition)
    {
        startGridPosition -= (Vector3Int)level.lowerBound;
        endGridPosition -= (Vector3Int)level.lowerBound;

        List<Node> openNodeList = new List<Node>();
        HashSet<Node> closedNodeHashSet = new HashSet<Node>();

        GridNode gridNodes = new GridNode(level.upperBound.x - level.lowerBound.x + 1, level.upperBound.y - level.lowerBound.y + 1);

        Node startNode = gridNodes.GetGridNode(startGridPosition.x, startGridPosition.y);
        Node targetNode = gridNodes.GetGridNode(endGridPosition.x, endGridPosition.y);
        //找到目标节点
        Node endPathNode = FindShortestPath(startNode, targetNode, gridNodes, openNodeList, closedNodeHashSet, level.instantiateLevel);
        
        if (endPathNode != null)
        {
            return CreatePathStack(endPathNode, level);
        }

        return null;
    }

    private static Stack<Vector3> CreatePathStack(Node targetNode, Level level)
    {
        Stack<Vector3> movementPathStack = new Stack<Vector3>();

        Node nextNode = targetNode;

        // 获取每个网格的中间偏移
        Vector3 cellMidPoint = level.prefab.transform.GetComponent<Grid>().cellSize * 0.5f;
        cellMidPoint.z = 0f;

        while (nextNode != null)
        {
            Vector3Int gridPosition = new Vector3Int(nextNode.gridPosition.x + level.lowerBound.x, nextNode.gridPosition.y + level.lowerBound.y, 0);
            // 将网格坐标转换为世界坐标
            Vector3 worldPosition = level.prefab.transform.GetComponent<Grid>().CellToWorld(gridPosition);
            // 将世界坐标加上偏移
            worldPosition += cellMidPoint;
            //将该世界坐标压栈
            movementPathStack.Push(worldPosition);
            //找到其父节点
            nextNode = nextNode.parentNode;
        }

        return movementPathStack;
    }

    private static Node FindShortestPath(Node startNode, Node targetNode, GridNode gridNodes, List<Node> openNodeList, HashSet<Node> closedNodeHashSet, InstantiateLevel instantiateLevel)
    {
        openNodeList.Add(startNode);

        // 循环openNodeList内的节点直到没有节点
        while (openNodeList.Count > 0)
        {
            // 对该List排序，在Node类中有规定排序方法
            openNodeList.Sort();

            // 找到List中的第一个Node及fcost值最小的Node
            Node currentNode = openNodeList[0];
            openNodeList.RemoveAt(0);

            // 若当前节点为目标节点则返回
            if (currentNode == targetNode)
            {
                return currentNode;
            }

            // 将当前节点添加到CloseNode哈希表中
            closedNodeHashSet.Add(currentNode);

            // 评估当前节点中所有邻居节点的fcost值
            EvaluateCurrentNodeNeighbours(currentNode, targetNode, gridNodes, openNodeList, closedNodeHashSet, instantiateLevel);
        }

        return null;
    }

    private static void EvaluateCurrentNodeNeighbours(Node currentNode, Node targetNode, GridNode gridNodes, List<Node> openNodeList, HashSet<Node> closedNodeHashSet, InstantiateLevel instantiateLevel)
    {
        Vector2Int currentNodeGridPosition = currentNode.gridPosition;

        Node validNeighbourNode;

        // 遍历各个方向
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                //当前节点跳过
                if ((i == 0 && j == 0)|| (i == 1 && j == 1) || (i == 1 && j == -1) || (i == -1 && j == -1) || (i == -1 && j == 1))
                    continue;

                Debug.Log(currentNodeGridPosition);
                //评估邻居节点
                validNeighbourNode = GetValidNodeNeighbour(currentNodeGridPosition.x + i, currentNodeGridPosition.y + j, gridNodes, closedNodeHashSet, instantiateLevel);

                //当该邻居节点不为空
                if (validNeighbourNode != null)
                {
                    // 计算gCost给该节点
                    int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, validNeighbourNode);

                    bool isValidNeighbourNodeInOpenList = openNodeList.Contains(validNeighbourNode);//判断在openNodeList中是否包含该邻居节点

                    if (newCostToNeighbour < validNeighbourNode.gCost || !isValidNeighbourNodeInOpenList)//当所计算的gcost值小于该邻居节点的gcost值或者该邻居节点不在openNodeList中则执行
                    {
                        validNeighbourNode.gCost = newCostToNeighbour;//该邻居节点的gcost赋于计算值
                        validNeighbourNode.hCost = GetDistance(validNeighbourNode, targetNode);//计算该邻居节点到目标节点的距离并获取hcost值
                        validNeighbourNode.parentNode = currentNode;//该邻居节点的父节点为当前节点

                        if (!isValidNeighbourNodeInOpenList)
                        {
                            openNodeList.Add(validNeighbourNode);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 计算两个节点之间的距离
    /// </summary>
    private static int GetDistance(Node node1, Node node2)
    {
        int dstX = Mathf.Abs(node1.gridPosition.x - node2.gridPosition.x);
        int dstY = Mathf.Abs(node1.gridPosition.y - node2.gridPosition.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);  // 10 used instead of 1, and 14 is a pythagoras approximation SQRT(10*10 + 10*10) - to avoid using floats
        return 14 * dstX + 10 * (dstY - dstX);
    }

    private static Node GetValidNodeNeighbour(int neighbourNodeXPosition, int neighbourNodeYPosition, GridNode gridNodes, HashSet<Node> closedNodeHashSet, InstantiateLevel instantiateLevel)
    {
        // 如果该邻居节点超出了网格则返回
        if (neighbourNodeXPosition >= instantiateLevel.level.upperBound.x - instantiateLevel.level.lowerBound.x || neighbourNodeXPosition < 0 || 
        neighbourNodeYPosition >= instantiateLevel.level.upperBound.y - instantiateLevel.level.lowerBound.y || neighbourNodeYPosition < 0)
        {
            return null;
        }

        // 获取邻居节点包含整个网格中的位置
        Node neighbourNode = gridNodes.GetGridNode(neighbourNodeXPosition, neighbourNodeYPosition);

        //判断在该网格中该邻居节点是否为碰撞体
        int movementPenaltyForGridPosition = instantiateLevel.aStarMovementPenalty[neighbourNodeXPosition,neighbourNodeYPosition];
        Debug.Log("XPosition is " + neighbourNodeXPosition + " YPosition is " + neighbourNodeYPosition + " movementPenaltyForGridPosition is " + movementPenaltyForGridPosition);
        // 如果邻居节点是障碍物或者在closedNode哈希表中则返回
        if (closedNodeHashSet.Contains(neighbourNode) || movementPenaltyForGridPosition == 0)
        {
            return null;
        }
        else
        {
            return neighbourNode;
        }
    }
}
