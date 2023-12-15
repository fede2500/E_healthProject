import numpy as np
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt

def runPCA(data):
    # finding the best PCA components
    nums = np.arange(25)
    var_ratio = []
    for num in nums:
        pca = PCA(n_components=num)
        pca.fit(data)
        var_ratio.append(np.sum(pca.explained_variance_ratio_))

    # plotting
    plt.figure()
    plt.grid()
    plt.plot(nums, var_ratio, marker='o')
    plt.xlabel('n_components')
    plt.ylabel('Explained variance ratio')
    plt.title('n_components vs. Explained Variance Ratio')

    plt.show()

    # definitive pca
    pca_sel = PCA(n_components=15)
    scores = pca_sel.fit_transform(data)
    print(pca_sel.explained_variance_ratio_)
    print(f'Total variance explained: {pca_sel.explained_variance_ratio_.sum()}%')

    return scores, pca_sel