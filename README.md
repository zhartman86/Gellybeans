# Gellybeans

Gellybeans is a C# library containing an expression parser built primarily for [Mathfinder](https://github.com/Gellybean/MathfinderBot), a Discord bot. Check out the [MF wiki](https://github.com/Gellybean/MathfinderBot/wiki) for more information.

Gellybeans supports:
- Integer math, interpolated strings, bools, arrays
- Variable assignment
- Dice expressions
- Conditionals
- For loops
- Functions

```
fizzbuzz = -> (count)
{
	
	** i : 0..count : {
		i % 3 == 0 && i % 5 == 0 ?? {
			print("FizzBuzz");
		} : i % 3 == 0 {
			print("Fizz)";
		} : i % 5 == 0 {
			print("Buzz");
		} : {
			print(i);
		}				
	}
}

```

``` fizzbuzz(25) ```


Output (From Mathfinder)

![Screenshot 2024-05-28 075534](https://github.com/Gellybean/Gellybeans/assets/10622391/7c4f40b9-be5e-42c5-bf58-68b6a0a96e5e)

