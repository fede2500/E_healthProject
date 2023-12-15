import pandas as pd
import numpy as np
from sklearn.preprocessing import MinMaxScaler
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.impute import SimpleImputer
from sklearn.decomposition import PCA

def prepareData(data):
    # the questionnaires condiered were:
    # - phq
    # - gad
    # - eheals
    data_new = data.iloc[:,0:29]
    print(data_new.head())

    # inspection of the features
    for feature in data_new.columns:
        print(feature)
    print('in total there are ' + str(len(data_new.columns)) + ' features')

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

    # the missing values are replaced with the most frequent among the specific feature
    # for education (high school) and age (42)
    data_new['age'].fillna(data_new['age'].mode()[0], inplace = True)
    data_new['education'].fillna(data_new['education'].mode()[0], inplace = True)

    print(data_new['age'].value_counts(ascending = False))
    print(data_new['education'].value_counts(ascending = False))

    # plotting gender distribution
    sns.displot(data_new['gender'], bins=4, kde=False, shrink=.8, height=6, aspect=1.5)
    plt.title('Gender Distribution')
    plt.xlabel('Gender')
    plt.ylabel('Count')
    # plt.show()
    print("Most of the survey partecipants didn't mention their gender")

    # splitting the questionnaires
    df_phq = data_new.iloc[:, 5:14]
    # print(df_phq.head(3))
    df_gad = data_new.iloc[:, 14:21]
    # print(df_gad.head(3))
    df_eheals = data_new.iloc[:,21:29]
    # print(df_eheals.head(3))

    # plotting
    fig, axs = plt.subplots(df_phq.shape[1], figsize=(8, 12))
    # for each value a different color
    colors = plt.cm.viridis(np.linspace(0, 1, df_phq.shape[0]))

    for i in range(df_phq.shape[1]):
        axs[i].scatter(range(df_phq.shape[0]), df_phq.iloc[:, i], c=colors)
        axs[i].set_xlabel('Dati')
        axs[i].set_ylabel(f'Feature {i + 1}')
        axs[i].set_title(f'Feature {i + 1} vs Dati')

    plt.tight_layout()

    # plt.show()

    # computation of the variance for each questionnaire
    def calcolo_var(df, n):
        dfc = df.copy()
        varianza = []
        for i in range(dfc.shape[0]):
            for j in range(dfc.shape[1]):
                dfc.iloc[i, j] = round(dfc.iloc[i, j] / n, 2)
            v = round(np.nanvar(dfc.iloc[i, :]), 4)
            varianza.append(v)
        return round(np.mean(varianza), 4)

    print("the phq questionnaire variance is: ", calcolo_var(df_phq, df_phq.max().max()))
    print("the gad questionnaire variance is: ", calcolo_var(df_gad, df_gad.max().max()))
    print("the eheals questionnaire variance is: ", calcolo_var(df_eheals, df_eheals.max().max()))

    # low variance for the various answers
    # so to handle the missing values within the questionnaires
    # it has been chosen to fill them with the mean of the answers of each questionnaire
    imp = SimpleImputer(missing_values=np.nan, strategy='mean')
    df_phq = pd.DataFrame(imp.fit_transform(df_phq.T).T, columns=df_phq.columns)
    df_gad = pd.DataFrame(imp.fit_transform(df_gad.T).T, columns=df_gad.columns)
    df_eheals = pd.DataFrame(imp.fit_transform(df_eheals.T).T, columns=df_eheals.columns)

    # additional variables added
    # these new features contains the sum of the answers of each questionnaire for each subject
    df_phq['sum phq'] =df_phq.sum(axis=1)
    df_gad['sum gad'] =df_gad.sum(axis=1)
    df_eheals['sum e_heals'] =df_eheals.sum(axis=1)

    # creation of the full data set
    data_full = pd.concat([data_new.iloc[:,0:5],df_phq,df_gad,df_eheals],axis=1)
    print(data_full.head(30))

    # check of the missing values
    print(data_full.isnull().sum())

    '''
    #%%
    # scaling data
    scaler = MinMaxScaler(feature_range=(0,1))
    scaler = scaler.fit(data_full)
    scaled_df=pd.DataFrame(scaler.transform(data_full))
    scaled_df.columns=data_full.columns
    '''

    return data_full