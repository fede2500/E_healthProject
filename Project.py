import pandas as pd
import missingno as msno
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
import sklearn as sk
from scipy import stats as st

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

fig, axs = plt.subplots(df_phq.shape[1], figsize=(8, 12))

# Assegna un colore diverso a ciascun dato
colors = plt.cm.viridis(np.linspace(0, 1, df_phq.shape[0]))

for i in range(df_phq.shape[1]):
    axs[i].scatter(range(df_phq.shape[0]), df_phq.iloc[:, i], c=colors)
    axs[i].set_xlabel('Dati')
    axs[i].set_ylabel(f'Feature {i+1}')
    axs[i].set_title(f'Feature {i+1} vs Dati')

plt.tight_layout()
plt.show()

def calcolo_var(df, n):
    varianza = []
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



df_complete = pd.concat([df_inf, df_phq, df_gad, df_eheals, df_heas, df_ccs], axis=1, ignore_index=True)
#print(df_complete)
#print(df_complete.info())
#print(df_complete.isnull().sum())



