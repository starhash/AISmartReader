from random import randint

def randomize():
	fw=open("Randomized.csv")
	fw2=open("Randomize2.csv","w")
	for line in fw.readlines():
		values = line.strip() + ',' + str(randint(0,1))
		fw2.write(values + '\n')

randomize()