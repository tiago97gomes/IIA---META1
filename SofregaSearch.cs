using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofregaSearch : SearchAlgorithm {

	private List<SearchState> SortedList;


	protected override void Begin () {
		startNode = GridMap.instance.NodeFromWorldPoint (startPos);
		targetNode = GridMap.instance.NodeFromWorldPoint (targetPos);
		//estas duas linhas funcionam para quase ou todos os algoritmos

		SearchState start = new SearchState (startNode, 0);
		SortedList = new List<SearchState> ();
		SortedList.Add(start);
		
	}
	
	protected override void Step () {
		
		if (SortedList.Count > 0)
		{
			SearchState currentState = SortedList[0];
			SortedList.RemoveAt(0);
			VisitNode (currentState);
			if (currentState.node == targetNode) {
				solution = currentState;
				finished = true;
				running = false;
				foundPath = true;
			} else {
				foreach (Node suc in GetNodeSucessors(currentState.node)) {
					SearchState new_node = new SearchState(suc, suc.gCost + currentState.g, currentState);

					InsertionSort (new_node);
				}
				if ((ulong) SortedList.Count > maxListSize) {
					maxListSize = (ulong) SortedList.Count;
				}
			}
		}
		else
		{
			finished = true;
			running = false;
		}

	}

	void InsertionSort(SearchState node){
		for (int i = 0; i < SortedList.Count; i++) {
			if (SortedList [i].g > node.g) {
				SortedList.Insert (i, node);
				return;
			}
		}
	}
}

