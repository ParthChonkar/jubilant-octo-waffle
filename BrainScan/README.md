# BrainScan
ThinkGear EEG client connection using C# and neural network using octave


# Description
Attempt at detecting "yes" or "no" from sketchy EEG (Neurosky Mindwave) and primitive Neural network
 
 

# Does it work??
No, test accuracy was actually exactly 50% for a decision with only two outcomes (flipping a coin would work the same). 

# More info
 This NN is similar to the basic one taught in the Andrew Ng coursera ML course and some of the aux and optimizer files are from the nn assignment  
 
 # Note
 Predict script is empty - code for predict is in data collector (predicting consisted of saving data from eeg to a file which was then opened in an octave predict script) 
