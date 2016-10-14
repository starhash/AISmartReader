#Imports
import scipy
from sklearn.cluster import KMeans
import time

#Functions
class Benchmarker:
    def __init__(self):
        self.__time__ = 0

    def benchmark(self, operation):
        self.__time__ = time.clock()
        self.showbenchmark(operation)

    def showbenchmark(self, operation):
        print operation, "@", self.__time__

#Classes
class WordRow:
    def __init__(self, kwargs):
        self.word = kwargs[0]
        self.cluster = kwargs[1]
        self.length = kwargs[2]
        self.freq = kwargs[3]
        self.logfreq = kwargs[4]
        self.nphon = kwargs[5]
        self.nsyll = kwargs[6]
        self.nmorph = kwargs[7]
        self.pos = kwargs[8]

    def __str__(self):
        return self.word + ": " + str(self.cluster)

#Variables
__POS = { "JJ" : 1, "NN" : 2, "RB" : 4, "VB" : 8, "encl" : 16, "minor" : 32, "?" : 0 }
        
#benchmarking 1
b = Benchmarker()
b.benchmark('start')

#Code
dataMat = []
fr = open("(exclude- NN,minor).csv")
for line in fr.readlines():
    values = line.strip().split(',')
    val = 0
    for x in values[7].split('|'):
        val = val | __POS[x]
    sub = values[1:7]
    sub.append(val)
    dataMat.append(sub)

fr.close()

#benchmarking 2
b.benchmark('read')

kmeans = KMeans(n_clusters = 32)
kmeans.fit(dataMat)
centroids = kmeans.cluster_centers_
labels = kmeans.labels_

#benchmarking 3
b.benchmark('kmeans')

print(centroids)
print(labels)

fr = open("(exclude- NN,minor).csv")
fw = open("(exclude- NN,minor)-output.csv", 'w')
i = 0
wordRows = []
for ln in fr.readlines():
    values = ln.strip().split(',')
    values.insert(1, labels[i])
    r = WordRow(values)
    wordRows.append(r)
    s = ln.strip() + "," + str(labels[i]) + "\n"
    fw.write(s)
    i = i + 1
fw.close()

#benchmarking 4
b.benchmark('wrote')

wordRows.sort(key = lambda x: x.cluster, reverse = True)

#benchmarking 5
b.benchmark('sort')

clusterWordRows = {}
for i in range(0, 128):
    clusterWordRows[i] = []

for i in range(0, len(wordRows)):
    clusterWordRows[labels[i]].append(wordRows[i])

#benchmarking 5
b.benchmark('clustergroups')

for k in clusterWordRows.keys():
    clusterWordRows[k].sort(key = lambda x: x.freq, reverse = True)

for k in clusterWordRows[0]:
    print k








