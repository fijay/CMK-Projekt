# Server (Node.js, Socket.IO)


## App

A Node.js application using Socket.IO for distributing updates in near real time.


### Flow

```
               {
                 "rotation": 5
               }
Some user      ----------------------------------->         Server
```

```
               {
                 "date": "2017-07-17",
                 "time": "17:18:15",
                 "dateTime": "2017-07-17 17:18:15",
                 "sound": "a",
                 "rotation": 5,
                 "size": 1
               }
Server         ----------------------------------->         All users
```


### Input

A JSON like this:

```
{
  "sound": "a",
  "rotation": 1,
  "size": 1
}
```

One key-value pair is preferred:

```
{
  "size": 5
}
```


### Output

A JSON like this:

```
{
  "date": "2017-07-17",
  "time": "17:18:15",
  "dateTime": "2017-07-17 17:18:15",
  "sound": "a",
  "rotation": 1,
  "size": 1
}
```


## Test

Simple test for your web browser. Connects to URL, emits "message", listens for "update". Use textarea for custom input. Open console for output.

Note: Replace `_____IP/domain_____` with a valid IP or domain.


## TODO

* improve efficiency
* update dependencies
* test
