// A Promise represents a value that comes back, after some work has completed
// For that reason we have 3 states that we want to think about
// The Promise will always be in one of these 3 states
const PENDING = 0; // work not done yet
const FULFILLED = 1; // work completed successfully - value is ready
const REJECTED = 2; // work not completed - error

// The executor function given to the Promise object,
// is the function that will actually do the work that we are
// talking about (will request the data from the database, or run a setTimeout)
// The Promise object will not actually do any of the work,
// it just wraps up the idea of waiting for that work to complete and
// then figuring out what to do after that work is completed.
// It is up to the coder using the Promise object to actually
// write the executor function that does the work and then give it to the promise.
// The promise then will run it.
function CustomPromise (executor) {
    let state = PENDING; // initial state - waiting for the work to complete
    let value = null; // the actual value we are waiting for
    let handlers = []; // which functions / callbacks are going to run when the work is complete and handle the value returned
    let catches = []; // which functions / callbacks are going to handle the possible errors that will be returned

    // The resolve function is called when the work is done
    // and we have a result
    // The resolve function will get called by the executor, 
    // so we will give this function to the executor
    // and that executor function, the function written by the coder
    // using the promise will call the resolve function that is on the 
    // promise object and give it that value that it just received.
    function resolve(result) {
        if (state !== PENDING) return; // If I am not pending anymore there is nothing to resolve - I already have the value

        // Set my internal state to fulfilled
        // and set my internal value to whatever result
        // the executor function gave to me
        state = FULFILLED;
        value = result;

        // Execute all callback functions registered and pass 
        // to each one of them the value that just came back
        handlers.forEach((h) => h(value));
    }

    // We can do something very similar if there is an error
    function reject(error) {
        if (state !== PENDING) return;

        state = REJECTED;
        value = error;

        catches.forEach((c) => c(error));
    }

    // This is another function that you see a lot when using Promises
    this.then = function(callback) {
        // If the state is FULFILLED
        // we are just going to execute the callback immediately
        // and we will give it the value we know that we already have.
        if (state === FULFILLED) {
            callback(value);
        } else {
            // If still waiting for the value to come
            // just add the callback to the array of callbacks
            // or handlers that we are going to execute
            // when it is done, when the Promise is resolved,
            // when the resolve function of the Promise is called.
            handlers.push(callback);
        }

        // How can we avoid the pyramid of doom?
        // What if the then function returned a Promise itself.
        // That would be great because if my callback passed here
        // also does something that requires us to wait for a returned
        // value, I could then chain a sequence of then's. 
        // That means I could flatten the pyramid.
        // This is actually the case with the real Promise object coming
        // with the javascript engine.
    }

    // Last thing to do is what always has to be done
    // when the Promise is created. We run the executor function.
    // That means that a Promise represents a process that is already
    // running, so when we create the promise, that actually starts
    // the work, it runs the executor function that is being given.
    // The executor function should expect a resolve and a reject function.
    executor(resolve, reject);
}