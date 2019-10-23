# Choose Your Own API

Hello, it's time to be a tad more interactive.

For the next few days, you will be assembling an API to serve data. The nature of the data doesn't matter too much - we'll be using gameshows in the examples here. But it needs to allow for certain data structures so have a look through the list of tasks to make sure you can come up with equivalent arrangements for your chosen theme. You'll also have to write a couple of `json` files with some seed data in your source code to interact with; please don't get too distracted with this!

Before you start you'll have to set up your solutions with both `src` and `test` directories. Create the source project with `dotnet new web`.

You are going to use the _file system_ as a database, but ensure that your model logic is entirely separated from your controllers, so you could convert to a database model by changing only code in `Startup.cs`.

### Testing

Create unit tests for both your models and controllers. Use TDD as much as possible. When it's impossible to write tests because methods / interfaces / whatever don't exist in your source code, then create empty methods / interfaces / whatever. But avoid writing logic without tests to back it up. When testing your models, ensure you are able to mock out your file system so you don't need to actually look at what's inside your `json` files in your tests. You could use [Moq](https://github.com/moq/moq4) for this, or have a look at [System.IO.Abstractions](https://github.com/System-IO-Abstractions/System.IO.Abstractions). **Ideally you will have a passing test before you ever run a method on the actual file system!** It would be a pain to reset the file system each time something went wrong in your source code... You do _not_ need to test _data transfer objects (DTOs)_.

### Handling your data

You will have to make decisions about how to store your data. As you aren't using a formal database, you will have almost _too much_ freedom, so think carefully and again, look ahead - aim to futureproof your design decisions because it's very awkward to go back and change your approach, even in 'real' databases. When dealing with **many-to-many relationships** in your data, you should address this with a [junction table](http://en.tekstenuitleg.net/articles/software/database-design-tutorial/many-to-many.html). Avoid duplicating any data anywhere - it will make `PATCH` and `DELETE` requests very difficult!

When querying the data you've extracted from your `json` files, you should look into [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/), a querying syntax for a variety of data structures in C#. It's not only for collections like `List` but can work on SQL databases, XML docs and more, so it's very much a transferable skill.

## Endpoints

Now on to the tasks! Your data should be queryable in the following ways:

- **`GET /api/presenters`**

Should return an array of top level data of all the presenters in your database. For example:

```json
[
  {
    "_id": 1,
    "name": "Bruce Forsyth",
    "birthYear": 1928,
    "deathYear": 2017 
  },
  {
    "_id": 2,
    "title": "Ben Shepard",
    "birthYear": 1974,
    "deathYear": null 
  }
]
```

If you've not yet come across nullable data types, then [this guide](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/nullable-types/) takes you through the basics.

- **`GET /api/gameshows`**

Should return an array of top level data of all the gameshows in your database. For example:

```json
[
  {
    "_id": 1,
    "title": "The Generation Game",
    "channel": "BBC1",
    "startYear": 1971,
    "prizes": [
      "cuddly toy",
      "family holiday"
    ]
  },
  {
    "_id": 2,
    "title": "Tipping Point",
    "channel": "ITV",
    "startYear": 2012,
    "prizes": [
      "cash"
    ]
  }
]
```

Note the [one-to-many](https://www.techopedia.com/definition/25122/one-to-many-relationship) relationship between gameshows and their prizes means that this data can live in the same JSON file.

- **`GET /api/presenting_blocks`**

Should return an array of top level data of all the presenting blocks, combined with the top level data of both the associated gameshow and the associated presenter. For example:

```json
[
  {
    "presentingBlockId": 1,
    "presenterId": 1,
    "name": "Bruce Forsyth",
    "presenterBirthYear": 1928,
    "presenterDeathYear": 2017,
    "gameshowId": 1,
    "gameshowTitle": "The Generation Game",
    "gameshowChannel": "BBC1",
    "gameshowStartYear": 1971,
    "firstShowPresentedYear": 1971,
    "lastShowPresentedYear": 2002
  }
  /// etc
]
```

Note the many-to-many relationship between gameshows and presenters means that this data must live in its own file to prevent duplication.

- **`GET /api/gameshows/:gameshowId`**
- **`GET /api/presenters/:presenterId`**

Should return the individual resource (i.e. the gameshow) _and_ any child resources associated with it (i.e. an array of all the presenting blocks). For example:

```json
{
  "_id": 1,
  "title": "The Generation Game",
  "channel": "BBC1",
  "startYear": 1971,
  "presentingBlocks": [
    {
      "presentingBlockId": 1,
      "presenterId": 1,
      "name": "Bruce Forsyth",
      "presenterBirthYear": 1928,
      "presenterDeathYear": 2017,
      "firstShowPresentedYear": 1971,
      "lastShowPresentedYear": 2002
    },
    {
      "presentingBlockId": 3,
      "presenterId": 3,
      "name": "Larry Grayson",
      "presenterBirthYear": 1923,
      "presenterDeathYear": 1995,
      "firstShowPresentedYear": 1978,
      "lastShowPresentedYear": 1981
    }
    //etc
  ]
}
```

Note that the information in the top level data isn't duplicated in the presenting block objects, which might mean the need for additional DTO structures.

You should additionally test for:
  - an invalid resource id (a non-numeric parameter that could never exist in your database)
  - an incorrect resource id (a number that doesn't exist in your database)

- **`POST /api/gameshows`**
- **`POST /api/presenters`**

Should accept the values required to create a new resource (not the `_id_`, which should be autogenerated), and
  - append the resource to the appropriate json array
  - return the newly created resource (including its `_id` and any other default values you might have included)
  - return in its headers the location of the new created resource

It's fine to entirely overwrite a file with a newly created array after an additional resource has been added to what's already there.

You should additionally test for:
  - a missing or incomplete body
  - any logical discrepancies in the submitted data (i.e. a `birthYear` that comes after a `deathYear`)

- **`POST /api/presenting_blocks`**

Similar to the above, but you should also test for
  - an invalid foreign key (i.e. a `gameshowId` that _could_ not exist in your database)
  - an incorrect foreign key (i.e. a `presenterId` that _could_ but _doesn't_ exist in your database)

- **`PATCH /api/gameshows/:gameshowId`**

Should accept a [JSON patch document](https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-3.0) for a resource and apply it to the relevant data, returning the updated resource as though it had been requested with `POST`.

An example patch doc:
```json
[
  {
    "op": "replace",
    "path": "/title",
    "value": "The Generation Game With Mel & Sue"
  },
  {
    "op": "add",
    "path": "/prizes/-",
    "value": "M&S vouchers"
  }
]
```

You should test for incorrect syntax in the PATCH doc, as well as any logical errors that would arise from the changes.

If you feel like you need more practice with patching, then feel free to implement endpoints for the other resources too - else you can move on!

- **`DELETE /api/gameshows/:gameshowId`**
- **`DELETE /api/presenters/:presenterId`**
- **`DELETE /api/presentingBlocks/:presentingBlockId`**

Should remove the requested resource.

It should also remove any child resources associated with (aka _cascade_). So removing The Generation Game should also remove Bruce Forsyth's and Larry Grayson's presenting blocks - but not the presenter resources themselves.

You should additionally test for invalid and incorrect ids. Perhaps that's logic you could extract elsewhere if you haven't yet...

## Additional tasks

- Write queries for ordering / filtering your data, i.e.
  - `/api/presenters?sort_by=yearOfBirth`
  - `/api/gameshows?channel=BBC1`

- Return your answers with sensible defaults for sorting (i.e. gameshows sorted alphabetically).

- Add pagination to your data, limiting search results to the first 10 unless a `page` query is submitted (`/api/presenters?page=2`). Return the total number of results too when page queries are submitted.