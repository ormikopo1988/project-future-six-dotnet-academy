setTimeout(function() {
    console.log("Waiting for 1 second...");
    getPerson(function() {
        console.log("getPerson callback called...");
        getLog(function(id) {
            console.log("getLog callback called...");
        });
    });
}, 1000);

function getPerson(callBack) {
    console.log("Simulating external process call to get data...");

    callBack();
}

function getLog(callBack) {
    console.log("Simulating external process call to get log...");

    callBack();
}

// Promises - 1st Example - Using our own custom implementation
const doWork = (res, rej) => {
    setTimeout(function() {
        res("Hello World!");
    }, 1000);
}

// let someText = new cp.CustomPromise(doWork);

// someText.then((val) => {
//     console.log("1st log: " + val);
// });

// someText.then((val) => {
//     console.log("2nd log: " + val);
// });

// setTimeout(function() {
//     someText.then((val) => {
//         console.log("3rd log: " + val);
//     });
// }, 3000);

// Promises - 2nd Example - Same idea as above but using the built-in Promise object inside the js engine
let someText = new Promise(doWork);

// You can do something like this
// let someOtherText = someText.then((val) => {
//     console.log("1st log: " + val);

//     return "How are you?"
// });

// someOtherText.then((val) => {
//     console.log(val);
// });

// Or something like this
var anotherWork = (res, rej) => {
    setTimeout(function() {
        res("How are you?")
    }, 3000);
}

someText
    .then((val) => {
        console.log("1st log: " + val);
        return new Promise(anotherWork);
    }).then((val) => {
        console.log(val);
    });

// A more realistic example
// We are going to use fetch()
// Fetch is not part of the js specification.
// It is provided to us by the browser to which 
// js engine is embedded into.
// It goes out to the internet and fetches some data from the internet or file etc.
fetch('https://jsonplaceholder.typicode.com/todos/1')
    .then(response => {
        return response.json();
    })
    .then(data => {
        console.log('Todo: ', data);
    });