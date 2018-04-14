using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrepaColinas :  SearchAlgorithm {

	SearchState currentState;

	protected override void Begin () {
		startNode = GridMap.instance.NodeFromWorldPoint (startPos);
		targetNode = GridMap.instance.NodeFromWorldPoint (targetPos);

		currentState = new SearchState (startNode, 0, GetHeuristic(startNode), null);
	}

	protected override void Step () {
		VisitNode (currentState);
		SearchState nextNode = null;
		foreach (Node suc in GetNodeSucessors(currentState.node)) {
			SearchState new_node = new SearchState(suc, suc.gCost + currentState.g, GetHeuristic(suc), currentState);
			if (nextNode == null || new_node.h < nextNode.h)
				nextNode = new_node;
		}
		
		if (nextNode.h > currentState.h) {
			solution = currentState;
			finished = true;
			running = false;
			foundPath = true;
		}
		currentState = nextNode;
	}

}
