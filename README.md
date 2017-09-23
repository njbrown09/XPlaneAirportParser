# XPlaneAirportParser
What is this? Its an X-Plane 11 apt.dat parser.


## Instructions
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

## Notes
1. This dosent process helipads, but that should be really easy to add (Im lazy and have no use for it)
2. Some latituse/longitude data may be very slightly off. Xplane does not provide airport lat lon data for all airports, so if it cant find it, it uses one of the airports runways as the lat and lon
3. You should get around 20263 airports parsed.
3. This runs significantly slower in visual studio. If you want it to parse in around 3 seconds, run the exe by itself
