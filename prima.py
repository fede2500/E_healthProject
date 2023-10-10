import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns

# loading of the database given by the professor
# main content of the csv:
# - generic data of the interviewed users (age, gender, education...)
# - answers to some questionnaries (to be decided which ones to keep)
data = pd.read_csv('dataset.csv')
print(data.head())

# we decide to not consider questionare about eco-anxiety
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
sns.histplot(data_new['age'], bins=20, kde=False)
plt.title('Age Distribution')
plt.xlabel('Age')
plt.ylabel('Count')
plt.show()
print('The most frequent age is ' + str(data_new['age'].mode()[0]) + ' years old')

# plotting education distribution
sns.histplot(data_new['education'], bins=20, kde=False)
plt.title('Education Distribution')
plt.xlabel('Education')
plt.ylabel('Count')
plt.show()
print('The most frequent education is ' + str(data_new['education'].mode()[0]) )

# the missing age is replaced with the most common value
data_new['age'].fillna(data_new['age'].mode()[0], inplace = True)
data_new['education'].fillna(data_new['education'].mode()[0], inplace = True)

# nan values for the age filled with the mode (in this case is 42)
print(data_new['age'].value_counts(ascending = False))
# nan values for education filled with the mode (in this case is high school)
print(data_new['education'].value_counts(ascending = False))


# plotting gender distribution
sns.displot(data_new['gender'], bins = 4, kde = False, shrink = .8, height = 6, aspect = 1.5, palette = 'Set2')
plt.title('Gender Distribution')
plt.xlabel('Gender')
plt.ylabel('Count')
plt.show()
print("Most of the survey partecipants didn't mention their gender")


