# Shopping Cart System Program

Rapanan, Mavrick S. BSIT 1-2

AI Usage

I used AI to understand how to use while loop in creating a yes or no question, how computation works, how do you use object arrays, how to use int.TryParse, and how do fields or objects in classes. As I was doing the project, little by little im understanding how to make things work, like how do i use the methods in the product class. While doing the project, im also watching tutorials on how to use different statements in C#, I learned alot from the youtuber Bro Code, he gives basic information about different statements. The hardest part I encounter during the project was the validation of duplicated products in my cart, since i still don't quite understand how it works.

Prompts/question I asked:
* "How do object array works, give an example of it"
* "How to exit from outer while loop using the inner loop"
* "How do i apply int.TryParse in my code"
* "How to calculate discounts in C#"

# Part 2 Changes
According to the Quiz Part 2, The system will now manage cart, which means features like view cart, remove and clear cart, and product search will be added, since form the previous quiz I did the add to cart logic, I just need to add the other features and create a menu in order to display the feature. Product Search was also required, I removed updated stocks in the previous Program, but instead I used Product Search so that user can view current stocks and see if there are changes, and I used Categories in order to filter the products so that user can just use the category to find what they need. You can also look your purchase history, by using the "View Order History". I watched youtube tutorials while working on the Quiz.

Ai Usage:

Prompts that I used in order to implement the changes;

* "Should i use switchcase or if statements to create my cart menu?"
  Instead of if statements, which is very long and confusing because of many curly braces and also not very clean to look at, I used switch case that is easy to use and much effiecient.


* How can I restock the removed or cleared item in my cart
  If you remove an item from the cart, the stock will remain from the time you added it from the cart, example you bought 10 Apples it only has 10 stock left, and you go to the menu and removed it from your cart, the 10 apple will not return to the stock but the stock will still be 0, same with the clear cart. the fix was "cart[removeItem].RemainingStock += cart_quantity[removeItem];" and "for (int i = 0; i < cart_count; i++) {cart[i].RemainingStock += cart_quantity[i];}".


  
    
