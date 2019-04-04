# Calculator Chat Bot

This is a sample Microsoft Teams bot to be able to conduct four basic arithmetic operations

## Premise to create bot

The reason for having this bot created was to actually showcase some of the basic bot functionality with regards to Microsoft Teams. The requirements could be found below:

### Requirements

We are members of a company - XYZ Software LLC and we are developing bots, messaging extensions, and cards for the Microsoft Teams platform. We are given a task that requires for us to build a chat bot that can conduct four basic arithmetic operations. To review they are:

1. Addition - returning the sum of the list of numbers

2. Subtraction - returning the difference in the list of numbers

3. Multiplication - returning the product when you multiply all of the numbers

4. Division - here we may have to implement some type of constraint saying that the number of inputs must be **two integers**

#### Future Features

The following features are suggested to extend the functionality of this bot to include some basic statistical computations which include:

1. Average - also known as the mean - calculate the sum of the list of numbers, and divide by the number of integers in the list

2. Median - with the list of integers provided, we calculate the middle value of the list. If the list has an odd length `list.Length % 2 == 1`, we would then access the middle element of the list. Keep in mind, that the list should be sorted. If the list has an even length `list.Length % 2 == 0`, we would then take the average of the middle two elements. Again, it is crucial that we sort the list first before doing anything.

3. Mode - with the list of integers that are provided we need to see which elements of the list appear the most. In this case, a list may have three scenarios:
   - The list is multi-modal - meaning that we have more than 1 element that appears more than once in the list
   - The list is uni-modal - meaning that there is 1 and only 1 element that appears multiple times in the list
   - The list contains no mode at all - meaning that this list contains unique and distinct values i.e. `[1, 0, 2, 3, 5, 52, 6]`

4. Range - with the list of integers we find the difference between the lowest valued element in the list and the highest valued element in the list. So from the list in the previous example, the range = 52 - 0 = 52 because the highest value in the list is 52 and the lowest value in the list is 0.

#### Some Fun Things

- The chat bot will actually greet you the number of times you ask it to greet you - `hello 4`
- Chat bot will also greet others in a team context, except the person that had invoked the `greet eveyone` intent - and it could make sure to have a track of the number of times that the `greet everyone` command in fact greeted the other team members
    - This will make sure that there are 1 on 1 conversations started with other team members - also another point of testing being done 
