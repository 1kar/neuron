import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'

import pandas
import numpy as np
from keras.models import Sequential
from keras.layers import Dense
from keras.wrappers.scikit_learn import KerasClassifier
from keras.utils import np_utils
from sklearn.model_selection import cross_val_score
from sklearn.model_selection import KFold
from sklearn.preprocessing import LabelEncoder
from sklearn.pipeline import Pipeline

# ЗАГРУЗКА ОБУЧАЮЩЕЙ ВЫБОРКИ (ИЗ ФАЙЛА)
dataframe = pandas.read_csv("pipis.csv", header=None)
dataset = dataframe.values
X = dataset[:,0:3].astype(float)
Y = dataset[:,3]

# КОДИРОВАНИЕ Y В ФОРМАТ "ГОРЯЧЕГО КОДИРОВАНИЯ"
encoder = LabelEncoder()
encoder.fit(Y)
encoded_Y = encoder.transform(Y)
print(X)
print(Y)
dummy_y = np_utils.to_categorical(encoded_Y)


# ФОРМИРОВАНИЕ МОДЕЛИ НЕЙРОНОЙ СЕТИ
model = Sequential()
# ДОБАВЛЕНИЕ СКРЫТОГО СЛОЯ НА 8 НЕЙРОНОВ ИЗ ВХОДНОГО НА 3 НЕЙРОНА И НАЗНАЧЕНИЯ ЕМУ ФУНКЦИИ АКТИВАЦИИ ReLU
model.add(Dense(8, input_dim=3, activation='relu'))
# ДОБАВЛЕНИЕ ВЫХОДНОГО СЛОЯ НА 3 НЕЙРОНА ИЗ СКРЫТОГО НА 8 НЕЙРОВ И НАЗНАЧЕНИЯ ЕМУ ФУНКЦИИ АКТИВАЦИИ SOFTMAX
model.add(Dense(3, activation='softmax'))
# КОМПИЛЯЦИЯ МОДЕЛИ (НАЗНАЧЕНИЯ МЕТОДА ВЫЧИСЛЕНИЯ ОШИБКИ И МЕТРИКИ)
model.compile(loss='categorical_crossentropy', optimizer='adam', metrics=['accuracy'])

# ОБУЧЕНИЕ НЕЙРОННОЙ СЕТИ С ВЫВОДОМ ОШИБКИ ДЛЯ КАЖДОЙ ЭПОХИ
hist = model.fit(X, dummy_y, epochs=1000, verbose=2)

print(hist)

# НОВЫЙ НАБОР ПАРАМЕТРОВ ДЛЯ ПРОВЕРКИ КОРРЕКТНОСТИ ПРЕДСКАЗАНИЙ НЕЙРОНОЙ СЕТИ
newX = np.array([[1.0,0.0,1.0]])
print ("Набор параметров: ", newX)
prd = (model.predict(newX)>0.5).astype("int32")
prd2 = model.predict(newX)
print(prd)
print(prd2)

newX = np.array([[0.0,1.0,1.0]])
print ("Набор параметров: ", newX)
prd = (model.predict(newX)>0.5).astype("int32")
prd2 = model.predict(newX)
print(prd)
print(prd2)

newX = np.array([[0.0,0.0,1.0]])
print ("Набор параметров: ", newX)
prd = (model.predict(newX)>0.5).astype("int32")
prd2 = model.predict(newX)
print(prd)
print(prd2)
