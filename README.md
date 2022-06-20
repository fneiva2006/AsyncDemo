# Async Demo

This solution is a simple demonstration on how an asp.net core application can benefit from using `asynchronous` methods instead of `synchronous` ones.

It is composed of two main applications: the server and the client.

The server has two endpoints that return information about its own running process after waiting for some seconds. 
These two endpoints wait for the same amount of time and execute the same routine to get the process information. The only difference is
one waits in a synchronous way (blocking the thread) and the other in an asynchronous way.

The idea here is to gauge how much performance is impacted due to thread blocking, specially when the server gets many concurrent calls and  therefore its thread pool
gets exhausted.


# How to Run

Simply execute both server and client applications: `AsyncDemo` and `AsyncDemoClient`.

Just follow the instructions in the client application console window.

# Dependencies

#### Client

- Restease 1.5.5
