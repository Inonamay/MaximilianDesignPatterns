using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AI
{
    //TODO: Implement IPathFinder using Dijsktra algorithm.
    public class Dijkstra : IPathFinder
    {
        List<Vector2Int> map;
        Vector2Int[] directions = new Vector2Int[4]
        {
           new Vector2Int(1,0) , new Vector2Int(-1,0), new Vector2Int(0,1), new Vector2Int(0,-1)
        };

        class Node
        {
            public Node()
            {

            }
            public Node(int steps, Vector2Int position, Node _previousNode)
            {
                stepsTaken = steps;
                currentPosition = position;
                previous = _previousNode;
            }
            public int stepsTaken;
            public Vector2Int currentPosition;
            public Node previous;

        }
        public Dijkstra(List<Vector2Int> _Accessables)
        {
            map = _Accessables;
        }
		public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
		{
            Node currentNode = new Node();
            currentNode.stepsTaken = 0;
            currentNode.currentPosition = start;
            List<Vector2Int> searched = new List<Vector2Int>();
            Queue<Node> searchList = new Queue<Node>();
            bool foundGoal = false;
            while(!foundGoal)
            {
                
                for (int i = 0; i < 4; i++)
                {
                   Vector2Int searchnode = currentNode.currentPosition + directions[i];
                   if(searchnode == goal)
                   {
                       foundGoal = true;
                   }
                   if((map.Contains(searchnode) || foundGoal) && !searched.Contains(searchnode))
                   {
                        searched.Add(currentNode.currentPosition);
                        Node node = new Node(currentNode.stepsTaken + 1, searchnode, currentNode);
                        searchList.Enqueue(node);
                        if (foundGoal)
                        {
                            currentNode = node;
                        }
                   }
                   if(foundGoal)
                   {
                        break;
                   }
                }
                if (!foundGoal)
                {
                    if (searchList.Count > 0)
                    {
                        currentNode = searchList.Dequeue();

                    }
                    else
                    {
                        return Enumerable.Empty<Vector2Int>();
                    }
                }
            }
            Node backwardsWalk = currentNode;
            searched.Clear();
            while (backwardsWalk.currentPosition != start)
            {
                searched.Add(backwardsWalk.currentPosition);
                if (backwardsWalk.previous != null)
                {
                    backwardsWalk = backwardsWalk.previous;
                }
            }
            searched.Add(start);
            searched.Reverse();

            return searched;
		}
	}    
     
}
