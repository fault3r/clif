# Mongo Database

a NoSQL database management system.
---

### ` use("[DATABASE]"); ` create or use the database.

### ` db.getCollection("[COLLECTION]"); ` create (after first insert) or use the collection.

---

### Methods in MongoDB for executing commands and queries.

### Syntax
` db.[COLLECTION].[METHOD]({"[KEY]":"[VALUE]"}); ` <br>
` db.[COLLECTION].[METHOD]({_id:ObjectId("[ID]")}); `

| **METHOD** | description |
|:---:|:---:|
| insertOne({}); | inserts a single document |
| insertMany([{},{}]); | inserts multiple documentsa |
| findOne({}); | retrieves a single document |
| find({}); | retrieves all documents that match |
| updateOne({},{$set:{}}); | updates a single document |
| updateMany({},{$set:{}}); | updates all documents that match |
| deleteOne({}); | deletes a single document |
| deleteMany({}); | deletes all documents that match |

## Examples

```
 db.getCollection("TestCollection").insertMany([
    {
        "tKey" : "test1"        
    },
    {
        "tKey" : "test2"        
    },
]); 
```

```
db.TestCollection.updateOne(
    { "tKey" : "test1-" },
    {
        $set:{
            tKey: "test3"
        }
    }
);
```