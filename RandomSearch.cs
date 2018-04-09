using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSearch : SearchAlgorithm {

	private List<SearchState> openQueue;


	protected override void Begin () {
		startNode = GridMap.instance.NodeFromWorldPoint (startPos);
		targetNode = GridMap.instance.NodeFromWorldPoint (targetPos);
		//estas duas linhas funcionam para quase ou todos os algoritmos

		SearchState start = new SearchState (startNode, 0);
		openQueue = new List<SearchState> ();
		openQueue.Add(start);
		
	}
	
	protected override void Step () {
		
		if (openQueue.Count > 0)
		{
			int index = Random.Range (0, openQueue.Count);
			SearchState currentState = openQueue[index];//tirar o nó da fila
			Debug.Log(index);
			openQueue.RemoveAt(index);
			VisitNode (currentState);
			if (currentState.node == targetNode) {
				solution = currentState;
				finished = true;
				running = false;
				foundPath = true;
			} else {
				foreach (Node suc in GetNodeSucessors(currentState.node)) {
					SearchState new_node = new SearchState(suc, suc.gCost + currentState.g, currentState);
					openQueue.Add (new_node);
				}
				// for energy
				if ((ulong) openQueue.Count > maxListSize) {
					maxListSize = (ulong) openQueue.Count;
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
