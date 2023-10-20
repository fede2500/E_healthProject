import pandas as pd
import numpy as np
from sklearn.preprocessing import MinMaxScaler
import matplotlib as plt
import seaborn as sns
from sklearn.impute import SimpleImputer

from sklearn.decomposition import PCA

def prepareData(data):
    # we decide to not consider questionare about climate change
    data_new = data.iloc[:,0:29]
    print(data_new.head())

    # inspection of the features
    for feature in data_new.columns:
        print(feature)
    print('in total there are ' + str(len(data_new.columns)) + ' features')


    #%%
    # finding the missing values
    missing_values = data_new.isnull().sum()
    print(missing_values)

    # plotting age distribution
    plt.figure()
    sns.histplot(data_new['age'], bins=20, kde=False)
    plt.title('Age Distribution')
    plt.xlabel('Age')
    plt.ylabel('Count')
    # plt.show()
    print('The most frequent age is ' + str(data_new['age'].mode()[0]) + ' years old')


    # plotting education distribution
    plt.figure()
    sns.histplot(data_new['education'], bins=20, kde=False)
    plt.title('Education Distribution')
    plt.xlabel('Education')
    plt.ylabel('Count')
    # plt.show()
    print('The most frequent education is ' + str(data_new['education'].mode()[0]))


    #%%
    # the missing age is replaced with the most common value
    data_new['age'].fillna(data_new['age'].mode()[0], inplace = True)
    data_new['education'].fillna(data_new['education'].mode()[0], inplace = True)
    #%%
    # nan values for the age filled with the mode (in this case is 42)
    print(data_new['age'].value_counts(ascending = False))
    # nan values for education filled with the mode (in this case is high school)
    print(data_new['education'].value_counts(ascending = False))

    # plotting gender distribution
    sns.displot(data_new['gender'], bins=4, kde=False, shrink=.8, height=6, aspect=1.5, palette='Set2')
    plt.title('Gender Distribution')
    plt.xlabel('Gender')
    plt.ylabel('Count')
    # plt.show()
    print("Most of the survey partecipants didn't mention their gender")


    #%%


    #%%
    # select subsets of the different questionnaires
    df_phq = data_new.iloc[:, 5:14]
    #print(df_phq.head(3))
    df_gad = data_new.iloc[:, 14:21]
    #print(df_gad.head(3))
    df_eheals = data_new.iloc[:,21:29]
    #print(df_eheals.head(3))

    fig, axs = plt.subplots(df_phq.shape[1], figsize=(8, 12))
    # Assegna un colore diverso a ciascun dato
    colors = plt.cm.viridis(np.linspace(0, 1, df_phq.shape[0]))

    for i in range(df_phq.shape[1]):
        axs[i].scatter(range(df_phq.shape[0]), df_phq.iloc[:, i], c=colors)
        axs[i].set_xlabel('Dati')
        axs[i].set_ylabel(f'Feature {i + 1}')
        axs[i].set_title(f'Feature {i + 1} vs Dati')

    plt.tight_layout()

    # plt.show()
    def calcolo_var(df, n):
        dfc = df.copy()
        varianza = []
        for i in range(dfc.shape[0]):
            for j in range(dfc.shape[1]):
                dfc.iloc[i, j] = round(dfc.iloc[i, j] / n, 2)
            v = round(np.nanvar(dfc.iloc[i, :]), 4)
            varianza.append(v)
        return round(np.mean(varianza), 4)

    print("la varianza di phq è: ", calcolo_var(df_phq, df_phq.max().max()))
    print("la varianza di gad è: ", calcolo_var(df_gad, df_gad.max().max()))
    print("la varianza di eheals è: ", calcolo_var(df_eheals, df_eheals.max().max()))

    # la varianza tra le risposte di ciasuna persona è bassa, dunque
    # substitute the NaN values with the mean of the answers of each person to that questionnaires

    #%%
    #fillin up NaN values in the questionnaires

    imp = SimpleImputer(missing_values=np.nan, strategy='mean')
    df_phq = pd.DataFrame(imp.fit_transform(df_phq.T).T, columns=df_phq.columns)
    df_gad = pd.DataFrame(imp.fit_transform(df_gad.T).T, columns=df_gad.columns)
    df_eheals = pd.DataFrame(imp.fit_transform(df_eheals.T).T, columns=df_eheals.columns)

    df_phq['sum phq'] =df_phq.sum(axis=1)
    df_gad['sum gad'] =df_gad.sum(axis=1)
    df_eheals['sum e_heals'] =df_eheals.sum(axis=1)


    #%%
    #create the full data set
    data_full = pd.concat([data_new.iloc[:,0:5],df_phq,df_gad,df_eheals],axis=1)
    print(data_full.head(30))
    #%%
    # check the missing values
    print(data_full.isnull().sum())

    #%%
    # scaling data
    std_scaler = MinMaxScaler()
    scaled_df = std_scaler.fit_transform(data_full)

    return scaled_df, data_full