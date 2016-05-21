var signalR = require('signalr-client');

console.log("Test Client");

var client  = new signalR.client(

	//signalR service URL
	"http://localhost:8177/buzz",

	// array of hubs to be supported in the connection
	['BuzzHub']
    //, 10 /* Reconnection Timeout is optional and defaulted to 10 seconds */
    //, false 
    /* doNotStart is option and defaulted to false. If set to true 
    client will not start until .start() is called */
);