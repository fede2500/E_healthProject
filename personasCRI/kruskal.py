import pandas as pd
from scipy import stats

def kruskal(originalClusterized):
    # for the very same feature (so speaking about the columns):
    # cluster1_answers will contain the list of the answers of the xth column of the subjects within the cluster 1
    # cluster2_answers will contain the list of the answers of the xth column of the subjects within the cluster 2
    # cluster3_answers will contain the list of the answers of the xth column of the subjects within the cluster 3

    # the result is a list with the name of the columns (features) to test
    # (this is valid for all the columns except 'Labels K_means')
    var_to_test = originalClusterized.columns.difference(['Labels K-means'])

    # level of significance
    alpha = 0.05 / 3

    # empty dataframe which will host the results
    result_df = pd.DataFrame(columns=['Variabile', 'P-Value Kruskal', "Result Kruskal"])

    for variable in var_to_test:
        cluster1_answers = originalClusterized[(originalClusterized['Labels K-means'] == 0.0)][variable].values
        cluster2_answers = originalClusterized[(originalClusterized['Labels K-means'] == 1.0)][variable].values
        cluster3_answers = originalClusterized[(originalClusterized['Labels K-means'] == 2.0)][variable].values

        result_kruskal, p_value_kruskal = stats.kruskal(cluster1_answers, cluster2_answers, cluster3_answers)

        print("Result column " + variable + " : ")
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

        # adding the p-value
        result_df.loc[len(result_df)] = [variable, p_value_kruskal, string_result_kruskal]

    return result_df
