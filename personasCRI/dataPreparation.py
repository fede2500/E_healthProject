import pandas as pd
import numpy as np
from sklearn.preprocessing import StandardScaler

from sklearn.decomposition import PCA

def prepareData(data):
    # we decide to not consider questionare about climate change
    data_new = data.iloc[:,0:29]
    print(data_new.head())
    #%%
    # finding the missing values
    missing_values = data_new.isnull().sum()
    print(missing_values)
    #%%
    # the missing age is replaced with the most common value
    data_new['age'].fillna(data_new['age'].mode()[0], inplace = True)
    data_new['education'].fillna(data_new['education'].mode()[0], inplace = True)
    #%%
    # nan values for the age filled with the mode (in this case is 42)
    print(data_new['age'].value_counts(ascending = False))
    # nan values for education filled with the mode (in this case is high school)
    print(data_new['education'].value_counts(ascending = False))
    #%%
    # la varianza tra le risposte di ciasuna persona è bassa, dunque
    # substitute the NaN values with the mean of the answers of each person to that questionnaires
    def fill_nan_rows(dataf):
        for i in range(dataf.shape[0]):
            for j in range(dataf.shape[1]):
                if pd.isnull(dataf.iloc[i, j]):
                    M = round(np.nanmean(dataf.iloc[i, :]))
                    dataf.iloc[i, j] = M
        return dataf
    #%%
    # select subsets of the different questionnaires
    df_phq = data_new.iloc[:, 5:14]
    #print(df_phq.head(3))
    df_gad = data_new.iloc[:, 14:21]
    #print(df_gad.head(3))
    df_eheals = data_new.iloc[:,21:29]
    #print(df_eheals.head(3))
    #%%
    #fillin up NaN values in the questionnaires
    df_phq = fill_nan_rows(df_phq)
    df_gad = fill_nan_rows(df_gad)
    df_eheals = fill_nan_rows(df_eheals)
    #%%
    #create the full data set
    data_full = pd.concat([data_new.iloc[:,0:5],df_phq,df_gad,df_eheals],axis=1)
    print(data_full.head(30))
    #%%
    # check the missing values
    print(data_full.isnull().sum())
    #%%
    # scaling data
    std_scaler = StandardScaler()
    scaled_df = std_scaler.fit_transform(data_full)

    return scaled_df