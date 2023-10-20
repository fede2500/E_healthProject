import numpy as np
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt
import prince

def runFDMA(data):

    num = ["age", "income"]
    cat = ["gender", "education", "marital"]
    data=data.astype("object")
    data[num] = data[num].astype("float64")

    print(data.dtypes)

    # Find the best PCA components
    nums = range(25)
    var_ratio = []
    '''
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
        famd2 = famd.fit(data)
        #var_ratio.append(famd.eigenvalues_summary.iloc[num-1,2])
    '''

    famd = prince.FAMD(
        n_components=15,
        n_iter=3,
        copy=True,
        check_input=True,
        random_state=42,
        engine="sklearn",
        handle_unknown="error"  # same parameter as sklearn.preprocessing.OneHotEncoder
    )

    famd = famd.fit(data)
    var_ratio.append(famd.eigenvalues_summary.iloc[10 - 1, 2])
    '''
    plt.figure()
    plt.grid()
    plt.plot(nums, var_ratio, marker='o')
    plt.xlabel('n_components')
    plt.ylabel('Explained variance ratio')
    plt.title('n_components vs. Explained Variance Ratio')

    plt.show()
    '''

    # definitive pca
    scores = famd.row_coordinates(data)
    print(scores)

    return scores