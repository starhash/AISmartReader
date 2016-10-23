#coding=utf-8
import re
import codecs
from nltk.tokenize import word_tokenize
from nltk.tag import StanfordNERTagger
import textract
from string import punctuation
from collections import Counter
import httplib, urllib, base64


def strip_punctuation(s):
    return ''.join(c for c in s if c not in punctuation)

def find_between( s, first_index, last_index ):
    try:
        start = first_index
        end = last_index
        return s[start:end]
    except ValueError:
        return ""


#st = StanfordNERTagger('english.all.3class.distsim.crf.ser.gz')
st = StanfordNERTagger('/home/serveradmin/stanford-ner-2015-12-09/classifiers/english.all.3class.distsim.crf.ser.gz','/home/serveradmin/stanford-ner-2015-12-09/stanford-ner.jar')
#text = (textract.process('/Users/anmol/Downloads/potter.pdf')).decode('unicode_escape').encode('ascii','ignore')
fr = open('/home/serveradmin/HP.txt')
text = ""
for line in fr.readlines():
    text = text + " " + line

entityclass_text = st.tag(text.split())
freq_person,person_list,location_list=[],[],[]

for i in xrange(len(entityclass_text)):
        for j in xrange(len(entityclass_text[i])):
            if(entityclass_text[i][1] == "PERSON"):
                person_list.append(entityclass_text[i][0])
            elif(entityclass_text[i][1] == "LOCATION"):
                location_list.append(entityclass_text[i][0])

for i in xrange(len(person_list)):
    person_list[i] = strip_punctuation(person_list[i])

starts,clumps=[],[]
sentence = " "
person_counts = Counter(person_list).most_common(7)
for i in xrange(len(person_counts)):
    freq_person.append(person_counts[i][0])

for i in xrange(len(freq_person)):
    for j in xrange(i+1,len(freq_person)):
        starts.append([(a.start(), a.end()) for a in list(re.finditer('{}(.*){}(.*)\.'.format(freq_person[i],freq_person[j]), text))])

for i in xrange(len(starts)):
    for j in xrange(len(starts[i])):
        sentence += find_between(text, starts[i][j][0], starts[i][j][1])
    clumps.append(sentence)
    sentence = ''

for i in xrange(len(clumps)):
    if len(clumps[i]) > 5000:
        clumps[i] = clumps[i][0:4900]


print freq_person

headers = {
    'Content-Type': 'application/json',
    'Ocp-Apim-Subscription-Key': 'e70a4ddf9e594418908b74239215e4dc',
}

params = urllib.urlencode({  })

try:
    conn = httplib.HTTPSConnection('westus.api.cognitive.microsoft.com')
    data = {"documents": [
    {
    "language": "en",
    "id": "string",
    "text": clumps[1]
    }
    ]
    }

    conn.request("POST", "/text/analytics/v2.0/sentiment?%s" % params,str(data),headers)

    response = conn.getresponse()
    data = response.read()
    print(data)
    conn.close()
except Exception as e:
    print e
