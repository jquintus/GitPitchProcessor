open System
type Book = { title: string; pubDate: DateTime }
type Magazine = { title: string; pubDate: DateTime }

let mad = { title = "Mad"; pubDate = new DateTime(1952, 10, 1)}

let hitchiker = { Book.title = "The Hitchhiker's Guide to the Galaxy"
                  pubDate = new DateTime(1979, 10, 12) }