import numpy as np
from scipy import stats
import itertools
import pandas as pd


def mwu(originalClusterized):

    # the result is a list with the name of the columns (aka the features) to test
    # (this is true for all the features except 'Labels K-means')
    var_to_test = originalClusterized.columns.difference(['Labels K-means'])

    # empty dataframe which will host all the restults
    result_df = pd.DataFrame(columns=['Variable', "Result MWU"])

    summary_p_values = pd.DataFrame(columns=['Variable', "p 0 vs 1", "Diff 0 1", "p 0 vs 2", "Diff 0 2", "p 1 vs 2", "Diff 1 2",])

    # level of significance/3 so to apply the Bonferroni correction
    alpha = 0.05 / 3

    for variable in var_to_test:
        # fot the very same feature (column)
        # cluster1_answers will hold the list of the given answers at the xth column
        # for the memebers of the given cluster (and so on for the other clusters)
        cluster1_answers = np.array(
            originalClusterized[(originalClusterized['Labels K-means'] == 0.0)][variable].values)
        cluster2_answers = np.array(
            originalClusterized[(originalClusterized['Labels K-means'] == 1.0)][variable].values)
        cluster3_answers = np.array(
            originalClusterized[(originalClusterized['Labels K-means'] == 2.0)][variable].values)

        # creation of an array with the answers of each cluster in the column 'variable'
        groups = [cluster1_answers, cluster2_answers, cluster3_answers]

        # creation of all the possible pairs of answers (pairs of clusters)
        pairs = list(itertools.combinations(range(len(groups)), 2))

        # MWU for each pair
        results = {}
        for i, j in pairs:
            group1, group2 = groups[i], groups[j]
            statistics, p_value = stats.mannwhitneyu(group1, group2)
            results[(i, j)] = (statistics, p_value)

        # definition of a variable that will be 0 if there are no significant differences among the pairs
        # otherwise 1
        isDifferenceSign = 1
        diff_0_1 = "Diff. significative"
        diff_0_2 = "Diff. significative"
        diff_1_2 = "Diff. significative"
        result_0_1 = 0
        result_0_2 = 0
        result_1_2 = 0

        for keys, (statistics, p_value) in results.items():
             i, j = keys
             #print(f"Mann-Whitney tra Cluster {i} e Cluster {j}:")
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

             #print(f"P value: {p_value}")
             if p_value >= alpha:
                isDifferenceSign = 0

        # if isDifferenceSign has never been 0 then it means that the difference is relevant for all the pairs
        if (isDifferenceSign):
            string_result_mwu = "Diff. significative"
        else:
            string_result_mwu = "Diff. NON significative"
        result_df.loc[len(result_df)] = [variable, string_result_mwu]

        summary_p_values.loc[len(summary_p_values)] = [variable, result_0_1, diff_0_1, result_0_2, diff_0_2, result_1_2, diff_1_2]

    return result_df, summary_p_values