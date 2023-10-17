import statistics

import pandas as pd
from matplotlib import pyplot as plt
import seaborn as sns

def clustersToPersonas(pca, pcaScores, data, kmeans_pca):

    #Loadings are correlations between an original variable and the component.

    #For instance, the first value of the array shows the loading of the first feature on the first component.

    df_pca_comp = pd.DataFrame(data = pca.components_,
                               columns = data.iloc[:, :32].columns ,
                  index = ['PC1', 'PC2', 'PC3', 'PC4', 'PC5', 'PC6', 'PC7', 'PC8', 'PC9', 'PC10', 'PC11', 'PC12', 'PC13', 'PC14',
               'PC15'])
    df_pca_comp

    #display heatmap Components vs Original Features: explain how much each original feature contribute to components

    plt.figure(figsize=(48,36))
    sns.heatmap(df_pca_comp,
                vmin = -1,
                vmax = 1,
                cmap = 'RdBu',
                annot = True)
    plt.yticks([0, 1, 2,3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14],
                ['PC1', 'PC2', 'PC3', 'PC4', 'PC5', 'PC6', 'PC7', 'PC8', 'PC9', 'PC10', 'PC11', 'PC12', 'PC13', 'PC14',
               'PC15'],
               rotation = 45,
               fontsize = 12)
    plt.title('Components vs Original Features',fontsize = 14)
    plt.show()

    #K-Means algorithm has learnt from our new components and created 3 clusters .
    # I would like to see old datasets with new components and labels .

    df_segm_pca_kmeans = pd.concat([data.iloc[:, :32].reset_index(drop = True), pd.DataFrame(pcaScores)], axis = 1)
    df_segm_pca_kmeans.columns.values[-15: ] = ['PC1', 'PC2', 'PC3', 'PC4', 'PC5', 'PC6', 'PC7', 'PC8', 'PC9', 'PC10', 'PC11', 'PC12', 'PC13', 'PC14',
               'PC15']

    df_segm_pca_kmeans['Segment K-means PCA'] = kmeans_pca.labels_
    df_segm_pca_kmeans.head()

    # We calculate the means by segments.
    df_segm_pca_kmeans_freq = df_segm_pca_kmeans.groupby(['Segment K-means PCA']).median()

    #We round the results and return Dataframe without components

    return df_segm_pca_kmeans_freq.round().iloc[:, :32]