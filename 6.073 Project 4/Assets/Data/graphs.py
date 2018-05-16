import matplotlib.pyplot as plt
import os, json
import numpy as np

def load_data():
	all_data = []
	for filename in os.listdir(os.getcwd()):
		if filename.endswith('.json'):
			with open(filename) as f:
				data = json.loads(f.read())
				all_data.append(data)

	return all_data

def enemyFrequency():
	data = load_data()
	frequencies = {}
	for session in data:
		for enemies in session['enemiesHit']:
			if enemies['enemyType'].replace("Dogurai","") not in frequencies:
				frequencies[enemies['enemyType'].replace("Dogurai","")] = 0
			frequencies[enemies['enemyType'].replace("Dogurai","")] += 1
	return frequencies

def plotEnemyFrequency():
	frequencies = enemyFrequency()
	
	plt.bar(range(len(frequencies)), frequencies.values(), align='center',
		color = ["blue", "brown", "purple", "yellow", "green", "orange", "white", "red"])
	plt.xticks(range(len(frequencies)), frequencies.keys())

	plt.xlabel("Enemy")
	plt.ylabel("Frequency Hit")
	plt.title("Enemy Hit Frequencies")

	plt.show()

def averageLevelTime():
	data = load_data()
	time = {}
	frequency = {}
	for session in data:
		for levels in session['levels']:
			if levels['result'] != 'game_over':
				if levels['levelNumber'] not in time:
					time[levels['levelNumber']] = 0
					frequency[levels['levelNumber']] = 0
				time[levels['levelNumber']] += float(levels['completeTime'])
				frequency[levels['levelNumber']] += 1
	averages = {}
	for level in time:
		averages[level] = time[level]/frequency[level]
	return averages, frequency

def improvement():
	data = load_data()
	max_sessions = 0
	most_plays = None
	for session in data:
		if int(session['plays']) > max_sessions:
			max_sessions = int(session['plays'])
			most_plays = session
	levelTime = {}
	current_level = most_plays['levels'][0]['playNumber']
	times = []
	current_time = 0
	for level in most_plays['levels']:
		if level['playNumber'] != current_level:
			levelTime[current_level] = times
			current_level = level['playNumber']
			times = []
			current_time = 0
		if level['result'] == "complete":
			current_time += float(level['completeTime'])
			times.append(current_time)
	for level in levelTime:
		plt.plot(range(1, len(levelTime[level])+1), levelTime[level], marker='o', linewidth=2, label="Attempt " + str(level))
	plt.xlabel('Level')
	plt.ylabel('Time')
	plt.title('Improvement Per Attempt')
	plt.legend(loc = 'bottom right')
	plt.show()

def buttonSmash():
	data = load_data()

	total_valid = 0
	total_invalid = 0
	total = 0

	for session in data:
		total_time = float(session['keysHit'][-1]['hitTime'])
		valid_keys = 0
		invalid_keys = 0
		for keysHit in session['keysHit']:
			if keysHit['valid']:
				valid_keys += 1
			else:
				invalid_keys += 1
		total_valid += valid_keys/total_time
		total_invalid += invalid_keys/total_time
		total += (valid_keys + invalid_keys)/total_time

	print total_valid/len(data)
	print total_invalid/len(data)
	print total/len(data)


buttonSmash()


