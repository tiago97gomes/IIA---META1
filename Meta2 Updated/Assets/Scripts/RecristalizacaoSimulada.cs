using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecristalizacaoSimulada : SearchAlgorithm {

	private SearchState currentState;
	public int t;
	public float T;
	public float D;
	public float tt;
	public float div;

	protected override void Begin () {
		startNode = GridMap.instance.NodeFromWorldPoint (startPos);
		targetNode = GridMap.instance.NodeFromWorldPoint (targetPos);
		currentState = new SearchState (startNode, 0, GetHeuristic(startNode), null);
		t = 0;
	}

	protected override void Step () {
		VisitNode (currentState);
		t++;
		T = PolEscalona(t);

		if (currentState.node == targetNode) {
			solution = currentState;
			finished = true;
			running = false;
			foundPath = true;
		}
		if (T <= 0) {
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
		tt = 20.0f;
		D = 100000;
		div = tt / D;
		return (tt - t*(tt/D));
	}

	private float PolEscalona2(int t){
		return 2.0f - Mathf.Exp(-t);
	}

	private float PolEscalona3(int t){ 
		return 2.0f - (float)Mathf.Exp((-1)*(Mathf.Pow(t - 0.5f,2))/(2*Mathf.Pow(0.12f,2)));
	}

}
