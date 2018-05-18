using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MetaHeuristic {
	public float mutationProbability;
	public float crossoverProbability;
	public int tournamentSize;
	public int numerocortes;
	public bool elitist;

	public override void InitPopulation () {
	
		population = new List<Individual> ();
		// jncor 
		while (population.Count < populationSize) {
			GeneticIndividual new_ind = new GeneticIndividual (topology);
			new_ind.Initialize ();
			population.Add (new_ind);
		}
	}

	//The Step function assumes that the fitness values of all the individuals in the population have been calculated.
	public override void Step() {
		List<Individual> new_pop = new List<Individual> ();

		updateReport (); //called to get some stats
		// fills the rest with mutations of the best !
		for (int i = 0; i < populationSize; i++) {
			
			GeneticIndividual pai = (GeneticIndividual)Tournament (population, tournamentSize).Clone();
			GeneticIndividual pai2 = (GeneticIndividual)Tournament (population, tournamentSize).Clone();

			pai.Crossover (pai2.Clone(), crossoverProbability, numerocortes);
			pai.Mutate (mutationProbability);
			new_pop.Add (pai.Clone());
		}
		//if (elitist)
		population = new_pop;

		generation++;
	}

	private Individual Tournament(List<Individual> populacao, int num){
		List<int> lista = new List<int>();
		Individual individo;
		int rand;
		for (int i = 0; i < num; i++) {
			while (lista.Contains (rand = Random.Range (0, populacao.Count)));
			lista.Add (rand);
		}

		individo = populacao [lista [0]];
		for (int i = 1; i < num; i++) {
			if (individo.Fitness < populacao [lista[i]].Fitness){
				individo = populacao [lista [i]];
			}
		}

		return individo;
	}
}
