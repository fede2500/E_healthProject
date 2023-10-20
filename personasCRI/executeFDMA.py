import numpy as np
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt
import prince

def runFDMA(data):
    # Find the best PCA components
    nums = np.arange(25)
    var_ratio = []
    for num in nums:
        famd = prince.FAMD(
            n_components=num,
            n_iter=3,
            copy=True,
            check_input=True,
            random_state=42,
            engine="sklearn",
            handle_unknown="error"  # same parameter as sklearn.preprocessing.OneHotEncoder
        )
        famd = famd.fit(data)
        var_ratio.append(famd.eigenvalues_summary.iloc[num-1,2])

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