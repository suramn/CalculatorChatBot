# Calculator Chat Bot

This is a sample Microsoft Teams bot to be able to conduct four basic arithmetic operations

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

#### Some Fun Things

- The chat bot will actually greet you the number of times you ask it to greet you - `hello 4`
- Chat bot will also greet others in a team context, except the person that had invoked the `greet eveyone` intent - and it could make sure to have a track of the number of times that the `greet everyone` command in fact greeted the other team members
    - This will make sure that there are 1 on 1 conversations started with other team members - also another point of testing being done 

# Deployment of the code
<a href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fcoderkrishna%2FCalculatorChatBot%2Fmaster%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/deploybutton.png"/>
</a>
