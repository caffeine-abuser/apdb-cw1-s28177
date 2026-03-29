# exercise 02 for apbd 2026 - equipment rental system

the file structure is as follows:

- `README.md` - you're reading this right now. hi!
- `Entry.cs` - self-explanatory. program entry, also where all the test cases are contained.
- `datamodel/` - contains all the necessary objects.
    - `devices/` - device definitions.
	- `users/` - user definitions.
	- `rental/` - contains the rental data model, relevant business logic and a place to store all the rentals in (see [`RentalService`](datamodel/rental/RentalService.cs)) and [a simple reporting module](datamodel/rental/RentalReporter.cs) that supports a bunch of prebuilt, as well support for arbitrary linq queries because it exposes the lists directly, as such:

	```cs
	var svc = new RentalService();
	// add data in the meantime...
	var usersStartingWithA = svc.Users
        .Where(user => user.Name.StartsWith('A'))
        .ToList();
	```

in a real application, the RentalService class would probably be replaced with a proper database (or at least a thin wrapper over one),
and RentalReporter would only run queries against it.

> [!NOTE]  
> fun fact! i did initially try to actually get this thing working with sqlite; alas, properly constructing parametrized queries turned out to be a titanic job  
> ESPECIALLY queries with `SELECT ... WHERE foo IN (...)`, because apparently there's... no proper mapping from C# arrays to `IN (...)`?

used the following structure because separating data from code is a long-standing software design pattern; note how the devices and users have basically no logic inside. if we really want to split hairs here and waffle about it more, then yeah i can say it's a divide between the data itself, business logic and presentation. look, it's a bit how basically everyone uses agile principles (>literally just grading features by priority, then implementing them, then reevaluating the list) without even knowing what the hell agile even *is.*

spent around 2h screwing around with trying to do users and devices w/ interfaces when i really should have used an abstract class.
for a sec i was thinking that this is rust and everything is a trait lol. alas, this is not rust.

## on cohesion, coupling and responsibilities

for solid cohesion, see [`RentalService`](datamodel/rental/RentalService.cs). it's narrow in scope, that is being an interface to whatever storage layer we're using and enforcing our rules. right now it's in-memory, but could be swapped out for a proper database Soon™. it doesn't interact with stdout, it doesn't format text, it doesn't do much else.

similarly, low coupling would be demonstrated by... once again [`RentalService`](datamodel/rental/RentalService.cs), but this time also with [`RentalReporter`](datamodel/rental/RentalReporter.cs); as long as the underlying API doesn't change, [`RentalReporter`](datamodel/rental/RentalReporter.cs) doesn't give a single toss about where the data is actually coming *from*

do i even have to say much more about responsibilities? there's a class for everything we need here, that's what it's literally about. not too many, not too little. a reasonable balance.