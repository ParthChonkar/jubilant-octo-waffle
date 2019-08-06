fprintf('\nRunning Analyisis ... \n')

 load theta1.mat;
 load theta2.mat;
 load theta3.mat;

 load analyze.txt;

analyze = analyze/40;
 X = zeros(29,1000);

count = 1;
for i = 1:29
	for k = 1:1000
		X(i,k) = analyze(count);
		count = count + 1;
	endfor
endfor


y = ones(29,1);


for i = [1;3;5;7;9;11;13;15;17;19;21;23;25;27;29]   % NO YES NO YES...
	y(i) = 2;
endfor

y = y-1;


X1 = [ones(29,1) X];
z_layer1 = X1 * Theta1';
activation_layer1 = sigmoid(z_layer1);
activation_layer1_input = [ones(29,1) activation_layer1];
z_layer2 = activation_layer1_input * Theta2';
activation_layer2 = sigmoid(z_layer2);
activation_layer2_input = [ones(29,1) activation_layer2];
z_layer3 = activation_layer2_input * Theta3';
final_activation_layer = sigmoid(z_layer3)
[dummy, p] = max(final_activation_layer');


p = p -1;

p = p';

gate = xor(y,p);

accuracy = 1 - (sum(gate)/30)