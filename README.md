# XPlaneAirportParser
What is this? Its an X-Plane 11 apt.dat parser.

#Instructions
1. Compile
2. Put your apt.dat file next to the binary. (apt.dat should be in ```X-Plane 11\Resources\default scenery\default apt dat\Earth nav data\apt.dat```)
3. Put your code in HandleAirport in Parser.cs (Shown Below)
```csharp
/// <summary>
/// Gets called whenever an airport is parsed, but your stuff you want to use the airport stuff with (Like a db insert Query!)
/// </summary>
/// <param name="airport">Parsed Airport</param>
public void HandleAirport(Airport airport)
{
	Console.WriteLine(airport.ToString());
	//Put your code in here!
}

```