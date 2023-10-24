import numpy as np
from scipy import stats
import itertools
import pandas as pd


def mwu(originalClusterized):

    # Ottieni una lista di nomi di colonne (variabili) da testare (escludendo la colonna "Labels K-means")
    variabili_da_testare = originalClusterized.columns.difference(['Labels K-means'])

    # Creo un DataFrame vuoto per raccogliere i risultati
    # result_df = pd.DataFrame(columns=['Variabile', 'P-Value Kruskal', "Result Kruskal", "P-value MWY", "Result MWY"])
    result_df = pd.DataFrame(columns=['Variabile', "Result MWU"])

    summary_p_values = pd.DataFrame(columns=['Variabile', "p 0 vs 1", "Diff 0 1", "p 0 vs 2", "Diff 0 2", "p 1 vs 2", "Diff 1 2",])

    alpha = 0.05 / 3  # Livello di significatività , /3 per Bonferroni (?)

    for variabile in variabili_da_testare:
        # per uno stesso attributo (colonna):
        # risposte_cluster_1 conterrà la lista di risposte date alla colonna X dagli appartenti al cluster 1 ecc.
        risposte_cluster_1 = np.array(
            originalClusterized[(originalClusterized['Labels K-means'] == 0.0)][variabile].values)
        risposte_cluster_2 = np.array(
            originalClusterized[(originalClusterized['Labels K-means'] == 1.0)][variabile].values)
        risposte_cluster_3 = np.array(
            originalClusterized[(originalClusterized['Labels K-means'] == 2.0)][variabile].values)

        # creiamo un array con le risposte dei singoli cluster nella colonna "variabile"
        gruppi = [risposte_cluster_1, risposte_cluster_2, risposte_cluster_3, ]

        # Creo tutte le possibili coppie di risposte (coppie di cluster)
        coppie = list(itertools.combinations(range(len(gruppi)), 2))

        # MWU per ogni coppia
        risultati = {}
        for i, j in coppie:
            gruppo1, gruppo2 = gruppi[i], gruppi[j]
            statistiche, p_value = stats.mannwhitneyu(gruppo1, gruppo2)
            risultati[(i, j)] = (statistiche, p_value)

        # Definiamo una variabile che vale 0 se non cè differenza significativa
        # tra tutte le coppie, 1 altrimenti

        isDifferenceSign = 1
        diff_0_1 = "Diff. significative"
        diff_0_2 = "Diff. significative"
        diff_1_2 = "Diff. significative"
        result_0_1 = 0
        result_0_2 = 0
        result_1_2 = 0

        for chiave, (statistiche, p_value) in risultati.items():
             i, j = chiave
             print(f"Mann-Whitney tra Cluster {i} e Cluster {j}:")
             if(i == 0 and j == 1):
                 result_0_1 = p_value
                 if p_value >= alpha:
                     diff_0_1 = "Diff. NON significative"
             if(i == 0 and j == 2):
                 result_0_2 = p_value
                 if p_value >= alpha:
                     diff_0_2 = "Diff. NON significative"
             if(i == 1 and j == 2):
                 result_1_2 = p_value
                 if p_value >= alpha:
                     diff_1_2 = "Diff. NON significative"

             print(f"Valore p: {p_value}")
             if p_value >= alpha:
                isDifferenceSign = 0



        # se isDifferenceSign non è mai andato a 0, vuol dire che la diff è significativa
        # per ogni coppia
        if (isDifferenceSign):
            string_result_mwu = "Diff. significative"
        else:
            string_result_mwu = "Diff. NON significative"
        result_df.loc[len(result_df)] = [variabile, string_result_mwu]

        summary_p_values.loc[len(summary_p_values)] = [variabile, result_0_1, diff_0_1, result_0_2, diff_0_2, result_1_2, diff_1_2]



    return result_df, summary_p_values