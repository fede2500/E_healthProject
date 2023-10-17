import pandas as pd
import missingno as msno
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
import sklearn as sk
from scipy import stats as st
import prince
from sklearn_extra.cluster import KMedoids
from sklearn.cluster import KMeans


df = pd.read_csv("DataProject.csv")
#print(df.head(5), df.info())

df_inf = df.iloc[:, 0:5]
#print(df_inf.head(3))
df_phq = df.iloc[:, 5:14]
#print(df_phq.head(3))
df_gad = df.iloc[:, 14:21]
#print(df_gad.head(3))
df_eheals = df.iloc[:,21:29]
#print(df_eheals.head(3))
df_heas = df.iloc[:, 29:42]
#print(df_heas.head(3))
df_ccs = df.iloc[:, 42:54]
#print(df_ccs.head(3))

'''
fig, axes = plt.subplots(1,5,figsize=(9, 5))
axes[0] = plt.boxplot(df_inf["age"])
axes[1] = plt.boxplot(df_inf["gender"])
axes[2] = plt.boxplot(df_inf["education"])
axes[3] = plt.boxplot(df_inf["marital"])
axes[4] = plt.boxplot(df_inf["income"])
fig.suptitle('boxplots')
plt.show()
plt.figure()
boxes = df_inf.boxplot(column=["age","gender","education","marital"])
plt.show()
plt.figure()
boxe = df_inf.boxplot(column=["marital"])
plt.show()'''
def calcolo_var(df_origin, n):
    varianza = []
    df = df_origin.copy()
    for i in range(df.shape[0]):
        for j in range(df.shape[1]):
            df.iloc[i,j] = round(df.iloc[i,j]/n, 2)
        v = round(np.nanvar(df.iloc[i,:]), 4)
        varianza.append(v)
    print(len(varianza))
    return round(np.mean(varianza),4)

print("la varianza di phq è: ", calcolo_var(df_phq,df_phq.max().max()))

print("la varianza di gad è: ", calcolo_var(df_gad,df_gad.max().max()))

print("la varianza di eheals è: ", calcolo_var(df_eheals,df_eheals.max().max()))


#substitute the NaN values with the mean of the answers to that questionnaires
#can be changed with the median or mode
def fill_nan_rows(dataf):
    for i in range(dataf.shape[0]):
        for j in range(dataf.shape[1]):
            if pd.isnull(dataf.iloc[i, j]):
                M = round(np.nanmean(dataf.iloc[i, :]))
                dataf.iloc[i, j] = M
    return dataf

#calculating mode
def moda(arr):
    x,counts = np.unique(arr, return_counts=True)
    index = np.argmax(counts)
    return x[index]

#substitute the NaN values with the most probable value of that feature
#can be changed with the median or mean
def fill_nan_cols(dataf):
    for i in range(dataf.shape[0]):
        for j in range(dataf.shape[1]):
            if pd.isnull(dataf.iloc[i, j]):
                M = round(moda(dataf.iloc[:,j]))
                #print(M)
                dataf.iloc[i, j] = M
    return dataf

#fillin up NaN values in the questionnaires
df_phq = fill_nan_rows(df_phq)
df_gad = fill_nan_rows(df_gad)
df_eheals = fill_nan_rows(df_eheals)
df_heas = fill_nan_rows(df_heas)
df_ccs = fill_nan_rows(df_ccs)

#filling NaN values in the info section
df_inf = fill_nan_cols(df_inf)

num = ["age", "income"]
cat = ["gender", "education", "marital"]
df_inf_num = df_inf[num]
df_inf_cat = df_inf[cat]

df_inf_num = df_inf_num.astype("float64")
df_inf_cat = df_inf_cat.astype("object")
df_phq_o = df_phq.astype("object")
df_gad_o = df_gad.astype("object")
df_eheals_o = df_eheals.astype("object")



df_complete = pd.concat([df_inf_cat, df_inf_num, df_phq_o, df_gad_o, df_eheals_o], axis=1)
print(df_complete.head(20))
print(df_complete.info())
#print(df_complete.isnull().sum())

famd = prince.FAMD(
    n_components=5,
    n_iter=100,
    copy=True,
    check_input=True,
    #random_state=25,
    engine="sklearn",
    handle_unknown="error"
    )  # same parameter as sklearn.preprocessing.OneHotEncoder
famd = famd.fit(df_complete)
print(famd.eigenvalues_summary)

#print(famd.row_contributions_
 #   .sort_values(0, ascending=False)
  #  .head(5)
   # .style.format('{:.3%}'))

#print(famd.column_contributions_.style.format('{:.0%}'))
#
#try to clustering with kmedoids
scores = famd.transform(df_complete)
kmedoids = KMedoids(n_clusters=3, init='random', method='pam').fit(scores)
labels = kmedoids.labels_

unique_labels = set(labels)
colors = [
    plt.cm.Spectral(each) for each in np.linspace(0, 1, len(unique_labels))
]
for k, col in zip(unique_labels, colors):
    class_member_mask = labels == k

    xy = scores[class_member_mask]
    plt.plot(
        xy.iloc[:, 0],
        xy.iloc[:, 1],
        "o",
        markerfacecolor=tuple(col),
        markeredgecolor="k",
        markersize=6,
    )

plt.plot(
    kmedoids.cluster_centers_[:, 0],
    kmedoids.cluster_centers_[:, 1],
    "o",
    markerfacecolor="cyan",
    markeredgecolor="k",
    markersize=6,
)

plt.title("KMedoids clustering. Medoids are represented in cyan.")
plt.show()

kmean = KMeans(n_clusters=3, init='random').fit(scores)
label = kmean.labels_

unique_label = set(label)
colors = [
    plt.cm.Spectral(each) for each in np.linspace(0, 1, len(unique_label))
]
for k, col in zip(unique_label, colors):
    class_member_mask = label == k

    xy = scores[class_member_mask]
    plt.plot(
        xy.iloc[:, 0],
        xy.iloc[:, 1],
        "o",
        markerfacecolor=tuple(col),
        markeredgecolor="k",
        markersize=6,
    )

plt.plot(
    kmean.cluster_centers_[:, 0],
    kmean.cluster_centers_[:, 1],
    "o",
    markerfacecolor="cyan",
    markeredgecolor="k",
    markersize=6,
)

plt.title("KMean clustering. Centers are represented in cyan.")
plt.show()

#df_inv = famd.inverse_transform(scores)
#print=(df_inv.head())

def Normalize(df_origin):
    n = df_origin.max().max()
    df = df_origin.copy()
    for i in range(df.shape[0]):
        for j in range(df.shape[1]):
            df.iloc[i, j] = df.iloc[i, j] / n
    return df
df_inf_n = Normalize(df_inf)
df_phq_n = Normalize(df_phq)
df_gad_n = Normalize(df_gad)
df_eheals_n = Normalize(df_eheals)
df_PCA = pd.concat([df_inf_n, df_phq_n, df_gad_n, df_eheals_n], axis=1)
df_PCA.astype('float64')

pca_m = sk.decomposition.PCA(n_components=15,
                             copy = True,
                             random_state = None)
model = pca_m.fit(df_PCA)
print(model.explained_variance_ratio_)
scores_p = model.transform(df_PCA)
scores_pca = pd.DataFrame(scores_p)
print(scores_pca.head())

kmean_pca = KMeans(n_clusters=3, init='random').fit(scores_pca)
label_pca = kmean_pca.labels_

unique_label = set(label_pca)
colors = [
    plt.cm.Spectral(each) for each in np.linspace(0, 1, len(unique_label))
]
for k, col in zip(unique_label, colors):
    class_member_mask = label_pca == k

    xy = scores_pca[class_member_mask]
    plt.plot(
        xy.iloc[:, 0],
        xy.iloc[:, 1],
        "o",
        markerfacecolor=tuple(col),
        markeredgecolor="k",
        markersize=6,
    )

plt.plot(
    kmean_pca.cluster_centers_[:, 0],
    kmean_pca.cluster_centers_[:, 1],
    "o",
    markerfacecolor="cyan",
    markeredgecolor="k",
    markersize=6,
)

plt.title("KMean clustering. Centers are represented in cyan.")
plt.show()

df_inverse = pd.DataFrame(model.inverse_transform(scores_p))
print(df_inverse.head())

df_w_label = df_inverse.copy()
df_w_label["labels"] = kmean_pca.labels_
print(df_w_label.head())

def Mann_Whitney(c1, c2):
    pv = []
    for i in range(29):
        h, p = st.mannwhitneyu(c1.iloc[:,i], c2.iloc[:, i])
        pv.append(p)
    print(len(pv))
    return pv

label_count = df_w_label['labels'].value_counts(ascending=False)
print(label_count.head())

pv12 = pd.DataFrame(np.array(Mann_Whitney(df_w_label.loc[df_w_label["labels"] == 1], df_w_label.loc[df_w_label["labels"] == 2])))
pv01 = pd.DataFrame(np.array(Mann_Whitney(df_w_label.loc[df_w_label["labels"] == 0], df_w_label.loc[df_w_label["labels"] == 1])))
pv02 = pd.DataFrame(np.array(Mann_Whitney(df_w_label.loc[df_w_label["labels"] == 0], df_w_label.loc[df_w_label["labels"] == 2])))
PV = pd.concat([pv01, pv02, pv12], axis = 1)
df_stat = PV.set_index([df_PCA.columns])
df_stat.columns = ["01", "02" , "12"]
print(df_stat)
df_th = df_stat.loc[(df_stat["01"] < 0.001) | (df_stat["02"] < 0.001) | (df_stat["12"]< 0.001)]
print(df_th)



