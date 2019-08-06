input_layer_size = 1000;
hiddenlayer1_size = 500;
hiddenlayer2_size = 25;
outputlayer_size = 2;

fprintf('Reading training matrix... \n');


load data.txt;

trainingsize = 200;

X = zeros(trainingsize,1000);

count = 1;
for i = 1:trainingsize
	for k = 1:1000
		X(i,k) = data(count);
		count = count + 1;
	endfor
endfor


X = X/40;


Theta1 = zeros(500,1001);
Theta2 = zeros(25,501);
Theta3 = zeros(2,26);

y1 = zeros(trainingsize/2,2);
y2 = zeros(trainingsize/2,2);

y1(:,1) = 1;
y2(:,2) = 1;

y = [y1;y2];   % 500 x 2 matrix with [1 0] = YES and [0 1] = NO



%Unrolled theta


%lambda = 1;

%J = nnCostFunction(nn_params,input_layer_size,hiddenlayer1_size,hiddenlayer2_size,outputlayer_size,X,y,lambda);





initial_Theta1 = randInitializeWeights(1000, 500);
initial_Theta2 = randInitializeWeights(500, 25);
initial_Theta3 = randInitializeWeights(25,2);

% Unroll parameters
initial_nn_params = [initial_Theta1(:) ; initial_Theta2(:);initial_Theta3(:)];



size(initial_nn_params)

fprintf('\nTraining Neural Network... \n')
options = optimset('MaxIter', 100);
lambda = 0.001;

costFunction = @(p) nnCostFunction(p,input_layer_size,hiddenlayer1_size,hiddenlayer2_size,outputlayer_size,X,y,lambda);
[nn_params, cost] = fmincg(costFunction, initial_nn_params, options);





Theta1 = Theta1 + reshape(nn_params(1:500500),500,1001);
Theta2 = Theta2 +  reshape(nn_params(500501:513025),25,501);
Theta3 = Theta3 +  reshape(nn_params(513026:513077),2,26);


optimizedparams = [Theta1(:); Theta2(:); Theta3(:)];


save theta1.mat Theta1;
save theta2.mat Theta2;
save theta3.mat Theta3;


 