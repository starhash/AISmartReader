#Imports
import scipy
from sklearn.cluster import KMeans
from matplotlib import pyplot	
import numpy as np
import matplotlib.pyplot as plt
from matplotlib import mlab

#Classes
class WordRow:
    def __init__(self, kwargs):
        self.word = kwargs[0]
        self.length = kwargs[1]
        self.freq = kwargs[2]
        self.logfreq = kwargs[3]
        self.nphon = kwargs[4]
        self.nsyll = kwargs[5]
        self.nmorph = kwargs[6]
        #Variables
__POS = { "JJ" : 1, "NN" : 2, "RB" : 4, "VB" : 8, "encl" : 16, "minor" : 32, "?" : 0 }

#Code
dataMat = []
fr = open("(exclude- NN,minor).csv")
for line in fr.readlines():
    values = line.strip().split(',')
    val = 0
    sub = values[1:6]
    sub.append(val)
    dataMat.append(sub)
fr.close()
kmeans = KMeans(n_clusters = 3)
res=kmeans.fit(dataMat)
print res
centroids = kmeans.cluster_centers_
labels = kmeans.labels_

'''
assignment,cdist = cluster.vq.vq(tests,centroids)
'''
#pyplot.plot(dataMat[:, 0], dataMat[:, 1], 'k.', markersize=2)
pyplot.scatter(centroids[:, 0], centroids[:, 1], marker='x')
pyplot.show()

print(centroids)
print(labels)
fr = open("(exclude- NN,minor).csv")
fw = open("(exclude- NN,minor)_output.csv", 'w')
fw_levels = [open("(exclude- NN,minor)_output_label=0.csv", 'w'), open("(exclude- NN,minor)_output_label=1.csv", 'w'), open("(exclude- NN,minor)_output_label=2.csv", 'w') ]
i = 0
for ln in fr.readlines():
    s = ln.strip() + "," + str(labels[i]) + "\n"
    fw_levels[int(labels[i])].write(s)
    fw.write(s)
    i = i + 1
#    print s
# fw.close()

for i in range(0, 3):
    fw_levels[i].close()

fr_levels = [open("(exclude- NN,minor)_output_label=0.csv", 'r'), open("(exclude- NN,minor)_output_label=1.csv", 'r'), open("(exclude- NN,minor)_output_label=2.csv", 'r') ]
for fr in fr_levels:
    dataMat = []
    for line in fr.readlines():
        values = line.strip().split(',')
        val = 0
        sub = values[1:6]
        sub.append(val)
        dataMat.append(sub)
    fr.close()
    kmeans = KMeans(n_clusters = 3)
    dataMat=kmeans.fit(dataMat)
    print dataMat
    centroids = kmeans.cluster_centers_
    labels = kmeans.labels_

    '''
    assignment,cdist = cluster.vq.vq(tests,centroids)
    '''
    #pyplot.plot(dataMat[:, 0], dataMat[:, 1], 'k.', markersize=2)
    pyplot.scatter(centroids[:, 0], centroids[:, 1], marker='x')
    pyplot.show()

    fr = open(fr.name)
    fw = open(fr.name + '_labels.csv', 'w')
    i = 0
    for ln in fr.readlines():
        s = ln.strip() + "," + str(labels[i]) + "\n"
        fw.write(s)
        i = i + 1
    #    print s
    fw.close()