import matplotlib.pyplot as plt
from sklearn.cluster import KMeans
from yellowbrick.cluster import SilhouetteVisualizer
import pandas as pd
import seaborn as sns
import plotly.express as px

def runClusterizer(scores):
    # clustering with k medoids --> use silhouette method to assess k

    fig, ax = plt.subplots(5, 2, figsize=(15, 8))
    wcss = []
    for i in range(2, 10):
        km = KMeans(n_clusters=i, init='k-means++', n_init=10, max_iter=100, random_state=42)
        q, mod = divmod(i, 2)
        visualizer = SilhouetteVisualizer(km, colors='yellowbrick', ax=ax[q - 1][mod])
        visualizer.fit(scores)
        km.fit(scores)
        wcss.append(km.inertia_)

    plt.figure()
    plt.plot(range(2, 10), wcss, marker='o', linestyle='--')
    plt.xlabel('Number of clusters')
    plt.ylabel('WCSS')
    plt.title('K-means with PCA clustering')

    # %%
    # we can select k=3
    km_sel = KMeans(n_clusters=3, init='k-means++', n_init=10, max_iter=100, random_state=42)
    km_sel.fit(scores)
    # %%
    col = ['PC1', 'PC2', 'PC3', 'PC4', 'PC5', 'PC6', 'PC7', 'PC8', 'PC9', 'PC10', 'PC11', 'PC12', 'PC13', 'PC14',
           'PC15']
    data_scores = pd.DataFrame(scores, columns=col)
    data_scores['kmeans labels'] = km_sel.labels_
    print(data_scores.head())
    # %%
    # visualize clusters
    plt.figure()
    sns.scatterplot(x=data_scores['PC1'], y=data_scores['PC2'], hue=data_scores['kmeans labels'],
                    palette=['g', 'r', 'c'])
    plt.show()
    # %%
    fig = px.scatter_3d(
        data_scores, x='PC1', y='PC2', z='PC3', color='kmeans labels',
        title=f'Clusters',
        labels={'0': 'PC 1', '1': 'PC 2', '2': 'PC 3'}
    )
    fig.show()



    return km_sel
