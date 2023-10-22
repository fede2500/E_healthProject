import pandas as pd
from scipy import stats

def kruskal(originalClusterized):

    # per uno stesso attributo (colonna):
    # risposte_cluster_1 conterrà la lista di risposte date alla colonna X dagli appartenti al cluster 1
    # risposte_cluster_2 conterrà la lista di risposte date alla colonna X dagli appartenti al cluster 2
    # risposte_cluster_3 conterrà la lista di risposte date alla colonna X dagli appartenti al cluster 3

    # Ottieni una lista di nomi di colonne (variabili) da testare (escludendo la colonna "Labels K-means")
    variabili_da_testare = originalClusterized.columns.difference(['Labels K-means'])

    alpha = 0.05 / 3  # Livello di significatività

    # Creo un DataFrame vuoto per raccogliere i risultati
    # result_df = pd.DataFrame(columns=['Variabile', 'P-Value Kruskal', "Result Kruskal", "P-value MWY", "Result MWY"])
    result_df = pd.DataFrame(columns=['Variabile', 'P-Value Kruskal', "Result Kruskal"])

    for variabile in variabili_da_testare:
        risposte_cluster_1 = originalClusterized[(originalClusterized['Labels K-means'] == 0.0)][variabile].values
        risposte_cluster_2 = originalClusterized[(originalClusterized['Labels K-means'] == 1.0)][variabile].values
        risposte_cluster_3 = originalClusterized[(originalClusterized['Labels K-means'] == 2.0)][variabile].values

        result_kruskal, p_value_kruskal = stats.kruskal(risposte_cluster_1, risposte_cluster_2, risposte_cluster_3)

        # result_MWY, p_value_MWY = stats.mannwhitneyu(risposte_cluster_1, risposte_cluster_2, alternative = 'greater')

        print("Result column " + variabile + " : ")
        if p_value_kruskal < alpha:
            string_result_kruskal = "Diff. significative"
        else:
            string_result_kruskal = "Diff. NON significative"
        '''
        if p_value_MWY < alpha:
            string_result_MWY = "Diff. significative"
        else:
            string_result_MWY = "Diff. NON significative"
        '''

        # Aggiungo il p_value al DataFrame
        # result_df.loc[len(result_df)] = [variabile, p_value_kruskal, string_result_kruskal, p_value_MWY, string_result_MWY]
        result_df.loc[len(result_df)] = [variabile, p_value_kruskal, string_result_kruskal]

    return result_df