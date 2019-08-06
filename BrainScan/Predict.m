fprintf('\nPrediction TIME! ... \n')

 load theta1.mat;
 load theta2.mat;
 load theta3.mat;


while true
	fprintf('Hit any key !ONCE UR THOUGHT FILE IS WRITTEN! to run predict sequence \n')
	pause;

	load predictdata.txt
	predictdata = predictdata/40;
    m = size(predictdata,1);
	X1 = [1, predictdata'];
	z_layer1 = X1 * Theta1';
	activation_layer1 = sigmoid(z_layer1);

	activation_layer1_input = [1 activation_layer1];
	z_layer2 = activation_layer1_input * Theta2';
	activation_layer2 = sigmoid(z_layer2);

	activation_layer2_input = [1 activation_layer2];
	z_layer3 = activation_layer2_input * Theta3';
	final_activation_layer = sigmoid(z_layer3)

	[dummy, p] = max(final_activation_layer);
	if (p==1) fprintf('Prediction: Y E S \n') endif
	if (p==2 ) fprintf('Prediction: N O \n') endif

endwhile
