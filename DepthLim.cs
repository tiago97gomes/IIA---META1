using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthLim : SearchAlgorithm {
	private Stack<SearchState> openStack;
	public int limit;
	protected override void Begin () {
		startNode = GridMap.instance.NodeFromWorldPoint (startPos);
		targetNode = GridMap.instance.NodeFromWorldPoint (targetPos);
		//estas duas linhas funcionam para quase ou todos os algoritmos

		SearchState start = new SearchState (startNode, 0);

		openStack = new Stack<SearchState> ();
		openStack.Push (start);

	}
	
	protected override void Step () {
		
		if (openStack.Count > 0)
		{
			SearchState currentState = openStack.Pop();//tirar o nó da pilha

			VisitNode (currentState);
			if (currentState.node == targetNode) {
				solution = currentState;
				finished = true;
				running = false;
				foundPath = true;
			} else if (currentState.depth<limit){
				foreach (Node suc in GetNodeSucessors(currentState.node)) {
					
						SearchState new_node = new SearchState (suc, suc.gCost + currentState.g, currentState);
						openStack.Push (new_node);
				
				}
				// for energy
				if ((ulong) openStack.Count > maxListSize) {
					maxListSize = (ulong) openStack.Count;
				}
			}
		}
		else
		{
			finished = true;
			running = false;
			foundPath = true;
		}

	}
}
