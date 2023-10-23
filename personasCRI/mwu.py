import numpy as np
import pandas as pd
from scipy import stats
import itertools

def mwu(originalClusterized):

    # per uno stesso attributo (colonna):
    # risposte_cluster_1 conterrà la lista di risposte date alla colonna X dagli appartenti al cluster 1
    # risposte_cluster_2 conterrà la lista di risposte date alla colonna X dagli appartenti al cluster 2
    # risposte_cluster_3 conterrà la lista di risposte date alla colonna X dagli appartenti al cluster 3

    # Ottieni una lista di nomi di colonne (variabili) da testare (escludendo la colonna "Labels K-means")
    variabili_da_testare = originalClusterized.columns.difference(['Labels K-means'])

    alpha = 0.05 / 3  # Livello di significatività

    # Creo un DataFrame vuoto per raccogliere i risultati
    # result_df = pd.DataFrame(columns=['Variabile', 'P-Value Kruskal', "Result Kruskal", "P-value MWY", "Result MWY"])
    result_df = pd.DataFrame(columns=['Variabile', 'P-Value mwu', "Result mwu"])

    for variabile in variabili_da_testare:
        risposte_cluster_1 = np.array(originalClusterized[(originalClusterized['Labels K-means'] == 0.0)][variabile].values)
        risposte_cluster_2 = np.array(originalClusterized[(originalClusterized['Labels K-means'] == 1.0)][variabile].values)
        risposte_cluster_3 = np.array(originalClusterized[(originalClusterized['Labels K-means'] == 2.0)][variabile].values)

        gruppi = [risposte_cluster_1, risposte_cluster_2, risposte_cluster_3]

        # Crea tutte le possibili coppie di gruppi
        coppie = list(itertools.combinations(range(len(gruppi)), 2))

        # Esegui il test di Mann-Whitney per ogni coppia
        risultati = {}
        for i, j in coppie:
            gruppo1, gruppo2 = gruppi[i], gruppi[j]
            statistiche, p_value = stats.mannwhitneyu(gruppo1, gruppo2)
            risultati[(i, j)] = (statistiche, p_value)

        print("Risultati variabile " + variabile)
        #Definiamo una variabile che vale 0 se non cè differenza significativa
        # tra tutte le coppie, 1 altrimenti
        # Stampare i risultati
        for chiave, (statistiche, p_value) in risultati.items():
            i, j = chiave
            print(f"Mann-Whitney tra Gruppo {i} e Gruppo {j}:")
            print(f"Statistiche U: {statistiche}")
            print(f"Valore p: {p_value}")
            if p_value < alpha:
                print("Differenza significativa")
            else:
                print("Nessuna differenza significativa")
