function [J grad] = nnCostFunction(nn_params,input_layer_size,hiddenlayer1_size,hiddenlayer2_size,output_size,X, y, lambda)


%   [J grad] = NNCOSTFUNCTON(nn_params, hidden_layer_size, num_labels, ...
%   X, y, lambda) computes the cost and gradient of the neural network. The
%   parameters for the neural network are "unrolled" into the vector
%   nn_params and need to be converted back into the weight matrices. 
% 
%   The returned parameter grad should be a "unrolled" vector of the
%   partial derivatives of the neural network.


% Reshape nn_params back into original theta
Theta1 = reshape(nn_params(1:500500),500,1001);
Theta2 = reshape(nn_params(500501:513025),25,501);
Theta3 = reshape(nn_params(513026:513077),2,26);

% Setup some useful variables
m = size(X, 1);  %number of training examples
         
J = 0;
Theta1_grad = zeros(size(Theta1));
Theta2_grad = zeros(size(Theta2));
Theta3_grad = zeros(size(Theta3));

% ====================== YOUR CODE HERE ======================
% Instructions: You should complete the code by working through the
%               following parts.
%
% Part 1: Feedforward the neural network and return the cost in the
%         variable J. After implementing Part 1, you can verify that your
%         cost function computation is correct by verifying the cost
%         computed in ex4.m
%
% Part 2: Implement the backpropagation algorithm to compute the gradients
%         Theta1_grad and Theta2_grad. You should return the partial derivatives of
%         the cost function with respect to Theta1 and Theta2 in Theta1_grad and
%         Theta2_grad, respectively. After implementing Part 2, you can check
%         that your implementation is correct by running checkNNGradients
%
%         Note: The vector y passed into the function is a vector of labels
%               containing values from 1..K. You need to map this vector into a 
%               binary vector of 1's and 0's to be used with the neural network
%               cost function.
%
%         Hint: We recommend implementing backpropagation using a for-loop
%               over the training examples if you are implementing it for the 
%               first time.
%
% Part 3: Implement regularization with the cost function and gradients.
%
%         Hint: You can implement this around the code for
%               backpropagation. That is, you can compute the gradients for
%               the regularization separately and then add them to Theta1_grad
%               and Theta2_grad from Part 2.
%




%_____________________________________________________________________FORWARD PROPOGATION_________________________________________________________________%

X1 = [ones(m, 1) X];
z_layer1 = X1 * Theta1';
activation_layer1 = sigmoid(z_layer1);

activation_layer1_input = [ones(m,1) activation_layer1];
z_layer2 = activation_layer1_input * Theta2';
activation_layer2 = sigmoid(z_layer2);

activation_layer2_input = [ones(m,1) activation_layer2];
z_layer3 = activation_layer2_input * Theta3';
final_activation_layer = sigmoid(z_layer3);


final_activation_layer;
size(final_activation_layer);

y;
size(y);









J = 1/m * sum(sum(-1 * y .* log(final_activation_layer)-(1-y) .* log(1-final_activation_layer)));


thetaz1 = Theta1(:,2:end).^2;
thetaz2 = Theta2(:,2:end).^2;
thetaz3 = Theta3(:,2:end).^2;

regCONS = sum(sum(thetaz1)) + sum(sum(thetaz2)) + sum(sum(thetaz3));

regCONS = regCONS * lambda/(2*m);



J = J + regCONS;

% -------------------------------------------------------------



for i = 1:m
	zvec_layer1 = [ones(1,1) X(i,:)] * Theta1';
	avec_layer1 = sigmoid(zvec_layer1);
	zvec_layer2 = [ones(1,1) avec_layer1] * Theta2';
	avec_layer2 = sigmoid(zvec_layer2);
  zvec_layer3 = [ones(1,1) avec_layer2] * Theta3';
  avec_final = sigmoid(zvec_layer3);
  


	delta_layer1 = avec_final - y(i,:);
      


    
	delta_layer2 = (Theta3' * delta_layer1') .* [1;sigmoidGradient(zvec_layer2)'];
    
  delta_layer2 = delta_layer2(2:end);


zvec_layer1';
 sigmoidGradient(zvec_layer1)';




  delta_layer3 = (Theta2' * delta_layer2) .* [1;sigmoidGradient(zvec_layer1)'];

  delta_layer3 = delta_layer3(2:end);

  
 delta_layer3;
   
     
  Theta3_grad = Theta3_grad + delta_layer1' * [1, avec_layer2];
    
  Theta2_grad = Theta2_grad + delta_layer2 * [1,avec_layer1];

  Theta1_grad = Theta1_grad + delta_layer3 *  [1, X(i,:)];

endfor

Theta3_grad = Theta3_grad/m;
Theta2_grad = Theta2_grad/m;
Theta1_grad = Theta1_grad/m;

Theta3_grad(:,2:end) = Theta3_grad(:,2:end) + lambda/m * Theta3(:,2:end);

Theta2_grad(:, 2:end) = Theta2_grad(:, 2:end) + lambda/m * Theta2(:, 2:end);
Theta1_grad(:, 2:end) = Theta1_grad(:, 2:end) + lambda/m * Theta1(:, 2:end);











Theta1_grad;

% =========================================================================

% Unroll gradients
grad = [Theta1_grad(:) ; Theta2_grad(:); Theta3_grad(:)];


end