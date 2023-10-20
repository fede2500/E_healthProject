import numpy as np
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt
from prince import FAMD

def runFDMA(data):

    cat = ["gender","marital","education"]
    num = data.columns.difference(cat)

    data[num] = data[num].astype(float)
    data[cat] = data[cat].astype(str)

    print(data.dtypes)
    print(data)

    # Find the best FDMA components
    var_ratio = []

    for num in range(1,25):
        famd = FAMD(
            n_components=num,
            n_iter=3
              # same parameter as sklearn.preprocessing.OneHotEncoder
        )
        famd = famd.fit(data)
        var_ratio.append(famd.eigenvalues_summary.iloc[num-1,2])


    plt.figure()
    plt.grid()
    plt.plot(range(1,25), var_ratio, marker='o')
    plt.xlabel('n_components')
    plt.ylabel('Explained variance ratio')
    plt.title('n_components vs. Explained Variance Ratio')

    plt.show()

    famd_fin = FAMD(
        n_components=15,
        n_iter=3
        # same parameter as sklearn.preprocessing.OneHotEncoder
    )
    famd_fin = famd_fin.fit(data)

    # definitive FDMA
    scores = famd_fin.row_coordinates(data)

    return famd_fin, scores