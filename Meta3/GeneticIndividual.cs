using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticIndividual : Individual {


	GeneticIndividual(int[] topology) : base(topology) {
	}

	public override void Initialize () 
	{
		for (int i = 0; i < totalSize; i++) {
			genotype [i] = Random.Range (-1.0f, 1.0f);
		}
	}
		
	public override void Crossover (Individual partner, float probability, int termos)
	{
		int rand;
		if (Random.Range (0.0f, 1.0f) < probability) {
			List<int> cortes = new List<int>();
			for (int i = 0; i < termos; i++) {
				while (cortes.Contains(rand = Random.Range(0, totalSize)));

				cortes.Add (rand);
			}
			cortes.Sort();
			bool v = true;
			for (int i = 0; i < termos; i++) {
				if (v) {
					if (i - 1 == termos)
						partner.getGenotype.CopyTo (genotype, termos [i]);
					partner.getGenotype.CopyTo (genotype, termos [i], termos [i + 1]);
				}
				v = v + false;
			}
		}
	}

	public override void Mutate (float probability)
	{
		for (int i = 0; i < totalSize; i++) {
			if (Random.Range (0.0f, 1.0f) < probability) {
				genotype [i] = Random.Range (-1.0f, 1.0f);
			}
		}
	}

	public override Individual Clone ()
	{
		GeneticIndividual new_ind = new GeneticIndividual(this.topology);

		genotype.CopyTo (new_ind.genotype, 0);
		new_ind.fitness = this.Fitness;
		new_ind.evaluated = false;

		return new_ind;
	}

}
