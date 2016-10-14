#Imports
from nltk.stem import WordNetLemmatizer
import nltk
import pdfminer
from nltk.corpus import cmudict
from scipy import stats
from sklearn.cluster import KMeans
import numpy as np
from matplotlib import pyplot
from sklearn import datasets, linear_model,preprocessing
from cStringIO import StringIO
from pdfminer.pdfinterp import PDFResourceManager, PDFPageInterpreter
from pdfminer.converter import TextConverter
from pdfminer.layout import LAParams
from pdfminer.pdfpage import PDFPage
from pdfminer.pdfparser import PDFParser
from pdfminer.pdfdocument import PDFDocument
from random import randint
#Classes


def nsyl(word):
	d=cmudict.dict()
  	#lowercase = word.lowercase()
  	if word not in d:
  		return -1
  	else:
  		print max([len([y for y in x if y[-1].isdigit()]) for x in d[word]])
nsyl("ambidextrous")

def convert(fname, pages=None):
    if not pages:
        pagenums = set()
    else:
        pagenums = set(pages)

    output = StringIO()
    manager = PDFResourceManager()
    converter = TextConverter(manager, output, laparams=LAParams())
    interpreter = PDFPageInterpreter(manager, converter)

    infile = file(fname, 'rb')
    parser = PDFParser(infile)
    document = PDFDocument(parser)

    for page in PDFPage.get_pages(infile, pagenums):
        interpreter.process_page(page)
    toc = list()
    for (level,title,dest,a,structelem) in document.get_outlines():
        toc.append((level, title))
    print toc
    infile.close()
    converter.close()
    text = output.getvalue()
    output.close
    #print text 

#convert("TCP.pdf",pages=[1,2])

def lemma():
	
	lemmatizer=WordNetLemmatizer()
	print (lemmatizer.lemmatize("cats"))

def POS():
	fr=open("definitions.txt")
	fw=open("def_pos.txt")
	STOP_TYPES = ['DT','IN','CD','NNP','NN','NNS','PRP','TO','.',',','?','!','\'']
	for text in fr.readlines():
		t=nltk.word_tokenize(text)
		t= nltk.pos_tag(t)
		good_words = [(w,wtype) for w, wtype in t if wtype not in STOP_TYPES]
		print good_words
		lemmatizer=WordNetLemmatizer()
		for x in range(0,len(good_words)):
			if(str(good_words[x][1])[0]=="J"):
				print (lemmatizer.lemmatize(t[x][0],"a"))
			elif str(good_words[x][1])[0]=="V" :
				print (lemmatizer.lemmatize(t[x][0],"v"))
		#for i in range(0,len(t)):
		#	st+=str(t[0])+","str(t)
		#fw.write(t)
	fr.close()
	fw.close()

#POS()
def linear():
	attr=[]
	labels=[]
	dataMat=[]
	fr = open("words-lim-len4_output.csv")
	for line in fr.readlines():
		values = line.strip().split(',')
		val = 0
		sub = values[1:5]
		sub.append(val)
		dataMat.append(sub)
		attr.append(values[1:5])
		labels.append(values[5])

	fr.close()
	attr=np.array(attr).astype(np.float64)
	labels=np.array(labels).astype(np.float64)
	#min_max_scaler = preprocessing.MinMaxScaler()
	#attr= min_max_scaler.fit_transform(attr)
	regr = linear_model.LinearRegression()
	
	regr.fit(attr, labels)
	coeff=regr.coef_
	#print regr.predict(attr)
	print "coefficients are"
	print  coeff
	pyplot.scatter(labels, regr.predict(attr),  color='black')
	
	pyplot.show()
#linear()
'''
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
i=0
fr = open("Input3.csv")
for line in fr.readlines():
	i=i+1
	
	values = line.strip().split(',')
	val = 0
	sub = values[1:5]
	
	sub = [float(x) for x in sub]
	#sub.append(val)
	dataMat.append(sub)
fr.close()
print sub[0]
print i
try:
	dataMat=np.array(dataMat).astype(np.float64)
except Exception, e:
	print dataMat[-1]

	i=0
dataMat=np.array(dataMat)
min_max_scaler = preprocessing.MinMaxScaler()
#dataMat= min_max_scaler.fit_transform(dataMat)
print dataMat[-1]
for x in range(0,len(dataMat)):
	if(dataMat[x][1]!=0):
		dataMat[x][1]=1/dataMat[x][1]*10
	else:
		dataMat[x][1]=10
kmeans = KMeans(n_clusters = 5)
kmeans.fit(dataMat)
centroids = kmeans.cluster_centers_
labels = kmeans.labels_
#print centroids
'''
#assignment,cdist = cluster.vq.vq(tests,centroids)
'''
dataMat= kmeans.transform(dataMat)
centroids=kmeans.transform(centroids)
print dataMat[-1]
for i in range(0,len(dataMat[0:20000])):
	if labels[i]==0:	
		pyplot.scatter(dataMat[i,0],dataMat[i,1],color='blue')
	elif labels[i]==1:
		pyplot.scatter(dataMat[i,0],dataMat[i,1],color='red')
	elif labels[i]==2:
		pyplot.scatter(dataMat[i,0],dataMat[i,1],color='green')
	elif labels[i]==3:
		pyplot.scatter(dataMat[i,0],dataMat[i,1],color='yellow')
	elif labels[i]==4:
		pyplot.scatter(dataMat[i,0],dataMat[i,1],color='orange')

print centroids
pyplot.scatter(centroids[:,0],centroids[:,1],color='black')
pyplot.show()
fr = open("Input3.csv")
fw = open("words-lim-len4_output.csv", 'w')
i = 0
for ln in fr.readlines():
	s = ln.strip() + "," + str(labels[i]) + "\n"
	fw.write(s)
	i = i + 1
fw.close()
'''
#y=-3.246/10x1 -3.617/10^15x2 + 6.59/10x3+-5.699/10x4 + -2.901/1000x5
'''

'''