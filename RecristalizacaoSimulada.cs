using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecristalizacaoSimulada : SearchAlgorithm {

	private SearchState currentState;
	private int t;

	protected override void Begin () {
		startNode = GridMap.instance.NodeFromWorldPoint (startPos);
		targetNode = GridMap.instance.NodeFromWorldPoint (targetPos);

		currentState = new SearchState (startNode, 0, GetHeuristic(startNode), null);
		t = 0;
	}

	protected override void Step () {
		VisitNode (currentState);
		t++;
		float T = PolEscalona(t);
		Debug.Log (T);
		if (currentState.node == targetNode) {
			solution = currentState;
			finished = true;
			running = false;
			foundPath = true;
		}
		if (T == 0) {
			solution = currentState;
			finished = true;
			running = false;
		}
		List<Node> listNodes = GetNodeSucessors (currentState.node);
		Node suc = listNodes[Random.Range (0, listNodes.Count)]; 
		SearchState new_node = new SearchState(suc, suc.gCost + currentState.g, GetHeuristic(suc), currentState);
		if (new_node.h > currentState.h) {
			currentState = new_node;
		} else {
			if (Mathf.Exp((currentState.h - new_node.h)/T) > Random.value){
				currentState = new_node;
			}
		}
	}

	private float PolEscalona(int t){
		return (float)(2 - t*0.0001f);
	}

	private float PolEscalona2(int t){
		return t;
	}

	private float PolEscalona3(int t){
		return (int)Mathf.Exp((-1)*(Mathf.Pow(t,2))/(Mathf.Pow(1,2)));
	}

}