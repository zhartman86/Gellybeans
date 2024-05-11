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
fizzbuzz = -> ()
{
	_str = "";
	
	** i : 0..100 :{
		i % 3 == 0 && i % 5 == 0 ?? {
			_str += "FizzBuzz\n";
		} :	
		i % 3 == 0 {
			_str += "Fizz\n";
		} :		
		i % 5 == 0  {
			_str += "Buzz\n";
		} :	{
			_str += %"{i}\n";
		}				
	}
	
	_str;
}
```
