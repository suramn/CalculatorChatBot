# Calculator Chat Bot

This is a sample Microsoft Teams bot to be able to conduct mathematical operations

## Premise to create bot

The reason for having this bot created was to actually showcase some of the basic bot functionality with regards to Microsoft Teams. The requirements could be found below:

### Requirements

We are members of a company - XYZ Software LLC and we are developing bots, messaging extensions, and cards for the Microsoft Teams platform. We are given a task that requires for us to build a chat bot which acts as a mathematical assistant to the users. Our mathematical assistant conducts three genres of operations: arithmetic, geometric, and statistical. 

#### Arithmetic

1. Addition - returning the sum of the list of numbers  
2. Subtraction - returning the difference in the list of numbers  
3. Multiplication - returning the product when you multiply all of the numbers  
4. Division - here we may have to implement some type of constraint saying that the number of inputs must be **two integers**

#### Statistical

1. Mean - returning the average of the list of numbers
2. Median - returns the middle of the sorted list
3. Mode - returns which number(s) appear the most in a sorted numerical list
4. Range - returns the difference between the largest value and the smallest value
5. Standard Deviation - a measure that would determine how far off the data is from the mean
6. Variance - a measure that would determine the degrees of separation from the mean
7. Geometric Mean - the central number in a geometric progression
8. Root Mean Square - the square root of the arithmetic mean of the squares of a set of values

#### Geometry

1. Discriminant - this value will indicate the nature of the roots for a quadratic equation
2. Distance - this value will indicate how far apart two coordinate points are spread out. The coordinate points are 2-tuples of (x,y)
3. Midpoint - A tuple (x,y) that would represent the middle point between two coordinate points (x,y)
4. Pythagorean - Implements the Pythagoras Theorem. Returns the value of the hypotenuse when given values of the legs
5. Quadratic Solver - returns the actual roots of a quadratic equation

#### Some Fun Things

- The chat bot will actually greet you the number of times you ask it to greet you - `hello 4`
- Chat bot will also greet others in a team context, except the person that had invoked the `greet eveyone` intent - and it could make sure to have a track of the number of times that the `greet everyone` command in fact greeted the other team members
    - This will make sure that there are 1 on 1 conversations started with other team members - also another point of testing being done 

# Deployment of the code
<a href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fcoderkrishna%2FCalculatorChatBot%2Fmaster%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/deploybutton.png"/>
</a>
